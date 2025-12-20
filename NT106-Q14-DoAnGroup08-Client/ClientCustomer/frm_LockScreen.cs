//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
//using System.IO;
//using System.Linq;
//using System.Security.Cryptography;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Forms;

//namespace NewNet_Customer.ClientCustomer
//{
//    public partial class frm_LockScreen : Form
//    {
//        frm_Login f = new frm_Login();
//        public frm_LockScreen()
//        {
//            InitializeComponent();this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
//            f.LoginSuccess += HandleLoginSuccess;
//        }
//        private void HandleLoginSuccess()
//        {
//            Form newlyOpenedForm = Form.ActiveForm;
//            if(newlyOpenedForm != null && newlyOpenedForm != this)
//            {
//                newlyOpenedForm.Hide();
//                DTO.UserSession.NextForm = newlyOpenedForm;
//            }
//            f.LoginSuccess -= HandleLoginSuccess;
//            f.Dispose();
//            AllowClose();
//        }
//        private void lblTitle_Click(object sender, EventArgs e)
//        {

//        }
//        private void MergerForm(Form form)
//        {
//            panelMainForm.Controls.Clear();
//            form.TopLevel = false;
//            form.FormBorderStyle = FormBorderStyle.None;
//            form.Dock = DockStyle.Fill;
//            panelMainForm.Controls.Add(form);
//            form.Show();
//        }

//        private bool _canClose = false;

//        public void AllowClose()
//        {
//            _canClose = true;
//            this.Close(); 
//        }
//        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
//        {
//            if (keyData == (Keys.Control | Keys.Shift | Keys.Q))
//            {

//                AllowClose();
//                return true; 
//            }
//            if (keyData == (Keys.Alt | Keys.Tab) ||
//                keyData == (Keys.Alt | Keys.Escape) ||
//                keyData == (Keys.Control | Keys.Escape) ||
//                keyData == Keys.LWin || keyData == Keys.RWin)
//            {
//                return true;
//            }
//            return base.ProcessCmdKey(ref msg, keyData);
//        }
//        protected override void OnFormClosing(FormClosingEventArgs e)
//        {
//            if (!_canClose)
//            {
//                e.Cancel = true;
//            }
//            base.OnFormClosing(e);
//        }
//        private void frm_LockScreen_Load(object sender, EventArgs e)
//        {
//            lblTitle.Visible = false;
//            MergerForm(f);
//        }

//        private void panelMainForm_Paint(object sender, PaintEventArgs e)
//        {

//        }
//    }
//}
using System;
using System.Drawing;
using System.Windows.Forms;

namespace NewNet_Customer.ClientCustomer
{
    public partial class frm_LockScreen : Form
    {
        private frm_Login f;
        private bool _canClose = false;

        public frm_LockScreen()
        {
            InitializeComponent(); this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            f = new frm_Login();
            f.LoginSuccess += HandleLoginSuccess;
        }

        private void HandleLoginSuccess()
        {
            try
            {
                _canClose = true;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi unlock: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MergeForm(Form form)
        {
            if (panelMainForm == null || form == null) return;

            panelMainForm.Controls.Clear();
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;
            panelMainForm.Controls.Add(form);
            form.Show();
        }

        public void AllowClose()
        {
            _canClose = true;
            this.DialogResult = DialogResult.Cancel;  // Dev unlock
            this.Close();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // Dev unlock shortcut
            if (keyData == (Keys.Control | Keys.Shift | Keys.Q))
            {
                AllowClose();
                return true;
            }

            // Block các phím nguy hiểm
            if (keyData == (Keys.Alt | Keys.Tab) ||
                keyData == (Keys.Alt | Keys.Escape) ||
                keyData == (Keys.Control | Keys.Escape) ||
                keyData == Keys.LWin || keyData == Keys.RWin)
            {
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // Chỉ block nếu user cố close thủ công và chưa unlock
            if (!_canClose && e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
            }
            base.OnFormClosing(e);
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            // Cleanup
            if (f != null)
            {
                f.LoginSuccess -= HandleLoginSuccess;
                f.Dispose();
                f = null;
            }
            base.OnFormClosed(e);
        }
        private void panelMainForm_Paint(object sender, PaintEventArgs e)
        {

        }
        private void lblTitle_Click(object sender, EventArgs e)
        {

        }
        private void frm_LockScreen_Load(object sender, EventArgs e)
        {
            lblTitle.Visible = false;
            MergeForm(f);
        }
    }
}