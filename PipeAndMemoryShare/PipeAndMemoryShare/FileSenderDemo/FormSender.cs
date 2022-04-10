using Common;
using System;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Common.PipeCommand;

namespace FileSenderDemo
{
    public partial class FormSender : Form
    {
        /// <summary>
        /// 通讯管道
        /// </summary>
        private PipeServer m_pipeServer = null;
        /// <summary>
        /// 共享内存
        /// </summary>
        private MemoryMappedFile m_memoryMappedFile = null;

        public FormSender()
        {
            InitializeComponent();
        }

        private void FormSender_Load(object sender, EventArgs e)
        {
            txtGuid.Text = Guid.NewGuid().ToString();
        }

        private void FormSender_Shown(object sender, EventArgs e)
        {
            btnStart.Focus();
        }

        private void PipeServer_Command(string command)
        {
            if (command == CMD_EXIT)
            {
                btnSend.Invoke(new Action(() => { btnSend.Enabled = false; }));
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

        private void BtnSelectFilePath_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "请选择待发送文件",
                Filter = "所有文件|*.*"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                txtFilePath.Text = openFileDialog.FileName;
            }
        }

        private void BtnSelectSaveFilePath_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Title = "请选择文件保存路径",
                Filter = "所有文件|*.*"
            };

            if (File.Exists(txtFilePath.Text))
            {
                saveFileDialog.FileName = Path.GetFileName(txtFilePath.Text);
            }

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                txtSavePath.Text = saveFileDialog.FileName;
            }
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            m_pipeServer = new PipeServer(txtGuid.Text);
            m_pipeServer.LogEvent += (location, message) => PushLog($"[{location}]{message}");
            m_pipeServer.CommandEvent += PipeServer_Command;
            m_pipeServer.StartPipeStream();

            m_memoryMappedFile = MemoryMappedFile.CreateNew(txtGuid.Text, 1024 * 1024 * 1024);
            PushLog($"[BtnStart_Click]共享内存创建成功: {txtGuid.Text}");

            btnStart.Enabled = false;
            btnStart.Text = "已启动";
        }

        private void BtnSend_Click(object sender, EventArgs e)
        {
            if (m_pipeServer == null)
            {
                MessageBox.Show("请先初始化管道与共享内存！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!m_pipeServer.IsConnected)
            {
                MessageBox.Show("请等待接收端连接！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (string.IsNullOrEmpty(txtFilePath.Text))
            {
                MessageBox.Show("请选择待发送文件！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrEmpty(txtSavePath.Text))
            {
                MessageBox.Show("请选择文件保存路径！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (txtFilePath.Text == txtSavePath.Text)
            {
                MessageBox.Show("待发送文件与保存文件路径相同，无法发送！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string filePath = txtFilePath.Text;
            string saveFilePath = txtSavePath.Text;
            bool isNeedCheckSum = chkCheckSum.Checked;

            btnSend.Enabled = false;

            Task.Factory.StartNew(() =>
            {
                SetStatus("开始发送文件");
                PushLog($"[SendFileThread]开始发送文件: {filePath}");

                SetStatus("校验文件信息");
                FileInfo file = new FileInfo(filePath);
                if (!file.Exists) { PushLog("文件不存在，结束文件发送"); return; }
                if (file.Length > 1024 * 1024 * 1024) { PushLog("[SendFileThread]文件大小 >1GB，结束文件发送"); return; }

                SetStatus("开始发送文件");
                string guid = Guid.NewGuid().ToString();
                m_pipeServer.SendCommand($"{CMD_START}{SYM_SPLIT}{guid}");
                if (!m_pipeServer.JudgeRevGuid(out string errMsg, guid, 5)) { PushLog("[SendFileThread]请求文件转储超时，结束文件发送"); return; }
                else if (!string.IsNullOrEmpty(errMsg)) { PushLog("[SendFileThread]请求文件转储错误，结束文件发送: " + errMsg); return; }

                SetStatus("发送文件路径");
                guid = Guid.NewGuid().ToString();
                m_pipeServer.SendCommand($"{CMD_PATH}{SYM_SPLIT}{filePath}{SYM_SPLIT}{guid}");
                if (!m_pipeServer.JudgeRevGuid(out errMsg, guid, 5)) { PushLog("[SendFileThread]发送文件路径超时，结束文件发送"); return; }
                else if (!string.IsNullOrEmpty(errMsg)) { PushLog("[SendFileThread]发送文件路径错误，结束文件发送: " + errMsg); return; }

                SetStatus("发送文件保存路径");
                guid = Guid.NewGuid().ToString();
                m_pipeServer.SendCommand($"{CMD_SAVE_PATH}{SYM_SPLIT}{saveFilePath}{SYM_SPLIT}{guid}");
                if (!m_pipeServer.JudgeRevGuid(out errMsg, guid, 5)) { PushLog("[SendFileThread]发送文件保存路径超时，结束文件发送"); return; }
                else if (!string.IsNullOrEmpty(errMsg)) { PushLog("[SendFileThread]发送文件保存路径错误，结束文件发送: " + errMsg); return; }

                Invoke(new Action(() => { txtFileSize.Text = file.Length.ToString(); }));

                SetStatus("发送文件大小");
                guid = Guid.NewGuid().ToString();
                m_pipeServer.SendCommand($"{CMD_SIZE}{SYM_SPLIT}{file.Length}{SYM_SPLIT}{guid}");
                if (!m_pipeServer.JudgeRevGuid(out errMsg, guid, 5)) { PushLog("[SendFileThread]发送文件大小超时，结束文件发送"); return; }
                else if (!string.IsNullOrEmpty(errMsg)) { PushLog("[SendFileThread]发送文件大小错误，结束文件发送: " + errMsg); return; }

                if (isNeedCheckSum)
                {
                    try
                    {
                        SetStatus("计算文件校验值");
                        string checkSum = EncodeHelper.CalcFileMD5(filePath);

                        Invoke(new Action(() => { txtCheckSum.Text = checkSum; }));

                        SetStatus("发送文件校验值");
                        guid = Guid.NewGuid().ToString();
                        m_pipeServer.SendCommand($"{CMD_MD5}{SYM_SPLIT}{checkSum}{SYM_SPLIT}{guid}");
                        if (!m_pipeServer.JudgeRevGuid(out errMsg, guid, 5)) { PushLog("[SendFileThread]发送文件校验码计算超时，结束文件发送"); return; }
                        else if (!string.IsNullOrEmpty(errMsg)) { PushLog("[SendFileThread]发送文件校验码计算错误，结束文件发送: " + errMsg); return; }
                    }
                    catch (Exception ex)
                    {
                        PushLog($"[SendFileThread]文件校验码计算失败，结束文件发送: {ex.Message}"); return;
                    }
                }

                SetStatus("将文件内容写入内存");
                try
                {
                    using (var viewStream = m_memoryMappedFile.CreateViewStream())
                    {
                        using (BinaryWriter binaryWriter = new BinaryWriter(viewStream))
                        {
                            binaryWriter.Write(File.ReadAllBytes(file.FullName));
                        }
                    }
                }
                catch (Exception ex)
                {
                    PushLog($"[SendFileThread]文件内容读取失败，结束文件发送: {ex.Message}"); return;
                }

                SetStatus("等待接收文件");
                guid = Guid.NewGuid().ToString();
                m_pipeServer.SendCommand($"{CMD_READY}{SYM_SPLIT}{guid}");
                if (!m_pipeServer.JudgeRevGuid(out errMsg, guid, 60)) { PushLog("文件内容转储超时"); return; }
                else if (!string.IsNullOrEmpty(errMsg)) { PushLog("文件内容转储错误: " + errMsg); return; }

                SetStatus("确认接收文件");
                guid = Guid.NewGuid().ToString();
                m_pipeServer.SendCommand($"{CMD_END}{SYM_SPLIT}{guid}");
                if (!m_pipeServer.JudgeRevGuid(out errMsg, guid, 5)) { PushLog("确认文件转储超时"); return; }
                else if (!string.IsNullOrEmpty(errMsg)) { PushLog("确认文件转储错误: " + errMsg); return; }

                SetStatus("文件发送完成");
                PushLog($"[SendFileThread]文件发送完成！");

                Invoke(new Action(() => { btnSend.Enabled = true; }));
            });
        }
    }
}
