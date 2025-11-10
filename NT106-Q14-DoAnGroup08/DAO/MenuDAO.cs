using Guna.UI2.WinForms;
using NT106_Q14_DoAnGroup08.Uc_Staff;
using QuanLyQuanNet.DAO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NT106_Q14_DoAnGroup08.DAO
{
    internal class MenuDAO
    {
        private static MenuDAO instance;

        public static MenuDAO Instance
        {
            get { if (instance == null) instance = new MenuDAO(); return MenuDAO.instance; }
            private set => instance = value;
        }
        private MenuDAO() { }
    }
}
