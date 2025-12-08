using Newtonsoft.Json.Linq;
using System;
using System.Data;
using System.Data.SqlClient;

namespace TcpServer.Handlers
{
    public class HandlerImportGood
    {
        private readonly DatabaseHelper db;

        public HandlerImportGood(DatabaseHelper database)
        {
            db = database;
        }

        public object HandleGetImportGoods()
        {
            try
            {
                string query = "SELECT ImportId, ImportDate, ItemName, Quantity, Supplier FROM dbo.ImportGoods ORDER BY ImportDate DESC";
                DataTable dt = db.ExecuteQuery(query);
                return new { status = "success", data = dt };
            }
            catch (Exception ex)
            {
                return new { status = "error", message = ex.Message };
            }
        }

        public object HandleAddImportGood(dynamic data)
        {
            try
            {
                string importId = data.ImportId != null ? (string)data.ImportId : Guid.NewGuid().ToString("N").Substring(0, 8);
                DateTime importDate = data.ImportDate != null ? (DateTime)data.ImportDate : DateTime.Now;
                string itemName = data.ItemName != null ? (string)data.ItemName : string.Empty;
                int qty = data.Quantity != null ? (int)data.Quantity : 0;
                string supplier = data.Supplier != null ? (string)data.Supplier : string.Empty;

                string insert = "INSERT INTO dbo.ImportGoods (ImportId, ImportDate, ItemName, Quantity, Supplier) VALUES (@id, @date, @name, @qty, @sup)";
                int rows = db.ExecuteNonQuery(insert,
                    new SqlParameter("@id", importId),
                    new SqlParameter("@date", importDate),
                    new SqlParameter("@name", itemName),
                    new SqlParameter("@qty", qty),
                    new SqlParameter("@sup", supplier)
                );

                if (rows > 0)
                    return new { status = "success", importId = importId };
                return new { status = "fail" };
            }
            catch (Exception ex)
            {
                return new { status = "error", message = ex.Message };
            }
        }

        public object HandleDeleteImportGood(dynamic data)
        {
            try
            {
                string importId = (string)data.ImportId;
                string delete = "DELETE FROM dbo.ImportGoods WHERE ImportId=@id";
                int rows = db.ExecuteNonQuery(delete, new SqlParameter("@id", importId));
                if (rows > 0) return new { status = "success" };
                return new { status = "fail", message = "Not found" };
            }
            catch (Exception ex)
            {
                return new { status = "error", message = ex.Message };
            }
        }
    }
}
