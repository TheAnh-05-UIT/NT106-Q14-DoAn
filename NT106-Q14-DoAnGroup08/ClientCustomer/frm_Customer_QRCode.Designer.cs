namespace NT106_Q14_DoAnGroup08.ClientCustomer
{
    partial class frm_Customer_QRCode
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
            this.picQR = new Guna.UI2.WinForms.Guna2PictureBox();
            this.btn_Confirm = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picQR)).BeginInit();
            this.SuspendLayout();
            // 
            // picQR
            // 
            this.picQR.ImageRotate = 0F;
            this.picQR.Location = new System.Drawing.Point(141, 66);
            this.picQR.Name = "picQR";
            this.picQR.Size = new System.Drawing.Size(514, 488);
            this.picQR.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picQR.TabIndex = 0;
            this.picQR.TabStop = false;
            // 
            // btn_Confirm
            // 
            this.btn_Confirm.Font = new System.Drawing.Font("Segoe UI", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Confirm.Location = new System.Drawing.Point(268, 620);
            this.btn_Confirm.Name = "btn_Confirm";
            this.btn_Confirm.Size = new System.Drawing.Size(246, 75);
            this.btn_Confirm.TabIndex = 1;
            this.btn_Confirm.Text = "Xác nhận thanh toán";
            this.btn_Confirm.UseVisualStyleBackColor = true;
            // 
            // frm_Customer_QRCode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 742);
            this.Controls.Add(this.btn_Confirm);
            this.Controls.Add(this.picQR);
            this.Name = "frm_Customer_QRCode";
            this.Text = "frm_Customer_QRCode";
            this.Load += new System.EventHandler(this.frm_Customer_QRCode_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picQR)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2PictureBox picQR;
        private System.Windows.Forms.Button btn_Confirm;
    }
}