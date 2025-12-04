namespace NT106_Q14_DoAnGroup08.ClientAdmin
{
    partial class frm_Revenue
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.lblTitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.cboMode = new Guna.UI2.WinForms.Guna2ComboBox();
            this.dtMonth = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.dtYear = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.dtWeekDate = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.btnFilter = new Guna.UI2.WinForms.Guna2Button();
            this.btnExport = new Guna.UI2.WinForms.Guna2Button();
            this.btnRefresh = new Guna.UI2.WinForms.Guna2Button();
            this.dgvSales = new Guna.UI2.WinForms.Guna2DataGridView();
            this.chartRevenue = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.lblTotal = new Guna.UI2.WinForms.Guna2HtmlLabel();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSales)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartRevenue)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(12, 10);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(198, 32);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Thống kê doanh thu";
            // 
            // cboMode
            // 
            this.cboMode.BackColor = System.Drawing.Color.Transparent;
            this.cboMode.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMode.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cboMode.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cboMode.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboMode.ForeColor = System.Drawing.Color.Black;
            this.cboMode.ItemHeight = 30;
            this.cboMode.Items.AddRange(new object[] {
            "Theo Ngày",
            "Theo Tuần",
            "Theo Tháng",
            "Theo Năm"});
            this.cboMode.Location = new System.Drawing.Point(12, 60);
            this.cboMode.Name = "cboMode";
            this.cboMode.Size = new System.Drawing.Size(160, 36);
            this.cboMode.TabIndex = 1;
            this.cboMode.SelectedIndexChanged += new System.EventHandler(this.cboMode_SelectedIndexChanged);
            // 
            // dtMonth
            // 
            this.dtMonth.Checked = true;
            this.dtMonth.CustomFormat = "MMMM yyyy";
            this.dtMonth.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtMonth.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtMonth.Location = new System.Drawing.Point(184, 60);
            this.dtMonth.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtMonth.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtMonth.Name = "dtMonth";
            this.dtMonth.ShowUpDown = true;
            this.dtMonth.Size = new System.Drawing.Size(170, 36);
            this.dtMonth.TabIndex = 2;
            this.dtMonth.Value = new System.DateTime(2025, 11, 13, 19, 22, 16, 461);
            // 
            // dtYear
            // 
            this.dtYear.Checked = true;
            this.dtYear.CustomFormat = "yyyy";
            this.dtYear.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtYear.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtYear.Location = new System.Drawing.Point(184, 60);
            this.dtYear.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtYear.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtYear.Name = "dtYear";
            this.dtYear.ShowUpDown = true;
            this.dtYear.Size = new System.Drawing.Size(120, 36);
            this.dtYear.TabIndex = 3;
            this.dtYear.Value = new System.DateTime(2025, 11, 13, 19, 35, 1, 810);
            this.dtYear.Visible = false;
            // 
            // dtWeekDate
            // 
            this.dtWeekDate.Checked = true;
            this.dtWeekDate.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtWeekDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtWeekDate.Location = new System.Drawing.Point(184, 60);
            this.dtWeekDate.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtWeekDate.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtWeekDate.Name = "dtWeekDate";
            this.dtWeekDate.Size = new System.Drawing.Size(160, 36);
            this.dtWeekDate.TabIndex = 4;
            this.dtWeekDate.Value = new System.DateTime(2025, 11, 13, 19, 35, 44, 728);
            this.dtWeekDate.Visible = false;
            // 
            // btnFilter
            // 
            this.btnFilter.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnFilter.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnFilter.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnFilter.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnFilter.FillColor = System.Drawing.Color.Orange;
            this.btnFilter.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFilter.ForeColor = System.Drawing.Color.Black;
            this.btnFilter.Location = new System.Drawing.Point(360, 60);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(90, 36);
            this.btnFilter.TabIndex = 5;
            this.btnFilter.Text = "Lọc";
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // btnExport
            // 
            this.btnExport.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnExport.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnExport.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnExport.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnExport.FillColor = System.Drawing.Color.Orange;
            this.btnExport.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExport.ForeColor = System.Drawing.Color.Black;
            this.btnExport.Location = new System.Drawing.Point(462, 60);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(90, 36);
            this.btnExport.TabIndex = 6;
            this.btnExport.Text = "Xuất";
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnRefresh.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnRefresh.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnRefresh.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnRefresh.FillColor = System.Drawing.Color.Orange;
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.ForeColor = System.Drawing.Color.Black;
            this.btnRefresh.Location = new System.Drawing.Point(564, 60);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(90, 36);
            this.btnRefresh.TabIndex = 7;
            this.btnRefresh.Text = "Làm mới";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // dgvSales
            // 
            this.dgvSales.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.dgvSales.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvSales.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.PaleGoldenrod;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvSales.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvSales.ColumnHeadersHeight = 19;
            this.dgvSales.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.Bisque;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvSales.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvSales.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvSales.Location = new System.Drawing.Point(12, 102);
            this.dgvSales.Name = "dgvSales";
            this.dgvSales.ReadOnly = true;
            this.dgvSales.RowHeadersVisible = false;
            this.dgvSales.Size = new System.Drawing.Size(456, 428);
            this.dgvSales.TabIndex = 8;
            this.dgvSales.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvSales.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.dgvSales.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.dgvSales.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.dgvSales.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.dgvSales.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.dgvSales.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvSales.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.dgvSales.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvSales.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvSales.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.dgvSales.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.dgvSales.ThemeStyle.HeaderStyle.Height = 19;
            this.dgvSales.ThemeStyle.ReadOnly = true;
            this.dgvSales.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvSales.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvSales.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvSales.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.dgvSales.ThemeStyle.RowsStyle.Height = 22;
            this.dgvSales.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvSales.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            // 
            // chartRevenue
            // 
            chartArea1.Name = "Default";
            this.chartRevenue.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chartRevenue.Legends.Add(legend1);
            this.chartRevenue.Location = new System.Drawing.Point(474, 102);
            this.chartRevenue.Name = "chartRevenue";
            series1.ChartArea = "Default";
            series1.Legend = "Legend1";
            series1.Name = "Doanh thu";
            this.chartRevenue.Series.Add(series1);
            this.chartRevenue.Size = new System.Drawing.Size(562, 440);
            this.chartRevenue.TabIndex = 9;
            this.chartRevenue.Text = "chart1";
            this.chartRevenue.Click += new System.EventHandler(this.chartRevenue_Click);
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = false;
            this.lblTotal.BackColor = System.Drawing.Color.Transparent;
            this.lblTotal.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.Location = new System.Drawing.Point(12, 540);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(560, 36);
            this.lblTotal.TabIndex = 10;
            this.lblTotal.Text = "Tổng doanh thu:";
            // 
            // frm_Revenue
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1064, 588);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.chartRevenue);
            this.Controls.Add(this.dgvSales);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.btnFilter);
            this.Controls.Add(this.dtWeekDate);
            this.Controls.Add(this.dtYear);
            this.Controls.Add(this.dtMonth);
            this.Controls.Add(this.cboMode);
            this.Controls.Add(this.lblTitle);
            this.Name = "frm_Revenue";
            this.Text = "Thống kê doanh thu";
            this.Load += new System.EventHandler(this.frm_Revenue_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSales)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartRevenue)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Guna.UI2.WinForms.Guna2HtmlLabel lblTitle;
        private Guna.UI2.WinForms.Guna2ComboBox cboMode;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtMonth;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtYear;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtWeekDate;
        private Guna.UI2.WinForms.Guna2Button btnFilter;
        private Guna.UI2.WinForms.Guna2Button btnExport;
        private Guna.UI2.WinForms.Guna2Button btnRefresh;
        private Guna.UI2.WinForms.Guna2DataGridView dgvSales;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartRevenue;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTotal;
    }
}