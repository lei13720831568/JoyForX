namespace JoyForX.UI
{
    partial class MainForm
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
            this.btnOpenPort = new System.Windows.Forms.Button();
            this.btnClosePort = new System.Windows.Forms.Button();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.btnRead = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.cbPorts = new System.Windows.Forms.ComboBox();
            this.cbRef = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lbResultCount = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cbJoy = new System.Windows.Forms.ComboBox();
            this.rc_keep = new MultiWiiGUIControls.rc_input_control();
            this.rc_Read = new MultiWiiGUIControls.rc_input_control();
            this.btnCheckJoy = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnOpenPort
            // 
            this.btnOpenPort.Location = new System.Drawing.Point(12, 91);
            this.btnOpenPort.Name = "btnOpenPort";
            this.btnOpenPort.Size = new System.Drawing.Size(75, 23);
            this.btnOpenPort.TabIndex = 0;
            this.btnOpenPort.Text = "打开端口";
            this.btnOpenPort.UseVisualStyleBackColor = true;
            this.btnOpenPort.Click += new System.EventHandler(this.btnOpenPort_Click);
            // 
            // btnClosePort
            // 
            this.btnClosePort.Enabled = false;
            this.btnClosePort.Location = new System.Drawing.Point(114, 91);
            this.btnClosePort.Name = "btnClosePort";
            this.btnClosePort.Size = new System.Drawing.Size(75, 23);
            this.btnClosePort.TabIndex = 1;
            this.btnClosePort.Text = "关闭端口";
            this.btnClosePort.UseVisualStyleBackColor = true;
            this.btnClosePort.Click += new System.EventHandler(this.btnClosePort_Click);
            // 
            // txtLog
            // 
            this.txtLog.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.txtLog.Location = new System.Drawing.Point(0, 367);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(975, 105);
            this.txtLog.TabIndex = 3;
            this.txtLog.TabStop = false;
            // 
            // btnRead
            // 
            this.btnRead.Location = new System.Drawing.Point(14, 259);
            this.btnRead.Name = "btnRead";
            this.btnRead.Size = new System.Drawing.Size(75, 23);
            this.btnRead.TabIndex = 4;
            this.btnRead.Text = "测试读数";
            this.btnRead.UseVisualStyleBackColor = true;
            this.btnRead.Click += new System.EventHandler(this.btnRead_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(105, 259);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 5;
            this.button4.Text = "SendRC";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // cbPorts
            // 
            this.cbPorts.FormattingEnabled = true;
            this.cbPorts.Location = new System.Drawing.Point(68, 13);
            this.cbPorts.Name = "cbPorts";
            this.cbPorts.Size = new System.Drawing.Size(121, 20);
            this.cbPorts.TabIndex = 6;
            // 
            // cbRef
            // 
            this.cbRef.FormattingEnabled = true;
            this.cbRef.Items.AddRange(new object[] {
            "1000",
            "500",
            "250",
            "100",
            "50",
            "20"});
            this.cbRef.Location = new System.Drawing.Point(124, 39);
            this.cbRef.Name = "cbRef";
            this.cbRef.Size = new System.Drawing.Size(65, 20);
            this.cbRef.TabIndex = 25;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 12);
            this.label1.TabIndex = 26;
            this.label1.Text = "刷新频率(单位ms)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(329, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(137, 12);
            this.label2.TabIndex = 27;
            this.label2.Text = "接收到的反馈消息数量：";
            // 
            // lbResultCount
            // 
            this.lbResultCount.AutoSize = true;
            this.lbResultCount.Location = new System.Drawing.Point(462, 16);
            this.lbResultCount.Name = "lbResultCount";
            this.lbResultCount.Size = new System.Drawing.Size(41, 12);
            this.lbResultCount.TabIndex = 28;
            this.lbResultCount.Text = "label3";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(200, 259);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 30;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 31;
            this.label3.Text = "串口选择";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 69);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 32;
            this.label4.Text = "手柄选择";
            // 
            // cbJoy
            // 
            this.cbJoy.FormattingEnabled = true;
            this.cbJoy.Location = new System.Drawing.Point(68, 65);
            this.cbJoy.Name = "cbJoy";
            this.cbJoy.Size = new System.Drawing.Size(121, 20);
            this.cbJoy.TabIndex = 33;
            // 
            // rc_keep
            // 
            this.rc_keep.Location = new System.Drawing.Point(545, 29);
            this.rc_keep.Name = "rc_keep";
            this.rc_keep.Size = new System.Drawing.Size(200, 150);
            this.rc_keep.TabIndex = 29;
            this.rc_keep.Text = "rc_input_control1";
            // 
            // rc_Read
            // 
            this.rc_Read.Location = new System.Drawing.Point(751, 29);
            this.rc_Read.Name = "rc_Read";
            this.rc_Read.Size = new System.Drawing.Size(200, 150);
            this.rc_Read.TabIndex = 24;
            this.rc_Read.Text = "rc_input_control1";
            // 
            // btnCheckJoy
            // 
            this.btnCheckJoy.Location = new System.Drawing.Point(200, 64);
            this.btnCheckJoy.Name = "btnCheckJoy";
            this.btnCheckJoy.Size = new System.Drawing.Size(75, 23);
            this.btnCheckJoy.TabIndex = 34;
            this.btnCheckJoy.Text = "检查手柄";
            this.btnCheckJoy.UseVisualStyleBackColor = true;
            this.btnCheckJoy.Click += new System.EventHandler(this.btnCheckJoy_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(975, 472);
            this.Controls.Add(this.btnCheckJoy);
            this.Controls.Add(this.cbJoy);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.rc_keep);
            this.Controls.Add(this.lbResultCount);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbRef);
            this.Controls.Add(this.rc_Read);
            this.Controls.Add(this.cbPorts);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.btnRead);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.btnClosePort);
            this.Controls.Add(this.btnOpenPort);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOpenPort;
        private System.Windows.Forms.Button btnClosePort;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.Button btnRead;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.ComboBox cbPorts;
        private MultiWiiGUIControls.rc_input_control rc_Read;
        private System.Windows.Forms.ComboBox cbRef;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbResultCount;
        private MultiWiiGUIControls.rc_input_control rc_keep;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbJoy;
        private System.Windows.Forms.Button btnCheckJoy;
    }
}

