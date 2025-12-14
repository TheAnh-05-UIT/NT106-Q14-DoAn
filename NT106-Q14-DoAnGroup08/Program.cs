using NT106_Q14_DoAnGroup08.ClientAdmin;
using NT106_Q14_DoAnGroup08.ClientCustomer;
using NT106_Q14_DoAnGroup08.ClientStaff;
using NT106_Q14_DoAnGroup08.Uc_Staff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
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
            ServerConfig.Configure("127.0.0.1", 8080);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frm_Login());
            //Application.Run(new frm_Staff("U002"));
            Form nextform = DTO.UserSession.NextForm;
            if (nextform != null)
            {
                DTO.UserSession.NextForm = null;
                Application.Run(nextform);
            }

        }
    }
}