namespace ChatApp
{
    partial class AsyncChatWindow
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
            this.textSend = new System.Windows.Forms.TextBox();
            this.textConv = new System.Windows.Forms.RichTextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textSend
            // 
            this.textSend.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.textSend.Location = new System.Drawing.Point(32, 399);
            this.textSend.Name = "textSend";
            this.textSend.Size = new System.Drawing.Size(520, 24);
            this.textSend.TabIndex = 0;
            this.textSend.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textSend_KeyDown);
            // 
            // textConv
            // 
            this.textConv.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.textConv.Location = new System.Drawing.Point(32, 26);
            this.textConv.Name = "textConv";
            this.textConv.Size = new System.Drawing.Size(666, 341);
            this.textConv.TabIndex = 1;
            this.textConv.Text = "";
            // 
            // btnSend
            // 
            this.btnSend.Enabled = false;
            this.btnSend.Location = new System.Drawing.Point(602, 391);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(96, 34);
            this.btnSend.TabIndex = 4;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // AsyncChatWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(730, 447);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.textConv);
            this.Controls.Add(this.textSend);
            this.Name = "AsyncChatWindow";
            this.Text = "ChatWindow";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ChatWindow_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textSend;
        private System.Windows.Forms.RichTextBox textConv;
        private System.Windows.Forms.Button btnSend;
    }
}