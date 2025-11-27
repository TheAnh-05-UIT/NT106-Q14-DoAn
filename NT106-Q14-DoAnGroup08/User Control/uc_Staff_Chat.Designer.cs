namespace NT106_Q14_DoAnGroup08.Uc_Staff
{
    partial class uc_Staff_Chat
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lst_Chat = new System.Windows.Forms.ListBox();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.txt_Chat = new System.Windows.Forms.TextBox();
            this.btn_SendMessage = new System.Windows.Forms.Button();
            this.panelBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // lst_Chat
            // 
            this.lst_Chat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lst_Chat.FormattingEnabled = true;
            this.lst_Chat.ItemHeight = 20;
            this.lst_Chat.Location = new System.Drawing.Point(0, 0);
            this.lst_Chat.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.lst_Chat.Name = "lst_Chat";
            this.lst_Chat.Size = new System.Drawing.Size(674, 579);
            this.lst_Chat.TabIndex = 0;
            // 
            // panelBottom
            // 
            this.panelBottom.Controls.Add(this.txt_Chat);
            this.panelBottom.Controls.Add(this.btn_SendMessage);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 579);
            this.panelBottom.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(674, 49);
            this.panelBottom.TabIndex = 1;
            // 
            // txt_Chat
            // 
            this.txt_Chat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_Chat.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Chat.Location = new System.Drawing.Point(0, 0);
            this.txt_Chat.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txt_Chat.Name = "txt_Chat";
            this.txt_Chat.Size = new System.Drawing.Size(558, 35);
            this.txt_Chat.TabIndex = 0;
            // 
            // btn_SendMessage
            // 
            this.btn_SendMessage.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_SendMessage.Location = new System.Drawing.Point(558, 0);
            this.btn_SendMessage.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btn_SendMessage.Name = "btn_SendMessage";
            this.btn_SendMessage.Size = new System.Drawing.Size(116, 49);
            this.btn_SendMessage.TabIndex = 1;
            this.btn_SendMessage.Text = "Gửi";
            this.btn_SendMessage.UseVisualStyleBackColor = true;
            // 
            // uc_Staff_Chat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lst_Chat);
            this.Controls.Add(this.panelBottom);
            this.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.Name = "uc_Staff_Chat";
            this.Size = new System.Drawing.Size(674, 628);
            this.panelBottom.ResumeLayout(false);
            this.panelBottom.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lst_Chat;
        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.Button btn_SendMessage;
        private System.Windows.Forms.TextBox txt_Chat;
    }
}
