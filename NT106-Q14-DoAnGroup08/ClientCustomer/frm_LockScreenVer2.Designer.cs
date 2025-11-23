namespace NT106_Q14_DoAnGroup08.ClientCustomer
{
    partial class frm_LockScreenVer2
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
            this.txtUnlockPassword = new System.Windows.Forms.TextBox();
            this.btnUnlock = new System.Windows.Forms.Button();
            this.lblNotification = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(635, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(136, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "MÁY BỊ KHÓA";
            // 
            // txtUnlockPassword
            // 
            this.txtUnlockPassword.Location = new System.Drawing.Point(612, 181);
            this.txtUnlockPassword.Multiline = true;
            this.txtUnlockPassword.Name = "txtUnlockPassword";
            this.txtUnlockPassword.Size = new System.Drawing.Size(189, 25);
            this.txtUnlockPassword.TabIndex = 1;
            this.txtUnlockPassword.TextChanged += new System.EventHandler(this.txtUnlockPassword_TextChanged);
            // 
            // btnUnlock
            // 
            this.btnUnlock.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUnlock.Location = new System.Drawing.Point(667, 222);
            this.btnUnlock.Name = "btnUnlock";
            this.btnUnlock.Size = new System.Drawing.Size(94, 29);
            this.btnUnlock.TabIndex = 2;
            this.btnUnlock.Text = "Xác nhận";
            this.btnUnlock.UseVisualStyleBackColor = true;
            this.btnUnlock.Click += new System.EventHandler(this.btnUnlock_Click);
            // 
            // lblNotification
            // 
            this.lblNotification.AutoSize = true;
            this.lblNotification.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNotification.Location = new System.Drawing.Point(479, 127);
            this.lblNotification.Name = "lblNotification";
            this.lblNotification.Size = new System.Drawing.Size(500, 17);
            this.lblNotification.TabIndex = 3;
            this.lblNotification.Text = "Đã hết thời gian, vui lòng nạp thêm tiền và liên hệ nhân viên để được mở khóa";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(544, 181);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "Mật khẩu";
            // 
            // frm_LockScreenVer2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1283, 450);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblNotification);
            this.Controls.Add(this.btnUnlock);
            this.Controls.Add(this.txtUnlockPassword);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frm_LockScreenVer2";
            this.Opacity = 0.9D;
            this.Text = "LockScreenVer2";
            this.TopMost = true;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtUnlockPassword;
        private System.Windows.Forms.Button btnUnlock;
        private System.Windows.Forms.Label lblNotification;
        private System.Windows.Forms.Label label2;
    }
}