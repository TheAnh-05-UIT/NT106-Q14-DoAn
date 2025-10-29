namespace NT106_Q14_DoAnGroup08.ClientCustomer
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.btn_TopUp = new System.Windows.Forms.Button();
            this.btnLogOut = new System.Windows.Forms.Button();
            this.btn_FoodMenu = new System.Windows.Forms.Button();
            this.btn_Chat = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbl_Username
            // 
            this.lbl_Username.AutoSize = true;
            this.lbl_Username.Font = new System.Drawing.Font("Times New Roman", 13.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Username.Location = new System.Drawing.Point(130, 43);
            this.lbl_Username.Name = "lbl_Username";
            this.lbl_Username.Size = new System.Drawing.Size(167, 42);
            this.lbl_Username.TabIndex = 0;
            this.lbl_Username.Text = "Username";
            // 
            // lbl_Used
            // 
            this.lbl_Used.AutoSize = true;
            this.lbl_Used.Font = new System.Drawing.Font("Times New Roman", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Used.Location = new System.Drawing.Point(12, 129);
            this.lbl_Used.Name = "lbl_Used";
            this.lbl_Used.Size = new System.Drawing.Size(111, 31);
            this.lbl_Used.TabIndex = 1;
            this.lbl_Used.Text = "Sử dụng:";
            // 
            // lbl_Remain
            // 
            this.lbl_Remain.AutoSize = true;
            this.lbl_Remain.Font = new System.Drawing.Font("Times New Roman", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Remain.Location = new System.Drawing.Point(12, 187);
            this.lbl_Remain.Name = "lbl_Remain";
            this.lbl_Remain.Size = new System.Drawing.Size(100, 31);
            this.lbl_Remain.TabIndex = 2;
            this.lbl_Remain.Text = "Còn lại:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(137, 130);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 31);
            this.textBox1.TabIndex = 3;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(137, 188);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 31);
            this.textBox2.TabIndex = 4;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(243, 130);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(169, 31);
            this.textBox3.TabIndex = 5;
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(243, 188);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(169, 31);
            this.textBox4.TabIndex = 6;
            // 
            // btn_TopUp
            // 
            this.btn_TopUp.Image = global::NT106_Q14_DoAnGroup08.Properties.Resources.top_up__1___2_;
            this.btn_TopUp.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btn_TopUp.Location = new System.Drawing.Point(18, 470);
            this.btn_TopUp.Name = "btn_TopUp";
            this.btn_TopUp.Size = new System.Drawing.Size(141, 125);
            this.btn_TopUp.TabIndex = 10;
            this.btn_TopUp.Text = "Nạp tiền";
            this.btn_TopUp.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btn_TopUp.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btn_TopUp.UseVisualStyleBackColor = true;
            this.btn_TopUp.Click += new System.EventHandler(this.btn_TopUp_Click);
            // 
            // btnLogOut
            // 
            this.btnLogOut.Image = global::NT106_Q14_DoAnGroup08.Properties.Resources.logout__1_;
            this.btnLogOut.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnLogOut.Location = new System.Drawing.Point(348, 325);
            this.btnLogOut.Name = "btnLogOut";
            this.btnLogOut.Size = new System.Drawing.Size(141, 125);
            this.btnLogOut.TabIndex = 9;
            this.btnLogOut.Text = "Đăng xuất";
            this.btnLogOut.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnLogOut.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnLogOut.UseVisualStyleBackColor = true;
            this.btnLogOut.Click += new System.EventHandler(this.btnLogOut_Click);
            // 
            // btn_FoodMenu
            // 
            this.btn_FoodMenu.Image = global::NT106_Q14_DoAnGroup08.Properties.Resources.food__1_;
            this.btn_FoodMenu.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btn_FoodMenu.Location = new System.Drawing.Point(178, 325);
            this.btn_FoodMenu.Name = "btn_FoodMenu";
            this.btn_FoodMenu.Size = new System.Drawing.Size(149, 125);
            this.btn_FoodMenu.TabIndex = 8;
            this.btn_FoodMenu.Text = "Gọi món";
            this.btn_FoodMenu.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btn_FoodMenu.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btn_FoodMenu.UseVisualStyleBackColor = true;
            this.btn_FoodMenu.Click += new System.EventHandler(this.btn_FoodMenu_Click);
            // 
            // btn_Chat
            // 
            this.btn_Chat.Image = global::NT106_Q14_DoAnGroup08.Properties.Resources.chat__1___1___1___1_;
            this.btn_Chat.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btn_Chat.Location = new System.Drawing.Point(18, 325);
            this.btn_Chat.Name = "btn_Chat";
            this.btn_Chat.Size = new System.Drawing.Size(141, 125);
            this.btn_Chat.TabIndex = 7;
            this.btn_Chat.Text = "Giao tiếp";
            this.btn_Chat.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btn_Chat.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btn_Chat.UseVisualStyleBackColor = true;
            this.btn_Chat.Click += new System.EventHandler(this.btn_Chat_Click);
            // 
            // Customer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(512, 867);
            this.Controls.Add(this.btn_TopUp);
            this.Controls.Add(this.btnLogOut);
            this.Controls.Add(this.btn_FoodMenu);
            this.Controls.Add(this.btn_Chat);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.lbl_Remain);
            this.Controls.Add(this.lbl_Used);
            this.Controls.Add(this.lbl_Username);
            this.Name = "Customer";
            this.Text = "Customer";
            this.Load += new System.EventHandler(this.Customer_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_Username;
        private System.Windows.Forms.Label lbl_Used;
        private System.Windows.Forms.Label lbl_Remain;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Button btn_Chat;
        private System.Windows.Forms.Button btn_FoodMenu;
        private System.Windows.Forms.Button btnLogOut;
        private System.Windows.Forms.Button btn_TopUp;
    }
}