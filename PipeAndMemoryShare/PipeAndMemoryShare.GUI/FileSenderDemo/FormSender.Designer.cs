
namespace FileSenderDemo
{
    partial class FormSender
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSender));
            this.lblTitle = new System.Windows.Forms.Label();
            this.grpSend = new System.Windows.Forms.GroupBox();
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.txtCheckSum = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtFileSize = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtSavePath = new System.Windows.Forms.TextBox();
            this.grpSelectFile = new System.Windows.Forms.GroupBox();
            this.chkCheckSum = new System.Windows.Forms.CheckBox();
            this.btnSelectSaveFilePath = new System.Windows.Forms.Button();
            this.btnSelectFilePath = new System.Windows.Forms.Button();
            this.btnSend = new System.Windows.Forms.Button();
            this.grpComm = new System.Windows.Forms.GroupBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.txtGuid = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lstLog = new System.Windows.Forms.ListBox();
            this.grpSend.SuspendLayout();
            this.grpSelectFile.SuspendLayout();
            this.grpComm.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft YaHei", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(800, 70);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "文 件 发 送 端";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // grpSend
            // 
            this.grpSend.Controls.Add(this.txtStatus);
            this.grpSend.Controls.Add(this.txtCheckSum);
            this.grpSend.Controls.Add(this.label6);
            this.grpSend.Controls.Add(this.label5);
            this.grpSend.Controls.Add(this.txtFileSize);
            this.grpSend.Controls.Add(this.label4);
            this.grpSend.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpSend.Location = new System.Drawing.Point(0, 292);
            this.grpSend.Name = "grpSend";
            this.grpSend.Size = new System.Drawing.Size(800, 137);
            this.grpSend.TabIndex = 3;
            this.grpSend.TabStop = false;
            this.grpSend.Text = "发送文件信息";
            // 
            // txtStatus
            // 
            this.txtStatus.BackColor = System.Drawing.Color.White;
            this.txtStatus.Location = new System.Drawing.Point(112, 28);
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.ReadOnly = true;
            this.txtStatus.Size = new System.Drawing.Size(655, 29);
            this.txtStatus.TabIndex = 1;
            this.txtStatus.Text = "等待发送文件";
            // 
            // txtCheckSum
            // 
            this.txtCheckSum.BackColor = System.Drawing.Color.White;
            this.txtCheckSum.Location = new System.Drawing.Point(112, 98);
            this.txtCheckSum.Name = "txtCheckSum";
            this.txtCheckSum.ReadOnly = true;
            this.txtCheckSum.Size = new System.Drawing.Size(655, 29);
            this.txtCheckSum.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(33, 32);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 21);
            this.label6.TabIndex = 0;
            this.label6.Text = "当前状态";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(49, 102);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 21);
            this.label5.TabIndex = 4;
            this.label5.Text = "校验码";
            // 
            // txtFileSize
            // 
            this.txtFileSize.BackColor = System.Drawing.Color.White;
            this.txtFileSize.Location = new System.Drawing.Point(112, 63);
            this.txtFileSize.Name = "txtFileSize";
            this.txtFileSize.ReadOnly = true;
            this.txtFileSize.Size = new System.Drawing.Size(655, 29);
            this.txtFileSize.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(33, 67);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 21);
            this.label4.TabIndex = 2;
            this.label4.Text = "文件大小";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 21);
            this.label2.TabIndex = 0;
            this.label2.Text = "文件路径";
            // 
            // txtFilePath
            // 
            this.txtFilePath.BackColor = System.Drawing.Color.White;
            this.txtFilePath.Location = new System.Drawing.Point(112, 28);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.ReadOnly = true;
            this.txtFilePath.Size = new System.Drawing.Size(564, 29);
            this.txtFilePath.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(33, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 21);
            this.label3.TabIndex = 3;
            this.label3.Text = "保存路径";
            // 
            // txtSavePath
            // 
            this.txtSavePath.BackColor = System.Drawing.Color.White;
            this.txtSavePath.Location = new System.Drawing.Point(112, 63);
            this.txtSavePath.Name = "txtSavePath";
            this.txtSavePath.ReadOnly = true;
            this.txtSavePath.Size = new System.Drawing.Size(564, 29);
            this.txtSavePath.TabIndex = 4;
            // 
            // grpSelectFile
            // 
            this.grpSelectFile.Controls.Add(this.chkCheckSum);
            this.grpSelectFile.Controls.Add(this.btnSelectSaveFilePath);
            this.grpSelectFile.Controls.Add(this.btnSelectFilePath);
            this.grpSelectFile.Controls.Add(this.btnSend);
            this.grpSelectFile.Controls.Add(this.txtFilePath);
            this.grpSelectFile.Controls.Add(this.txtSavePath);
            this.grpSelectFile.Controls.Add(this.label2);
            this.grpSelectFile.Controls.Add(this.label3);
            this.grpSelectFile.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpSelectFile.Location = new System.Drawing.Point(0, 142);
            this.grpSelectFile.Name = "grpSelectFile";
            this.grpSelectFile.Size = new System.Drawing.Size(800, 150);
            this.grpSelectFile.TabIndex = 2;
            this.grpSelectFile.TabStop = false;
            this.grpSelectFile.Text = "发送设置";
            // 
            // chkCheckSum
            // 
            this.chkCheckSum.AutoSize = true;
            this.chkCheckSum.Location = new System.Drawing.Point(527, 106);
            this.chkCheckSum.Name = "chkCheckSum";
            this.chkCheckSum.Size = new System.Drawing.Size(93, 25);
            this.chkCheckSum.TabIndex = 6;
            this.chkCheckSum.Text = "校验文件";
            this.chkCheckSum.UseVisualStyleBackColor = true;
            // 
            // btnSelectSaveFilePath
            // 
            this.btnSelectSaveFilePath.Location = new System.Drawing.Point(682, 62);
            this.btnSelectSaveFilePath.Name = "btnSelectSaveFilePath";
            this.btnSelectSaveFilePath.Size = new System.Drawing.Size(85, 30);
            this.btnSelectSaveFilePath.TabIndex = 5;
            this.btnSelectSaveFilePath.Text = "选 择";
            this.btnSelectSaveFilePath.UseVisualStyleBackColor = true;
            // 
            // btnSelectFilePath
            // 
            this.btnSelectFilePath.Location = new System.Drawing.Point(682, 27);
            this.btnSelectFilePath.Name = "btnSelectFilePath";
            this.btnSelectFilePath.Size = new System.Drawing.Size(85, 30);
            this.btnSelectFilePath.TabIndex = 2;
            this.btnSelectFilePath.Text = "选 择";
            this.btnSelectFilePath.UseVisualStyleBackColor = true;
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(625, 98);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(142, 41);
            this.btnSend.TabIndex = 7;
            this.btnSend.Text = "发 送 文 件";
            this.btnSend.UseVisualStyleBackColor = true;
            // 
            // grpComm
            // 
            this.grpComm.Controls.Add(this.btnStart);
            this.grpComm.Controls.Add(this.txtGuid);
            this.grpComm.Controls.Add(this.label1);
            this.grpComm.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpComm.Location = new System.Drawing.Point(0, 70);
            this.grpComm.Name = "grpComm";
            this.grpComm.Size = new System.Drawing.Size(800, 72);
            this.grpComm.TabIndex = 1;
            this.grpComm.TabStop = false;
            this.grpComm.Text = "通讯设置";
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(682, 27);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(85, 30);
            this.btnStart.TabIndex = 2;
            this.btnStart.Text = "初始化";
            this.btnStart.UseVisualStyleBackColor = true;
            // 
            // txtGuid
            // 
            this.txtGuid.BackColor = System.Drawing.Color.White;
            this.txtGuid.Location = new System.Drawing.Point(112, 28);
            this.txtGuid.Name = "txtGuid";
            this.txtGuid.ReadOnly = true;
            this.txtGuid.Size = new System.Drawing.Size(564, 29);
            this.txtGuid.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(48, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "标识ID";
            // 
            // lstLog
            // 
            this.lstLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstLog.Font = new System.Drawing.Font("Microsoft YaHei", 12F);
            this.lstLog.FormattingEnabled = true;
            this.lstLog.IntegralHeight = false;
            this.lstLog.ItemHeight = 21;
            this.lstLog.Location = new System.Drawing.Point(0, 429);
            this.lstLog.Name = "lstLog";
            this.lstLog.Size = new System.Drawing.Size(800, 182);
            this.lstLog.TabIndex = 17;
            // 
            // FormSender
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(800, 611);
            this.Controls.Add(this.lstLog);
            this.Controls.Add(this.grpSend);
            this.Controls.Add(this.grpSelectFile);
            this.Controls.Add(this.grpComm);
            this.Controls.Add(this.lblTitle);
            this.Font = new System.Drawing.Font("Microsoft YaHei", 12F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(816, 650);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(816, 650);
            this.Name = "FormSender";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "文件发送端";
            this.grpSend.ResumeLayout(false);
            this.grpSend.PerformLayout();
            this.grpSelectFile.ResumeLayout(false);
            this.grpSelectFile.PerformLayout();
            this.grpComm.ResumeLayout(false);
            this.grpComm.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.GroupBox grpSend;
        private System.Windows.Forms.TextBox txtStatus;
        private System.Windows.Forms.TextBox txtCheckSum;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.TextBox txtFileSize;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtSavePath;
        private System.Windows.Forms.GroupBox grpSelectFile;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.CheckBox chkCheckSum;
        private System.Windows.Forms.Button btnSelectSaveFilePath;
        private System.Windows.Forms.Button btnSelectFilePath;
        private System.Windows.Forms.GroupBox grpComm;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.TextBox txtGuid;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox lstLog;
    }
}

