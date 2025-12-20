using Newtonsoft.Json.Linq;
using NT106_Q14_DoAnGroup08.Uc_Staff;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace NT106_Q14_DoAnGroup08.ClientCustomer
{
    public partial class frm_Customer_FoodMenu : CustomForm
    {
        
        private Dictionary<string, string> currentNotes = new Dictionary<string, string>();

        string _cusId;

        public frm_Customer_FoodMenu(string customerId)
        {
            InitializeComponent();
            _cusId = customerId;
        }

        private void frm_Customer_FoodMenu_Load(object sender, EventArgs e)
        {
            guna2DataGridView1.BorderStyle = BorderStyle.FixedSingle;
            AddCategory();
            LoadProduct();
        }
        public void AddCategory()
        {
            var res = ApiClient.Client.Send(new { action = "get_all_categories" });

            if (res == null || res.status != "success")
            {
                MessageBox.Show("Không tải được Category!");
                return;
            }

            JArray arr = (JArray)res.data;
            CategoryPanel.Controls.Clear();

            foreach (var row in arr)
            {
                var btn = new Guna.UI2.WinForms.Guna2Button();
                btn.FillColor = Color.FromArgb(0, 32, 63);
                btn.Size = new Size(120, 50);
                btn.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
                btn.Text = row["CategoryName"].ToString();
                btn.Click += b_Click;
                CategoryPanel.Controls.Add(btn);
            }
        }

        private void b_Click(object sender, EventArgs e)
        {
            var btn = sender as Guna.UI2.WinForms.Guna2Button;

            foreach (uc_Product pro in ProductPanel.Controls)
            {
                pro.Visible = pro.FoodCategory
                    .ToLower()
                    .Contains(btn.Text.Trim().ToLower());
            }
        }
        private void LoadProduct()
        {
            var res = ApiClient.Client.Send(new { action = "get_all_food" });
            if (res == null || res.status != "success")
            {
                MessageBox.Show("Không tải được danh sách món ăn!");
                return;
            }

            ProductPanel.Controls.Clear();
            JArray arr = (JArray)res.data;

            foreach (var item in arr)
            {
                string imgName = item["Image"]?.ToString();
                Image foodImg;


                if (!string.IsNullOrEmpty(imgName))
                {
                    object imgres = Properties.Resources.ResourceManager.GetObject(imgName);
                    foodImg = imgres != null ? (Image)imgres : Properties.Resources.defaultImage;
                }
                else
                {
                    foodImg = Properties.Resources.defaultImage;
                }


                AddItems(
                    item["FoodId"].ToString(),
                    item["FoodName"].ToString(),
                    item["CategoryName"].ToString(),
                    item["Price"].ToString(),
                    foodImg
                );
            }
        }

        private void AddItems(string id, string name, string cat, string price, Image image)
        {
            var pro = new uc_Product
            {
                FoodName = name,
                FoodPrice = price,
                FoodCategory = cat,
                FoodImage = image,
                Id = int.Parse(id)
            };

            ProductPanel.Controls.Add(pro);

            pro.onselect += (ss, ee) =>
            {
                int foodId = pro.Id;
                double itemPrice = double.Parse(price);
                bool found = false;
               
                foreach (DataGridViewRow row in guna2DataGridView1.Rows)
                {
                    if (row.Cells["dgvid"].Value != null &&
                        Convert.ToInt32(row.Cells["dgvid"].Value) == foodId)
                    {
                        int qty = Convert.ToInt32(row.Cells["dgvQty"].Value) + 1;
                        row.Cells["dgvQty"].Value = qty;
                        row.Cells["dgvAmount"].Value = qty * itemPrice;
                        
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    int sno = guna2DataGridView1.Rows.Count - 1;
                    guna2DataGridView1.Rows.Add(new object[]
                    {
                        sno + 1, foodId, name, 1, itemPrice, itemPrice
                    });
                }

                GetTotal();
            };
        }

        private void txt_Search_TextChanged(object sender, EventArgs e)
        {
            string keyword = txt_Search.Text.Trim().ToLower();

            foreach (uc_Product pro in ProductPanel.Controls)
            {
                pro.Visible = string.IsNullOrEmpty(keyword)
                    || pro.FoodName.ToLower().Contains(keyword);
            }
        }
        private void GetTotal()
        {
            double total = 0;

            foreach (DataGridViewRow row in guna2DataGridView1.Rows)
            {
                if (row.Cells["dgvAmount"].Value != null &&
                    double.TryParse(row.Cells["dgvAmount"].Value.ToString(), out double t))
                    total += t;
            }

            lbl_Total.Text = total.ToString("N2");
        }
        private void btn_Note_Click(object sender, EventArgs e)
        {
            if (guna2DataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn món cần ghi chú.");
                return;
            }

            string foodName = guna2DataGridView1.SelectedRows[0].Cells["dgvName"].Value.ToString();

            frm_Customer_Note note = new frm_Customer_Note(foodName);
            if (note.ShowDialog() == DialogResult.OK)
                currentNotes[foodName] = note.NoteText;
        }

        private void btn_Order_Click(object sender, EventArgs e)
        {
            if (guna2DataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("Chưa có món nào để đặt!");
                return;
            }


            var maxInv = ApiClient.Client.Send(new { action = "get_max_invoice_id" });
            string lastId = maxInv?["maxId"]?.ToString(); 

            string invoiceId;

            if (string.IsNullOrEmpty(lastId))
                invoiceId = "HD001";
            else
            {
                int num = int.Parse(lastId.Substring(2)) + 1;
                invoiceId = "HD" + num.ToString("D3");
            }


            string customerId = _cusId;

            decimal totalAmount = 0;
            foreach (DataGridViewRow row in guna2DataGridView1.Rows)
            {
                if (row.Cells["dgvAmount"].Value != null)
                    totalAmount += Convert.ToDecimal(row.Cells["dgvAmount"].Value);
            }

            
            var invoiceRes = ApiClient.Client.Send(new
            {
                action = "create_invoice",
                data = new
                {
                    invoiceId,
                    customerId,
                    totalAmount
                }
            });

            if (invoiceRes == null || invoiceRes.status != "success")
            {
                MessageBox.Show("Tạo hóa đơn thất bại!");
                return;
            }

           
            foreach (DataGridViewRow row in guna2DataGridView1.Rows)
            {
                if (row.IsNewRow) continue;
                if (row.Cells["dgvid"].Value == null) continue;
                string foodId = row.Cells["dgvid"].Value.ToString();
                int qty = Convert.ToInt32(row.Cells["dgvQty"].Value);
                decimal price = Convert.ToDecimal(row.Cells["dgvAmount"].Value);
                string foodName = row.Cells["dgvName"].Value.ToString();
                string note = currentNotes.ContainsKey(foodName) ? currentNotes[foodName] : "";

                var maxDetail = ApiClient.Client.Send(new { action = "get_max_invoice_detail_id" });

                string detailId;
                if (maxDetail?.maxId == null || maxDetail.maxId == "")
                    detailId = "CTHD001";
                else
                {
                    int num = int.Parse(maxDetail.maxId.ToString().Substring(4)) + 1;
                    detailId = "CTHD" + num.ToString("D3");
                }
                string serviceId = "1";
                ApiClient.Client.Send(new
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
                });
            }

            MessageBox.Show("Đặt món thành công!");

            guna2DataGridView1.Rows.Clear();
            lbl_Total.Text = "";
            currentNotes.Clear();
        }

        private void btn_New_Click(object sender, EventArgs e)
        {
            guna2DataGridView1.Rows.Clear();
            lbl_Total.Text = "";
            currentNotes.Clear();
        }

        private void btn_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_Bill_Click(object sender, EventArgs e)
        {
            frm_Customer_BillList f = new frm_Customer_BillList();
            f.ShowDialog();
        }
    }
}
