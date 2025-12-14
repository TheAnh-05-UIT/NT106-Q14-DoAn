namespace NT106_Q14_DoAnGroup08.ClientAdmin
{
    partial class frm_Account_Admin
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
            this.lblTitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblUserName = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblUserNameVer2 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblFullName = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblFullNameVer2 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.btnLogout = new Guna.UI2.WinForms.Guna2Button();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(23, 12);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(342, 39);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Thông tin tài khoản admin";
            // 
            // lblUserName
            // 
            this.lblUserName.BackColor = System.Drawing.Color.Transparent;
            this.lblUserName.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUserName.Location = new System.Drawing.Point(23, 89);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(142, 27);
            this.lblUserName.TabIndex = 1;
            this.lblUserName.Text = "Tên đăng nhập:";
            // 
            // lblUserNameVer2
            // 
            this.lblUserNameVer2.BackColor = System.Drawing.Color.Transparent;
            this.lblUserNameVer2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUserNameVer2.ForeColor = System.Drawing.Color.Black;
            this.lblUserNameVer2.Location = new System.Drawing.Point(206, 97);
            this.lblUserNameVer2.Name = "lblUserNameVer2";
            this.lblUserNameVer2.Size = new System.Drawing.Size(35, 19);
            this.lblUserNameVer2.TabIndex = 2;
            this.lblUserNameVer2.Text = "trống";
            // 
            // lblFullName
            // 
            this.lblFullName.BackColor = System.Drawing.Color.Transparent;
            this.lblFullName.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFullName.Location = new System.Drawing.Point(23, 123);
            this.lblFullName.Name = "lblFullName";
            this.lblFullName.Size = new System.Drawing.Size(142, 27);
            this.lblFullName.TabIndex = 4;
            this.lblFullName.Text = "Tên đăng nhập:";
            // 
            // lblFullNameVer2
            // 
            this.lblFullNameVer2.BackColor = System.Drawing.Color.Transparent;
            this.lblFullNameVer2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFullNameVer2.Location = new System.Drawing.Point(206, 131);
            this.lblFullNameVer2.Name = "lblFullNameVer2";
            this.lblFullNameVer2.Size = new System.Drawing.Size(35, 19);
            this.lblFullNameVer2.TabIndex = 5;
            this.lblFullNameVer2.Text = "trống";
            // 
            // btnLogout
            // 
            this.btnLogout.BackColor = System.Drawing.Color.Wheat;
            this.btnLogout.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnLogout.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnLogout.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnLogout.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnLogout.FillColor = System.Drawing.Color.PeachPuff;
            this.btnLogout.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogout.ForeColor = System.Drawing.Color.Black;
            this.btnLogout.Location = new System.Drawing.Point(656, 12);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(110, 56);
            this.btnLogout.TabIndex = 6;
            this.btnLogout.Text = "Đăng xuất";
            // 
            // frm_Account_Admin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.lblFullNameVer2);
            this.Controls.Add(this.lblFullName);
            this.Controls.Add(this.lblUserNameVer2);
            this.Controls.Add(this.lblUserName);
            this.Controls.Add(this.lblTitle);
            this.Name = "frm_Account_Admin";
            this.Text = "frm_Account_Admin";
            this.Load += new System.EventHandler(this.frm_Account_Admin_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Guna.UI2.WinForms.Guna2HtmlLabel lblTitle;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblUserName;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblUserNameVer2;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblFullName;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblFullNameVer2;
        private Guna.UI2.WinForms.Guna2Button btnLogout;
    }
}