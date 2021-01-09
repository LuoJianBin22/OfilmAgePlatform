namespace OfilmAgePlatform
{
    partial class Form_Communication
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tcpClientCtrl_Slave = new Jim.CommunicationCtrls.TcpClientCtrl();
            this.tcpClientCtrl_ageForm = new Jim.CommunicationCtrls.TcpClientCtrl();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(800, 450);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tcpClientCtrl_ageForm);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(792, 421);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "与老化平台通讯";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.tcpClientCtrl_Slave);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(792, 421);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "与主控上位机服务端通讯";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tcpClientCtrl_Slave
            // 
            this.tcpClientCtrl_Slave.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tcpClientCtrl_Slave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcpClientCtrl_Slave.EndChar = "CRLF";
            this.tcpClientCtrl_Slave.HeartBeatFeedBackData = "Online";
            this.tcpClientCtrl_Slave.HeartBeatSendData = "Online";
            this.tcpClientCtrl_Slave.HeartBeatWaitTimes = ((uint)(1u));
            this.tcpClientCtrl_Slave.HeatBeatElapsedTime = ((uint)(3000u));
            this.tcpClientCtrl_Slave.Location = new System.Drawing.Point(3, 3);
            this.tcpClientCtrl_Slave.Name = "tcpClientCtrl_Slave";
            this.tcpClientCtrl_Slave.PrintMaxLines = ((uint)(1000u));
            this.tcpClientCtrl_Slave.RecvDataType = Jim.CommunicationCtrls.TCPClient.emRecvDataType.NormalStr;
            this.tcpClientCtrl_Slave.RecvEncode = Jim.CommunicationCtrls.TCPClient.emRecvEncoding.Default;
            this.tcpClientCtrl_Slave.RemoteIP = "127.0.0.1";
            this.tcpClientCtrl_Slave.RemotePort = 5000;
            this.tcpClientCtrl_Slave.SendDataType = Jim.CommunicationCtrls.TCPClient.emSendDataType.NormalStr;
            this.tcpClientCtrl_Slave.SendText = new string[0];
            this.tcpClientCtrl_Slave.Size = new System.Drawing.Size(786, 415);
            this.tcpClientCtrl_Slave.TabIndex = 0;
            // 
            // tcpClientCtrl_ageForm
            // 
            this.tcpClientCtrl_ageForm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tcpClientCtrl_ageForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcpClientCtrl_ageForm.EndChar = "CRLF";
            this.tcpClientCtrl_ageForm.HeartBeatFeedBackData = "Online";
            this.tcpClientCtrl_ageForm.HeartBeatSendData = "Online";
            this.tcpClientCtrl_ageForm.HeartBeatWaitTimes = ((uint)(1u));
            this.tcpClientCtrl_ageForm.HeatBeatElapsedTime = ((uint)(3000u));
            this.tcpClientCtrl_ageForm.Location = new System.Drawing.Point(3, 3);
            this.tcpClientCtrl_ageForm.Name = "tcpClientCtrl_ageForm";
            this.tcpClientCtrl_ageForm.PrintMaxLines = ((uint)(1000u));
            this.tcpClientCtrl_ageForm.RecvDataType = Jim.CommunicationCtrls.TCPClient.emRecvDataType.NormalStr;
            this.tcpClientCtrl_ageForm.RecvEncode = Jim.CommunicationCtrls.TCPClient.emRecvEncoding.Default;
            this.tcpClientCtrl_ageForm.RemoteIP = "127.0.0.1";
            this.tcpClientCtrl_ageForm.RemotePort = 5000;
            this.tcpClientCtrl_ageForm.SendDataType = Jim.CommunicationCtrls.TCPClient.emSendDataType.NormalStr;
            this.tcpClientCtrl_ageForm.SendText = new string[0];
            this.tcpClientCtrl_ageForm.Size = new System.Drawing.Size(786, 415);
            this.tcpClientCtrl_ageForm.TabIndex = 1;
            // 
            // Form_Communication
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form_Communication";
            this.Text = "Form_Communication";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        public Jim.CommunicationCtrls.TcpClientCtrl tcpClientCtrl_Slave;
        private System.Windows.Forms.TabControl tabControl1;
        public Jim.CommunicationCtrls.TcpClientCtrl tcpClientCtrl_ageForm;
    }
}