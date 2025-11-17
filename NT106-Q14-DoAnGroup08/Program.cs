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
<<<<<<< HEAD
            Application.Run(new frm_Admin_Employee_management());
=======
            Application.Run(new frm_Staff_ComputerManagement());
>>>>>>> 2722f5a5678d165b4df5804db581a19e02e65111
        }
    }
}
