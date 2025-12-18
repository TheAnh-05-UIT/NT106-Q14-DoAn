using Newtonsoft.Json;
using NT106_Q14_DoAnGroup08.ConnectionServser;
using System;
using System.Collections.Generic;
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
            LoadCategories();
            LoadInvoices(); // Load invoices on menu load
            textBox1.TextChanged += (s, ev) => ApplyFilter();
            button3.Click += BtnExport_Click;
            button4.Click += BtnCalculate_Click;
            button1.Click += BtnAddSelectedItem_Click;
            button2.Click += BtnCategory_Click;
            dataGridView2.CellDoubleClick += DataGridView2_CellDoubleClick;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            LoadCustomers();
            LoadInvoices();
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
                    menuTable.Columns.Add("CategoryId", typeof(string)); // Add CategoryId for filtering
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
                                row.Table.Columns.Contains("CategoryId") ? Convert.ToString(row["CategoryId"]) : string.Empty, // Include CategoryId
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
                            string catId = item.CategoryId != null ? (string)item.CategoryId : string.Empty; // Include CategoryId
                            string catName = item.CategoryName != null ? (string)item.CategoryName : string.Empty;
                            decimal price = item.Price != null ? (decimal)item.Price : 0m;
                            menuTable.Rows.Add(id, name, catId, catName, price);
                        }
                    }

                    dataGridView2.DataSource = menuTable.DefaultView.ToTable(false, "FoodId", "FoodName", "CategoryName", "Price"); // Exclude CategoryId from view
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

                    int yOffset = 10;
                    foreach (var category in response.data)
                    {
                        string categoryName = category.CategoryName.ToString();


                        Button button = new Button
                        {
                            Text = categoryName,
                            AutoSize = true,
                            Location = new System.Drawing.Point(groupBox8.Location.X, yOffset),
                        };
                        button.Click += (s, e) => FilterMenuByCategory(category.CategoryId.ToString());
                        groupBox8.Controls.Add(button);
                        yOffset += 35;
                    }
                }
                else
                {
                    MessageBox.Show("Lỗi tải danh mục: " + response.message);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối: " + ex.Message);
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
                    comboBox1.DisplayMember = "Họ tên"; // Assuming "Họ tên" is the column for customer names
                    comboBox1.ValueMember = "CustomerID"; // Assuming "CustomerID" is the unique identifier

                    if (customerTable.Rows.Count > 0)
                    {
                        comboBox1.SelectedIndex = 0; // Set default to the first item if available
                    }
                    else
                    {
                        comboBox1.SelectedIndex = -1; // Set default to -1 if no items are available
                    }
                }
                else
                {
                    MessageBox.Show("Lỗi tải danh sách khách hàng: " + response.message);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối: " + ex.Message);
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

                    // Filter invoices to include only those with ServiceId = 1 in InvoiceDetails
                    var filteredInvoices = invoiceTable.AsEnumerable()
                        .Where(row =>
                        {
                            string invoiceId = row.Field<string>("InvoiceId");
                            var detailRequest = new { action = "GET_INVOICE_DETAILS", data = new { invoiceId } };
                            string detailResponse = ServerConnection.SendRequest(JsonConvert.SerializeObject(detailRequest));
                            dynamic detailData = JsonConvert.DeserializeObject(detailResponse);

                            if (detailData.status == "success")
                            {
                                DataTable detailsTable = detailData.data.ToObject<DataTable>();
                                return detailsTable.AsEnumerable().Any(detailRow => detailRow.Field<int>("ServiceId") == 1);
                            }

                            return false;
                        });

                    if (filteredInvoices.Any())
                    {
                        DataTable filteredTable = filteredInvoices.CopyToDataTable();
                        comboBox2.DataSource = filteredTable;
                        comboBox2.DisplayMember = "InvoiceId"; // Only show InvoiceId
                        comboBox2.ValueMember = "InvoiceId";
                        comboBox2.SelectedIndex = -1; // Set default to -1
                    }
                    else
                    {
                        comboBox2.DataSource = null;
                    }
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

        private void FilterMenuByCategory(string categoryId)
        {
            if (menuTable == null) return;

            var filteredMenu = menuTable.AsEnumerable()
                .Where(row => row.Field<string>("CategoryId") == categoryId);

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

        private void BtnCreateInvoice_Click(object sender, EventArgs e)
        {
            try
            {
                string customerId = comboBox1.SelectedValue?.ToString();
                if (string.IsNullOrEmpty(customerId))
                {
                    MessageBox.Show("Vui lòng chọn khách hàng trước khi tạo hóa đơn mới.");
                    return;
                }

                // Get the latest active session for the customer
                var sessionResponse = ServerConnection.SendRequest(JsonConvert.SerializeObject(new
                {
                    action = "get_latest_active_session",
                    data = new { customerId }
                }));

                dynamic sessionData = JsonConvert.DeserializeObject(sessionResponse);
                string sessionId = sessionData?.status == "success" ? sessionData.sessionId?.ToString() : string.Empty;

                // Generate new invoice ID
                var maxInv = ServerConnection.SendRequest(JsonConvert.SerializeObject(new { action = "get_max_invoice_id" }));
                dynamic maxInvResponse = JsonConvert.DeserializeObject(maxInv);
                string lastId = maxInvResponse?.maxId?.ToString();

                string invoiceId;
                if (string.IsNullOrEmpty(lastId))
                    invoiceId = "HD001";
                else
                {
                    int num = int.Parse(lastId.Substring(2)) + 1;
                    invoiceId = "HD" + num.ToString("D3");
                }

                // Create the new invoice
                var createInvoiceResponse = ServerConnection.SendRequest(JsonConvert.SerializeObject(new
                {
                    action = "create_invoice",
                    data = new
                    {
                        invoiceId,
                        customerId,
                        sessionId,
                        totalAmount = 0 // Default total amount
                    }
                }));

                dynamic createResponse = JsonConvert.DeserializeObject(createInvoiceResponse);
                if (createResponse.status == "success")
                {
                    var maxDetail = ApiClient.Client.Send(new { action = "get_max_invoice_detail_id" });

                    string detailId;
                    if (maxDetail?.maxId == null || maxDetail.maxId == "")
                        detailId = "CTHD001";
                    else
                    {
                        int num = int.Parse(maxDetail.maxId.ToString().Substring(4)) + 1;
                        detailId = "CTHD" + num.ToString("D3");
                    }
                    // Add a default service with ServiceId = 1 and a valid FoodId
                    var addServiceResponse = ServerConnection.SendRequest(JsonConvert.SerializeObject(new
                    {
                        action = "create_invoice_detail",
                        data = new
                        {
                            detailId,
                            invoiceId,
                            serviceId = 1,
                        }
                    }));

                    dynamic serviceResponse = JsonConvert.DeserializeObject(addServiceResponse);
                    if (serviceResponse.status == "success")
                    {
                        MessageBox.Show("Hóa đơn mới đã được tạo thành công.");
                        LoadInvoices(); // Refresh the invoice list
                        comboBox2.SelectedValue = invoiceId; // Automatically select the new invoice
                    }
                    else
                    {
                        MessageBox.Show("Tạo hóa đơn thất bại khi thêm dịch vụ: " + serviceResponse.message);
                    }
                }
                else
                {
                    MessageBox.Show("Tạo hóa đơn thất bại: " + createResponse.message);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tạo hóa đơn mới: " + ex.Message);
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string invoiceId = comboBox2.SelectedValue?.ToString();
            if (string.IsNullOrEmpty(invoiceId) || invoiceId == "-1")
            {
                orderTable.Rows.Clear();
                dataGridView1.DataSource = orderTable.Copy();
                return;
            }

            try
            {
                var request = new { action = "GET_INVOICE_DETAILS", data = new { invoiceId } };
                string jsonResponse = ServerConnection.SendRequest(JsonConvert.SerializeObject(request));
                dynamic response = JsonConvert.DeserializeObject(jsonResponse);

                if (response.status == "success")
                {
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
                }
                else
                {
                    MessageBox.Show("Lỗi tải chi tiết hóa đơn: " + response.message);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối: " + ex.Message);
            }
        }
    }
}
