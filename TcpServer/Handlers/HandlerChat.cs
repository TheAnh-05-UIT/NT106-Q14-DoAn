using System.Data;
using System.Data.SqlClient;

namespace TcpServer.Handlers
{
    public class HandlerChat
    {
        private readonly DatabaseHelper db;

        public HandlerChat(DatabaseHelper db)
        {
            this.db = db;
        }

        public object HandleSendMessageStaff(dynamic obj)
        {
            string from = obj.from;
            string to = obj.to;
            string content = obj.content;

            string sql = @"INSERT INTO ChatMessage (FromId, ToId, Content, IsRead)
                           VALUES (@f, @t, @c, 0)";

            db.ExecuteNonQuery(
                sql,
                new SqlParameter("@f", from),
                new SqlParameter("@t", to),
                new SqlParameter("@c", content)
            );

            return new { status = "ok" };
        }

        public object HandleSendMessageCustomer(dynamic obj)
        {
            string from = obj.from;
            string content = obj.content;

            string staffId = OnlineStaffManager.GetOne();

            if (staffId == null)
            {
                return new { status = "error", message = "Không có nhân viên online" };
            }

            string sql = @"INSERT INTO ChatMessage (FromId, ToId, Content, IsRead)
                   VALUES (@f,@t,@c,0)";

            db.ExecuteNonQuery(sql,
                new SqlParameter("@f", from),
                new SqlParameter("@t", staffId),
                new SqlParameter("@c", content));

            return new { status = "ok", to = staffId };
        }


        public object HandleGetUnreadForStaff(dynamic obj)
        {
            string staffId = obj.staffId;

            string sql = @"SELECT Id, FromId, Content 
                           FROM ChatMessage 
                           WHERE ToId = @id AND IsRead = 0";

            var list = db.ExecuteQuery(
                sql,
                new SqlParameter("@id", staffId)
            );

            foreach (DataRow row in list.Rows)
            {
                db.ExecuteNonQuery(
                    "UPDATE ChatMessage SET IsRead = 1 WHERE Id = @id",
                    new SqlParameter("@id", row["Id"])
                );
            }

            return list;
        }

        public object HandleGetUnreadForCustomer(dynamic obj)
        {
            string customerId = obj.userId;

            string sql = @"SELECT Id, FromId, Content 
                           FROM ChatMessage 
                           WHERE ToId = @id AND IsRead = 0";

            var list = db.ExecuteQuery(
                sql,
                new SqlParameter("@id", customerId)
            );

            foreach (DataRow row in list.Rows)
            {
                db.ExecuteNonQuery(
                    "UPDATE ChatMessage SET IsRead = 1 WHERE Id = @id",
                    new SqlParameter("@id", row["Id"])
                );
            }

            return list;
        }
    }
}
