using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using WebClone.Models;
using WebClone.Services;
using WebClone.Tools;

/*
 * 作者：jonlan
 * 时间：2018-8-25
 * 邮箱：547294770@qq.com
 * */
namespace WebClone
{
    public class WebCloneWorker : IWebCloneWorker
    {
        //网站页面克隆深度(如：0-首页,1-分类页,2-详细页面)
        public static int depth = 0;
        
        //要克隆的网站网址
        public string Url { get; set; }

        //克隆后，保存的路径
        public string SavePath { get; set; }

        private BackgroundWorker backgroundWorker1 = null;
        public event UrlChangedEventHandler UrlChanged;
        public event FileSavedSuccessEventHandler FileSavedSuccess;
        public event FileSavedFailEventHandler FileSavedFail;
        public event DownloadCompletedEventHandler DownloadCompleted;
        public event CollectingUrlEventHandler CollectingUrl;
        public event CollectedUrlEventHandler CollectedUrl;
        public event ProgressChangedEventHandler ProgressChanged;

        //所有页面、文件资源地址集合
        private Dictionary<string, UrlModel> _Hrefs = new Dictionary<string, UrlModel>();

        /// <summary>
        /// 所有页面、文件资源地址集合
        /// </summary>
        public Dictionary<string,UrlModel> Hrefs
        {
            get { return _Hrefs; }
            set { _Hrefs = value; }
        }

        //网站页面请求编码,默认为UTF-8
        private string _Encoding = "utf-8";

        //网站页面请求编码,默认为UTF-8
        public string Encoding
        {
            get { return _Encoding; }
            set { _Encoding = value; }
        }

        public WebCloneWorker() { }

        public WebCloneWorker(string url,string path) 
        {
            //设置网站、保存路径
            this.Url = url;
            this.SavePath = path;

            if (string.IsNullOrEmpty(this.Url))
                throw new Exception("请输入网址");

            if (string.IsNullOrEmpty(this.SavePath))
                throw new Exception("请选择要保存的目录");

            backgroundWorker1 = new BackgroundWorker();

            //设置报告进度更新
            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.WorkerSupportsCancellation = true;

            //注册线程主体方法
            backgroundWorker1.DoWork += backgroundWorker1_DoWork;

            //注册更新UI方法
            backgroundWorker1.ProgressChanged += backgroundWorker1_ProgressChanged;

            //处理完毕
            backgroundWorker1.RunWorkerCompleted += backgroundWorker1_RunWorkerCompleted;
        }

        void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled) {
                return;
            }

            if (this.DownloadCompleted != null)
            {
                DownloadCompletedEventArgs eventArgs = new DownloadCompletedEventArgs(e.Result, e.Error, e.Cancelled);
                this.DownloadCompleted(this, eventArgs);
            }
        }

        void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //进度回调
            if (this.ProgressChanged != null) 
                this.ProgressChanged(this, e);

            UrlModel model = (UrlModel)e.UserState;

            if (this.UrlChanged != null)
            {
                //Url改变后，回调
                UrlChangedEventArgs eventArgs = new UrlChangedEventArgs(model);
                this.UrlChanged(this, eventArgs);
            }

            try
            {
                string dir = this.SavePath;
                string url = model.AbsoluteUri;
                string AbsolutePath = url.Substring(url.IndexOf('/', 8));
                string fileName = "";

                if (url.IndexOf('?') > 0)
                {
                    string path = AbsolutePath.Substring(0, model.RelatedPath.IndexOf('?'));
                    fileName = System.IO.Path.GetFileName(path);
                }
                else
                {
                    fileName = System.IO.Path.GetFileName(AbsolutePath);
                }

                //默认首页
                if (string.IsNullOrEmpty(fileName) || fileName.IndexOf(".") < 0)
                {
                    fileName = "index.html";

                    if (!AbsolutePath.EndsWith("/"))
                        AbsolutePath = AbsolutePath + "/";
                }

                fileName = System.Web.HttpUtility.UrlDecode(fileName);

                string localPath = string.Format("{0}{1}", dir, System.IO.Path.GetDirectoryName(AbsolutePath));
                if (!System.IO.Directory.Exists(localPath))
                {
                    System.IO.Directory.CreateDirectory(localPath);
                }

                //判断文件是否存在，存在不再下载
                string path2 = Path.Combine(localPath, fileName);
                if (File.Exists(path2))
                {
                    return;
                }

                //下载网页、图片、资源文件
                HttpTool.DownFile(url, localPath, fileName);

                //保存成功后，回调
                if (this.FileSavedSuccess != null)
                {
                    FileSavedSuccessEventArgs eventArgs = new FileSavedSuccessEventArgs(model);
                    this.FileSavedSuccess(this, eventArgs);
                }
            }
            catch (Exception ex)
            {
                //保存失败后，回调
                if (this.FileSavedFail != null)
                {
                    FileSavedFailEventArgs eventArgs = new FileSavedFailEventArgs(ex);
                    this.FileSavedFail(this, eventArgs);
                }
            }
        }

        void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            //获取资源
            GetResource();

            int index = 1;
            if (this.Hrefs.Keys.Count > 0)
            {
                foreach (var k in this.Hrefs.Keys)
                {
                    //取消操作
                    if (backgroundWorker1.CancellationPending)
                    {
                        e.Cancel = true;
                        return;
                    }

                    backgroundWorker1.ReportProgress(index, this.Hrefs[k]);
                    index++;

                    //挂起当前线程200毫秒
                    Thread.Sleep(200);
                }
            }
        }

        public void Start()
        {
            if (this.backgroundWorker1.IsBusy)
                return;

            this.backgroundWorker1.RunWorkerAsync();
        }

        public void Cancel()
        {
            if (this.backgroundWorker1.CancellationPending)
                return;

            this.backgroundWorker1.CancelAsync();
        }
        
        private void GetResource()
        {
            string url = this.Url;
            string referer = this.Url;
            string msg = "";
            string html = HttpTool.HttpGet(url, referer, this.Encoding, out msg);

            //收集页面链接
            GetHrefs(0, url, html);

            //收集完毕
            if (null != CollectedUrl)
            {
                UrlModel urlModel = new UrlModel();
                CollectedUrlEventArgs eventArgs = new CollectedUrlEventArgs(urlModel);
                this.CollectedUrl(this, eventArgs);
            }

        }

        private void GetHrefs(int level,string url,string html)
        {
            #region 添加当前页

            UrlModel currUrl = UrlParser.Parse(url);

            try
            {
                //取消
                if (backgroundWorker1.CancellationPending)
                    return;

                this.Hrefs.Add(currUrl.RelatedPath, currUrl);

                //收集回调
                if (null != CollectingUrl)
                {
                    CollectingUrlEventArgs eventArgs = new CollectingUrlEventArgs(currUrl);
                    this.CollectingUrl(this, eventArgs);
                }
            }
            catch
            {
            }

            #endregion

            //获取相关链接(含有href属性的)
            List<UrlModel> list1 = WebPageService.GetLocalHrefs(url,html);

            //获取图片，文件等资源文件(含有src属性的)
            List<UrlModel> listSrcs = WebPageService.GetLocalSrcs(url,html);

            #region 获取当级资源文件

            if (listSrcs != null)
            {
                for (int i = 0; i < listSrcs.Count; i++)
                {
                    UrlModel urlModel = listSrcs[i];
                    try
                    {
                        //取消
                        if (backgroundWorker1.CancellationPending) 
                            return;

                        this.Hrefs.Add(urlModel.RelatedPath, urlModel);

                        //收集回调
                        if (null != CollectingUrl)
                        {
                            CollectingUrlEventArgs eventArgs = new CollectingUrlEventArgs(urlModel);
                            this.CollectingUrl(this, eventArgs);
                        }
                    }
                    catch
                    { }
                }
            }

            #endregion

            #region 获取子级页面资源

            //获取第二级
            if (list1 != null)
            {
                for (int i = 0; i < list1.Count; i++)
                {
                    UrlModel urlModel = list1[i];

                    try
                    {
                        //取消
                        if (backgroundWorker1.CancellationPending)
                            return;

                        this.Hrefs.Add(urlModel.RelatedPath, urlModel);

                        //收集回调
                        if (null != CollectingUrl)
                        {
                            CollectingUrlEventArgs eventArgs = new CollectingUrlEventArgs(urlModel);
                            this.CollectingUrl(this, eventArgs);
                        }
                    }
                    catch
                    { }

                    string msg = "";
                    html = HttpTool.HttpGet(urlModel.AbsoluteUri, urlModel.AbsoluteUri, this.Encoding, out msg);

                    #region 获取子级资源文件

                    /*
                     * 获取二级资源文件
                     * */
                    listSrcs = WebPageService.GetLocalSrcs(urlModel.AbsoluteUri, html);//资源文件

                    if (listSrcs != null)
                    {
                        for (int j = 0; j < listSrcs.Count; j++)
                        {
                            UrlModel urlModel2 = listSrcs[j];

                            try
                            {
                                //取消
                                if (backgroundWorker1.CancellationPending)
                                    return;

                                this.Hrefs.Add(urlModel2.RelatedPath, urlModel2);

                                //收集回调
                                if (null != CollectingUrl)
                                {
                                    CollectingUrlEventArgs eventArgs = new CollectingUrlEventArgs(urlModel2);
                                    this.CollectingUrl(this, eventArgs);
                                }
                            }
                            catch
                            { }

                            //挂起线程20毫秒
                            Thread.Sleep(20);
                        }
                    }
                    #endregion

                    //挂起线程20毫秒
                    Thread.Sleep(20);

                    //到达指定深度后，退出
                    if (level >= depth)
                        return;

                    //递归
                    GetHrefs(level + 1, urlModel.AbsoluteUri, html);
                }
            }

            #endregion
        }
    }
}
