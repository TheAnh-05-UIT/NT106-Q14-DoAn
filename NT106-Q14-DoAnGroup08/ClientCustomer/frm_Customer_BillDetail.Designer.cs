namespace NT106_Q14_DoAnGroup08.ClientCustomer
{
    partial class frm_Customer_BillDetail
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
            this.guna2Panel2 = new Guna.UI2.WinForms.Guna2Panel();
            this.guna2PictureBox1 = new Guna.UI2.WinForms.Guna2PictureBox();
            this.lbl_Menu = new System.Windows.Forms.Label();
            this.guna2CirclePictureBox1 = new Guna.UI2.WinForms.Guna2CirclePictureBox();
            this.dataGridViewDetail = new System.Windows.Forms.DataGridView();
            this.guna2Panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.guna2CirclePictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDetail)).BeginInit();
            this.SuspendLayout();
            // 
            // guna2Panel2
            // 
            this.guna2Panel2.Controls.Add(this.guna2PictureBox1);
            this.guna2Panel2.Controls.Add(this.lbl_Menu);
            this.guna2Panel2.Controls.Add(this.guna2CirclePictureBox1);
            this.guna2Panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.guna2Panel2.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(32)))), ((int)(((byte)(63)))));
            this.guna2Panel2.Location = new System.Drawing.Point(0, 0);
            this.guna2Panel2.Name = "guna2Panel2";
            this.guna2Panel2.Size = new System.Drawing.Size(933, 144);
            this.guna2Panel2.TabIndex = 3;
            // 
            // guna2PictureBox1
            // 
            this.guna2PictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.guna2PictureBox1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(32)))), ((int)(((byte)(63)))));
            this.guna2PictureBox1.Image = global::NT106_Q14_DoAnGroup08.Properties.Resources.close;
            this.guna2PictureBox1.ImageRotate = 0F;
            this.guna2PictureBox1.Location = new System.Drawing.Point(805, 29);
            this.guna2PictureBox1.Name = "guna2PictureBox1";
            this.guna2PictureBox1.Size = new System.Drawing.Size(86, 72);
            this.guna2PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.guna2PictureBox1.TabIndex = 2;
            this.guna2PictureBox1.TabStop = false;
            this.guna2PictureBox1.UseTransparentBackground = true;
            this.guna2PictureBox1.Click += new System.EventHandler(this.guna2PictureBox1_Click);
            // 
            // lbl_Menu
            // 
            this.lbl_Menu.AutoSize = true;
            this.lbl_Menu.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Menu.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Menu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(239)))), ((int)(((byte)(209)))));
            this.lbl_Menu.Location = new System.Drawing.Point(179, 42);
            this.lbl_Menu.Name = "lbl_Menu";
            this.lbl_Menu.Size = new System.Drawing.Size(201, 59);
            this.lbl_Menu.TabIndex = 1;
            this.lbl_Menu.Text = "Bill Detail";
            // 
            // guna2CirclePictureBox1
            // 
            this.guna2CirclePictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.guna2CirclePictureBox1.Image = global::NT106_Q14_DoAnGroup08.Properties.Resources.invoice;
            this.guna2CirclePictureBox1.ImageRotate = 0F;
            this.guna2CirclePictureBox1.Location = new System.Drawing.Point(28, 12);
            this.guna2CirclePictureBox1.Name = "guna2CirclePictureBox1";
            this.guna2CirclePictureBox1.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.guna2CirclePictureBox1.Size = new System.Drawing.Size(127, 115);
            this.guna2CirclePictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.guna2CirclePictureBox1.TabIndex = 0;
            this.guna2CirclePictureBox1.TabStop = false;
            this.guna2CirclePictureBox1.UseTransparentBackground = true;
            // 
            // dataGridViewDetail
            // 
            this.dataGridViewDetail.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridViewDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewDetail.Location = new System.Drawing.Point(0, 144);
            this.dataGridViewDetail.Name = "dataGridViewDetail";
            this.dataGridViewDetail.RowHeadersWidth = 82;
            this.dataGridViewDetail.RowTemplate.Height = 33;
            this.dataGridViewDetail.Size = new System.Drawing.Size(933, 439);
            this.dataGridViewDetail.TabIndex = 4;
            // 
            // frm_Customer_BillDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(933, 583);
            this.Controls.Add(this.dataGridViewDetail);
            this.Controls.Add(this.guna2Panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frm_Customer_BillDetail";
            this.Text = "frm_Customer_BillDetail";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.frm_Customer_BillDetail_Load);
            this.guna2Panel2.ResumeLayout(false);
            this.guna2Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.guna2CirclePictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDetail)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel guna2Panel2;
        private Guna.UI2.WinForms.Guna2PictureBox guna2PictureBox1;
        private System.Windows.Forms.Label lbl_Menu;
        private Guna.UI2.WinForms.Guna2CirclePictureBox guna2CirclePictureBox1;
        private System.Windows.Forms.DataGridView dataGridViewDetail;
    }
}