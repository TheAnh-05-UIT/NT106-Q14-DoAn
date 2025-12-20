namespace NewNet_Customer.ClientCustomer
{
    partial class frm_Customer
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
            this.lbl_Username = new System.Windows.Forms.Label();
            this.lbl_Used = new System.Windows.Forms.Label();
            this.lbl_Remain = new System.Windows.Forms.Label();
            this.txt_TimeUsed = new System.Windows.Forms.TextBox();
            this.txt_TimeRemain = new System.Windows.Forms.TextBox();
            this.txt_BalanceUsed = new System.Windows.Forms.TextBox();
            this.txt_BalanceRemain = new System.Windows.Forms.TextBox();
            this.btn_TopUp = new System.Windows.Forms.Button();
            this.btnLogOut = new System.Windows.Forms.Button();
            this.btn_FoodMenu = new System.Windows.Forms.Button();
            this.btn_Chat = new System.Windows.Forms.Button();
            this.lbl_CName = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbl_Username
            // 
            this.lbl_Username.AutoSize = true;
            this.lbl_Username.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_Username.Font = new System.Drawing.Font("Times New Roman", 13.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Username.Location = new System.Drawing.Point(0, 0);
            this.lbl_Username.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_Username.Name = "lbl_Username";
            this.lbl_Username.Size = new System.Drawing.Size(84, 21);
            this.lbl_Username.TabIndex = 0;
            this.lbl_Username.Text = "Username";
            // 
            // lbl_Used
            // 
            this.lbl_Used.AutoSize = true;
            this.lbl_Used.Font = new System.Drawing.Font("Times New Roman", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Used.Location = new System.Drawing.Point(6, 67);
            this.lbl_Used.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_Used.Name = "lbl_Used";
            this.lbl_Used.Size = new System.Drawing.Size(58, 16);
            this.lbl_Used.TabIndex = 1;
            this.lbl_Used.Text = "Sử dụng:";
            // 
            // lbl_Remain
            // 
            this.lbl_Remain.AutoSize = true;
            this.lbl_Remain.Font = new System.Drawing.Font("Times New Roman", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Remain.Location = new System.Drawing.Point(6, 98);
            this.lbl_Remain.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_Remain.Name = "lbl_Remain";
            this.lbl_Remain.Size = new System.Drawing.Size(49, 16);
            this.lbl_Remain.TabIndex = 2;
            this.lbl_Remain.Text = "Còn lại:";
            // 
            // txt_TimeUsed
            // 
            this.txt_TimeUsed.ForeColor = System.Drawing.Color.White;
            this.txt_TimeUsed.Location = new System.Drawing.Point(69, 67);
            this.txt_TimeUsed.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txt_TimeUsed.Name = "txt_TimeUsed";
            this.txt_TimeUsed.ReadOnly = true;
            this.txt_TimeUsed.Size = new System.Drawing.Size(52, 20);
            this.txt_TimeUsed.TabIndex = 3;
            this.txt_TimeUsed.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_TimeRemain
            // 
            this.txt_TimeRemain.Location = new System.Drawing.Point(69, 98);
            this.txt_TimeRemain.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txt_TimeRemain.Name = "txt_TimeRemain";
            this.txt_TimeRemain.ReadOnly = true;
            this.txt_TimeRemain.Size = new System.Drawing.Size(52, 20);
            this.txt_TimeRemain.TabIndex = 4;
            this.txt_TimeRemain.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_BalanceUsed
            // 
            this.txt_BalanceUsed.Location = new System.Drawing.Point(130, 67);
            this.txt_BalanceUsed.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txt_BalanceUsed.Name = "txt_BalanceUsed";
            this.txt_BalanceUsed.ReadOnly = true;
            this.txt_BalanceUsed.Size = new System.Drawing.Size(100, 20);
            this.txt_BalanceUsed.TabIndex = 5;
            this.txt_BalanceUsed.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_BalanceRemain
            // 
            this.txt_BalanceRemain.Location = new System.Drawing.Point(130, 98);
            this.txt_BalanceRemain.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txt_BalanceRemain.Name = "txt_BalanceRemain";
            this.txt_BalanceRemain.ReadOnly = true;
            this.txt_BalanceRemain.Size = new System.Drawing.Size(100, 20);
            this.txt_BalanceRemain.TabIndex = 6;
            this.txt_BalanceRemain.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btn_TopUp
            // 
            this.btn_TopUp.Image = global::NewNet_Customer.Properties.Resources.top_up__1___2_;
            this.btn_TopUp.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btn_TopUp.Location = new System.Drawing.Point(9, 245);
            this.btn_TopUp.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btn_TopUp.Name = "btn_TopUp";
            this.btn_TopUp.Size = new System.Drawing.Size(70, 65);
            this.btn_TopUp.TabIndex = 10;
            this.btn_TopUp.Text = "Nạp tiền";
            this.btn_TopUp.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btn_TopUp.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btn_TopUp.UseVisualStyleBackColor = true;
            this.btn_TopUp.Click += new System.EventHandler(this.btn_TopUp_Click);
            // 
            // btnLogOut
            // 
            this.btnLogOut.Image = global::NewNet_Customer.Properties.Resources.logout__1_;
            this.btnLogOut.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnLogOut.Location = new System.Drawing.Point(174, 169);
            this.btnLogOut.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnLogOut.Name = "btnLogOut";
            this.btnLogOut.Size = new System.Drawing.Size(70, 65);
            this.btnLogOut.TabIndex = 9;
            this.btnLogOut.Text = "Đăng xuất";
            this.btnLogOut.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnLogOut.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnLogOut.UseVisualStyleBackColor = true;
            this.btnLogOut.Click += new System.EventHandler(this.btnLogOut_Click);
            // 
            // btn_FoodMenu
            // 
            this.btn_FoodMenu.Image = global::NewNet_Customer.Properties.Resources.food__1_;
            this.btn_FoodMenu.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btn_FoodMenu.Location = new System.Drawing.Point(89, 169);
            this.btn_FoodMenu.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btn_FoodMenu.Name = "btn_FoodMenu";
            this.btn_FoodMenu.Size = new System.Drawing.Size(75, 65);
            this.btn_FoodMenu.TabIndex = 8;
            this.btn_FoodMenu.Text = "Gọi món";
            this.btn_FoodMenu.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btn_FoodMenu.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btn_FoodMenu.UseVisualStyleBackColor = true;
            this.btn_FoodMenu.Click += new System.EventHandler(this.btn_FoodMenu_Click);
            // 
            // btn_Chat
            // 
            this.btn_Chat.Image = global::NewNet_Customer.Properties.Resources.chat__1___1___1___1_;
            this.btn_Chat.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btn_Chat.Location = new System.Drawing.Point(9, 169);
            this.btn_Chat.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btn_Chat.Name = "btn_Chat";
            this.btn_Chat.Size = new System.Drawing.Size(70, 65);
            this.btn_Chat.TabIndex = 7;
            this.btn_Chat.Text = "Giao tiếp";
            this.btn_Chat.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btn_Chat.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btn_Chat.UseVisualStyleBackColor = true;
            this.btn_Chat.Click += new System.EventHandler(this.btn_Chat_Click);
            // 
            // lbl_CName
            // 
            this.lbl_CName.AutoSize = true;
            this.lbl_CName.Font = new System.Drawing.Font("Segoe UI", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_CName.Location = new System.Drawing.Point(171, 10);
            this.lbl_CName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_CName.Name = "lbl_CName";
            this.lbl_CName.Size = new System.Drawing.Size(84, 13);
            this.lbl_CName.TabIndex = 11;
            this.lbl_CName.Text = "Computer Type";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lbl_Username);
            this.panel1.Location = new System.Drawing.Point(84, 32);
            this.panel1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(99, 28);
            this.panel1.TabIndex = 12;
            // 
            // frm_Customer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(256, 389);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lbl_CName);
            this.Controls.Add(this.btn_TopUp);
            this.Controls.Add(this.btnLogOut);
            this.Controls.Add(this.btn_FoodMenu);
            this.Controls.Add(this.btn_Chat);
            this.Controls.Add(this.txt_BalanceRemain);
            this.Controls.Add(this.txt_BalanceUsed);
            this.Controls.Add(this.txt_TimeRemain);
            this.Controls.Add(this.txt_TimeUsed);
            this.Controls.Add(this.lbl_Remain);
            this.Controls.Add(this.lbl_Used);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "frm_Customer";
            this.Text = "Customer";
            this.Load += new System.EventHandler(this.Customer_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_Username;
        private System.Windows.Forms.Label lbl_Used;
        private System.Windows.Forms.Label lbl_Remain;
        private System.Windows.Forms.TextBox txt_TimeUsed;
        private System.Windows.Forms.TextBox txt_TimeRemain;
        private System.Windows.Forms.TextBox txt_BalanceUsed;
        private System.Windows.Forms.TextBox txt_BalanceRemain;
        private System.Windows.Forms.Button btn_Chat;
        private System.Windows.Forms.Button btn_FoodMenu;
        private System.Windows.Forms.Button btnLogOut;
        private System.Windows.Forms.Button btn_TopUp;
        private System.Windows.Forms.Label lbl_CName;
        private System.Windows.Forms.Panel panel1;
    }
}