using System.Collections.Generic;

namespace TcpServer
{
    public static class OnlineStaffManager
    {
        private static List<string> onlineStaffs = new List<string>();

        public static void Add(string staffId)
        {
            if (!onlineStaffs.Contains(staffId))
                onlineStaffs.Add(staffId);
        }

        public static void Remove(string staffId)
        {
            onlineStaffs.Remove(staffId);
        }

        public static string GetOne()
        {
            if (onlineStaffs.Count == 0)
                return null;

            return onlineStaffs[0];
        }
    }

}
