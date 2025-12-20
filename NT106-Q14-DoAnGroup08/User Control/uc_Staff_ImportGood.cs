using NT106_Q14_DoAnGroup08.ConnectionServser;
using Newtonsoft.Json;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace NT106_Q14_DoAnGroup08.Uc_Staff
{
    public partial class uc_Staff_ImportGood : UserControl
    {
        private DataTable importTable;

        public uc_Staff_ImportGood()
        {
            InitializeComponent();
            this.Load += Uc_Staff_ImportGood_Load;
        }

        private void Uc_Staff_ImportGood_Load(object sender, EventArgs e)
        {
            LoadImportGoods();
            textBox3.TextChanged += (s, ev) => ApplyFilter();
            button3.Click += BtnRefresh_Click;
            button2.Click += BtnAddImport_Click;
            button1.Click += BtnConfirm_Click;
            dataGridView1.KeyDown += DataGridView1_KeyDown;
        }

        private void LoadImportGoods()
        {
            try
            {
                var request = new { action = "GET_IMPORT_GOODS" };
                string jsonResponse = ServerConnection.SendRequest(JsonConvert.SerializeObject(request));
                dynamic response = JsonConvert.DeserializeObject(jsonResponse);
                if (response.status == "success")
                {
                    importTable = new DataTable();
                    importTable.Columns.Add("ImportId", typeof(string));
                    importTable.Columns.Add("ImportDate", typeof(DateTime));
                    importTable.Columns.Add("ItemName", typeof(string));
                    importTable.Columns.Add("Quantity", typeof(int));
                    importTable.Columns.Add("Supplier", typeof(string));

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
                            importTable.Rows.Add(
                                row.Table.Columns.Contains("ImportId") ? Convert.ToString(row["ImportId"]) : string.Empty,
                                row.Table.Columns.Contains("ImportDate") && row["ImportDate"] != DBNull.Value ? Convert.ToDateTime(row["ImportDate"]) : DateTime.MinValue,
                                row.Table.Columns.Contains("ItemName") ? Convert.ToString(row["ItemName"]) : string.Empty,
                                row.Table.Columns.Contains("Quantity") && row["Quantity"] != DBNull.Value ? Convert.ToInt32(row["Quantity"]) : 0,
                                row.Table.Columns.Contains("Supplier") ? Convert.ToString(row["Supplier"]) : string.Empty
                            );
                        }
                    }
                    else
                    {
                        foreach (var item in response.data)
                        {
                            string id = item.ImportId != null ? (string)item.ImportId : string.Empty;
                            DateTime dtim = item.ImportDate != null ? (DateTime)item.ImportDate : DateTime.MinValue;
                            string name = item.ItemName != null ? (string)item.ItemName : string.Empty;
                            int qty = item.Quantity != null ? (int)item.Quantity : 0;
                            string sup = item.Supplier != null ? (string)item.Supplier : string.Empty;
                            importTable.Rows.Add(id, dtim, name, qty, sup);
                        }
                    }

                    dataGridView1.DataSource = importTable.Copy();
                }
                else
                {
                    MessageBox.Show("Lỗi tải nhập hàng: " + response.message);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối: " + ex.Message);
            }
        }

        private void ApplyFilter()
        {
            if (importTable == null) return;
            string tf = textBox3.Text?.Trim();
            var query = importTable.AsEnumerable();
            if (!string.IsNullOrEmpty(tf))
            {
                string up = tf.ToUpperInvariant();
                query = query.Where(r => ((r.Field<string>("ImportId") ?? string.Empty).ToUpperInvariant().Contains(up)
                    || (r.Field<string>("ItemName") ?? string.Empty).ToUpperInvariant().Contains(up)
                    || (r.Field<string>("Supplier") ?? string.Empty).ToUpperInvariant().Contains(up)));
            }
            DataTable dt = query.Any() ? query.CopyToDataTable() : importTable.Clone();
            dataGridView1.DataSource = dt;
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            LoadImportGoods();
            textBox3.Text = string.Empty;
        }

        private void BtnAddImport_Click(object sender, EventArgs e)
        {
            try
            {
                string importId = textBox4.Text.Trim();
                if (string.IsNullOrEmpty(importId)) importId = null;
                string itemName = textBox5.Text.Trim();
                if (string.IsNullOrEmpty(itemName))
                {
                    MessageBox.Show("Vui lòng nhập tên hàng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                int qty = 0;
                if (!int.TryParse(textBox7.Text.Trim(), out qty)) qty = 0;
                string supplier = comboBox1.SelectedItem != null ? comboBox1.SelectedItem.ToString() : string.Empty;

                var req = new
                {
                    action = "ADD_IMPORT_GOOD",
                    data = new
                    {
                        ImportId = importId,
                        ImportDate = DateTime.Now,
                        ItemName = itemName,
                        Quantity = qty,
                        Supplier = supplier
                    }
                };

                string res = ServerConnection.SendRequest(JsonConvert.SerializeObject(req));
                if (string.IsNullOrWhiteSpace(res))
                {
                    MessageBox.Show("Không nhận được phản hồi từ server.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                dynamic obj = JsonConvert.DeserializeObject(res);
                if (obj.status == "success")
                {
                    MessageBox.Show("Thêm nhập hàng thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadImportGoods();
                }
                else
                {
                    MessageBox.Show((string)(obj.message ?? "Thêm thất bại"), "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            try
            {
                var dt = dataGridView1.DataSource as DataTable ?? (dataGridView1.DataSource as DataView)?.ToTable();
                if (dt == null || dt.Rows.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu để xuất.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
                    sfd.FileName = $"ImportGoods_{DateTime.Now:yyyyMMdd_HHmmss}.csv";
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

        private void BtnConfirm_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn bản ghi để xác nhận.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            MessageBox.Show("Xác nhận nhận hàng thành công (tạm thời).", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void DataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && dataGridView1.CurrentRow != null)
            {
                var drv = dataGridView1.CurrentRow.DataBoundItem as DataRowView;
                if (drv != null)
                {
                    string id = drv.Row.Field<string>("ImportId");
                    if (MessageBox.Show($"Xóa nhập hàng {id}?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        var req = new { action = "DELETE_IMPORT_GOOD", data = new { ImportId = id } };
                        string res = ServerConnection.SendRequest(JsonConvert.SerializeObject(req));
                        dynamic obj = JsonConvert.DeserializeObject(res);
                        if (obj.status == "success")
                        {
                            MessageBox.Show("Xóa thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadImportGoods();
                        }
                        else
                        {
                            MessageBox.Show("Xóa thất bại: " + (string)(obj.message ?? ""), "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
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

        private void label6_Click(object sender, EventArgs e)
        {
        }
    }
}
