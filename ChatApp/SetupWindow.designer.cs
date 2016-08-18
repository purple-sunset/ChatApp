namespace ChatApp
{
    partial class SetupWindow
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
            this.label1 = new System.Windows.Forms.Label();
            this.textMyAddress = new System.Windows.Forms.TextBox();
            this.textFriendAddress = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textSendPort = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textReceivePort = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "IP Máy";
            // 
            // textMyAddress
            // 
            this.textMyAddress.Location = new System.Drawing.Point(109, 26);
            this.textMyAddress.Name = "textMyAddress";
            this.textMyAddress.Size = new System.Drawing.Size(216, 20);
            this.textMyAddress.TabIndex = 1;
            // 
            // textFriendAddress
            // 
            this.textFriendAddress.Location = new System.Drawing.Point(109, 80);
            this.textFriendAddress.Name = "textFriendAddress";
            this.textFriendAddress.Size = new System.Drawing.Size(216, 20);
            this.textFriendAddress.TabIndex = 3;
            this.textFriendAddress.Text = "169.254.80.80";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "IP Bạn";
            // 
            // textSendPort
            // 
            this.textSendPort.Location = new System.Drawing.Point(109, 139);
            this.textSendPort.Name = "textSendPort";
            this.textSendPort.Size = new System.Drawing.Size(216, 20);
            this.textSendPort.TabIndex = 5;
            this.textSendPort.Text = "54595";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 142);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Port Gửi";
            // 
            // textReceivePort
            // 
            this.textReceivePort.Location = new System.Drawing.Point(109, 192);
            this.textReceivePort.Name = "textReceivePort";
            this.textReceivePort.Size = new System.Drawing.Size(216, 20);
            this.textReceivePort.TabIndex = 7;
            this.textReceivePort.Text = "54595";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(30, 195);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Port Nhận";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(343, 256);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(111, 45);
            this.btnOK.TabIndex = 8;
            this.btnOK.Text = "Connect";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // SetupWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(498, 320);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.textReceivePort);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textSendPort);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textFriendAddress);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textMyAddress);
            this.Controls.Add(this.label1);
            this.Name = "SetupWindow";
            this.Text = "SetupWindow";
            this.Load += new System.EventHandler(this.SetupWindow_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textMyAddress;
        private System.Windows.Forms.TextBox textFriendAddress;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textSendPort;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textReceivePort;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnOK;
    }
}