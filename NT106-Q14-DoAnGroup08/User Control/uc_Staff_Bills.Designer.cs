namespace NT106_Q14_DoAnGroup08.Uc_Staff
{
    partial class uc_Staff_Bills
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnPrint = new System.Windows.Forms.Button();
            this.groupBoxMonthStats = new System.Windows.Forms.GroupBox();
            this.txtMonthBProfit = new System.Windows.Forms.TextBox();
            this.btnViewMonthB = new System.Windows.Forms.Button();
            this.txtMonthAProfit = new System.Windows.Forms.TextBox();
            this.btnViewMonthA = new System.Windows.Forms.Button();
            this.labelMonthA = new System.Windows.Forms.Label();
            this.dtpMonthA = new System.Windows.Forms.DateTimePicker();
            this.labelMonthB = new System.Windows.Forms.Label();
            this.dtpMonthB = new System.Windows.Forms.DateTimePicker();
            this.groupBoxRangeStats = new System.Windows.Forms.GroupBox();
            this.labelRangeFrom = new System.Windows.Forms.Label();
            this.labelRangeTo = new System.Windows.Forms.Label();
            this.dtpRangeFrom = new System.Windows.Forms.DateTimePicker();
            this.dtpRangeTo = new System.Windows.Forms.DateTimePicker();
            this.labelRangeTotalThu = new System.Windows.Forms.Label();
            this.labelRangeTotalChi = new System.Windows.Forms.Label();
            this.labelRangeProfit = new System.Windows.Forms.Label();
            this.txtRangeTotalThu = new System.Windows.Forms.TextBox();
            this.txtRangeTotalChi = new System.Windows.Forms.TextBox();
            this.txtRangeProfit = new System.Windows.Forms.TextBox();
            this.panelRight = new System.Windows.Forms.Panel();
            this.panelFilter = new System.Windows.Forms.Panel();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.dataGridViewBills = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.cmbStaff = new System.Windows.Forms.ComboBox();
            this.rbThu = new System.Windows.Forms.RadioButton();
            this.labelStaff = new System.Windows.Forms.Label();
            this.rbChi = new System.Windows.Forms.RadioButton();
            this.panelLeft = new System.Windows.Forms.Panel();
            this.groupBoxMonthStats.SuspendLayout();
            this.groupBoxRangeStats.SuspendLayout();
            this.panelRight.SuspendLayout();
            this.panelFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewBills)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.panelLeft.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 16);
            this.label1.TabIndex = 5;
            this.label1.Text = "Lợi nhuận";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 158);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 16);
            this.label2.TabIndex = 6;
            this.label2.Text = "Lợi nhuận";
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(76, 511);
            this.btnPrint.Margin = new System.Windows.Forms.Padding(2);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(144, 26);
            this.btnPrint.TabIndex = 10;
            this.btnPrint.Text = "In hóa đơn";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // groupBoxMonthStats
            // 
            this.groupBoxMonthStats.Controls.Add(this.txtMonthBProfit);
            this.groupBoxMonthStats.Controls.Add(this.btnViewMonthB);
            this.groupBoxMonthStats.Controls.Add(this.txtMonthAProfit);
            this.groupBoxMonthStats.Controls.Add(this.btnViewMonthA);
            this.groupBoxMonthStats.Controls.Add(this.labelMonthA);
            this.groupBoxMonthStats.Controls.Add(this.dtpMonthA);
            this.groupBoxMonthStats.Controls.Add(this.labelMonthB);
            this.groupBoxMonthStats.Controls.Add(this.dtpMonthB);
            this.groupBoxMonthStats.Location = new System.Drawing.Point(7, 290);
            this.groupBoxMonthStats.Margin = new System.Windows.Forms.Padding(2);
            this.groupBoxMonthStats.Name = "groupBoxMonthStats";
            this.groupBoxMonthStats.Padding = new System.Windows.Forms.Padding(2);
            this.groupBoxMonthStats.Size = new System.Drawing.Size(300, 217);
            this.groupBoxMonthStats.TabIndex = 1;
            this.groupBoxMonthStats.TabStop = false;
            this.groupBoxMonthStats.Text = "Thống kê theo tháng";
            // 
            // txtMonthBProfit
            // 
            this.txtMonthBProfit.Location = new System.Drawing.Point(95, 150);
            this.txtMonthBProfit.Margin = new System.Windows.Forms.Padding(2);
            this.txtMonthBProfit.Name = "txtMonthBProfit";
            this.txtMonthBProfit.ReadOnly = true;
            this.txtMonthBProfit.Size = new System.Drawing.Size(175, 22);
            this.txtMonthBProfit.TabIndex = 6;
            this.txtMonthBProfit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnViewMonthB
            // 
            this.btnViewMonthB.Location = new System.Drawing.Point(95, 178);
            this.btnViewMonthB.Margin = new System.Windows.Forms.Padding(2);
            this.btnViewMonthB.Name = "btnViewMonthB";
            this.btnViewMonthB.Size = new System.Drawing.Size(96, 26);
            this.btnViewMonthB.TabIndex = 8;
            this.btnViewMonthB.Text = "Xem chi tiết";
            this.btnViewMonthB.UseVisualStyleBackColor = true;
            this.btnViewMonthB.Click += new System.EventHandler(this.btnViewMonthB_Click);
            // 
            // txtMonthAProfit
            // 
            this.txtMonthAProfit.Location = new System.Drawing.Point(95, 60);
            this.txtMonthAProfit.Margin = new System.Windows.Forms.Padding(2);
            this.txtMonthAProfit.Name = "txtMonthAProfit";
            this.txtMonthAProfit.ReadOnly = true;
            this.txtMonthAProfit.Size = new System.Drawing.Size(175, 22);
            this.txtMonthAProfit.TabIndex = 5;
            this.txtMonthAProfit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnViewMonthA
            // 
            this.btnViewMonthA.Location = new System.Drawing.Point(95, 85);
            this.btnViewMonthA.Margin = new System.Windows.Forms.Padding(2);
            this.btnViewMonthA.Name = "btnViewMonthA";
            this.btnViewMonthA.Size = new System.Drawing.Size(96, 26);
            this.btnViewMonthA.TabIndex = 7;
            this.btnViewMonthA.Text = "Xem chi tiết";
            this.btnViewMonthA.UseVisualStyleBackColor = true;
            this.btnViewMonthA.Click += new System.EventHandler(this.btnViewMonthA_Click);
            // 
            // labelMonthA
            // 
            this.labelMonthA.AutoSize = true;
            this.labelMonthA.Location = new System.Drawing.Point(18, 31);
            this.labelMonthA.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelMonthA.Name = "labelMonthA";
            this.labelMonthA.Size = new System.Drawing.Size(46, 16);
            this.labelMonthA.TabIndex = 0;
            this.labelMonthA.Text = "Tháng";
            // 
            // dtpMonthA
            // 
            this.dtpMonthA.CustomFormat = "MM/yyyy";
            this.dtpMonthA.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpMonthA.Location = new System.Drawing.Point(95, 25);
            this.dtpMonthA.Margin = new System.Windows.Forms.Padding(2);
            this.dtpMonthA.Name = "dtpMonthA";
            this.dtpMonthA.ShowUpDown = true;
            this.dtpMonthA.Size = new System.Drawing.Size(175, 22);
            this.dtpMonthA.TabIndex = 1;
            this.dtpMonthA.ValueChanged += new System.EventHandler(this.dtpMonthA_ValueChanged);
            // 
            // labelMonthB
            // 
            this.labelMonthB.AutoSize = true;
            this.labelMonthB.Location = new System.Drawing.Point(21, 119);
            this.labelMonthB.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelMonthB.Name = "labelMonthB";
            this.labelMonthB.Size = new System.Drawing.Size(46, 16);
            this.labelMonthB.TabIndex = 3;
            this.labelMonthB.Text = "Tháng";
            // 
            // dtpMonthB
            // 
            this.dtpMonthB.CustomFormat = "MM/yyyy";
            this.dtpMonthB.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpMonthB.Location = new System.Drawing.Point(95, 119);
            this.dtpMonthB.Margin = new System.Windows.Forms.Padding(2);
            this.dtpMonthB.Name = "dtpMonthB";
            this.dtpMonthB.ShowUpDown = true;
            this.dtpMonthB.Size = new System.Drawing.Size(175, 22);
            this.dtpMonthB.TabIndex = 4;
            this.dtpMonthB.ValueChanged += new System.EventHandler(this.dtpMonthB_ValueChanged);
            // 
            // groupBoxRangeStats
            // 
            this.groupBoxRangeStats.Controls.Add(this.labelRangeFrom);
            this.groupBoxRangeStats.Controls.Add(this.labelRangeTo);
            this.groupBoxRangeStats.Controls.Add(this.dtpRangeFrom);
            this.groupBoxRangeStats.Controls.Add(this.dtpRangeTo);
            this.groupBoxRangeStats.Controls.Add(this.labelRangeTotalThu);
            this.groupBoxRangeStats.Controls.Add(this.labelRangeTotalChi);
            this.groupBoxRangeStats.Controls.Add(this.labelRangeProfit);
            this.groupBoxRangeStats.Controls.Add(this.txtRangeTotalThu);
            this.groupBoxRangeStats.Controls.Add(this.txtRangeTotalChi);
            this.groupBoxRangeStats.Controls.Add(this.txtRangeProfit);
            this.groupBoxRangeStats.Location = new System.Drawing.Point(7, 42);
            this.groupBoxRangeStats.Margin = new System.Windows.Forms.Padding(2);
            this.groupBoxRangeStats.Name = "groupBoxRangeStats";
            this.groupBoxRangeStats.Padding = new System.Windows.Forms.Padding(2);
            this.groupBoxRangeStats.Size = new System.Drawing.Size(324, 186);
            this.groupBoxRangeStats.TabIndex = 0;
            this.groupBoxRangeStats.TabStop = false;
            this.groupBoxRangeStats.Text = "Thống kê theo khoảng";
            // 
            // labelRangeFrom
            // 
            this.labelRangeFrom.AutoSize = true;
            this.labelRangeFrom.Location = new System.Drawing.Point(8, 30);
            this.labelRangeFrom.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelRangeFrom.Name = "labelRangeFrom";
            this.labelRangeFrom.Size = new System.Drawing.Size(56, 16);
            this.labelRangeFrom.TabIndex = 0;
            this.labelRangeFrom.Text = "Từ ngày";
            // 
            // labelRangeTo
            // 
            this.labelRangeTo.AutoSize = true;
            this.labelRangeTo.Location = new System.Drawing.Point(8, 61);
            this.labelRangeTo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelRangeTo.Name = "labelRangeTo";
            this.labelRangeTo.Size = new System.Drawing.Size(31, 16);
            this.labelRangeTo.TabIndex = 1;
            this.labelRangeTo.Text = "Đến";
            // 
            // dtpRangeFrom
            // 
            this.dtpRangeFrom.CustomFormat = "dd/MM/yyyy HH:mm:ss";
            this.dtpRangeFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpRangeFrom.Location = new System.Drawing.Point(106, 25);
            this.dtpRangeFrom.Margin = new System.Windows.Forms.Padding(2);
            this.dtpRangeFrom.Name = "dtpRangeFrom";
            this.dtpRangeFrom.Size = new System.Drawing.Size(183, 22);
            this.dtpRangeFrom.TabIndex = 2;
            this.dtpRangeFrom.ValueChanged += new System.EventHandler(this.FilterChanged);
            // 
            // dtpRangeTo
            // 
            this.dtpRangeTo.CustomFormat = "dd/MM/yyyy HH:mm:ss";
            this.dtpRangeTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpRangeTo.Location = new System.Drawing.Point(106, 55);
            this.dtpRangeTo.Margin = new System.Windows.Forms.Padding(2);
            this.dtpRangeTo.Name = "dtpRangeTo";
            this.dtpRangeTo.Size = new System.Drawing.Size(183, 22);
            this.dtpRangeTo.TabIndex = 3;
            this.dtpRangeTo.ValueChanged += new System.EventHandler(this.FilterChanged);
            // 
            // labelRangeTotalThu
            // 
            this.labelRangeTotalThu.AutoSize = true;
            this.labelRangeTotalThu.Location = new System.Drawing.Point(8, 98);
            this.labelRangeTotalThu.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelRangeTotalThu.Name = "labelRangeTotalThu";
            this.labelRangeTotalThu.Size = new System.Drawing.Size(59, 16);
            this.labelRangeTotalThu.TabIndex = 4;
            this.labelRangeTotalThu.Text = "Tổng thu";
            // 
            // labelRangeTotalChi
            // 
            this.labelRangeTotalChi.AutoSize = true;
            this.labelRangeTotalChi.Location = new System.Drawing.Point(8, 126);
            this.labelRangeTotalChi.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelRangeTotalChi.Name = "labelRangeTotalChi";
            this.labelRangeTotalChi.Size = new System.Drawing.Size(59, 16);
            this.labelRangeTotalChi.TabIndex = 5;
            this.labelRangeTotalChi.Text = "Tổng chi";
            // 
            // labelRangeProfit
            // 
            this.labelRangeProfit.AutoSize = true;
            this.labelRangeProfit.Location = new System.Drawing.Point(8, 152);
            this.labelRangeProfit.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelRangeProfit.Name = "labelRangeProfit";
            this.labelRangeProfit.Size = new System.Drawing.Size(64, 16);
            this.labelRangeProfit.TabIndex = 6;
            this.labelRangeProfit.Text = "Lợi nhuận";
            // 
            // txtRangeTotalThu
            // 
            this.txtRangeTotalThu.Location = new System.Drawing.Point(106, 95);
            this.txtRangeTotalThu.Margin = new System.Windows.Forms.Padding(2);
            this.txtRangeTotalThu.Name = "txtRangeTotalThu";
            this.txtRangeTotalThu.ReadOnly = true;
            this.txtRangeTotalThu.Size = new System.Drawing.Size(183, 22);
            this.txtRangeTotalThu.TabIndex = 7;
            this.txtRangeTotalThu.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtRangeTotalChi
            // 
            this.txtRangeTotalChi.Location = new System.Drawing.Point(106, 120);
            this.txtRangeTotalChi.Margin = new System.Windows.Forms.Padding(2);
            this.txtRangeTotalChi.Name = "txtRangeTotalChi";
            this.txtRangeTotalChi.ReadOnly = true;
            this.txtRangeTotalChi.Size = new System.Drawing.Size(183, 22);
            this.txtRangeTotalChi.TabIndex = 8;
            this.txtRangeTotalChi.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtRangeProfit
            // 
            this.txtRangeProfit.Location = new System.Drawing.Point(106, 146);
            this.txtRangeProfit.Margin = new System.Windows.Forms.Padding(2);
            this.txtRangeProfit.Name = "txtRangeProfit";
            this.txtRangeProfit.ReadOnly = true;
            this.txtRangeProfit.Size = new System.Drawing.Size(183, 22);
            this.txtRangeProfit.TabIndex = 9;
            this.txtRangeProfit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // panelRight
            // 
            this.panelRight.Controls.Add(this.groupBoxRangeStats);
            this.panelRight.Controls.Add(this.groupBoxMonthStats);
            this.panelRight.Controls.Add(this.btnPrint);
            this.panelRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelRight.Location = new System.Drawing.Point(767, 0);
            this.panelRight.Margin = new System.Windows.Forms.Padding(2);
            this.panelRight.Name = "panelRight";
            this.panelRight.Size = new System.Drawing.Size(347, 565);
            this.panelRight.TabIndex = 1;
            // 
            // panelFilter
            // 
            this.panelFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelFilter.Controls.Add(this.textBox4);
            this.panelFilter.Location = new System.Drawing.Point(7, 6);
            this.panelFilter.Margin = new System.Windows.Forms.Padding(2);
            this.panelFilter.Name = "panelFilter";
            this.panelFilter.Size = new System.Drawing.Size(1154, 60);
            this.panelFilter.TabIndex = 0;
            // 
            // textBox4
            // 
            this.textBox4.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox4.Location = new System.Drawing.Point(32, 14);
            this.textBox4.Name = "textBox4";
            this.textBox4.ReadOnly = true;
            this.textBox4.Size = new System.Drawing.Size(172, 21);
            this.textBox4.TabIndex = 0;
            this.textBox4.Text = "Lịch sử thanh toán";
            // 
            // dataGridViewBills
            // 
            this.dataGridViewBills.AllowUserToAddRows = false;
            this.dataGridViewBills.AllowUserToDeleteRows = false;
            this.dataGridViewBills.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridViewBills.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewBills.Location = new System.Drawing.Point(11, 174);
            this.dataGridViewBills.Name = "dataGridViewBills";
            this.dataGridViewBills.ReadOnly = true;
            this.dataGridViewBills.RowHeadersWidth = 51;
            this.dataGridViewBills.RowTemplate.Height = 24;
            this.dataGridViewBills.Size = new System.Drawing.Size(747, 388);
            this.dataGridViewBills.StandardTab = true;
            this.dataGridViewBills.TabIndex = 10;
            this.dataGridViewBills.Visible = false;
            this.dataGridViewBills.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewBills_CellContentClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnRefresh);
            this.groupBox1.Controls.Add(this.cmbStaff);
            this.groupBox1.Controls.Add(this.rbThu);
            this.groupBox1.Controls.Add(this.labelStaff);
            this.groupBox1.Controls.Add(this.rbChi);
            this.groupBox1.Location = new System.Drawing.Point(11, 73);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(747, 89);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Bộ lọc";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(484, 30);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(2);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(136, 39);
            this.btnRefresh.TabIndex = 4;
            this.btnRefresh.Text = "Làm mới bộ lọc";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // cmbStaff
            // 
            this.cmbStaff.FormattingEnabled = true;
            this.cmbStaff.Location = new System.Drawing.Point(108, 37);
            this.cmbStaff.Name = "cmbStaff";
            this.cmbStaff.Size = new System.Drawing.Size(214, 24);
            this.cmbStaff.TabIndex = 11;
            this.cmbStaff.SelectedIndexChanged += new System.EventHandler(this.cmbStaff_SelectedIndexChanged);
            // 
            // rbThu
            // 
            this.rbThu.AutoSize = true;
            this.rbThu.Location = new System.Drawing.Point(348, 39);
            this.rbThu.Margin = new System.Windows.Forms.Padding(2);
            this.rbThu.Name = "rbThu";
            this.rbThu.Size = new System.Drawing.Size(51, 20);
            this.rbThu.TabIndex = 2;
            this.rbThu.Text = "Thu";
            this.rbThu.UseVisualStyleBackColor = true;
            // 
            // labelStaff
            // 
            this.labelStaff.AutoSize = true;
            this.labelStaff.Location = new System.Drawing.Point(25, 37);
            this.labelStaff.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelStaff.Name = "labelStaff";
            this.labelStaff.Size = new System.Drawing.Size(77, 16);
            this.labelStaff.TabIndex = 5;
            this.labelStaff.Text = "Khách hàng";
            // 
            // rbChi
            // 
            this.rbChi.AutoSize = true;
            this.rbChi.Location = new System.Drawing.Point(419, 39);
            this.rbChi.Margin = new System.Windows.Forms.Padding(2);
            this.rbChi.Name = "rbChi";
            this.rbChi.Size = new System.Drawing.Size(47, 20);
            this.rbChi.TabIndex = 3;
            this.rbChi.Text = "Chi";
            this.rbChi.UseVisualStyleBackColor = true;
            // 
            // panelLeft
            // 
            this.panelLeft.Controls.Add(this.groupBox1);
            this.panelLeft.Controls.Add(this.dataGridViewBills);
            this.panelLeft.Controls.Add(this.panelFilter);
            this.panelLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelLeft.Location = new System.Drawing.Point(0, 0);
            this.panelLeft.Margin = new System.Windows.Forms.Padding(2);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Size = new System.Drawing.Size(767, 565);
            this.panelLeft.TabIndex = 0;
            // 
            // uc_Staff_Bills
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.panelLeft);
            this.Controls.Add(this.panelRight);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "uc_Staff_Bills";
            this.Size = new System.Drawing.Size(1114, 565);
            this.groupBoxMonthStats.ResumeLayout(false);
            this.groupBoxMonthStats.PerformLayout();
            this.groupBoxRangeStats.ResumeLayout(false);
            this.groupBoxRangeStats.PerformLayout();
            this.panelRight.ResumeLayout(false);
            this.panelFilter.ResumeLayout(false);
            this.panelFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewBills)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panelLeft.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.GroupBox groupBoxMonthStats;
        private System.Windows.Forms.TextBox txtMonthBProfit;
        private System.Windows.Forms.Button btnViewMonthB;
        private System.Windows.Forms.TextBox txtMonthAProfit;
        private System.Windows.Forms.Button btnViewMonthA;
        private System.Windows.Forms.Label labelMonthA;
        private System.Windows.Forms.DateTimePicker dtpMonthA;
        private System.Windows.Forms.Label labelMonthB;
        private System.Windows.Forms.DateTimePicker dtpMonthB;
        private System.Windows.Forms.GroupBox groupBoxRangeStats;
        private System.Windows.Forms.Label labelRangeFrom;
        private System.Windows.Forms.Label labelRangeTo;
        private System.Windows.Forms.DateTimePicker dtpRangeFrom;
        private System.Windows.Forms.DateTimePicker dtpRangeTo;
        private System.Windows.Forms.Label labelRangeTotalThu;
        private System.Windows.Forms.Label labelRangeTotalChi;
        private System.Windows.Forms.Label labelRangeProfit;
        private System.Windows.Forms.TextBox txtRangeTotalThu;
        private System.Windows.Forms.TextBox txtRangeTotalChi;
        private System.Windows.Forms.TextBox txtRangeProfit;
        private System.Windows.Forms.Panel panelRight;
        private System.Windows.Forms.Panel panelFilter;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.DataGridView dataGridViewBills;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.ComboBox cmbStaff;
        private System.Windows.Forms.RadioButton rbThu;
        private System.Windows.Forms.Label labelStaff;
        private System.Windows.Forms.RadioButton rbChi;
        private System.Windows.Forms.Panel panelLeft;
    }
}
