using NewNet_Manager.ConnectionServser;
using Newtonsoft.Json;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace NewNet_Manager.Uc_Staff
{
    public partial class uc_Staff_Menu : UserControl
    {
        private DataTable menuTable;
        private DataTable orderTable;
        private string currentInvoiceStatus = "NONE";

        public uc_Staff_Menu()
        {
            InitializeComponent();
            this.Load += Uc_Staff_Menu_Load;
        }

        private void Uc_Staff_Menu_Load(object sender, EventArgs e)
        {
            InitializeOrderTable();
            LoadMenu();
            LoadCategories();
            LoadCustomers();
            LoadInvoices();

            textBox1.TextChanged += (s, ev) => ApplyFilter();
            button1.Click += BtnAddSelectedItem_Click;
            button2.Click += BtnCategory_Click;
            dataGridView2.CellDoubleClick += DataGridView2_CellDoubleClick;

            if (comboBox1 != null)
            {
                comboBox1.SelectedIndexChanged += (s, ev) => LoadInvoices();
            }

            if (comboBox2 != null)
            {
                comboBox2.SelectedIndexChanged += comboBox2_SelectedIndexChanged;
            }
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
                    menuTable.Columns.Add("CategoryId", typeof(string));
                    menuTable.Columns.Add("CategoryName", typeof(string));
                    menuTable.Columns.Add("Price", typeof(decimal));

                    DataTable dt = null;
                    try { dt = response.data.ToObject<DataTable>(); } catch { }

                    if (dt != null)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            menuTable.Rows.Add(
                                row.Table.Columns.Contains("FoodId") ? Convert.ToString(row["FoodId"]) : string.Empty,
                                row.Table.Columns.Contains("FoodName") ? Convert.ToString(row["FoodName"]) : string.Empty,
                                row.Table.Columns.Contains("CategoryId") ? Convert.ToString(row["CategoryId"]) : string.Empty,
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
                            string catId = item.CategoryId != null ? (string)item.CategoryId : string.Empty;
                            string catName = item.CategoryName != null ? (string)item.CategoryName : string.Empty;
                            decimal price = item.Price != null ? (decimal)item.Price : 0m;
                            menuTable.Rows.Add(id, name, catId, catName, price);
                        }
                    }
                    dataGridView2.DataSource = menuTable.DefaultView.ToTable(false, "FoodId", "FoodName", "CategoryName", "Price");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối: " + ex.Message);
            }
        }

        private void LoadCategories()
        {
            try
            {
                var request = new { action = "get_all_categories" };
                string jsonResponse = ServerConnection.SendRequest(JsonConvert.SerializeObject(request));
                dynamic response = JsonConvert.DeserializeObject(jsonResponse);
                if (response.status == "success")
                {
                    groupBox8.Controls.Clear();
                    int yOffset = 15;
                    foreach (var category in response.data)
                    {
                        string categoryName = category.CategoryName.ToString();
                        string categoryId = category.CategoryId.ToString();

                        Button button = new Button
                        {
                            Text = categoryName,
                            Width = groupBox8.Width - 20,
                            Height = 30,
                            Location = new System.Drawing.Point(10, yOffset),
                        };
                        button.Click += (s, e) => FilterMenuByCategory(categoryId);
                        groupBox8.Controls.Add(button);
                        yOffset += 35;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh mục: " + ex.Message);
            }
        }

        private void LoadCustomers()
        {
            try
            {
                var request = new { action = "GET_ALL_CUSTOMERS" };
                string jsonResponse = ServerConnection.SendRequest(JsonConvert.SerializeObject(request));
                dynamic response = JsonConvert.DeserializeObject(jsonResponse);

                if (response.status == "success")
                {
                    DataTable customerTable = response.data.ToObject<DataTable>();
                    comboBox1.DataSource = customerTable;
                    comboBox1.DisplayMember = "Họ tên";
                    comboBox1.ValueMember = "CustomerID";

                    if (customerTable.Rows.Count > 0) comboBox1.SelectedIndex = 0;
                    else comboBox1.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khách hàng: " + ex.Message);
            }
        }

        private void LoadInvoices()
        {
            try
            {
                string customerId = comboBox1.SelectedValue?.ToString();
                if (string.IsNullOrEmpty(customerId))
                {
                    comboBox2.DataSource = null;
                    return;
                }

                var request = new { action = "GET_INVOICES_BY_CUSTOMER", data = new { customerId } };
                string jsonResponse = ServerConnection.SendRequest(JsonConvert.SerializeObject(request));
                dynamic response = JsonConvert.DeserializeObject(jsonResponse);

                if (response.status == "success")
                {
                    DataTable invoiceTable = response.data.ToObject<DataTable>();

                    if (invoiceTable != null && invoiceTable.Rows.Count > 0)
                    {
                        comboBox2.DataSource = invoiceTable;
                        comboBox2.DisplayMember = "InvoiceId";
                        comboBox2.ValueMember = "InvoiceId";
                        comboBox2.SelectedIndex = -1;
                        currentInvoiceStatus = "NONE";
                    }
                    else
                    {
                        comboBox2.DataSource = null;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải hóa đơn: " + ex.Message);
            }
        }


        private void FilterMenuByCategory(string categoryId)
        {
            if (menuTable == null) return;
            var filteredMenu = menuTable.AsEnumerable().Where(row => row.Field<string>("CategoryId") == categoryId);
            DataTable dt = filteredMenu.Any() ? filteredMenu.CopyToDataTable() : menuTable.Clone();
            dataGridView2.DataSource = dt;
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

        private bool IsInvoiceEditable()
        {
            if (currentInvoiceStatus == "COMPLETED" || currentInvoiceStatus == "PAID")
            {
                MessageBox.Show("Hóa đơn này đã ở trạng thái COMPLETED hoặc PAID, không thể chỉnh sửa thêm.");
                return false;
            }
            return true;
        }

        private void AddMenuItemToOrder(string foodId, string foodName, decimal price)
        {
            if (!IsInvoiceEditable()) return;

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
                row["Total"] = price;
                orderTable.Rows.Add(row);
            }
            dataGridView1.DataSource = orderTable.Copy();
            UpdateTotalAmount();
        }

        private void UpdateTotalAmount()
        {
            decimal total = 0;
            foreach (DataRow row in orderTable.Rows)
            {
                total += Convert.ToDecimal(row["Total"]);
            }
            textBox2.Text = total.ToString("N0");
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
            if (dataGridView2.CurrentRow == null) return;
            var row = (dataGridView2.CurrentRow.DataBoundItem as DataRowView).Row;
            string id = row["FoodId"].ToString();
            string name = row["FoodName"].ToString();
            decimal price = row["Price"] != DBNull.Value ? Convert.ToDecimal(row["Price"]) : 0m;
            AddMenuItemToOrder(id, name, price);
        }

        private void BtnCategory_Click(object sender, EventArgs e)
        {
            if (menuTable != null) dataGridView2.DataSource = menuTable.Copy();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string invoiceId = comboBox2.SelectedValue?.ToString();
            if (string.IsNullOrEmpty(invoiceId) || comboBox2.SelectedIndex == -1)
            {
                orderTable.Rows.Clear();
                dataGridView1.DataSource = orderTable.Copy();
                currentInvoiceStatus = "NONE";
                textBox2.Text = "0";
                return;
            }

            try
            {
                var request = new { action = "GET_INVOICE_DETAILS", data = new { invoiceId } };
                string jsonResponse = ServerConnection.SendRequest(JsonConvert.SerializeObject(request));
                dynamic response = JsonConvert.DeserializeObject(jsonResponse);

                if (response.status == "success")
                {
                    currentInvoiceStatus = response.invoiceStatus != null ? response.invoiceStatus.ToString() : "PENDING";
                    DataTable invoiceDetails = response.data.ToObject<DataTable>();
                    orderTable.Rows.Clear();

                    foreach (DataRow row in invoiceDetails.Rows)
                    {
                        var newRow = orderTable.NewRow();
                        newRow["FoodId"] = row["FoodId"];
                        newRow["FoodName"] = row["FoodName"];
                        newRow["Quantity"] = row["Quantity"];
                        newRow["Price"] = row["Price"];
                        newRow["Total"] = row["Total"];
                        orderTable.Rows.Add(newRow);
                    }

                    dataGridView1.DataSource = orderTable.Copy();
                    UpdateTotalAmount();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi chi tiết: " + ex.Message);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (!IsInvoiceEditable()) return;
            if (orderTable.Rows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn món ăn trước khi xác nhận.");
                return;
            }

            try
            {
                string customerId = comboBox1.SelectedValue?.ToString();
                string invoiceId = comboBox2.SelectedValue?.ToString();

                if (string.IsNullOrEmpty(invoiceId) || comboBox2.SelectedIndex == -1)
                {
                    var maxInv = ServerConnection.SendRequest(JsonConvert.SerializeObject(new { action = "get_max_invoice_id" }));
                    dynamic maxInvResponse = JsonConvert.DeserializeObject(maxInv);
                    string lastId = maxInvResponse?.maxId?.ToString();
                    invoiceId = string.IsNullOrEmpty(lastId) ? "HD001" : "HD" + (int.Parse(lastId.Substring(2)) + 1).ToString("D3");

                    var createInvoiceRequest = new
                    {
                        action = "create_invoice",
                        data = new { invoiceId, customerId, totalAmount = 0, status = "PENDING" }
                    };
                    ServerConnection.SendRequest(JsonConvert.SerializeObject(createInvoiceRequest));
                }

                foreach (DataRow row in orderTable.Rows)
                {
                    string foodId = row["FoodId"].ToString();
                    string foodName = row["FoodName"].ToString();
                    int qty = Convert.ToInt32(row["Quantity"]);
                    decimal price = Convert.ToDecimal(row["Price"]);
                    string note = "";

                    var maxDetailJson = ServerConnection.SendRequest(JsonConvert.SerializeObject(new { action = "get_max_invoice_detail_id" }));
                    dynamic maxDetail = JsonConvert.DeserializeObject(maxDetailJson);

                    string detailId;
                    if (maxDetail?.maxId == null || maxDetail.maxId == "")
                        detailId = "CTHD001";
                    else
                    {
                        int num = int.Parse(maxDetail.maxId.ToString().Substring(4)) + 1;
                        detailId = "CTHD" + num.ToString("D3");
                    }

                    string serviceId = "1";
                    ServerConnection.SendRequest(JsonConvert.SerializeObject(new
                    {
                        action = "create_invoice_detail",
                        data = new
                        {
                            detailId,
                            invoiceId,
                            serviceId,
                            foodId,
                            quantity = qty,
                            price,
                            note
                        }
                    }));
                }

                MessageBox.Show("Đã lưu chi tiết hóa đơn thành công.");
                LoadInvoices();
                comboBox2.SelectedValue = invoiceId;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xác nhận: " + ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                string invoiceId = comboBox2.SelectedValue?.ToString();
                if (string.IsNullOrEmpty(invoiceId))
                {
                    MessageBox.Show("Vui lòng chọn hóa đơn.");
                    return;
                }

                decimal currentTotal = 0;
                decimal.TryParse(textBox2.Text.Replace(",", ""), out currentTotal);

                var updateRequest = new
                {
                    action = "update_invoice_status",
                    data = new
                    {
                        invoiceId = invoiceId,
                        status = "COMPLETED",
                        totalAmount = currentTotal
                    }
                };

                string jsonResponse = ServerConnection.SendRequest(JsonConvert.SerializeObject(updateRequest));
                dynamic response = JsonConvert.DeserializeObject(jsonResponse);

                if (response != null && response.status == "success")
                {
                    MessageBox.Show("Hóa đơn đã được chuyển sang trạng thái COMPLETED.");
                    LoadInvoices();
                }
                else
                {
                    MessageBox.Show("Thất bại: " + response?.message);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi cập nhật: " + ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex == -1)
            {
                orderTable.Rows.Clear();
                dataGridView1.DataSource = orderTable.Copy();
                textBox2.Text = "0";
            }
            else
            {
                MessageBox.Show("Hóa đơn đã được lưu trên hệ thống, không thể xóa nhanh danh sách.");
            }
        }

        private void BtnCreateInvoice_Click(object sender, EventArgs e)
        {
            comboBox2.SelectedIndex = -1;
        }
    }
}