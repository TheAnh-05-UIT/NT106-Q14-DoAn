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
            this.ButtonGroup.Location = new System.Drawing.Point(9, 10);
            this.ButtonGroup.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ButtonGroup.Name = "ButtonGroup";
            this.ButtonGroup.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ButtonGroup.Size = new System.Drawing.Size(70, 511);
            this.ButtonGroup.TabIndex = 1;
            this.ButtonGroup.TabStop = false;
            this.ButtonGroup.Text = "Trang";
            this.ButtonGroup.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // ImportGoodButton
            // 
            this.ImportGoodButton.Location = new System.Drawing.Point(4, 18);
            this.ImportGoodButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ImportGoodButton.Name = "ImportGoodButton";
            this.ImportGoodButton.Size = new System.Drawing.Size(56, 49);
            this.ImportGoodButton.TabIndex = 2;
            this.ImportGoodButton.Text = " Nhập hàng";
            this.ImportGoodButton.UseVisualStyleBackColor = true;
            this.ImportGoodButton.Click += new System.EventHandler(this.ImportGoodButton_Click);
            // 
            // btnTaiKhoan
            // 
            this.btnTaiKhoan.Location = new System.Drawing.Point(4, 285);
            this.btnTaiKhoan.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnTaiKhoan.Name = "btnTaiKhoan";
            this.btnTaiKhoan.Size = new System.Drawing.Size(56, 49);
            this.btnTaiKhoan.TabIndex = 5;
            this.btnTaiKhoan.Text = "Tài khoản";
            this.btnTaiKhoan.UseVisualStyleBackColor = true;
            this.btnTaiKhoan.Click += new System.EventHandler(this.btnTaiKhoan_Click);
            // 
            // btnChat
            // 
            this.btnChat.Location = new System.Drawing.Point(4, 232);
            this.btnChat.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnChat.Name = "btnChat";
            this.btnChat.Size = new System.Drawing.Size(56, 49);
            this.btnChat.TabIndex = 4;
            this.btnChat.Text = "Chat";
            this.btnChat.UseVisualStyleBackColor = true;
            this.btnChat.Click += new System.EventHandler(this.btnChat_Click);
            // 
            // btnHoaDon
            // 
            this.btnHoaDon.Location = new System.Drawing.Point(4, 178);
            this.btnHoaDon.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnHoaDon.Name = "btnHoaDon";
            this.btnHoaDon.Size = new System.Drawing.Size(56, 49);
            this.btnHoaDon.TabIndex = 3;
            this.btnHoaDon.Text = "Hóa đơn";
            this.btnHoaDon.UseVisualStyleBackColor = true;
            this.btnHoaDon.Click += new System.EventHandler(this.btnHoaDon_Click);
            // 
            // btnThucDon
            // 
            this.btnThucDon.Location = new System.Drawing.Point(4, 71);
            this.btnThucDon.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnThucDon.Name = "btnThucDon";
            this.btnThucDon.Size = new System.Drawing.Size(56, 49);
            this.btnThucDon.TabIndex = 1;
            this.btnThucDon.Text = "Thực đơn";
            this.btnThucDon.UseVisualStyleBackColor = true;
            this.btnThucDon.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnQuanLyMay
            // 
            this.btnQuanLyMay.Location = new System.Drawing.Point(4, 124);
            this.btnQuanLyMay.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnQuanLyMay.Name = "btnQuanLyMay";
            this.btnQuanLyMay.Size = new System.Drawing.Size(56, 49);
            this.btnQuanLyMay.TabIndex = 0;
            this.btnQuanLyMay.Text = "Quản lý máy";
            this.btnQuanLyMay.UseVisualStyleBackColor = true;
            this.btnQuanLyMay.Click += new System.EventHandler(this.btnQuanLyMay_Click);
            // 
            // UserPanel
            // 
            this.UserPanel.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.UserPanel.Location = new System.Drawing.Point(83, 10);
            this.UserPanel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.UserPanel.Name = "UserPanel";
            this.UserPanel.Size = new System.Drawing.Size(775, 520);
            this.UserPanel.TabIndex = 2;
            // 
            // frm_Staff
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(867, 540);
            this.Controls.Add(this.UserPanel);
            this.Controls.Add(this.ButtonGroup);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "frm_Staff";
            this.Text = "Staff App";
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