using System;
using System.Drawing;
using System.Windows.Forms;

namespace NT106_Q14_DoAnGroup08.ClientCustomer
{
    public partial class frm_LockScreenVer2 : Form
    {
        private const string STAFF_UNLOCK_PASSWORD = "staff_secret";
        public frm_LockScreenVer2()
        {
            InitializeComponent(); this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {

            if (keyData == (Keys.Control | Keys.Shift | Keys.Q))
            {

                AllowClose();
                return true;
            }
            if (keyData == (Keys.Alt | Keys.Tab) ||
                keyData == (Keys.Alt | Keys.Escape) ||
                keyData == (Keys.Control | Keys.Escape) ||
                keyData == Keys.LWin || keyData == Keys.RWin)
            {
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        private bool _canClose = false;

        public void AllowClose()
        {
            _canClose = true;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (!_canClose)
            {
                e.Cancel = true;
            }
            base.OnFormClosing(e);
        }

        private void txtUnlockPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnUnlock_Click(object sender, EventArgs e)
        {
            if (txtUnlockPassword.Text == STAFF_UNLOCK_PASSWORD)
            {
                AllowClose();
            }
            else
            {
                MessageBox.Show("Mật khẩu nhân viên không đúng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUnlockPassword.Clear();
            }

        }
    }
}
