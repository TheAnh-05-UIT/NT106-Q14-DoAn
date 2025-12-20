namespace NewNet_Customer.ClientCustomer
{
    partial class frm_LockScreen
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.panelMainForm = new System.Windows.Forms.Panel();
            this.pictureBoxNet = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxNet)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(632, 69);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(57, 21);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "label1";
            this.lblTitle.Click += new System.EventHandler(this.lblTitle_Click);
            // 
            // panelMainForm
            // 
            this.panelMainForm.Location = new System.Drawing.Point(400, 107);
            this.panelMainForm.Name = "panelMainForm";
            this.panelMainForm.Size = new System.Drawing.Size(511, 388);
            this.panelMainForm.TabIndex = 1;
            this.panelMainForm.Paint += new System.Windows.Forms.PaintEventHandler(this.panelMainForm_Paint);
            // 
            // pictureBoxNet
            // 
            this.pictureBoxNet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxNet.Image = global::NewNet_Customer.Properties.Resources.kinh_nghiem_kinh_doanh_quan_net_thanh_cong_toi_uu_chi_phi;
            this.pictureBoxNet.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxNet.Name = "pictureBoxNet";
            this.pictureBoxNet.Size = new System.Drawing.Size(1251, 450);
            this.pictureBoxNet.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxNet.TabIndex = 2;
            this.pictureBoxNet.TabStop = false;
            // 
            // frm_LockScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1251, 450);
            this.Controls.Add(this.panelMainForm);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.pictureBoxNet);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frm_LockScreen";
            this.Opacity = 0.9D;
            this.Text = "frm_LockScreen";
            this.TopMost = true;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frm_LockScreen_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxNet)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel panelMainForm;
        private System.Windows.Forms.PictureBox pictureBoxNet;
    }
}