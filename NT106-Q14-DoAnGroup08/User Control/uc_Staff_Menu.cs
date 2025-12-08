using Newtonsoft.Json;
using NT106_Q14_DoAnGroup08.ConnectionServser;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace NT106_Q14_DoAnGroup08.Uc_Staff
{
    public partial class uc_Staff_Menu : UserControl
    {
        private DataTable menuTable;
        private DataTable orderTable;

        public uc_Staff_Menu()
        {
            InitializeComponent();
            this.Load += Uc_Staff_Menu_Load;
        }

        private void Uc_Staff_Menu_Load(object sender, EventArgs e)
        {
            InitializeOrderTable();
            LoadMenu();
            textBox1.TextChanged += (s, ev) => ApplyFilter();
            button3.Click += BtnExport_Click;
            button4.Click += BtnCalculate_Click;
            button1.Click += BtnAddSelectedItem_Click;
            button2.Click += BtnCategory_Click;
            dataGridView2.CellDoubleClick += DataGridView2_CellDoubleClick;
        }

        private void InitializeOrderTable()
        {
            orderTable = new DataTable();
            orderTable.Columns.Add("FoodId", typeof(string));
            orderTable.Columns.Add("FoodName", typeof(string));
            orderTable.Columns.Add("Quantity", typeof(int));
            orderTable.Columns.Add("Price", typeof(decimal));
            orderTable.Columns.Add("Total", typeof(decimal));
            dataGridView1.DataSource = orderTable;
        }

        private void LoadMenu()
        {
            try
            {
                var request = new { action = "GET_ALL_FOOD" };
                string jsonResponse = ServerConnection.SendRequest(JsonConvert.SerializeObject(request));
                dynamic response = JsonConvert.DeserializeObject(jsonResponse);
                if (response.status == "success")
                {
                    menuTable = new DataTable();
                    menuTable.Columns.Add("FoodId", typeof(string));
                    menuTable.Columns.Add("FoodName", typeof(string));
                    menuTable.Columns.Add("CategoryName", typeof(string));
                    menuTable.Columns.Add("Price", typeof(decimal));

                    DataTable dt = null;
                    try
                    {
                        dt = response.data.ToObject<DataTable>();
                    }
                    catch { }

                    if (dt != null)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            menuTable.Rows.Add(
                                row.Table.Columns.Contains("FoodId") ? Convert.ToString(row["FoodId"]) : string.Empty,
                                row.Table.Columns.Contains("FoodName") ? Convert.ToString(row["FoodName"]) : string.Empty,
                                row.Table.Columns.Contains("CategoryName") ? Convert.ToString(row["CategoryName"]) : string.Empty,
                                row.Table.Columns.Contains("Price") && row["Price"] != DBNull.Value ? Convert.ToDecimal(row["Price"]) : 0m
                            );
                        }
                    }
                    else
                    {
                        foreach (var item in response.data)
                        {
                            string id = item.FoodId != null ? (string)item.FoodId : string.Empty;
                            string name = item.FoodName != null ? (string)item.FoodName : string.Empty;
                            string cat = item.CategoryName != null ? (string)item.CategoryName : string.Empty;
                            decimal price = item.Price != null ? (decimal)item.Price : 0m;
                            menuTable.Rows.Add(id, name, cat, price);
                        }
                    }

                    dataGridView2.DataSource = menuTable.Copy();
                }
                else
                {
                    MessageBox.Show("Lỗi tải menu: " + response.message);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối: " + ex.Message);
            }
        }

        private void ApplyFilter()
        {
            if (menuTable == null) return;
            string tf = textBox1.Text?.Trim();
            var query = menuTable.AsEnumerable();
            if (!string.IsNullOrEmpty(tf))
            {
                string up = tf.ToUpperInvariant();
                query = query.Where(r => ((r.Field<string>("FoodId") ?? string.Empty).ToUpperInvariant().Contains(up)
                    || (r.Field<string>("FoodName") ?? string.Empty).ToUpperInvariant().Contains(up)
                    || (r.Field<string>("CategoryName") ?? string.Empty).ToUpperInvariant().Contains(up)));
            }
            DataTable dt = query.Any() ? query.CopyToDataTable() : menuTable.Clone();
            dataGridView2.DataSource = dt;
        }

        private void AddMenuItemToOrder(string foodId, string foodName, decimal price)
        {
            var existing = orderTable.AsEnumerable().FirstOrDefault(r => r.Field<string>("FoodId") == foodId);
            if (existing != null)
            {
                int q = existing.Field<int>("Quantity");
                q++;
                existing.SetField("Quantity", q);
                existing.SetField("Total", q * existing.Field<decimal>("Price"));
            }
            else
            {
                var row = orderTable.NewRow();
                row["FoodId"] = foodId;
                row["FoodName"] = foodName;
                row["Quantity"] = 1;
                row["Price"] = price;
                row["Total"] = price * 1;
                orderTable.Rows.Add(row);
            }
            dataGridView1.DataSource = orderTable.Copy();
        }

        private void DataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var row = (dataGridView2.DataSource as DataTable).Rows[e.RowIndex];
            string id = row["FoodId"].ToString();
            string name = row["FoodName"].ToString();
            decimal price = row["Price"] != DBNull.Value ? Convert.ToDecimal(row["Price"]) : 0m;
            AddMenuItemToOrder(id, name, price);
        }

        private void BtnAddSelectedItem_Click(object sender, EventArgs e)
        {
            var dv = dataGridView2.DataSource as DataTable;
            if (dv == null || dv.Rows.Count == 0) return;
            if (dataGridView2.CurrentRow == null) return;
            var row = (dataGridView2.CurrentRow.DataBoundItem as DataRowView).Row;
            string id = row["FoodId"].ToString();
            string name = row["FoodName"].ToString();
            decimal price = row["Price"] != DBNull.Value ? Convert.ToDecimal(row["Price"]) : 0m;
            AddMenuItemToOrder(id, name, price);
        }

        private void BtnCalculate_Click(object sender, EventArgs e)
        {
            decimal total = 0m;
            foreach (DataRow r in orderTable.Rows)
            {
                int q = r.Field<int>("Quantity");
                decimal p = r.Field<decimal>("Price");
                total += q * p;
            }
            textBox2.Text = total.ToString("N0");
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            try
            {
                var dt = dataGridView2.DataSource as DataTable ?? (dataGridView2.DataSource as DataView)?.ToTable();
                if (dt == null || dt.Rows.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu để xuất.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
                    sfd.FileName = $"Menu_{DateTime.Now:yyyyMMdd_HHmmss}.csv";
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

        private void BtnCategory_Click(object sender, EventArgs e)
        {
            if (menuTable == null) return;
            var cats = menuTable.AsEnumerable().Select(r => r.Field<string>("CategoryName")).Distinct().ToList();
            if (cats.Count == 0) return;
            string sel = cats.First();
            var qry = menuTable.AsEnumerable().Where(r => r.Field<string>("CategoryName") == sel);
            dataGridView2.DataSource = qry.Any() ? qry.CopyToDataTable() : menuTable.Clone();
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            orderTable.Rows.Clear();
            dataGridView1.DataSource = orderTable.Copy();
            textBox2.Text = string.Empty;
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

        private void groupBox3_Enter(object sender, EventArgs e)
        {
        }
    }
}
