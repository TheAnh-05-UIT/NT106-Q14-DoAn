using NT106_Q14_DoAnGroup08.ClientAdmin;
using NT106_Q14_DoAnGroup08.ClientCustomer;
using NT106_Q14_DoAnGroup08.ClientStaff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NT106_Q14_DoAnGroup08
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            frm_Staff newForm = new frm_Staff();
            newForm.Show();
            frm_Customer_TopUp newForm1 = new frm_Customer_TopUp();
            newForm1.Show();
            Application.Run();
            Form nextform = DTO.UserSession.NextForm;
            if (nextform != null)
            {
                DTO.UserSession.NextForm = null;
                Application.Run(nextform);
            }

        }
    }
}