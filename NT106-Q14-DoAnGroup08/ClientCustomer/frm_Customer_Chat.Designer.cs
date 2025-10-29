namespace NT106_Q14_DoAnGroup08.ClientCustomer
{
    partial class frm_Customer_Chat
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
            this.lst_Chat = new System.Windows.Forms.ListBox();
            this.txt_Chat = new System.Windows.Forms.MaskedTextBox();
            this.btn_SendMessage = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lst_Chat
            // 
            this.lst_Chat.FormattingEnabled = true;
            this.lst_Chat.ItemHeight = 25;
            this.lst_Chat.Location = new System.Drawing.Point(12, 25);
            this.lst_Chat.Name = "lst_Chat";
            this.lst_Chat.Size = new System.Drawing.Size(845, 604);
            this.lst_Chat.TabIndex = 0;
            // 
            // txt_Chat
            // 
            this.txt_Chat.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Chat.Location = new System.Drawing.Point(12, 635);
            this.txt_Chat.Name = "txt_Chat";
            this.txt_Chat.Size = new System.Drawing.Size(674, 44);
            this.txt_Chat.TabIndex = 1;
            // 
            // btn_SendMessage
            // 
            this.btn_SendMessage.Location = new System.Drawing.Point(704, 635);
            this.btn_SendMessage.Name = "btn_SendMessage";
            this.btn_SendMessage.Size = new System.Drawing.Size(153, 44);
            this.btn_SendMessage.TabIndex = 2;
            this.btn_SendMessage.Text = "button1";
            this.btn_SendMessage.UseVisualStyleBackColor = true;
            this.btn_SendMessage.Click += new System.EventHandler(this.btn_SendMessage_Click);
            // 
            // Customer_Chat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(869, 693);
            this.Controls.Add(this.btn_SendMessage);
            this.Controls.Add(this.txt_Chat);
            this.Controls.Add(this.lst_Chat);
            this.Name = "Customer_Chat";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Customer_Chat";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lst_Chat;
        private System.Windows.Forms.MaskedTextBox txt_Chat;
        private System.Windows.Forms.Button btn_SendMessage;
    }
}