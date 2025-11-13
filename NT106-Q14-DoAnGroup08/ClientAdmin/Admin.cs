using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using NT106_Q14_DoAnGroup08.Uc_Staff;

namespace NT106_Q14_DoAnGroup08.ClientAdmin
{
    public partial class Admin : Form
    {
        public Admin()
        {
            InitializeComponent();
        }

        private void Admin_Load(object sender, EventArgs e)
        {
            OpenChildControl(new uc_Staff_Account());
        }
        //hàm thêm form vào
        private void openChildForm(Form childForm)
        {
            panelContainer.Controls.Clear();
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            //this.ClientSize = childForm.Size;
            //int a = childForm.Size.Width;
            //int b = childForm.Size.Height;
            childForm.Dock = DockStyle.Fill;
            panelContainer.Controls.Add(childForm);
            childForm.Show();
        }
        private void OpenChildControl(Control child)
        {
            try
            {
                panelContainer.Controls.Clear();
               // this.ClientSize = child.Size;
                child.Dock = DockStyle.Fill;
                panelContainer.Controls.Add(child);
                child.BringToFront();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi mở control: " + ex.Message);
            }
        }

        private void btnNhapHang_Click(object sender, EventArgs e)
        {
            OpenChildControl(new uc_Staff_ImportGood());
        }

        private void btnManagerCPT_Click(object sender, EventArgs e)
        {
            openChildForm(new Admin_CustomerAccountManagement());
        }

        private void btnManagerStaff_Click(object sender, EventArgs e)
        {
            openChildForm(new frm_Admin_Employee_management());
        }

        private void btnDoanhThu_Click(object sender, EventArgs e)
        {
            openChildForm(new frm_Revenue());
        }

        private void btnChat_Click(object sender, EventArgs e)
        {
            //
        }

        private void btnAccount_Click(object sender, EventArgs e)
        {
            OpenChildControl(new uc_Staff_Account());
        }
    }
}
