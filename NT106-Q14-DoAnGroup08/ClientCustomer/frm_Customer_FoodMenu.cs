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
    public partial class frm_Customer_FoodMenu : Form
    {
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
            lbl_Total.Text = "0";
        }
    }
}
