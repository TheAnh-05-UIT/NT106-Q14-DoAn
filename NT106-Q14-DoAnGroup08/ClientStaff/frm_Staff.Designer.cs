namespace NT106_Q14_DoAnGroup08.ClientStaff
{
    partial class frm_Staff
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
            this.ButtonGroup = new System.Windows.Forms.GroupBox();
            this.ImportGoodButton = new System.Windows.Forms.Button();
            this.btnTaiKhoan = new System.Windows.Forms.Button();
            this.btnChat = new System.Windows.Forms.Button();
            this.btnHoaDon = new System.Windows.Forms.Button();
            this.btnThucDon = new System.Windows.Forms.Button();
            this.btnQuanLyMay = new System.Windows.Forms.Button();
            this.UserPanel = new System.Windows.Forms.Panel();
            this.ButtonGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // ButtonGroup
            // 
            this.ButtonGroup.Controls.Add(this.ImportGoodButton);
            this.ButtonGroup.Controls.Add(this.btnTaiKhoan);
            this.ButtonGroup.Controls.Add(this.btnChat);
            this.ButtonGroup.Controls.Add(this.btnHoaDon);
            this.ButtonGroup.Controls.Add(this.btnThucDon);
            this.ButtonGroup.Controls.Add(this.btnQuanLyMay);
            this.ButtonGroup.Location = new System.Drawing.Point(18, 19);
            this.ButtonGroup.Margin = new System.Windows.Forms.Padding(4);
            this.ButtonGroup.Name = "ButtonGroup";
            this.ButtonGroup.Padding = new System.Windows.Forms.Padding(4);
            this.ButtonGroup.Size = new System.Drawing.Size(140, 983);
            this.ButtonGroup.TabIndex = 1;
            this.ButtonGroup.TabStop = false;
            this.ButtonGroup.Text = "Trang";
            this.ButtonGroup.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // ImportGoodButton
            // 
            this.ImportGoodButton.Location = new System.Drawing.Point(8, 35);
            this.ImportGoodButton.Margin = new System.Windows.Forms.Padding(4);
            this.ImportGoodButton.Name = "ImportGoodButton";
            this.ImportGoodButton.Size = new System.Drawing.Size(112, 94);
            this.ImportGoodButton.TabIndex = 2;
            this.ImportGoodButton.Text = " Nhập hàng";
            this.ImportGoodButton.UseVisualStyleBackColor = true;
            this.ImportGoodButton.Click += new System.EventHandler(this.ImportGoodButton_Click);
            // 
            // btnTaiKhoan
            // 
            this.btnTaiKhoan.Location = new System.Drawing.Point(8, 548);
            this.btnTaiKhoan.Margin = new System.Windows.Forms.Padding(4);
            this.btnTaiKhoan.Name = "btnTaiKhoan";
            this.btnTaiKhoan.Size = new System.Drawing.Size(112, 94);
            this.btnTaiKhoan.TabIndex = 5;
            this.btnTaiKhoan.Text = "Tài khoản";
            this.btnTaiKhoan.UseVisualStyleBackColor = true;
            this.btnTaiKhoan.Click += new System.EventHandler(this.btnTaiKhoan_Click);
            // 
            // btnChat
            // 
            this.btnChat.Location = new System.Drawing.Point(8, 446);
            this.btnChat.Margin = new System.Windows.Forms.Padding(4);
            this.btnChat.Name = "btnChat";
            this.btnChat.Size = new System.Drawing.Size(112, 94);
            this.btnChat.TabIndex = 4;
            this.btnChat.Text = "Chat";
            this.btnChat.UseVisualStyleBackColor = true;
            this.btnChat.Click += new System.EventHandler(this.btnChat_Click);
            // 
            // btnHoaDon
            // 
            this.btnHoaDon.Location = new System.Drawing.Point(8, 342);
            this.btnHoaDon.Margin = new System.Windows.Forms.Padding(4);
            this.btnHoaDon.Name = "btnHoaDon";
            this.btnHoaDon.Size = new System.Drawing.Size(112, 94);
            this.btnHoaDon.TabIndex = 3;
            this.btnHoaDon.Text = "Hóa đơn";
            this.btnHoaDon.UseVisualStyleBackColor = true;
            this.btnHoaDon.Click += new System.EventHandler(this.btnHoaDon_Click);
            // 
            // btnThucDon
            // 
            this.btnThucDon.Location = new System.Drawing.Point(8, 137);
            this.btnThucDon.Margin = new System.Windows.Forms.Padding(4);
            this.btnThucDon.Name = "btnThucDon";
            this.btnThucDon.Size = new System.Drawing.Size(112, 94);
            this.btnThucDon.TabIndex = 1;
            this.btnThucDon.Text = "Thực đơn";
            this.btnThucDon.UseVisualStyleBackColor = true;
            this.btnThucDon.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnQuanLyMay
            // 
            this.btnQuanLyMay.Location = new System.Drawing.Point(8, 238);
            this.btnQuanLyMay.Margin = new System.Windows.Forms.Padding(4);
            this.btnQuanLyMay.Name = "btnQuanLyMay";
            this.btnQuanLyMay.Size = new System.Drawing.Size(112, 94);
            this.btnQuanLyMay.TabIndex = 0;
            this.btnQuanLyMay.Text = "Quản lý máy";
            this.btnQuanLyMay.UseVisualStyleBackColor = true;
            this.btnQuanLyMay.Click += new System.EventHandler(this.btnQuanLyMay_Click);
            // 
            // UserPanel
            // 
            this.UserPanel.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.UserPanel.Location = new System.Drawing.Point(166, 19);
            this.UserPanel.Margin = new System.Windows.Forms.Padding(4);
            this.UserPanel.Name = "UserPanel";
            this.UserPanel.Size = new System.Drawing.Size(1550, 1000);
            this.UserPanel.TabIndex = 2;
            // 
            // frm_Staff
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(1734, 1038);
            this.Controls.Add(this.UserPanel);
            this.Controls.Add(this.ButtonGroup);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frm_Staff";
            this.Text = "Staff App";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.Load += new System.EventHandler(this.Staff_Load);
            this.ButtonGroup.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox ButtonGroup;
        private System.Windows.Forms.Button btnChat;
        private System.Windows.Forms.Button btnHoaDon;
        private System.Windows.Forms.Button ImportGoodButton;
        private System.Windows.Forms.Button btnThucDon;
        private System.Windows.Forms.Button btnQuanLyMay;
        private System.Windows.Forms.Panel UserPanel;
        private System.Windows.Forms.Button btnTaiKhoan;
    }
}