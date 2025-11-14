using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NT106_Q14_DoAnGroup08.ClientCustomer
{
    public partial class frm_Customer_Note : CustomForm
    {
        public string NoteText { get; private set; }
        public frm_Customer_Note(string foodName)
        {
            InitializeComponent();
            lbl_FoodName.Text = foodName;
        }

        private void btn_Confirm_Click(object sender, EventArgs e)
        {
            NoteText = txt_Note.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
