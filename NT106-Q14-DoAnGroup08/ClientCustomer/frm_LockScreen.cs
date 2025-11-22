using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NT106_Q14_DoAnGroup08.ClientCustomer
{
    public partial class frm_LockScreen : Form
    {
        public frm_LockScreen()
        {
            InitializeComponent();
        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }
        private void MergerForm(Form form)
        {
            panelMainForm.Controls.Clear();
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;
            panelMainForm.Controls.Add(form);
            form.Show();
        }

        private bool _canClose = false;

        public void AllowClose()
        {
            _canClose = true;
            this.Close(); 
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
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (!_canClose)
            {
                e.Cancel = true;
            }
            base.OnFormClosing(e);
        }
        private void frm_LockScreen_Load(object sender, EventArgs e)
        {
            lblTitle.Visible = false;
            MergerForm(new frm_Login());
        }
    }
}
