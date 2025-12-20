using System;
using System.Drawing;
using System.Windows.Forms;

namespace NewNet_Customer.ClientCustomer
{
    public partial class frm_Customer_Note : CustomForm
    {
        public string NoteText { get; private set; }
        public frm_Customer_Note(string foodName)
        {
            InitializeComponent(); this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
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
