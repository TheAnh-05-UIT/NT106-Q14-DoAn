namespace NT106_Q14_DoAnGroup08.ClientStaff
{
    partial class frm_Staff_ComputerManagement
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle21 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lbl_MAINTENANCE = new System.Windows.Forms.Label();
            this.lbl_AVAILABLE = new System.Windows.Forms.Label();
            this.lbl_IN_USE = new System.Windows.Forms.Label();
            this.labelTitle = new System.Windows.Forms.Label();
            this.panelActions = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnKhoaMay = new Guna.UI2.WinForms.Guna2Button();
            this.btnKetThucPhien = new Guna.UI2.WinForms.Guna2Button();
            this.btnLamMoi = new Guna.UI2.WinForms.Guna2Button();
            this.panelMain = new System.Windows.Forms.Panel();
            this.flpComputers = new System.Windows.Forms.FlowLayoutPanel();
            this.dgvComputers = new System.Windows.Forms.DataGridView();
            this.btnMoMay = new Guna.UI2.WinForms.Guna2Button();
            this.btnBatDauPhien = new Guna.UI2.WinForms.Guna2Button();
            this.panelHeader.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panelActions.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvComputers)).BeginInit();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.panelHeader.Controls.Add(this.tableLayoutPanel1);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1200, 92);
            this.panelHeader.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80.3681F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 19.6319F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 164F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 185F));
            this.tableLayoutPanel1.Controls.Add(this.lbl_MAINTENANCE, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.lbl_AVAILABLE, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.lbl_IN_USE, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.labelTitle, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(15);
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 62F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1200, 92);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // lbl_MAINTENANCE
            // 
            this.lbl_MAINTENANCE.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lbl_MAINTENANCE.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_MAINTENANCE.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_MAINTENANCE.Location = new System.Drawing.Point(1003, 15);
            this.lbl_MAINTENANCE.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_MAINTENANCE.Name = "lbl_MAINTENANCE";
            this.lbl_MAINTENANCE.Size = new System.Drawing.Size(178, 62);
            this.lbl_MAINTENANCE.TabIndex = 2;
            this.lbl_MAINTENANCE.Text = "Mantenance: 0/0";
            this.lbl_MAINTENANCE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_AVAILABLE
            // 
            this.lbl_AVAILABLE.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lbl_AVAILABLE.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_AVAILABLE.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_AVAILABLE.Location = new System.Drawing.Point(839, 15);
            this.lbl_AVAILABLE.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_AVAILABLE.Name = "lbl_AVAILABLE";
            this.lbl_AVAILABLE.Size = new System.Drawing.Size(148, 62);
            this.lbl_AVAILABLE.TabIndex = 1;
            this.lbl_AVAILABLE.Text = "Available: 0/0";
            this.lbl_AVAILABLE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_IN_USE
            // 
            this.lbl_IN_USE.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lbl_IN_USE.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_IN_USE.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_IN_USE.Location = new System.Drawing.Point(678, 15);
            this.lbl_IN_USE.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_IN_USE.Name = "lbl_IN_USE";
            this.lbl_IN_USE.Size = new System.Drawing.Size(141, 62);
            this.lbl_IN_USE.TabIndex = 0;
            this.lbl_IN_USE.Text = "In Use: 0/0";
            this.lbl_IN_USE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelTitle.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitle.ForeColor = System.Drawing.Color.White;
            this.labelTitle.Location = new System.Drawing.Point(19, 15);
            this.labelTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(651, 62);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "QUẢN LÝ MÁY TRẠM";
            this.labelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelActions
            // 
            this.panelActions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.panelActions.Controls.Add(this.flowLayoutPanel1);
            this.panelActions.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelActions.Location = new System.Drawing.Point(0, 92);
            this.panelActions.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panelActions.Name = "panelActions";
            this.panelActions.Size = new System.Drawing.Size(1200, 77);
            this.panelActions.TabIndex = 1;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.flowLayoutPanel1.Controls.Add(this.btnKhoaMay);
            this.flowLayoutPanel1.Controls.Add(this.btnMoMay);
            this.flowLayoutPanel1.Controls.Add(this.btnBatDauPhien);
            this.flowLayoutPanel1.Controls.Add(this.btnKetThucPhien);
            this.flowLayoutPanel1.Controls.Add(this.btnLamMoi);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.ForeColor = System.Drawing.Color.Black;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(12);
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1200, 77);
            this.flowLayoutPanel1.TabIndex = 0;
            this.flowLayoutPanel1.WrapContents = false;
            // 
            // btnKhoaMay
            // 
            this.btnKhoaMay.AutoRoundedCorners = true;
            this.btnKhoaMay.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.btnKhoaMay.BorderRadius = 24;
            this.btnKhoaMay.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnKhoaMay.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnKhoaMay.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnKhoaMay.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnKhoaMay.FillColor = System.Drawing.Color.MediumTurquoise;
            this.btnKhoaMay.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnKhoaMay.ForeColor = System.Drawing.Color.Black;
            this.btnKhoaMay.HoverState.FillColor = System.Drawing.Color.LightSeaGreen;
            this.btnKhoaMay.Location = new System.Drawing.Point(16, 17);
            this.btnKhoaMay.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnKhoaMay.Name = "btnKhoaMay";
            this.btnKhoaMay.Size = new System.Drawing.Size(202, 51);
            this.btnKhoaMay.TabIndex = 1;
            this.btnKhoaMay.Text = "Khóa máy";
            this.btnKhoaMay.Click += new System.EventHandler(this.btnKhoaMay_Click);
            // 
            // btnKetThucPhien
            // 
            this.btnKetThucPhien.AutoRoundedCorners = true;
            this.btnKetThucPhien.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.btnKetThucPhien.BorderRadius = 24;
            this.btnKetThucPhien.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnKetThucPhien.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnKetThucPhien.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnKetThucPhien.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnKetThucPhien.FillColor = System.Drawing.Color.MediumTurquoise;
            this.btnKetThucPhien.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnKetThucPhien.ForeColor = System.Drawing.Color.Black;
            this.btnKetThucPhien.HoverState.FillColor = System.Drawing.Color.LightSeaGreen;
            this.btnKetThucPhien.Location = new System.Drawing.Point(646, 17);
            this.btnKetThucPhien.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnKetThucPhien.Name = "btnKetThucPhien";
            this.btnKetThucPhien.Size = new System.Drawing.Size(202, 51);
            this.btnKetThucPhien.TabIndex = 3;
            this.btnKetThucPhien.Text = "Kết thúc phiên chơi";
            this.btnKetThucPhien.Click += new System.EventHandler(this.btnKetThucPhien_Click);
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.AutoRoundedCorners = true;
            this.btnLamMoi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.btnLamMoi.BorderRadius = 24;
            this.btnLamMoi.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnLamMoi.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnLamMoi.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnLamMoi.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnLamMoi.FillColor = System.Drawing.Color.MediumTurquoise;
            this.btnLamMoi.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLamMoi.ForeColor = System.Drawing.Color.Black;
            this.btnLamMoi.HoverState.FillColor = System.Drawing.Color.LightSeaGreen;
            this.btnLamMoi.Location = new System.Drawing.Point(856, 17);
            this.btnLamMoi.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(202, 51);
            this.btnLamMoi.TabIndex = 2;
            this.btnLamMoi.Text = "Làm mới";
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            // 
            // panelMain
            // 
            this.panelMain.BackColor = System.Drawing.Color.White;
            this.panelMain.Controls.Add(this.flpComputers);
            this.panelMain.Controls.Add(this.dgvComputers);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 169);
            this.panelMain.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(1200, 523);
            this.panelMain.TabIndex = 2;
            // 
            // flpComputers
            // 
            this.flpComputers.AutoScroll = true;
            this.flpComputers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpComputers.Location = new System.Drawing.Point(0, 0);
            this.flpComputers.Name = "flpComputers";
            this.flpComputers.Size = new System.Drawing.Size(1200, 523);
            this.flpComputers.TabIndex = 1;
            // 
            // dgvComputers
            // 
            dataGridViewCellStyle19.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(251)))), ((int)(((byte)(255)))));
            this.dgvComputers.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle19;
            this.dgvComputers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle20.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle20.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle20.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle20.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle20.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle20.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvComputers.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle20;
            this.dgvComputers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvComputers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvComputers.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(230)))), ((int)(((byte)(241)))));
            this.dgvComputers.Location = new System.Drawing.Point(0, 0);
            this.dgvComputers.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dgvComputers.Name = "dgvComputers";
            this.dgvComputers.ReadOnly = true;
            this.dgvComputers.RowHeadersVisible = false;
            this.dgvComputers.RowHeadersWidth = 62;
            dataGridViewCellStyle21.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(251)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle21.Padding = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.dgvComputers.RowsDefaultCellStyle = dataGridViewCellStyle21;
            this.dgvComputers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvComputers.Size = new System.Drawing.Size(1200, 523);
            this.dgvComputers.TabIndex = 0;
            // 
            // btnMoMay
            // 
            this.btnMoMay.AutoRoundedCorners = true;
            this.btnMoMay.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.btnMoMay.BorderRadius = 24;
            this.btnMoMay.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnMoMay.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnMoMay.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnMoMay.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnMoMay.FillColor = System.Drawing.Color.MediumTurquoise;
            this.btnMoMay.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMoMay.ForeColor = System.Drawing.Color.Black;
            this.btnMoMay.HoverState.FillColor = System.Drawing.Color.LightSeaGreen;
            this.btnMoMay.Location = new System.Drawing.Point(226, 17);
            this.btnMoMay.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnMoMay.Name = "btnMoMay";
            this.btnMoMay.Size = new System.Drawing.Size(202, 51);
            this.btnMoMay.TabIndex = 4;
            this.btnMoMay.Text = "Mở máy";
            this.btnMoMay.Click += new System.EventHandler(this.btnMoMay_Click);
            // 
            // btnBatDauPhien
            // 
            this.btnBatDauPhien.AutoRoundedCorners = true;
            this.btnBatDauPhien.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.btnBatDauPhien.BorderRadius = 24;
            this.btnBatDauPhien.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnBatDauPhien.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnBatDauPhien.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnBatDauPhien.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnBatDauPhien.FillColor = System.Drawing.Color.MediumTurquoise;
            this.btnBatDauPhien.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBatDauPhien.ForeColor = System.Drawing.Color.Black;
            this.btnBatDauPhien.HoverState.FillColor = System.Drawing.Color.LightSeaGreen;
            this.btnBatDauPhien.Location = new System.Drawing.Point(436, 17);
            this.btnBatDauPhien.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnBatDauPhien.Name = "btnBatDauPhien";
            this.btnBatDauPhien.Size = new System.Drawing.Size(202, 51);
            this.btnBatDauPhien.TabIndex = 5;
            this.btnBatDauPhien.Text = "Bắt đầu phiên chơi";
            this.btnBatDauPhien.Click += new System.EventHandler(this.btnBatDauPhien_Click);
            // 
            // frm_Staff_ComputerManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 692);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.panelActions);
            this.Controls.Add(this.panelHeader);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "frm_Staff_ComputerManagement";
            this.Text = "Staff_ComputerManagement";
            this.Load += new System.EventHandler(this.frm_Staff_ComputerManagement_Load);
            this.panelHeader.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panelActions.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.panelMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvComputers)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panelActions;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Label lbl_IN_USE;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.DataGridView dgvComputers;
        private Guna.UI2.WinForms.Guna2Button btnKhoaMay;
        private Guna.UI2.WinForms.Guna2Button btnKetThucPhien;
        private Guna.UI2.WinForms.Guna2Button btnLamMoi;
        private System.Windows.Forms.FlowLayoutPanel flpComputers;
        private System.Windows.Forms.Label lbl_MAINTENANCE;
        private System.Windows.Forms.Label lbl_AVAILABLE;
        private Guna.UI2.WinForms.Guna2Button btnMoMay;
        private Guna.UI2.WinForms.Guna2Button btnBatDauPhien;
    }
}