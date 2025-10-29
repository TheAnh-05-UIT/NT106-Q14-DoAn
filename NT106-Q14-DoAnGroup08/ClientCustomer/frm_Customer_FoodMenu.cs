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
            MenuDAO.Instance.AddCategory(CategoryPanel1);

            ProductPanel.Controls.Clear();
            LoadProduct();
        }

        private void AddItems(string id, string name, string cat, string price, Image image)
        {
            var w = new uc_Product
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
                var wdg = (uc_Product)ss;
                int foodId = wdg.Id;
                double itemPrice = double.Parse(wdg.FoodPrice); // Đảm bảo FoodPrice là chuỗi số hợp lệ
                bool found = false;

                // 1. Duyệt qua các hàng để TÌM và CẬP NHẬT nếu món đã có
                foreach (DataGridViewRow item in guna2DataGridView1.Rows)
                {
                    // Kiểm tra cột dgvid của DataGridView (chắc chắn cột này tồn tại)
                    if (item.Cells["dgvid"].Value != null && Convert.ToInt32(item.Cells["dgvid"].Value) == foodId)
                    {
                        // Cập nhật số lượng
                        int currentQty = Convert.ToInt32(item.Cells["dgvQty"].Value);
                        currentQty++;
                        item.Cells["dgvQty"].Value = currentQty;

                        // Cập nhật tổng tiền hàng
                        item.Cells["dgvAmount"].Value = currentQty * itemPrice;

                        found = true;
                        break; // Thoát khỏi vòng lặp sau khi tìm và cập nhật
                    }
                }

                // 2. Nếu không tìm thấy, THÊM món mới vào DataGridView
                if (!found)
                {
                    // Thêm món mới với số lượng là 1 và tổng tiền bằng giá
                    guna2DataGridView1.Rows.Add(new object[] { 0, foodId, wdg.FoodName, 1, itemPrice, itemPrice });
                }
            };
        }

        private void LoadProduct()
        {

            string query = "SELECT f.*, c.CategoryName FROM FoodAndDrink f INNER JOIN Category c ON f.CategoryId = c.CategoryId";
            DataTable dt = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in dt.Rows)
            {
                Image foodImage = null;

                // ✅ Giả sử cột Image trong DB là NVARCHAR, lưu kiểu: "Images/burger.png"
                string imagePath = item["Image"].ToString();

                // Nếu cột ảnh rỗng hoặc file không tồn tại → dùng ảnh mặc định
                string fullPath = string.Empty;

                // Nếu cột ảnh rỗng hoặc file không tồn tại → dùng ảnh mặc định
                if (string.IsNullOrEmpty(imagePath))
                {
                    foodImage = Properties.Resources.defaultImage;
                }
                else
                {
                    // Now calculate the fullPath inside the 'else' block
                    fullPath = Path.Combine(Application.StartupPath, imagePath); // fullPath is set here

                    if (File.Exists(fullPath))
                    {
                        // **Apply the robust, non-locking file loading code here**
                        try
                        {
                            // Use MemoryStream to load the image data and immediately release the file lock
                            using (var stream = new MemoryStream(File.ReadAllBytes(fullPath)))
                            {
                                Image tempImage = Image.FromStream(stream);
                                foodImage = new Bitmap(tempImage); // Create a copy
                            }
                        }
                        catch
                        {
                            foodImage = Properties.Resources.defaultImage; // fallback on read error
                        }
                    }
                    else
                    {
                        foodImage = Properties.Resources.defaultImage; // fallback if file doesn't exist
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
    }
}
