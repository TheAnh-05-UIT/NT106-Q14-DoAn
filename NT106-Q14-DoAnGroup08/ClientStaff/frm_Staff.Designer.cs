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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.ImportGoodButton = new System.Windows.Forms.Button();
            this.btnTaiKhoan = new System.Windows.Forms.Button();
            this.btnChat = new System.Windows.Forms.Button();
            this.btnHoaDon = new System.Windows.Forms.Button();
            this.btnThucDon = new System.Windows.Forms.Button();
            this.btnQuanLyMay = new System.Windows.Forms.Button();
            this.UserPanel = new System.Windows.Forms.Panel();
            this.ButtonGroup.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ButtonGroup
            // 
            this.ButtonGroup.Controls.Add(this.groupBox1);
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
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.Window;
            this.groupBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox1.Font = new System.Drawing.Font("Merriweather", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(254)));
            this.groupBox1.Location = new System.Drawing.Point(5, 546);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 0, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(80, 82);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "0";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.LightSalmon;
            this.button1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Tomato;
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Tomato;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(3, 17);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(74, 63);
            this.button1.TabIndex = 6;
            this.button1.Text = "Thông báo";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ImportGoodButton
            // 
            this.ImportGoodButton.Location = new System.Drawing.Point(8, 35);
            this.ImportGoodButton.Margin = new System.Windows.Forms.Padding(4);
            this.ImportGoodButton.Name = "ImportGoodButton";
            this.ImportGoodButton.Size = new System.Drawing.Size(112, 94);
            this.ImportGoodButton.TabIndex = 2;
            this.ImportGoodButton.Text = " Nhập hàng";
            this.ImportGoodButton.UseVisualStyleBackColor = false;
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
            this.btnTaiKhoan.UseVisualStyleBackColor = false;
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
            this.btnChat.UseVisualStyleBackColor = false;
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
            this.btnHoaDon.UseVisualStyleBackColor = false;
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
            this.btnThucDon.UseVisualStyleBackColor = false;
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
            this.btnQuanLyMay.UseVisualStyleBackColor = false;
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
            this.UserPanel.Enter += new System.EventHandler(this.UserPanel_Enter);
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
            this.groupBox1.ResumeLayout(false);
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
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button1;
    }
}