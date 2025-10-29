using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace NT106_Q14_DoAnGroup08.ClientAdmin
{
    public partial class Admin_CustomerAccountManagement : Form
    {
        public Admin_CustomerAccountManagement()
        {
            InitializeComponent();
        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }

        private void Admin_CustomerAccountManagement_Load(object sender, EventArgs e)
        {
           
        
            dgvAccCustomers.Rows.Add("KH01", "Nguyen Van A", "50000", "Active", "UserA", "userA@123");
            dgvAccCustomers.Rows.Add("KH02", "Tran Thi B", "0","Inactive", "UserB", "userB@123");
            dgvAccCustomers.Rows.Add("KH03", "Le Van C", "120000", "Active", "UserC", "userC@123");
        

    }

        private void btnAddAccount_Click(object sender, EventArgs e)
        {
            ClientCustomer.frm_AddCustomer addUser = new ClientCustomer.frm_AddCustomer();
            if(addUser.ShowDialog() == DialogResult.OK)
            {
                string name = addUser.CustomerName;
                string balance = addUser.Balance;
                string status = addUser.Status;
                string username = addUser.userName;
                string userpass = addUser.userPassword;
                string id = "KH" + (dgvAccCustomers.Rows.Count).ToString("00");
                dgvAccCustomers.Rows.Add(id, name, balance, status, username, userpass);
            }
        }

        private void btnRepair_Click(object sender, EventArgs e)
        {
                if(dgvAccCustomers.CurrentRow == null)
                {
                    MessageBox.Show("Vui lòng chọn thông khách hàng để sửa!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            DataGridViewRow selectedRow = dgvAccCustomers.CurrentRow;
            string id = selectedRow.Cells["colID"].Value.ToString();
            string name = selectedRow.Cells["colName"].Value.ToString();
            string balance = selectedRow.Cells["colBalance"].Value.ToString();
            string username = selectedRow.Cells["colUser"].Value.ToString();
            string userpassword = selectedRow.Cells["colPass"].Value.ToString();
            string status = selectedRow.Cells["colStatus"].Value.ToString();
            ClientCustomer.frm_AddCustomer editFrom = new ClientCustomer.frm_AddCustomer();
            editFrom.Text = "Edit Customer";
            editFrom.LoadCustomerData(name, balance, status,  username, userpassword);
            if(editFrom.ShowDialog() == DialogResult.OK)
            {
                selectedRow.Cells["colName"].Value = editFrom.CustomerName;
                selectedRow.Cells["colBalance"].Value = editFrom.Balance;
                selectedRow.Cells["colUser"].Value = editFrom.userName;
                selectedRow.Cells["colPass"].Value = editFrom.userPassword;
                selectedRow.Cells["colStatus"].Value = editFrom.Status;
                MessageBox.Show("Dữ liệu đã được thay đổi!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if(dgvAccCustomers.CurrentRow != null)
            {
                DialogResult rs = MessageBox.Show("Bạn có chắc chắn muốn xóa người dùng này.", "Thông báo!", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (rs == DialogResult.OK)
                {
                    dgvAccCustomers.Rows.Remove(dgvAccCustomers.CurrentRow);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn hàng muốn xóa.", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnNaptien_Click(object sender, EventArgs e)
        {
            if(dgvAccCustomers.CurrentRow != null)
            {
                DataGridViewRow selectedRow = dgvAccCustomers.CurrentRow;
                string balance = selectedRow.Cells["colBalance"].Value.ToString();
                string username = selectedRow.Cells["colUser"].Value.ToString();
                string status = selectedRow.Cells["colStatus"].Value.ToString();
                frm_Deposit depositt = new frm_Deposit();
        
                depositt.LoadCustomerData(balance,status, username);
      
                if(depositt.ShowDialog() == DialogResult.OK)
                {
                    selectedRow.Cells["colBalance"].Value = depositt.Balance;
                    MessageBox.Show("Đã nạp tiền thành công!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn tài khoản khác hàng mà bạn muốn nạp tiền.", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }    
        }
    }
}
