using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NT106_Q14_DoAnGroup08.DAO;
using NT106_Q14_DoAnGroup08.Uc_Staff;
using QuanLyQuanNet.DAO;

namespace NT106_Q14_DoAnGroup08.ClientCustomer
{
    public partial class frm_Customer_FoodMenu : CustomForm
    {
        private Dictionary<string, string> currentNotes = new Dictionary<string, string>();

        public frm_Customer_FoodMenu()
        {
            InitializeComponent();
        }

        private void btn_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frm_Customer_FoodMenu_Load(object sender, EventArgs e)
        {
            guna2DataGridView1.BorderStyle = BorderStyle.FixedSingle;
            AddCategory();

            ProductPanel.Controls.Clear();
            LoadProduct();
        }

        public void AddCategory()
        {
            string query = "Select * from Category";
            DataTable dt = DataProvider.Instance.ExecuteQuery(query);
            CategoryPanel.Controls.Clear();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    Guna.UI2.WinForms.Guna2Button b = new Guna.UI2.WinForms.Guna2Button();
                    b.FillColor = Color.FromArgb(0, 32, 63);
                    b.Size = new Size(120, 50);
                    b.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
                    b.Text = row["CategoryName"].ToString();

                    b.Click += new EventHandler(b_Click);

                    CategoryPanel.Controls.Add(b);
                }
            }
        }

        private void b_Click(object sender, EventArgs e)
        {
            Guna.UI2.WinForms.Guna2Button b = (Guna.UI2.WinForms.Guna2Button)sender;
            foreach (var item in ProductPanel.Controls)
            {
                var pro = (uc_Product)item;
                pro.Visible = pro.FoodCategory.ToLower().Contains(b.Text.Trim().ToLower());
            }
        }
        private void AddItems(string id, string name, string cat, string price, Image image)
        {
            var w = new Uc_Staff.uc_Product
            {
                FoodName = name,
                FoodPrice = price,
                FoodCategory = cat,
                FoodImage = image,
                Id = Convert.ToInt32(id)
            };

            ProductPanel.Controls.Add(w);

            w.onselect += (ss, ee) =>
            {
                var wdg = (Uc_Staff.uc_Product)ss;
                int foodId = wdg.Id;
                double itemPrice = double.Parse(wdg.FoodPrice);
                bool found = false;

                foreach (DataGridViewRow item in guna2DataGridView1.Rows)
                {
                    if (item.Cells["dgvid"].Value != null && Convert.ToInt32(item.Cells["dgvid"].Value) == foodId)
                    {
                        int currentQty = Convert.ToInt32(item.Cells["dgvQty"].Value);
                        currentQty++;
                        item.Cells["dgvQty"].Value = currentQty;

                        item.Cells["dgvAmount"].Value = currentQty * itemPrice;

                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    guna2DataGridView1.Rows.Add(new object[] { 0, foodId, wdg.FoodName, 1, itemPrice, itemPrice });
                }
                GetTotal();
            };
        }

        private void LoadProduct()
        {

            string query = "SELECT f.*, c.CategoryName FROM FoodAndDrink f INNER JOIN Category c ON f.CategoryId = c.CategoryId";
            DataTable dt = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in dt.Rows)
            {
                Image foodImage = null;

                string imagePath = item["Image"].ToString();

                string fullPath = string.Empty;

                if (string.IsNullOrEmpty(imagePath))
                {
                    foodImage = Properties.Resources.defaultImage;
                }
                else
                {
                    fullPath = Path.Combine(Application.StartupPath, imagePath);

                    if (File.Exists(fullPath))
                    {
                        try
                        {
                            using (var stream = new MemoryStream(File.ReadAllBytes(fullPath)))
                            {
                                Image tempImage = Image.FromStream(stream);
                                foodImage = new Bitmap(tempImage);
                            }
                        }
                        catch
                        {
                            foodImage = Properties.Resources.defaultImage;
                        }
                    }
                    else
                    {
                        foodImage = Properties.Resources.defaultImage; 
                    }

                    AddItems(
                    item["FoodId"].ToString(),
                    item["FoodName"].ToString(),
                    item["CategoryName"].ToString(),
                    item["Price"].ToString(),
                    foodImage
                    );
                }
            }
        }

        private void txt_Search_TextChanged(object sender, EventArgs e)
        {
            string keyword = txt_Search.Text.Trim().ToLower();

            foreach (Control ctrl in ProductPanel.Controls)
            {
                if (ctrl is uc_Product pro)
                {
                    string name = pro.FoodName?.ToLower() ?? string.Empty;
                    bool match = string.IsNullOrEmpty(keyword) || name.Contains(keyword);

                    pro.Visible = match;
                }
            }
        }

        private void GetTotal()
        {
            double total = 0;
            lbl_Total.Text = "";
            foreach(DataGridViewRow item in guna2DataGridView1.Rows)
            {
                var value = item.Cells["dgvAmount"].Value;
                if (value != null && double.TryParse(value.ToString(), out double amount))
                {
                    total += amount;
                }
            }
            lbl_Total.Text = total.ToString("N2");
        }

        private void btn_New_Click(object sender, EventArgs e)
        {
            guna2DataGridView1.Rows.Clear();
            lbl_Total.Text = "";
        }

        private void btn_Note_Click(object sender, EventArgs e)
        {
            if (guna2DataGridView1.SelectedRows.Count > 0)
            {
                string foodName = guna2DataGridView1.SelectedRows[0].Cells["dgvName"].Value.ToString();

                frm_Customer_Note note = new frm_Customer_Note(foodName);


                if (note.ShowDialog() == DialogResult.OK)
                {
                    string note1 = note.NoteText;

                    currentNotes[foodName] = note1;
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn món cần ghi chú trước.");
            }
        }

        private void btn_Order_Click(object sender, EventArgs e)
        {
            if (guna2DataGridView1.Rows.Count <= 0)
            {
                MessageBox.Show("Chưa có món nào để đặt.");
                return;
            }
            string queryMaxId = "SELECT MAX(InvoiceId) FROM Invoices WHERE InvoiceId LIKE 'HD%'";
            object result = DataProvider.Instance.ExecuteScalar(queryMaxId);

            string invoiceId;
            if (result == DBNull.Value || result == null)
                invoiceId = "HD001";
            else
            {
                string maxId = result.ToString();
                int num = int.Parse(maxId.Substring(2)) + 1;
                invoiceId = "HD" + num.ToString("D3");
            }
            string customerId = "KH001";
            decimal totalAmount = 0;

            foreach (DataGridViewRow row in guna2DataGridView1.Rows) 
            {
                if (row.Cells["dgvAmount"].Value != null) 
                {
                    totalAmount += Convert.ToDecimal(row.Cells["dgvAmount"].Value);
                }
            }
            string createInvoiceQuery = $@"
                INSERT INTO Invoices 
                (InvoiceId, CustomerId, TotalAmount) 
                VALUES 
                (N'{invoiceId}', N'{customerId}', {totalAmount})";

            int resultInvoice = DataProvider.Instance.ExecuteNonQuery(createInvoiceQuery);

            if (resultInvoice <= 0)
            {
                MessageBox.Show("Tạo hóa đơn thất bại!");
                return;
            }

            foreach (DataGridViewRow row in guna2DataGridView1.Rows)
            {
                if (row.Cells["dgvid"].Value == null) continue;

                string foodId = row.Cells["dgvid"].Value.ToString();
                int quantity = Convert.ToInt32(row.Cells["dgvQty"].Value);
                decimal price = Convert.ToDecimal(row.Cells["dgvAmount"].Value); // tổng tiền món
                string foodName = row.Cells["dgvName"].Value.ToString();
                string note = currentNotes.ContainsKey(foodName) ? currentNotes[foodName] : null;

                string queryMaxDetailId = "SELECT MAX(InvoiceDetailId) FROM InvoiceDetails WHERE InvoiceDetailId LIKE 'CTHD%'";
                object resultDetail = DataProvider.Instance.ExecuteScalar(queryMaxDetailId);

                string detailId;
                if (resultDetail == DBNull.Value || resultDetail == null)
                    detailId = "CTHD001";
                else
                {
                    string maxId = resultDetail.ToString(); // VD: "CTHD005"
                    int num = int.Parse(maxId.Substring(4)) + 1; // bỏ "CTHD"
                    detailId = "CTHD" + num.ToString("D3");       // luôn 3 chữ số
                }

                string insertDetailQuery = $@"
                    INSERT INTO InvoiceDetails
                    (InvoiceDetailId, InvoiceId, FoodId, Quantity, Price, Status, Note)
                    VALUES
                    (N'{detailId}', N'{invoiceId}', N'{foodId}', {quantity}, {price}, 'PENDING', N'{note}')";

                DataProvider.Instance.ExecuteNonQuery(insertDetailQuery);
            }

            MessageBox.Show("Đặt món thành công!");
          
            guna2DataGridView1.Rows.Clear();
            lbl_Total.Text = "";
            currentNotes.Clear();
        }
    }
}
