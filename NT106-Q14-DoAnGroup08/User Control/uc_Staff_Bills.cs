using Newtonsoft.Json;
using NT106_Q14_DoAnGroup08.ClientCustomer;
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

            // add a quick text filter box to panelFilter at runtime (if not present)
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

            // wire radio buttons to trigger filter
            rbThu.CheckedChanged += FilterChanged;
            rbChi.CheckedChanged += FilterChanged;

            // also ensure staff selection updates the view
            cmbStaff.SelectedIndexChanged += (s, ev) => ApplyFilter();

            ApplyFilter();
        }

        private void LoadBills()
        {
            try
            {
                var request = new { action = "GET_ALL_INVOICES" };
                string jsonResponse = ServerConnection.SendRequest(JsonConvert.SerializeObject(request));
                dynamic response = JsonConvert.DeserializeObject(jsonResponse);

                if (response.status == "success")
                {
                    // Build a DataTable tailored to the Invoice schema while keeping friendly column names for the UI
                    billsTable = new DataTable();
                    billsTable.Columns.Add("Id", typeof(string)); // InvoiceId
                    billsTable.Columns.Add("Date", typeof(DateTime)); // CreatedAt
                    billsTable.Columns.Add("Staff", typeof(string)); // SessionId
                    billsTable.Columns.Add("CustomerId", typeof(string));
                    billsTable.Columns.Add("Status", typeof(string)); // Detail status
                    billsTable.Columns.Add("Amount", typeof(decimal)); // TotalAmount
                    billsTable.Columns.Add("FoodId", typeof(string));
                    billsTable.Columns.Add("FoodName", typeof(string));
                    billsTable.Columns.Add("ServiceId", typeof(string));
                    billsTable.Columns.Add("ServiceName", typeof(string));
                    billsTable.Columns.Add("Quantity", typeof(int));
                    billsTable.Columns.Add("Price", typeof(decimal));
                    billsTable.Columns.Add("Note", typeof(string));

                    // response.data may be a DataTable, array of objects, or JArray - try to convert to DataTable first
                    DataTable dt = null;
                    try
                    {
                        dt = response.data.ToObject<DataTable>();
                    }
                    catch
                    {
                        // fall through - try to read as dynamic array
                    }

                    if (dt != null)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            var row = dt.Rows[i];
                            billsTable.Rows.Add(
                                row.Table.Columns.Contains("InvoiceId") ? Convert.ToString(row["InvoiceId"]) : string.Empty,
                                row.Table.Columns.Contains("CreatedAt") && row["CreatedAt"] != DBNull.Value ? Convert.ToDateTime(row["CreatedAt"]) : DateTime.MinValue,
                                row.Table.Columns.Contains("SessionId") ? Convert.ToString(row["SessionId"]) : string.Empty,
                                row.Table.Columns.Contains("CustomerId") ? Convert.ToString(row["CustomerId"]) : string.Empty,
                                // DetailStatus column in handler
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
                        // try dynamic enumeration (e.g., JArray)
                        foreach (var item in response.data)
                        {
                            string invoiceId = item.InvoiceId != null ? (string)item.InvoiceId : string.Empty;
                            DateTime createdAt = item.CreatedAt != null ? (DateTime)item.CreatedAt : DateTime.MinValue;
                            string sessionId = item.SessionId != null ? (string)item.SessionId : string.Empty;
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

                            billsTable.Rows.Add(invoiceId, createdAt, sessionId, customerId, status, totalAmount, foodId, foodName, serviceId, serviceName, quantity, price, note);
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
        }

        private void PopulateStaffFilter()
        {
            if (billsTable == null) return;

            cmbStaff.Items.Clear();
            cmbStaff.Items.Add("Tất cả");
            var staffs = billsTable.AsEnumerable()
                .Select(r => r.Field<string>("Staff"))
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

            string selectedStaff = cmbStaff.SelectedItem == null ? "Tất cả" : cmbStaff.SelectedItem.ToString();
            string textFilter = panelFilter.Controls.OfType<TextBox>().FirstOrDefault(t => t.Name == "txtGlobalFilter")?.Text?.Trim() ?? string.Empty;

            var baseQuery = billsTable.AsEnumerable()
                .Where(r => r.Field<DateTime>("Date") >= start && r.Field<DateTime>("Date") <= end);

            if (selectedStaff != "Tất cả")
                baseQuery = baseQuery.Where(r => r.Field<string>("Staff") == selectedStaff);

            if (!string.IsNullOrEmpty(textFilter))
            {
                string tf = textFilter.ToUpperInvariant();
                baseQuery = baseQuery.Where(r => (
                    (r.Field<string>("Id") ?? string.Empty).ToUpperInvariant().Contains(tf) ||
                    (r.Field<string>("CustomerId") ?? string.Empty).ToUpperInvariant().Contains(tf) ||
                    (r.Field<string>("ServiceName") ?? string.Empty).ToUpperInvariant().Contains(tf) ||
                    (r.Field<string>("FoodName") ?? string.Empty).ToUpperInvariant().Contains(tf)
                ));
            }

            // filter by radio if set
            if (rbThu.Checked || rbChi.Checked)
            {
                var paidStatuses = new[] { "PAID", "COMPLETED" };
                if (rbThu.Checked)
                    baseQuery = baseQuery.Where(r => paidStatuses.Contains(((r.Field<string>("Status") ?? string.Empty).ToUpperInvariant())));
                else if (rbChi.Checked)
                    baseQuery = baseQuery.Where(r => !new[] { "PAID", "COMPLETED" }.Contains(((r.Field<string>("Status") ?? string.Empty).ToUpperInvariant())));
            }

            // Compute totals: treat PAID/COMPLETED as revenue (Thu), others as outstanding/cost (Chi)
            var paidStatusesFinal = new[] { "PAID", "COMPLETED" };

            // Sum distinct invoices to avoid double-counting when invoice has multiple details
            var invoicesGrouped = baseQuery
                .GroupBy(r => r.Field<string>("Id"))
                .Select(g => new
                {
                    Id = g.Key,
                    Date = g.First().Field<DateTime>("Date"),
                    Staff = g.First().Field<string>("Staff"),
                    CustomerId = g.First().Field<string>("CustomerId"),
                    Amount = g.First().Field<decimal>("Amount"),
                    IsPaid = g.Any(x => paidStatusesFinal.Contains((x.Field<string>("Status") ?? string.Empty).ToUpperInvariant()))
                }).ToList();

            decimal totalPaid = invoicesGrouped.Where(x => x.IsPaid).Sum(x => x.Amount);
            decimal totalOther = invoicesGrouped.Where(x => !x.IsPaid).Sum(x => x.Amount);

            txtRangeTotalThu.Text = totalPaid.ToString("N0");
            txtRangeTotalChi.Text = totalOther.ToString("N0");
            txtRangeProfit.Text = (totalPaid - totalOther).ToString("N0");

            DataTable dt = baseQuery.Any() ? baseQuery.CopyToDataTable() : billsTable.Clone();
            dataGridViewBills.DataSource = dt;
        }

        private void UpdateMonthProfit(DateTimePicker dtp, TextBox txtProfit)
        {
            if (billsTable == null) return;
            DateTime value = dtp.Value;
            var monthQuery = billsTable.AsEnumerable()
                .Where(r => r.Field<DateTime>("Date").Year == value.Year && r.Field<DateTime>("Date").Month == value.Month);

            var paidStatuses = new[] { "PAID", "COMPLETED" };

            var invoicesGrouped = monthQuery
                .GroupBy(r => r.Field<string>("Id"))
                .Select(g => new
                {
                    Amount = g.First().Field<decimal>("Amount"),
                    IsPaid = g.Any(x => paidStatuses.Contains((x.Field<string>("Status") ?? string.Empty).ToUpperInvariant()))
                });

            decimal totalPaid = invoicesGrouped.Where(x => x.IsPaid).Sum(x => x.Amount);
            decimal totalOther = invoicesGrouped.Where(x => !x.IsPaid).Sum(x => x.Amount);

            txtProfit.Text = (totalPaid - totalOther).ToString("N0");
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
            // If you want real-time filtering when radio buttons change, uncomment next line
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
            var min = billsTable.AsEnumerable().Min(r => r.Field<DateTime>("Date"));
            var max = billsTable.AsEnumerable().Max(r => r.Field<DateTime>("Date"));
            dtpRangeFrom.Value = min.Date;
            dtpRangeTo.Value = max.Date.AddDays(1).AddSeconds(-1);
            // clear text filter if exists
            var txt = panelFilter.Controls.OfType<TextBox>().FirstOrDefault(t => t.Name == "txtGlobalFilter");
            if (txt != null) txt.Text = string.Empty;
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
