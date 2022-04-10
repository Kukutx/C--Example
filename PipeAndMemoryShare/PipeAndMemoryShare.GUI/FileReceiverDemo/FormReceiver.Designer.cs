
namespace FileReceiverDemo
{
    partial class FormReceiver
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormReceiver));
            this.lblTitle = new System.Windows.Forms.Label();
            this.grpRev = new System.Windows.Forms.GroupBox();
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.txtCheckSum = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.txtFileSize = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtFileSavePath = new System.Windows.Forms.TextBox();
            this.lstLog = new System.Windows.Forms.ListBox();
            this.grpComm = new System.Windows.Forms.GroupBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.txtGuid = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.grpRev.SuspendLayout();
            this.grpComm.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft YaHei", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(800, 70);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "文 件 接 收 端";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // grpRev
            // 
            this.grpRev.Controls.Add(this.txtStatus);
            this.grpRev.Controls.Add(this.txtCheckSum);
            this.grpRev.Controls.Add(this.label6);
            this.grpRev.Controls.Add(this.label2);
            this.grpRev.Controls.Add(this.label5);
            this.grpRev.Controls.Add(this.txtFilePath);
            this.grpRev.Controls.Add(this.txtFileSize);
            this.grpRev.Controls.Add(this.label3);
            this.grpRev.Controls.Add(this.label4);
            this.grpRev.Controls.Add(this.txtFileSavePath);
            this.grpRev.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpRev.Location = new System.Drawing.Point(0, 142);
            this.grpRev.Name = "grpRev";
            this.grpRev.Size = new System.Drawing.Size(800, 214);
            this.grpRev.TabIndex = 2;
            this.grpRev.TabStop = false;
            this.grpRev.Text = "接收文件信息";
            // 
            // txtStatus
            // 
            this.txtStatus.BackColor = System.Drawing.Color.White;
            this.txtStatus.Location = new System.Drawing.Point(112, 28);
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.ReadOnly = true;
            this.txtStatus.Size = new System.Drawing.Size(655, 29);
            this.txtStatus.TabIndex = 1;
            this.txtStatus.Text = "等待接收文件";
            // 
            // txtCheckSum
            // 
            this.txtCheckSum.BackColor = System.Drawing.Color.White;
            this.txtCheckSum.Location = new System.Drawing.Point(112, 168);
            this.txtCheckSum.Name = "txtCheckSum";
            this.txtCheckSum.ReadOnly = true;
            this.txtCheckSum.Size = new System.Drawing.Size(655, 29);
            this.txtCheckSum.TabIndex = 9;
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
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 21);
            this.label2.TabIndex = 2;
            this.label2.Text = "文件路径";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(49, 172);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 21);
            this.label5.TabIndex = 8;
            this.label5.Text = "校验码";
            // 
            // txtFilePath
            // 
            this.txtFilePath.BackColor = System.Drawing.Color.White;
            this.txtFilePath.Location = new System.Drawing.Point(112, 63);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.ReadOnly = true;
            this.txtFilePath.Size = new System.Drawing.Size(655, 29);
            this.txtFilePath.TabIndex = 3;
            // 
            // txtFileSize
            // 
            this.txtFileSize.BackColor = System.Drawing.Color.White;
            this.txtFileSize.Location = new System.Drawing.Point(112, 133);
            this.txtFileSize.Name = "txtFileSize";
            this.txtFileSize.ReadOnly = true;
            this.txtFileSize.Size = new System.Drawing.Size(655, 29);
            this.txtFileSize.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(33, 102);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 21);
            this.label3.TabIndex = 4;
            this.label3.Text = "保存路径";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(33, 137);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 21);
            this.label4.TabIndex = 6;
            this.label4.Text = "文件大小";
            // 
            // txtFileSavePath
            // 
            this.txtFileSavePath.BackColor = System.Drawing.Color.White;
            this.txtFileSavePath.Location = new System.Drawing.Point(112, 98);
            this.txtFileSavePath.Name = "txtFileSavePath";
            this.txtFileSavePath.ReadOnly = true;
            this.txtFileSavePath.Size = new System.Drawing.Size(655, 29);
            this.txtFileSavePath.TabIndex = 5;
            // 
            // lstLog
            // 
            this.lstLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstLog.FormattingEnabled = true;
            this.lstLog.IntegralHeight = false;
            this.lstLog.ItemHeight = 21;
            this.lstLog.Location = new System.Drawing.Point(0, 356);
            this.lstLog.Name = "lstLog";
            this.lstLog.Size = new System.Drawing.Size(800, 255);
            this.lstLog.TabIndex = 3;
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
            // FormReceiver
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(800, 611);
            this.Controls.Add(this.lstLog);
            this.Controls.Add(this.grpRev);
            this.Controls.Add(this.grpComm);
            this.Controls.Add(this.lblTitle);
            this.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(816, 650);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(816, 650);
            this.Name = "FormReceiver";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "文件接收端";
            this.grpRev.ResumeLayout(false);
            this.grpRev.PerformLayout();
            this.grpComm.ResumeLayout(false);
            this.grpComm.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.GroupBox grpRev;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtFileSavePath;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtFileSize;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtCheckSum;
        private System.Windows.Forms.ListBox lstLog;
        private System.Windows.Forms.TextBox txtStatus;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox grpComm;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.TextBox txtGuid;
        private System.Windows.Forms.Label label1;
    }
}

