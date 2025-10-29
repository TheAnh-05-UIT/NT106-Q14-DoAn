using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace NT106_Q14_DoAnGroup08.Uc_Staff
{
    public partial class uc_Staff_Bills : UserControl
    {
        private DataTable billsTable;

        public uc_Staff_Bills()
        {
            InitializeComponent();
            this.Load += Uc_Staff_Bills_Load;
        }

        private void Uc_Staff_Bills_Load(object sender, EventArgs e)
        {
            InitializeSampleData();
            PopulateStaffFilter();

            if (billsTable.Rows.Count > 0)
            {
                var minDate = billsTable.AsEnumerable().Min(r => r.Field<DateTime>("Date"));
                var maxDate = billsTable.AsEnumerable().Max(r => r.Field<DateTime>("Date"));
                dtpRangeFrom.Value = minDate.Date;
                dtpRangeTo.Value = maxDate.Date.AddDays(1).AddSeconds(-1);
            }

            UpdateMonthProfit(dtpMonthA, txtMonthAProfit);
            UpdateMonthProfit(dtpMonthB, txtMonthBProfit);

            // wire radio buttons to trigger filter
            rbThu.CheckedChanged += FilterChanged;
            rbChi.CheckedChanged += FilterChanged;

            // also ensure staff selection updates the view
            cmbStaff.SelectedIndexChanged += (s, ev) => ApplyFilter();

            ApplyFilter();
        }

        private void InitializeSampleData()
        {
            billsTable = new DataTable();
            billsTable.Columns.Add("Id", typeof(string));
            billsTable.Columns.Add("Date", typeof(DateTime));
            billsTable.Columns.Add("Staff", typeof(string));
            billsTable.Columns.Add("Type", typeof(string)); // "Thu" or "Chi"
            billsTable.Columns.Add("Amount", typeof(decimal));
            billsTable.Columns.Add("Description", typeof(string));

            // Sample rows
            billsTable.Rows.Add("HD001", DateTime.Today.AddDays(-1), "Nguyen A", "Thu", 120000m, "Thanh toán dịch vụ");
            billsTable.Rows.Add("HD002", DateTime.Today.AddDays(-2), "Tran B", "Thu", 80000m, "Order đồ ăn");
            billsTable.Rows.Add("HD003", DateTime.Today.AddMonths(-1).AddDays(2), "Nguyen A", "Chi", 30000m, "Mua vật tư");
            billsTable.Rows.Add("HD004", DateTime.Today.AddMonths(-1).AddDays(5), "Le C", "Thu", 150000m, "Thanh toán");
            billsTable.Rows.Add("HD005", DateTime.Today.AddMonths(-2), "Tran B", "Chi", 50000m, "Chi phí");
            billsTable.Rows.Add("HD006", DateTime.Today, "Nguyen A", "Thu", 60000m, "Topup");
            billsTable.Rows.Add("HD007", DateTime.Today.AddDays(-10), "Le C", "Chi", 20000m, "Hoàn trả");
            billsTable.Rows.Add("HD008", DateTime.Today.AddDays(-20), "Tran B", "Thu", 90000m, "Thanh toán dịch vụ");

            dataGridViewBills.DataSource = billsTable.Copy();
        }

        private void PopulateStaffFilter()
        {
            cmbStaff.Items.Clear();
            cmbStaff.Items.Add("Tất cả");
            var staffs = billsTable.AsEnumerable().Select(r => r.Field<string>("Staff")).Distinct().OrderBy(s => s);
            foreach (var s in staffs)
                cmbStaff.Items.Add(s);
            cmbStaff.SelectedIndex = 0;
        }

        private void ApplyFilter()
        {
            if (billsTable == null) return;

            DateTime start = dtpRangeFrom.Value.Date;
            DateTime end = dtpRangeTo.Value.Date.AddDays(1).AddSeconds(-1);

            string selectedStaff = cmbStaff.SelectedItem == null ? "Tất cả" : cmbStaff.SelectedItem.ToString();
            bool typeFilterActive = rbThu.Checked || rbChi.Checked;
            string typeFilter = rbThu.Checked ? "Thu" : rbChi.Checked ? "Chi" : null;

            var baseQuery = billsTable.AsEnumerable()
                .Where(r => r.Field<DateTime>("Date") >= start && r.Field<DateTime>("Date") <= end);

            if (selectedStaff != "Tất cả")
            {
                baseQuery = baseQuery.Where(r => r.Field<string>("Staff") == selectedStaff);
            }

            decimal totalThu = baseQuery.Where(r => r.Field<string>("Type") == "Thu").Sum(r => r.Field<decimal>("Amount"));
            decimal totalChi = baseQuery.Where(r => r.Field<string>("Type") == "Chi").Sum(r => r.Field<decimal>("Amount"));

            txtRangeTotalThu.Text = totalThu.ToString("N0");
            txtRangeTotalChi.Text = totalChi.ToString("N0");
            txtRangeProfit.Text = (totalThu - totalChi).ToString("N0");

            var viewQuery = baseQuery;
            if (typeFilterActive && typeFilter != null)
            {
                viewQuery = viewQuery.Where(r => r.Field<string>("Type") == typeFilter);
            }

            DataTable dt = viewQuery.Any() ? viewQuery.CopyToDataTable() : billsTable.Clone();
            dataGridViewBills.DataSource = dt;
        }

        private void UpdateMonthProfit(DateTimePicker dtp, TextBox txtProfit)
        {
            if (billsTable == null) return;
            DateTime value = dtp.Value;
            var monthQuery = billsTable.AsEnumerable()
                .Where(r => r.Field<DateTime>("Date").Year == value.Year && r.Field<DateTime>("Date").Month == value.Month);

            decimal totalThu = monthQuery.Where(r => r.Field<string>("Type") == "Thu").Sum(r => r.Field<decimal>("Amount"));
            decimal totalChi = monthQuery.Where(r => r.Field<string>("Type") == "Chi").Sum(r => r.Field<decimal>("Amount"));

            txtProfit.Text = (totalThu - totalChi).ToString("N0");
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                var dt = dataGridViewBills.DataSource as DataTable ?? (dataGridViewBills.DataSource as DataView)?.ToTable();
                if (dt == null || dt.Rows.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu để xuất.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
                    sfd.FileName = $"Bills_{DateTime.Now:yyyyMMdd_HHmmss}.csv";
                    if (sfd.ShowDialog() != DialogResult.OK) return;

                    ExportDataTableToCsv(dt, sfd.FileName);
                    MessageBox.Show("Xuất dữ liệu thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xuất: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ExportDataTableToCsv(DataTable dt, string filePath)
        {
            using (var sw = new StreamWriter(filePath, false, System.Text.Encoding.UTF8))
            {
                sw.WriteLine(string.Join(",", dt.Columns.Cast<DataColumn>().Select(c => '"' + c.ColumnName + '"')));
                foreach (DataRow row in dt.Rows)
                {
                    var fields = dt.Columns.Cast<DataColumn>().Select(c =>
                    {
                        var val = row[c] == null ? string.Empty : row[c].ToString();
                        return '"' + val.Replace("\"", "\"\"") + '"';
                    });
                    sw.WriteLine(string.Join(",", fields));
                }
            }
        }

        private void dtpMonthA_ValueChanged(object sender, EventArgs e)
        {
            UpdateMonthProfit(dtpMonthA, txtMonthAProfit);
        }

        private void dtpMonthB_ValueChanged(object sender, EventArgs e)
        {
            UpdateMonthProfit(dtpMonthB, txtMonthBProfit);
        }

        private void btnViewMonthA_Click(object sender, EventArgs e)
        {
            ShowMonthInGrid(dtpMonthA.Value);
        }

        private void btnViewMonthB_Click(object sender, EventArgs e)
        {
            ShowMonthInGrid(dtpMonthB.Value);
        }

        private void ShowMonthInGrid(DateTime month)
        {
            var monthQuery = billsTable.AsEnumerable()
                .Where(r => r.Field<DateTime>("Date").Year == month.Year && r.Field<DateTime>("Date").Month == month.Month);
            DataTable dt = monthQuery.Any() ? monthQuery.CopyToDataTable() : billsTable.Clone();
            dataGridViewBills.DataSource = dt;
        }

        private void FilterChanged(object sender, EventArgs e)
        {
            ApplyFilter();
        }

        private void dataGridViewBills_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // placeholder for future detail view
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if (billsTable == null) return;
            cmbStaff.SelectedIndex = 0;
            rbThu.Checked = false;
            rbChi.Checked = false;
            dtpRangeFrom.Value = billsTable.AsEnumerable().Min(r => r.Field<DateTime>("Date")).Date;
            dtpRangeTo.Value = billsTable.AsEnumerable().Max(r => r.Field<DateTime>("Date")).Date.AddDays(1).AddSeconds(-1);
            ApplyFilter();
        }

        // Designer label handlers (if wired)
        private void labelFrom_Click(object sender, EventArgs e) { }
        private void labelStaff_Click(object sender, EventArgs e) { }
        private void labelRangeTo_Click(object sender, EventArgs e) { }
        private void labelRangeTotalChi_Click(object sender, EventArgs e) { }
        private void labelRangeProfit_Click(object sender, EventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }
        private void labelMonthB_Click(object sender, EventArgs e) { }
    }
}
