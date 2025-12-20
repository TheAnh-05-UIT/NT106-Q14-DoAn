using System;
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
            ServerConfig.Configure("192.168.244.17", 8080);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new frm_Login());
            Application.Run(new frm_Login());
            //Application.Run(new frm_Staff("123"));
            Form nextform = DTO.UserSession.NextForm;
            if (nextform != null)
            {
                DTO.UserSession.NextForm = null;
                //frm_Login newForm = new frm_Login();
                //newForm.Show();
                Application.Run(nextform);
            }

        }
    }
}