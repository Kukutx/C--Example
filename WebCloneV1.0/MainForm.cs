using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using WebClone.Models;
using WebClone.Services;
using WebClone.Tools;

namespace WebClone
{
    public partial class MainForm : Form
    {
        private WebCloneWorker oWebCloneWorker = null;
        private string encding = "utf-8";
       
        public MainForm()
        {
            InitializeComponent();

            this.progressBar1.Step = 1;
            this.progressBar1.Minimum = 0;
        }

        private void btnClone_Click(object sender, EventArgs e)
        {
            //禁用克隆深度、编码下拉选项
            cbEncode.Enabled = false;
            cbDepth.Enabled = false;

            string url = txtUrl.Text.Trim();
            string savePath = txtPath.Text.Trim();

            if (string.IsNullOrEmpty(url))
            {
                MessageBox.Show("请输入网址");
                return;
            }

            if (string.IsNullOrEmpty(savePath))
            {
                MessageBox.Show("请选择要保存到的目录");
                return;
            }

            if (!string.IsNullOrEmpty(url))
            {
                if (null == oWebCloneWorker)
                {
                    oWebCloneWorker = new WebCloneWorker(url, savePath);
                    oWebCloneWorker.Encoding = this.encding;

                    oWebCloneWorker.UrlChanged += oWebCloneWorker_UrlChanged;
                    oWebCloneWorker.FileSavedSuccess += oWebCloneWorker_FileSavedSuccess;
                    oWebCloneWorker.FileSavedFail += oWebCloneWorker_FileSavedFail;
                    oWebCloneWorker.DownloadCompleted += oWebCloneWorker_DownloadCompleted;
                    oWebCloneWorker.CollectingUrl += oWebCloneWorker_CollectingUrl;
                    oWebCloneWorker.CollectedUrl += oWebCloneWorker_CollectedUrl;
                    oWebCloneWorker.ProgressChanged += oWebCloneWorker_ProgressChanged;
                }

                oWebCloneWorker.Start();
            }
            else
            {
                MessageBox.Show("请输入网址");
            }
            
        }

        void oWebCloneWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.progressBar1.Value = e.ProgressPercentage;
            this.lbDownloaded.Text = e.ProgressPercentage.ToString();
        }

        void oWebCloneWorker_CollectedUrl(object sender, CollectedUrlEventArgs e)
        {
            this.txtProgress.AppendText("√----收集完毕！正在下载....\r\n");
            this.progressBar1.Maximum = oWebCloneWorker.Hrefs.Count;
        }

        void oWebCloneWorker_CollectingUrl(object sender, CollectingUrlEventArgs e)
        {
            this.txtProgress.AppendText("√----正在收集地址----" + e.Model.RelatedPath + "....\r\n");
            this.lbUrlCount.Text = oWebCloneWorker.Hrefs.Count.ToString();
        }

        void oWebCloneWorker_DownloadCompleted(object sender, DownloadCompletedEventArgs e)
        {
            this.txtProgress.AppendText("√----ok----....\r\n");
        }

        void oWebCloneWorker_FileSavedFail(object sender, FileSavedFailEventArgs e)
        {
            this.txtProgress.AppendText("×----Fail----" + e.Error.Message + "....\r\n");
        }

        void oWebCloneWorker_FileSavedSuccess(object sender, FileSavedSuccessEventArgs e)
        {
            UrlModel model = (UrlModel)e.UserState;
            this.txtProgress.AppendText("√----FileSavedSuccess--" + model.CurrPath + "|" + model.RelatedPath + "|" + model.AbsoluteUri + "....\r\n");
        }

        void oWebCloneWorker_UrlChanged(object sender, UrlChangedEventArgs e)
        {
            //UrlModel model = (UrlModel)e.UserState;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbDepth.SelectedItem != null)
            {
                WebCloneWorker.depth = Convert.ToInt32(cbDepth.SelectedItem);
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog path = new FolderBrowserDialog();
            if (path.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.txtPath.Text = path.SelectedPath;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            //启用克隆深度、编码下拉选项
            cbEncode.Enabled = true;
            cbDepth.Enabled = true;

            oWebCloneWorker.Cancel();
        }

        private void cbEncode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbEncode.SelectedItem != null)
            {
                this.encding = cbEncode.SelectedItem.ToString();
            }
        }
    }
}
