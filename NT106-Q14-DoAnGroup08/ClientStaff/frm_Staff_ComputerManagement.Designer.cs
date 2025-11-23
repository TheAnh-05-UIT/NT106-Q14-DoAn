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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.labelTitle = new System.Windows.Forms.Label();
            this.labelSummary = new System.Windows.Forms.Label();
            this.panelActions = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnKhoaMay = new Guna.UI2.WinForms.Guna2Button();
            this.btnKetThucPhien = new Guna.UI2.WinForms.Guna2Button();
            this.btnLamMoi = new Guna.UI2.WinForms.Guna2Button();
            this.panelMain = new System.Windows.Forms.Panel();
            this.dgvComputers = new System.Windows.Forms.DataGridView();
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
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(800, 60);
            this.panelHeader.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.Controls.Add(this.labelTitle, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.labelSummary, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(10);
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(800, 60);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitle.ForeColor = System.Drawing.Color.White;
            this.labelTitle.Location = new System.Drawing.Point(13, 10);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(200, 25);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "QUẢN LÝ MÁY TRẠM";
            // 
            // labelSummary
            // 
            this.labelSummary.AutoSize = true;
            this.labelSummary.BackColor = System.Drawing.Color.WhiteSmoke;
            this.labelSummary.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSummary.Location = new System.Drawing.Point(559, 10);
            this.labelSummary.Name = "labelSummary";
            this.labelSummary.Size = new System.Drawing.Size(74, 17);
            this.labelSummary.TabIndex = 0;
            this.labelSummary.Text = "Active: 0/0";
            this.labelSummary.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.labelSummary.Click += new System.EventHandler(this.labelSummary_Click);
            // 
            // panelActions
            // 
            this.panelActions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.panelActions.Controls.Add(this.flowLayoutPanel1);
            this.panelActions.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelActions.Location = new System.Drawing.Point(0, 60);
            this.panelActions.Name = "panelActions";
            this.panelActions.Size = new System.Drawing.Size(800, 50);
            this.panelActions.TabIndex = 1;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.flowLayoutPanel1.Controls.Add(this.btnKhoaMay);
            this.flowLayoutPanel1.Controls.Add(this.btnKetThucPhien);
            this.flowLayoutPanel1.Controls.Add(this.btnLamMoi);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.ForeColor = System.Drawing.Color.Black;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(8);
            this.flowLayoutPanel1.Size = new System.Drawing.Size(800, 50);
            this.flowLayoutPanel1.TabIndex = 0;
            this.flowLayoutPanel1.WrapContents = false;
            this.flowLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.flowLayoutPanel1_Paint);
            // 
            // btnKhoaMay
            // 
            this.btnKhoaMay.AutoRoundedCorners = true;
            this.btnKhoaMay.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.btnKhoaMay.BorderRadius = 15;
            this.btnKhoaMay.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnKhoaMay.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnKhoaMay.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnKhoaMay.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnKhoaMay.FillColor = System.Drawing.Color.MediumTurquoise;
            this.btnKhoaMay.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnKhoaMay.ForeColor = System.Drawing.Color.Black;
            this.btnKhoaMay.HoverState.FillColor = System.Drawing.Color.LightSeaGreen;
            this.btnKhoaMay.Location = new System.Drawing.Point(11, 11);
            this.btnKhoaMay.Name = "btnKhoaMay";
            this.btnKhoaMay.Size = new System.Drawing.Size(135, 33);
            this.btnKhoaMay.TabIndex = 1;
            this.btnKhoaMay.Text = "Khóa máy";
            this.btnKhoaMay.Click += new System.EventHandler(this.btnKhoaMay_Click);
            // 
            // btnKetThucPhien
            // 
            this.btnKetThucPhien.AutoRoundedCorners = true;
            this.btnKetThucPhien.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.btnKetThucPhien.BorderRadius = 15;
            this.btnKetThucPhien.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnKetThucPhien.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnKetThucPhien.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnKetThucPhien.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnKetThucPhien.FillColor = System.Drawing.Color.MediumTurquoise;
            this.btnKetThucPhien.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnKetThucPhien.ForeColor = System.Drawing.Color.Black;
            this.btnKetThucPhien.HoverState.FillColor = System.Drawing.Color.LightSeaGreen;
            this.btnKetThucPhien.Location = new System.Drawing.Point(152, 11);
            this.btnKetThucPhien.Name = "btnKetThucPhien";
            this.btnKetThucPhien.Size = new System.Drawing.Size(135, 33);
            this.btnKetThucPhien.TabIndex = 3;
            this.btnKetThucPhien.Text = "Kết thúc phiên chơi";
            this.btnKetThucPhien.Click += new System.EventHandler(this.btnKetThucPhien_Click);
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.AutoRoundedCorners = true;
            this.btnLamMoi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.btnLamMoi.BorderRadius = 15;
            this.btnLamMoi.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnLamMoi.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnLamMoi.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnLamMoi.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnLamMoi.FillColor = System.Drawing.Color.MediumTurquoise;
            this.btnLamMoi.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLamMoi.ForeColor = System.Drawing.Color.Black;
            this.btnLamMoi.HoverState.FillColor = System.Drawing.Color.LightSeaGreen;
            this.btnLamMoi.Location = new System.Drawing.Point(293, 11);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(135, 33);
            this.btnLamMoi.TabIndex = 2;
            this.btnLamMoi.Text = "Làm mới";
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            // 
            // panelMain
            // 
            this.panelMain.BackColor = System.Drawing.Color.White;
            this.panelMain.Controls.Add(this.dgvComputers);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 110);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(800, 340);
            this.panelMain.TabIndex = 2;
            // 
            // dgvComputers
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(251)))), ((int)(((byte)(255)))));
            this.dgvComputers.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvComputers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvComputers.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvComputers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvComputers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvComputers.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(230)))), ((int)(((byte)(241)))));
            this.dgvComputers.Location = new System.Drawing.Point(0, 0);
            this.dgvComputers.Name = "dgvComputers";
            this.dgvComputers.ReadOnly = true;
            this.dgvComputers.RowHeadersVisible = false;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(251)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.Padding = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.dgvComputers.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvComputers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvComputers.Size = new System.Drawing.Size(800, 340);
            this.dgvComputers.TabIndex = 0;
            // 
            // frm_Staff_ComputerManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.panelActions);
            this.Controls.Add(this.panelHeader);
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
        private System.Windows.Forms.Label labelSummary;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.DataGridView dgvComputers;
        private Guna.UI2.WinForms.Guna2Button btnKhoaMay;
        private Guna.UI2.WinForms.Guna2Button btnKetThucPhien;
        private Guna.UI2.WinForms.Guna2Button btnLamMoi;
    }
}