using Newtonsoft.Json;
using NT106_Q14_DoAnGroup08.ConnectionServser;
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
            LoadBills();
            PopulateStaffFilter();

            if (panelFilter.Controls.OfType<TextBox>().All(t => t.Name != "txtGlobalFilter"))
            {
                var txt = new TextBox { Name = "txtGlobalFilter", Width = 250, Left = 220, Top = 16 };
                txt.Text = string.Empty;
                txt.TextChanged += (s, ev) => ApplyFilter();
                panelFilter.Controls.Add(txt);
            }

            if (billsTable != null && billsTable.Rows.Count > 0)
            {
                var minDate = billsTable.AsEnumerable().Min(r => r.Field<DateTime>("Date"));
                var maxDate = billsTable.AsEnumerable().Max(r => r.Field<DateTime>("Date"));
                dtpRangeFrom.Value = minDate.Date;
                dtpRangeTo.Value = maxDate.Date.AddDays(1).AddSeconds(-1);
            }

            UpdateMonthProfit(dtpMonthA, txtMonthAProfit);
            UpdateMonthProfit(dtpMonthB, txtMonthBProfit);

            rbThu.CheckedChanged += FilterChanged;
            rbChi.CheckedChanged += FilterChanged;

            cmbStaff.SelectedIndexChanged += (s, ev) => ApplyFilter();

            ApplyFilter();
        }

        private void LoadBills()
        {
            dataGridViewBills.SuspendLayout();
            try
            {
                var request = new { action = "GET_ALL_INVOICES" };
                string jsonResponse = ServerConnection.SendRequest(JsonConvert.SerializeObject(request));
                dynamic response = JsonConvert.DeserializeObject(jsonResponse);

                if (response.status == "success")
                {
                    billsTable = new DataTable();
                    billsTable.Columns.Add("Id", typeof(string));
                    billsTable.Columns.Add("Date", typeof(DateTime));
                    billsTable.Columns.Add("CustomerId", typeof(string));
                    billsTable.Columns.Add("Status", typeof(string));
                    billsTable.Columns.Add("Amount", typeof(decimal));
                    billsTable.Columns.Add("FoodId", typeof(string));
                    billsTable.Columns.Add("FoodName", typeof(string));
                    billsTable.Columns.Add("ServiceId", typeof(string));
                    billsTable.Columns.Add("ServiceName", typeof(string));
                    billsTable.Columns.Add("Quantity", typeof(int));
                    billsTable.Columns.Add("Price", typeof(decimal));
                    billsTable.Columns.Add("Note", typeof(string));
                    billsTable.Columns.Add("  ", typeof(string));

                    DataTable dt = null;
                    try
                    {
                        dt = response.data.ToObject<DataTable>();
                    }
                    catch
                    {
                    }

                    if (dt != null)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            var row = dt.Rows[i];
                            billsTable.Rows.Add(
                                row.Table.Columns.Contains("InvoiceId") ? Convert.ToString(row["InvoiceId"]) : string.Empty,
                                row.Table.Columns.Contains("CreatedAt") && row["CreatedAt"] != DBNull.Value ? Convert.ToDateTime(row["CreatedAt"]) : DateTime.MinValue,
                                row.Table.Columns.Contains("CustomerId") ? Convert.ToString(row["CustomerId"]) : string.Empty,
                                row.Table.Columns.Contains("DetailStatus") ? Convert.ToString(row["DetailStatus"]) : (row.Table.Columns.Contains("Status") ? Convert.ToString(row["Status"]) : string.Empty),
                                row.Table.Columns.Contains("TotalAmount") && row["TotalAmount"] != DBNull.Value ? Convert.ToDecimal(row["TotalAmount"]) : 0m,
                                row.Table.Columns.Contains("FoodId") ? Convert.ToString(row["FoodId"]) : string.Empty,
                                row.Table.Columns.Contains("FoodName") ? Convert.ToString(row["FoodName"]) : string.Empty,
                                row.Table.Columns.Contains("ServiceId") ? Convert.ToString(row["ServiceId"]) : string.Empty,
                                row.Table.Columns.Contains("ServiceName") ? Convert.ToString(row["ServiceName"]) : string.Empty,
                                row.Table.Columns.Contains("Quantity") && row["Quantity"] != DBNull.Value ? Convert.ToInt32(row["Quantity"]) : 1,
                                row.Table.Columns.Contains("Price") && row["Price"] != DBNull.Value ? Convert.ToDecimal(row["Price"]) : 0m,
                                row.Table.Columns.Contains("Note") ? Convert.ToString(row["Note"]) : string.Empty
                            );
                        }
                    }
                    else
                    {
                        foreach (var item in response.data)
                        {
                            string invoiceId = item.InvoiceId != null ? (string)item.InvoiceId : string.Empty;
                            DateTime createdAt = item.CreatedAt != null ? (DateTime)item.CreatedAt : DateTime.MinValue;
                            string customerId = item.CustomerId != null ? (string)item.CustomerId : string.Empty;
                            decimal totalAmount = item.TotalAmount != null ? (decimal)item.TotalAmount : 0m;
                            string foodId = item.FoodId != null ? (string)item.FoodId : string.Empty;
                            string foodName = item.FoodName != null ? (string)item.FoodName : string.Empty;
                            string serviceId = item.ServiceId != null ? (string)item.ServiceId : string.Empty;
                            string serviceName = item.ServiceName != null ? (string)item.ServiceName : string.Empty;
                            int quantity = item.Quantity != null ? (int)item.Quantity : 1;
                            decimal price = item.Price != null ? (decimal)item.Price : 0m;
                            string status = item.DetailStatus != null ? (string)item.DetailStatus : (item.Status != null ? (string)item.Status : string.Empty);
                            string note = item.Note != null ? (string)item.Note : string.Empty;

                            billsTable.Rows.Add(invoiceId, createdAt, customerId, status, totalAmount, foodId, foodName, serviceId, serviceName, quantity, price, note);
                        }
                    }

                    dataGridViewBills.DataSource = billsTable.Copy();

                    dataGridViewBills.Visible = true;
                }
                else
                {
                    MessageBox.Show("Lỗi tải danh sách hóa đơn: " + response.message);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối: " + ex.Message);
            }
            dataGridViewBills.ResumeLayout(true);
            dataGridViewBills.Refresh();
        }

        private void PopulateStaffFilter()
        {
            if (billsTable == null) return;

            cmbStaff.Items.Clear();
            cmbStaff.Items.Add("Tất cả");
            var staffs = billsTable.AsEnumerable()
                .Select(r => r.Field<string>("CustomerId"))
                .Where(s => !string.IsNullOrEmpty(s))
                .Distinct()
                .OrderBy(s => s);

            foreach (var s in staffs)
                cmbStaff.Items.Add(s);

            cmbStaff.SelectedIndex = 0;
        }

        private void ApplyFilter()
        {
            if (billsTable == null) return;

            DateTime start = dtpRangeFrom.Value.Date;
            DateTime end = dtpRangeTo.Value.Date.AddDays(1).AddSeconds(-1);
            string selectedCustomer = cmbStaff.SelectedItem?.ToString() ?? "Tất cả";

            var txtFilter = panelFilter.Controls.OfType<TextBox>().FirstOrDefault(t => t.Name == "txtGlobalFilter");
            string tf = txtFilter?.Text?.Trim().ToUpperInvariant() ?? string.Empty;

            var validStatuses = new[] { "PAID", "COMPLETED" };

            var baseQuery = billsTable.AsEnumerable().Where(r =>
                r.Field<DateTime>("Date") >= start &&
                r.Field<DateTime>("Date") <= end &&
                (selectedCustomer == "Tất cả" || r.Field<string>("CustomerId") == selectedCustomer) &&
                (string.IsNullOrEmpty(tf) ||
                    (r.Field<string>("Id") ?? "").ToUpperInvariant().Contains(tf) ||
                    (r.Field<string>("FoodName") ?? "").ToUpperInvariant().Contains(tf) ||
                    (r.Field<string>("ServiceName") ?? "").ToUpperInvariant().Contains(tf))
            );

            decimal totalThu = baseQuery
                .Where(r => (r.Field<string>("ServiceId") ?? "") != "3" &&
                            validStatuses.Contains((r.Field<string>("Status") ?? "").ToUpperInvariant()))
                .Sum(r => r.Field<decimal>("Amount"));

            decimal totalChi = baseQuery
                .Where(r => (r.Field<string>("ServiceId") ?? "") == "3" &&
                            validStatuses.Contains((r.Field<string>("Status") ?? "").ToUpperInvariant()))
                .Sum(r => r.Field<decimal>("Amount"));

            var displayQuery = baseQuery;

            if (rbThu.Checked)
            {
                displayQuery = displayQuery.Where(r =>
                    (r.Field<string>("ServiceId") ?? "") != "3" &&
                    validStatuses.Contains((r.Field<string>("Status") ?? "").ToUpperInvariant()));
            }
            else if (rbChi.Checked)
            {
                displayQuery = displayQuery.Where(r =>
                    (r.Field<string>("ServiceId") ?? "") == "3" &&
                    validStatuses.Contains((r.Field<string>("Status") ?? "").ToUpperInvariant()));
            }

            txtRangeTotalThu.Text = totalThu.ToString("N0");
            txtRangeTotalChi.Text = totalChi.ToString("N0");
            txtRangeProfit.Text = (totalThu - totalChi).ToString("N0");

            dataGridViewBills.DataSource = displayQuery.Any() ? displayQuery.CopyToDataTable() : billsTable.Clone();
        }

        private void UpdateMonthProfit(DateTimePicker dtp, TextBox txtProfit)
        {
            if (billsTable == null) return;

            var validStatuses = new[] { "PAID", "COMPLETED" };
            DateTime val = dtp.Value;

            var monthRows = billsTable.AsEnumerable().Where(r =>
                r.Field<DateTime>("Date").Year == val.Year &&
                r.Field<DateTime>("Date").Month == val.Month &&
                validStatuses.Contains((r.Field<string>("Status") ?? "").ToUpperInvariant())
            );

            decimal thu = monthRows.Where(r => (r.Field<string>("ServiceId") ?? "") != "3").Sum(r => r.Field<decimal>("Amount"));
            decimal chi = monthRows.Where(r => (r.Field<string>("ServiceId") ?? "") == "3").Sum(r => r.Field<decimal>("Amount"));

            txtProfit.Text = (thu - chi).ToString("N0");
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
            if (billsTable == null) return;

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
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadBills();
            PopulateStaffFilter();
            if (billsTable == null) return;
            cmbStaff.SelectedIndex = 0;
            rbThu.Checked = false;
            rbChi.Checked = false;
            var min = billsTable.AsEnumerable().Min(r => r.Field<DateTime>("Date"));
            var max = billsTable.AsEnumerable().Max(r => r.Field<DateTime>("Date"));
            dtpRangeFrom.Value = min.Date;
            dtpRangeTo.Value = max.Date.AddDays(1).AddSeconds(-1);
            // clear text filter if exists
            var txt = panelFilter.Controls.OfType<TextBox>().FirstOrDefault(t => t.Name == "txtGlobalFilter");
            if (txt != null) txt.Text = string.Empty;
            ApplyFilter();
        }

        private void labelFrom_Click(object sender, EventArgs e) { }
        private void labelStaff_Click(object sender, EventArgs e) { }
        private void labelRangeTo_Click(object sender, EventArgs e) { }
        private void labelRangeTotalChi_Click(object sender, EventArgs e) { }
        private void labelRangeProfit_Click(object sender, EventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }
        private void labelMonthB_Click(object sender, EventArgs e) { }

        private void cmbStaff_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
