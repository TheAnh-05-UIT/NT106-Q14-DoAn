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
