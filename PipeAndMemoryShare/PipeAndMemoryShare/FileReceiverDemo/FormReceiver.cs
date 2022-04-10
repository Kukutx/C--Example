using Common;
using System;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Threading;
using System.Windows.Forms;
using static Common.PipeCommand;

namespace FileReceiverDemo
{
    public partial class FormReceiver : Form
    {
        /// <summary>
        /// 通讯管道
        /// </summary>
        private PipeClient m_pipeClient = null;
        /// <summary>
        /// 共享内存
        /// </summary>
        private MemoryMappedFile m_memoryMappedFile = null;

        /// <summary>
        /// 文件大小
        /// </summary>
        private int m_fileSize = -1;
        /// <summary>
        /// 文件路径
        /// </summary>
        private string m_filePath = string.Empty;
        /// <summary>
        /// 文件保存路径
        /// </summary>
        private string m_fileSavePath = string.Empty;
        /// <summary>
        /// 校验码
        /// </summary>
        private string m_fileCheckSum = string.Empty;

        public FormReceiver()
        {
            InitializeComponent();
        }

        private void FormReceiver_Shown(object sender, EventArgs e)
        {
            btnStart.Focus();
        }

        private void PipeClient_CommandEvent(string command)
        {
            if (command == CMD_EXIT)
            {
                m_pipeClient.CommandEvent -= PipeClient_CommandEvent;
            }

            if (command.StartsWith($"{CMD_START}{SYM_SPLIT}"))
            {
                SetStatus("开始接收文件");
                PushLog($"[ReceiveFileThread]开始接收文件！");

                Invoke(new Action(() =>
                {
                    txtFilePath.Clear();
                    txtFileSavePath.Clear();
                    txtFileSize.Clear();
                    txtCheckSum.Clear();
                }));

                m_fileSize = -1;
                m_filePath = string.Empty;
                m_fileSavePath = string.Empty;
                m_fileCheckSum = string.Empty;

                m_pipeClient.SendRevCommand(command.Split(SYM_SPLIT)[1]);
                return;
            }

            if (command.StartsWith($"{CMD_PATH}{SYM_SPLIT}"))
            {
                SetStatus("接收文件路径");

                m_filePath = command.Split(SYM_SPLIT)[1];

                Invoke(new Action(() => { txtFilePath.Text = m_filePath.ToString(); }));

                m_pipeClient.SendRevCommand(command.Split(SYM_SPLIT)[2]);
                return;
            }

            if (command.StartsWith($"{CMD_SAVE_PATH}{SYM_SPLIT}"))
            {
                SetStatus("接收文件保存路径");

                m_fileSavePath = command.Split(SYM_SPLIT)[1];

                Invoke(new Action(() => { txtFileSavePath.Text = m_fileSavePath.ToString(); }));

                m_pipeClient.SendRevCommand(command.Split(SYM_SPLIT)[2]);
                return;
            }

            if (command.StartsWith($"{CMD_SIZE}{SYM_SPLIT}"))
            {
                SetStatus("接收文件大小");

                string errMsg = string.Empty;
                if (!int.TryParse(command.Split(SYM_SPLIT)[1], out m_fileSize))
                {
                    m_fileSize = -1;
                    errMsg = $"文件大小错误-{command.Split(SYM_SPLIT)[1]}";
                }

                Invoke(new Action(() => { txtFileSize.Text = m_fileSize.ToString(); }));

                m_pipeClient.SendRevCommand(command.Split(SYM_SPLIT)[2], errMsg);
                return;
            }

            if (command.StartsWith($"{CMD_MD5}{SYM_SPLIT}"))
            {
                SetStatus("接收文件校验值");

                m_fileCheckSum = command.Split(SYM_SPLIT)[1];

                Invoke(new Action(() => { txtCheckSum.Text = m_fileCheckSum.ToString(); }));

                m_pipeClient.SendRevCommand(command.Split(SYM_SPLIT)[2]);
                return;
            }

            if (command.StartsWith($"{CMD_READY}{SYM_SPLIT}"))
            {
                SetStatus("开始读取文件内容");

                string errMsg = string.Empty;
                if (m_fileSize == -1)
                {
                    errMsg = "未接收到文件大小";
                }
                else if (string.IsNullOrEmpty(m_fileSavePath))
                {
                    errMsg = "未接收到文件路径";
                }
                else
                {
                    try
                    {
                        using (var viewStream = m_memoryMappedFile.CreateViewStream())
                        {
                            using (BinaryReader binaryReader = new BinaryReader(viewStream))
                            {
                                if (Directory.GetParent(m_fileSavePath) != null && !Directory.Exists(Directory.GetParent(m_fileSavePath).FullName))
                                {
                                    Directory.CreateDirectory(Directory.GetParent(m_fileSavePath).FullName);
                                }
                                using (FileStream fileStream = new FileStream(m_fileSavePath, FileMode.Create, FileAccess.Write))
                                {
                                    fileStream.Write(binaryReader.ReadBytes(m_fileSize), 0, m_fileSize);
                                }
                            }
                        }

                        if (!string.IsNullOrEmpty(m_fileCheckSum))
                        {
                            string md5 = EncodeHelper.CalcFileMD5(m_fileSavePath);
                            if (md5 != m_fileCheckSum)
                            {
                                File.Delete(m_fileSavePath);
                                errMsg = $"文件内容验证失败";
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        if (File.Exists(m_fileSavePath)) { File.Delete(m_fileSavePath); }
                        errMsg = $"文件内容读取失败-{ex.Message}";
                    }
                }

                m_pipeClient.SendRevCommand(command.Split(SYM_SPLIT)[1], errMsg);
                return;
            }

            if (command.StartsWith($"{CMD_END}{SYM_SPLIT}"))
            {
                SetStatus("文件接收完成");
                PushLog($"[ReceiveFileThread]文件接收完成！");

                m_pipeClient.SendRevCommand(command.Split(SYM_SPLIT)[1]);
                return;
            }
        }

        private void SetStatus(string status)
        {
            if (InvokeRequired)
            {
                Thread.Sleep(1000);
                txtStatus.Invoke(new Action(() => { SetStatus(status); }));
            }
            else
            {
                txtStatus.Text = status;
            }
        }

        private void PushLog(string message)
        {
            if (InvokeRequired)
            {
                lstLog.Invoke(new Action(() => { PushLog(message); }));
            }
            else
            {
                lstLog.Items.Add($"<{DateTime.Now:HH:mm:ss}> {message}");
                lstLog.SelectedIndex = lstLog.Items.Count - 1;
            }
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtGuid.Text))
            {
                MessageBox.Show("请输入标识ID！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            m_pipeClient = new PipeClient(txtGuid.Text);
            m_pipeClient.LogEvent += (location, message) => PushLog($"[{location}]{message}");
            m_pipeClient.CommandEvent += PipeClient_CommandEvent;
            m_pipeClient.StartPipeStream();

            m_memoryMappedFile = MemoryMappedFile.OpenExisting(txtGuid.Text);
            PushLog($"[BtnStart_Click]共享内存打开成功: {txtGuid.Text}");

            btnStart.Enabled = false;
            btnStart.Text = "已连接";
        }
    }
}
