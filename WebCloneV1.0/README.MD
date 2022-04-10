# 如何打造网站克隆、仿站工具（C#版）

界面：

![img](https://bbsmax.ikafan.com/static/L3Byb3h5L2h0dHBzL2ltYWdlczIwMTguY25ibG9ncy5jb20vYmxvZy8xMzM2OTkzLzIwMTgwOC8xMzM2OTkzLTIwMTgwODI4MTcxODU3MDU5LTE2OTQxODMwNDkucG5n.jpg)

简单的工作流程：

![img](https://bbsmax.ikafan.com/static/L3Byb3h5L2h0dHBzL2ltYWdlczIwMTguY25ibG9ncy5jb20vYmxvZy8xMzM2OTkzLzIwMTgwOC8xMzM2OTkzLTIwMTgwODI4MTc1OTMyNDcwLTY1NjI2MzI5NC5wbmc=.jpg)

**项目代码目录结构：**

**![img](https://bbsmax.ikafan.com/static/L3Byb3h5L2h0dHBzL2ltYWdlczIwMTguY25ibG9ncy5jb20vYmxvZy8xMzM2OTkzLzIwMTgwOC8xMzM2OTkzLTIwMTgwODI4MTcyMDQ4OTMzLTE5MzA2MTM0NDQucG5n.jpg)**

下面一步步实现程序功能：

1.新建主界面窗体（MainForm.cs）：

![img](https://bbsmax.ikafan.com/static/L3Byb3h5L2h0dHBzL2ltYWdlczIwMTguY25ibG9ncy5jb20vYmxvZy8xMzM2OTkzLzIwMTgwOC8xMzM2OTkzLTIwMTgwODI4MTcyNTU1OTI2LTE1NDIzOTAwMTkucG5n.jpg)

2.新建模型类（UrlModel.cs）

```C#
public class UrlModel
    {
        public string RelatedPath { get; set; }
        public string AbsoluteUri { get; set; }
        public string CurrPath { get; set; }
        public string RootPath { get; set; }         public string Host { get; set; }
        public int Port { get; set; }
        public string Scheme { get; set; }
    }
```

3.新建服务类（Services）

**UrlParser：**

```C#
public class UrlParser
    {
        public static UrlModel Parse(string url)
        {
            UrlModel model = new UrlModel();             //默认
            if (url.Length < 8)
                throw new Exception("url参数不正确");
            else if (!url.ToLower().StartsWith("http:") && !url.ToLower().StartsWith("https:"))
                throw new Exception("url格式有误");             if (url.LastIndexOf('/') < 8)
                url = url + "/";             Regex reg = new Regex("(?<scheme>(http|https))://(?<host>.+?)/", RegexOptions.Singleline);             if (reg.IsMatch(url))
            {
                string scheme = reg.Match(url).Groups["scheme"].Value;
                string host = reg.Match(url).Groups["host"].Value;
                if (host.Contains(":"))
                {
                    var aa = host.Split(':');
                    if (aa.Length == 2)
                    {
                        model.Host = aa[0];
                        model.Port = int.Parse(aa[1]);
                    }
                }
                else
                {
                    model.Host = host;
                    model.Port = 80;
                }                 int index = url.IndexOf('/', 8);                 model.RelatedPath = url.Substring(index);
                model.AbsoluteUri = url;
                model.Scheme = scheme;
                model.CurrPath = url.Substring(0, url.LastIndexOf("/"));                 if (80 == model.Port)
                {
                    model.RootPath = string.Format("{0}://{1}", model.Scheme, model.Host);
                }
                else
                {
                    model.RootPath = string.Format("{0}://{1}:{2", model.Scheme, model.Host, model.Port);
                }
            }
            else
            {
                throw new Exception("url解析失败!");
            }             return model;
        }
    }
```

**WebPageService：
**

```C#
/// <summary>
    /// 网页处理服务工具
    /// </summary>
    public class WebPageService
    {
        private static string[] excludekeys = { "http:", "https:", "//", "#", "javascript:", "?", "tel:", "mailto:" };
        /// <summary>
        /// 获取所有html元素的href属性值,只获取站点本地的链接，站外的不获取
        /// </summary>
        /// <param name="html">页面的html源码</param>
        /// <returns></returns>
        public static List<UrlModel> GetLocalHrefs(string url,string html)
        {
            if (string.IsNullOrEmpty(html))
                return new List<UrlModel>();             Dictionary<string, UrlModel> urls = GetHrefs(url,html);
            List<UrlModel> newUrls = new List<UrlModel>();             if (null != urls)
            {
                foreach (string key in urls.Keys)
                {
                    string newkey = key.ToLower();
                    bool iscontained = false;
                    foreach (var exkey in excludekeys)
                    {
                        if (newkey.IndexOf(exkey) == 0)
                        {
                            iscontained = true;
                            break;
                        }
                    }                     if (!iscontained) {
                        //只获取本地路径
                        newUrls.Add(urls[key]);
                    }
                }
            }             return newUrls;
        }         /// <summary>
        /// 获取所有html元素的src属性值,只获取站点本地的链接，站外的不获取
        /// </summary>
        /// <param name="html">页面的html源码</param>
        /// <returns></returns>
        public static List<UrlModel> GetLocalSrcs(string url,string html)
        {
            if (string.IsNullOrEmpty(html))
                return new List<UrlModel>();             Dictionary<string, UrlModel> urls = GetSrc(url, html);
            List<UrlModel> newUrls = new List<UrlModel>();             if (null != urls)
            {
                foreach (string key in urls.Keys)
                {
                    string newkey = key.ToLower();
                    bool iscontained = false;
                    foreach (var exkey in excludekeys)
                    {
                        if (newkey.IndexOf(exkey) == 0)
                        {
                            iscontained = true;
                            break;
                        }
                    }                     if (!iscontained)
                    {
                        //只获取本地路径
                        newUrls.Add(urls[key]);
                    }
                }
            }             return newUrls;
        }         private static Dictionary<string, UrlModel> GetHrefs(string url,string html)
        {
            if (string.IsNullOrEmpty(html))
                return null;             UrlModel currUrl = UrlParser.Parse(url);
            Dictionary<string, UrlModel> urls = new Dictionary<string, UrlModel>();
            Regex reg = new Regex("href=\"(?<Url>.+?)\"", RegexOptions.IgnoreCase);             if (currUrl != null)
            {
                AddUrlModel(html, currUrl, urls, reg);
            }             return urls;
        }         private static Dictionary<string, UrlModel> GetSrc(string url,string html)
        {
            if (string.IsNullOrEmpty(html))
                return null;             UrlModel currUrl = UrlParser.Parse(url);
            Dictionary<string, UrlModel> urls = new Dictionary<string, UrlModel>();
            Regex reg = new Regex("(src=\"(?<Url>.+?)\"|url\\((?<Url>.+?)\\))", RegexOptions.IgnoreCase);             if (currUrl != null)
            {
                AddUrlModel(html, currUrl, urls, reg);
            }             return urls;
        }         private static void AddUrlModel(string html, UrlModel currUrl, Dictionary<string, UrlModel> urls, Regex reg)
        {
            if (reg.IsMatch(html))
            {
                MatchCollection matchs = reg.Matches(html);
                foreach (Match item in matchs)
                {
                    try
                    {
                        string strUrl = item.Groups["Url"].Value;
                        UrlModel model = new UrlModel();
                        model.RelatedPath = strUrl;
                        model.CurrPath = currUrl.CurrPath;
                        model.RootPath = currUrl.RootPath;
                        model.Scheme = currUrl.Scheme;
                        model.Port = currUrl.Port;
                        model.Host = currUrl.Host;                         if (strUrl.StartsWith("/"))
                        {
                            //绝对目录情况下
                            model.AbsoluteUri = string.Format("{0}{1}", model.RootPath, model.RelatedPath);
                        }
                        else
                        {
                            //相对目录情况下
                            string currPath = model.CurrPath;
                            int depth = 0;
                            string path = model.RelatedPath;                             if (path.StartsWith(".."))
                            {
                                try
                                {
                                    while (path.StartsWith(".."))
                                    {
                                        depth++;
                                        path = path.Substring(3);
                                        currPath = currPath.Substring(0, currPath.LastIndexOf("/"));
                                    }                                     model.AbsoluteUri = string.Format("{0}/{1}", currPath, path);
                                }
                                catch
                                {                                 }
                            }
                            else
                            {
                                model.AbsoluteUri = string.Format("{0}/{1}", currPath, path);
                            }                         }                         strUrl = strUrl.Trim().ToLower();                         urls.Add(strUrl, model);
                    }
                    catch
                    {
                    }
                }
            }
        }
    }
```

**4.网页源码扒取类**

```C#
public class HttpTool
    {
        public static string HttpGet(string url, string referer, string encoding, out string msg)
        {
            msg = string.Empty;
            string result = string.Empty;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);                 //request.ContentType = "application/x-www-form-urlencoded";
                request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
                request.Referer = referer;
                request.Method = "GET";
                request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/38.0.2125.122 Safari/537.36";
                //request.Headers.Add("Accept-Language", "zh-cn");
                //request.Headers.Add("Accept-Encoding", "gzip,deflate");                 request.Timeout = 60000;//一分钟                 HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream responseStream = response.GetResponseStream();
                if (responseStream != null)
                {
                    StreamReader reader = new StreamReader(responseStream, System.Text.Encoding.GetEncoding(encoding));
                    result = reader.ReadToEnd();
                    reader.Close();
                    responseStream.Close();
                    request.Abort();
                    response.Close();
                    return result.Trim();
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message + ex.StackTrace;
            }             return result;
        }         public static void DownFile(string uRLAddress, string localPath, string filename)
        {
            WebClient client = new WebClient();
            Stream str = client.OpenRead(uRLAddress);
            StreamReader reader = new StreamReader(str);
            byte[] mbyte = new byte[1000000];
            int allmybyte = (int)mbyte.Length;
            int startmbyte = 0;             while (allmybyte > 0)
            {
                int m = str.Read(mbyte, startmbyte, allmybyte);
                if (m == 0)
                {
                    break;
                }
                startmbyte += m;
                allmybyte -= m;
            }             reader.Dispose();
            str.Dispose();             string path = Path.Combine(localPath, filename);
            FileStream fstr = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);
            fstr.Write(mbyte, 0, startmbyte);
            fstr.Flush();
            fstr.Close();
        }
    }
```

5.网站克隆主类

接口：

```C#
interface IWebCloneWorker
    {
        void Start();
        void Cancel();
    }
```

实现类：

```C#
public class WebCloneWorker : IWebCloneWorker
    {
        //网站页面克隆深度(如：0-首页,1-分类页,2-详细页面)
        public static int depth = 0;         //要克隆的网站网址
        public string Url { get; set; }         //克隆后，保存的路径
        public string SavePath { get; set; }         private BackgroundWorker backgroundWorker1 = null;
        public event UrlChangedEventHandler UrlChanged;
        public event FileSavedSuccessEventHandler FileSavedSuccess;
        public event FileSavedFailEventHandler FileSavedFail;
        public event DownloadCompletedEventHandler DownloadCompleted;
        public event CollectingUrlEventHandler CollectingUrl;
        public event CollectedUrlEventHandler CollectedUrl;
        public event ProgressChangedEventHandler ProgressChanged;         //所有页面、文件资源地址集合
        private Dictionary<string, UrlModel> _Hrefs = new Dictionary<string, UrlModel>();         /// <summary>
        /// 所有页面、文件资源地址集合
        /// </summary>
        public Dictionary<string,UrlModel> Hrefs
        {
            get { return _Hrefs; }
            set { _Hrefs = value; }
        }         //网站页面请求编码,默认为UTF-8
        private string _Encoding = "utf-8";         //网站页面请求编码,默认为UTF-8
        public string Encoding
        {
            get { return _Encoding; }
            set { _Encoding = value; }
        }         public WebCloneWorker() { }         public WebCloneWorker(string url,string path)
        {
            //设置网站、保存路径
            this.Url = url;
            this.SavePath = path;             if (string.IsNullOrEmpty(this.Url))
                throw new Exception("请输入网址");             if (string.IsNullOrEmpty(this.SavePath))
                throw new Exception("请选择要保存的目录");             backgroundWorker1 = new BackgroundWorker();    //设置报告进度更新
            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.WorkerSupportsCancellation = true;             //注册线程主体方法
            backgroundWorker1.DoWork += backgroundWorker1_DoWork;             //注册更新UI方法
            backgroundWorker1.ProgressChanged += backgroundWorker1_ProgressChanged;             //处理完毕
            backgroundWorker1.RunWorkerCompleted += backgroundWorker1_RunWorkerCompleted;
        }         void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled) {
                return;
            }             if (this.DownloadCompleted != null)
            {
                DownloadCompletedEventArgs eventArgs = new DownloadCompletedEventArgs(e.Result, e.Error, e.Cancelled);
                this.DownloadCompleted(this, eventArgs);
            }
        }         void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //进度回调
            if (this.ProgressChanged != null)
                this.ProgressChanged(this, e);             UrlModel model = (UrlModel)e.UserState;             if (this.UrlChanged != null)
            {
                //Url改变后，回调
                UrlChangedEventArgs eventArgs = new UrlChangedEventArgs(model);
                this.UrlChanged(this, eventArgs);
            }             try
            {
                string dir = this.SavePath;
                string url = model.AbsoluteUri;
                string AbsolutePath = url.Substring(url.IndexOf('/', 8));
                string fileName = "";                 if (url.IndexOf('?') > 0)
                {
                    string path = AbsolutePath.Substring(0, model.RelatedPath.IndexOf('?'));
                    fileName = System.IO.Path.GetFileName(path);
                }
                else
                {
                    fileName = System.IO.Path.GetFileName(AbsolutePath);
                }                 //默认首页
                if (string.IsNullOrEmpty(fileName) || fileName.IndexOf(".") < 0)
                {
                    fileName = "index.html";                     if (!AbsolutePath.EndsWith("/"))
                        AbsolutePath = AbsolutePath + "/";
                }                 fileName = System.Web.HttpUtility.UrlDecode(fileName);                 string localPath = string.Format("{0}{1}", dir, System.IO.Path.GetDirectoryName(AbsolutePath));
                if (!System.IO.Directory.Exists(localPath))
                {
                    System.IO.Directory.CreateDirectory(localPath);
                }                 //判断文件是否存在，存在不再下载
                string path2 = Path.Combine(localPath, fileName);
                if (File.Exists(path2))
                {
                    return;
                }                 //下载网页、图片、资源文件
                HttpTool.DownFile(url, localPath, fileName);                 //保存成功后，回调
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
        }         void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            //获取资源
            GetResource();             int index = 1;
            if (this.Hrefs.Keys.Count > 0)
            {
                foreach (var k in this.Hrefs.Keys)
                {
                    //取消操作
                    if (backgroundWorker1.CancellationPending)
                    {
                        e.Cancel = true;
                        return;
                    }                     backgroundWorker1.ReportProgress(index, this.Hrefs[k]);
                    index++;                     //挂起当前线程200毫秒
                    Thread.Sleep(200);
                }
            }
        }         public void Start()
        {
            if (this.backgroundWorker1.IsBusy)
                return;             this.backgroundWorker1.RunWorkerAsync();
        }         public void Cancel()
        {
            if (this.backgroundWorker1.CancellationPending)
                return;             this.backgroundWorker1.CancelAsync();
        }         private void GetResource()
        {
            string url = this.Url;
            string referer = this.Url;
            string msg = "";
            string html = HttpTool.HttpGet(url, referer, this.Encoding, out msg);             //收集页面链接
            GetHrefs(0, url, html);             //收集完毕
            if (null != CollectedUrl)
            {
                UrlModel urlModel = new UrlModel();
                CollectedUrlEventArgs eventArgs = new CollectedUrlEventArgs(urlModel);
                this.CollectedUrl(this, eventArgs);
            }         }         private void GetHrefs(int level,string url,string html)
        {
            #region 添加当前页             UrlModel currUrl = UrlParser.Parse(url);             try
            {
                //取消
                if (backgroundWorker1.CancellationPending)
                    return;                 this.Hrefs.Add(currUrl.RelatedPath, currUrl);                 //收集回调
                if (null != CollectingUrl)
                {
                    CollectingUrlEventArgs eventArgs = new CollectingUrlEventArgs(currUrl);
                    this.CollectingUrl(this, eventArgs);
                }
            }
            catch
            {
            }             #endregion             //获取相关链接(含有href属性的)
            List<UrlModel> list1 = WebPageService.GetLocalHrefs(url,html);             //获取图片，文件等资源文件(含有src属性的)
            List<UrlModel> listSrcs = WebPageService.GetLocalSrcs(url,html);             #region 获取当级资源文件             if (listSrcs != null)
            {
                for (int i = 0; i < listSrcs.Count; i++)
                {
                    UrlModel urlModel = listSrcs[i];
                    try
                    {
                        //取消
                        if (backgroundWorker1.CancellationPending)
                            return;                         this.Hrefs.Add(urlModel.RelatedPath, urlModel);                         //收集回调
                        if (null != CollectingUrl)
                        {
                            CollectingUrlEventArgs eventArgs = new CollectingUrlEventArgs(urlModel);
                            this.CollectingUrl(this, eventArgs);
                        }
                    }
                    catch
                    { }
                }
            }             #endregion             #region 获取子级页面资源             //获取第二级
            if (list1 != null)
            {
                for (int i = 0; i < list1.Count; i++)
                {
                    UrlModel urlModel = list1[i];                     try
                    {
                        //取消
                        if (backgroundWorker1.CancellationPending)
                            return;                         this.Hrefs.Add(urlModel.RelatedPath, urlModel);                         //收集回调
                        if (null != CollectingUrl)
                        {
                            CollectingUrlEventArgs eventArgs = new CollectingUrlEventArgs(urlModel);
                            this.CollectingUrl(this, eventArgs);
                        }
                    }
                    catch
                    { }                     string msg = "";
                    html = HttpTool.HttpGet(urlModel.AbsoluteUri, urlModel.AbsoluteUri, this.Encoding, out msg);                     #region 获取子级资源文件                     /*
                     * 获取二级资源文件
                     * */
                    listSrcs = WebPageService.GetLocalSrcs(urlModel.AbsoluteUri, html);//资源文件                     if (listSrcs != null)
                    {
                        for (int j = 0; j < listSrcs.Count; j++)
                        {
                            UrlModel urlModel2 = listSrcs[j];                             try
                            {
                                //取消
                                if (backgroundWorker1.CancellationPending)
                                    return;                                 this.Hrefs.Add(urlModel2.RelatedPath, urlModel2);                                 //收集回调
                                if (null != CollectingUrl)
                                {
                                    CollectingUrlEventArgs eventArgs = new CollectingUrlEventArgs(urlModel2);
                                    this.CollectingUrl(this, eventArgs);
                                }
                            }
                            catch
                            { }                             //挂起线程20毫秒
                            Thread.Sleep(20);
                        }
                    }
                    #endregion                     //挂起线程20毫秒
                    Thread.Sleep(20);                     //到达指定深度后，退出
                    if (level >= depth)
                        return;                     //递归
                    GetHrefs(level + 1, urlModel.AbsoluteUri, html);
                }
            }             #endregion
        }
    }
```

6.一些事件、委托类：

```C#
public delegate void UrlChangedEventHandler(object sender, UrlChangedEventArgs e);
    public delegate void FileSavedSuccessEventHandler(object sender, FileSavedSuccessEventArgs e);
    public delegate void FileSavedFailEventHandler(object sender, FileSavedFailEventArgs e);
    public delegate void DownloadCompletedEventHandler(object sender, DownloadCompletedEventArgs e);
    public delegate void CollectingUrlEventHandler(object sender, CollectingUrlEventArgs e);
    public delegate void CollectedUrlEventHandler(object sender, CollectedUrlEventArgs e);
    public delegate void ProgressChangedEventHandler(object sender, ProgressChangedEventArgs e);
public class CollectedUrlEventArgs : EventArgs
public class CollectingUrlEventArgs : EventArgs
public class DownloadCompletedEventArgs : EventArgs
public class FileSavedFailEventArgs : EventArgs
public class FileSavedSuccessEventArgs : EventArgs
public class UrlChangedEventArgs : EventArgs
```