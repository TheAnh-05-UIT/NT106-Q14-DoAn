using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace TcpServer.Handlers
{
    public class HandlerFood
    {
        private readonly DatabaseHelper db;
        public HandlerFood(DatabaseHelper database)
        {
            db = database;
        }

        private JArray ConvertDataTableToJson(DataTable dt)
        {
            string json = JsonConvert.SerializeObject(dt);
            return JArray.Parse(json);
        }

        public object HandleGetAllCategories()
        {
            DataTable dt = db.ExecuteQuery("SELECT * FROM Category");
            return new { status = "success", data = ConvertDataTableToJson(dt) };
        }

        public object HandleGetAllFood()
        {
            string query = @"SELECT f.FoodId, f.FoodName, f.Price, f.Image, c.CategoryName, c.CategoryId
                             FROM FoodAndDrink f
                             LEFT JOIN Category c ON f.CategoryId = c.CategoryId";
            
            DataTable dt = db.ExecuteQuery(query);
            return new { status = "success", data = ConvertDataTableToJson(dt) };
        }

        public object HandleCreateInvoice(JObject data)
        {
            string invoiceId = data["invoiceId"].ToString();
            string customerId = data["customerId"].ToString();
            decimal totalAmount = data["totalAmount"].ToObject<decimal>();

            string sessionQuery = "SELECT TOP 1 SessionId FROM Sessions WHERE CustomerId = @CustomerId AND EndTime IS NULL";
            DataTable sessionDt = db.ExecuteQuery(sessionQuery, new SqlParameter("@CustomerId", customerId));
            string sessionId = sessionDt.Rows[0]["SessionId"].ToString();

            string query = $@"
            INSERT INTO Invoices (InvoiceId, SessionId, CustomerId, TotalAmount)
            VALUES ('{invoiceId}', '{sessionId}', '{customerId}', {totalAmount})";

            int result = db.ExecuteNonQuery(query);
            return new { status = result > 0 ? "success" : "fail" };
        }

        public object HandleCreateInvoiceDetail(JObject data)
        {
            string detailId = data["detailId"].ToString();
            string invoiceId = data["invoiceId"].ToString();
            string foodId = data["foodId"].ToString();
            int quantity = data["quantity"].ToObject<int>();
            decimal price = data["price"].ToObject<decimal>();
            string note = data["note"].ToString();
            string serviceId = data["serviceId"].ToString();

            string query = $@"
                INSERT INTO InvoiceDetails
                (InvoiceDetailId, InvoiceId, ServiceId,FoodId, Quantity, Price, Status, Note)
                VALUES
                ('{detailId}', '{invoiceId}', '{serviceId}','{foodId}', {quantity}, {price}, 'PENDING', '{note}')";

            int result = db.ExecuteNonQuery(query);
            return new { status = result > 0 ? "success" : "fail" };
        }

        public object HandleGetMaxInvoiceId()
        {
            object result = db.ExecuteScalar("SELECT MAX(InvoiceId) FROM Invoices WHERE InvoiceId LIKE 'HD%'");
            return new { status = "success", maxId = result?.ToString() };
        }

        public object HandleGetMaxInvoiceDetailId()
        {
            object result = db.ExecuteScalar("SELECT MAX(InvoiceDetailId) FROM InvoiceDetails WHERE InvoiceDetailId LIKE 'CTHD%'");
            return new { status = "success", maxId = result?.ToString() };
        }

        public object HandleLoadInvoiceInSession(JObject data)
        {
            string machineId = data["customerId"].ToString();

            string sessionQuery = "SELECT TOP 1 SessionId FROM Sessions WHERE CustomerId = @MachineId AND EndTime IS NULL";
            DataTable sessionDt = db.ExecuteQuery(sessionQuery, new SqlParameter("@MachineId", machineId));

            if (sessionDt.Rows.Count == 0)
            {
                return new { status = "fail", message = "No active session for this machine." };
            }

            string sessionId = sessionDt.Rows[0]["SessionId"].ToString();


            string query = "SELECT DISTINCT i.InvoiceId, CreatedAt, TotalAmount FROM Invoices i JOIN InvoiceDetails id ON i.InvoiceId = id.InvoiceId WHERE id.ServiceId = '1' AND SessionId=@SessionId ORDER BY CreatedAt DESC";

            DataTable dt = db.ExecuteQuery(query, new SqlParameter("@SessionId", sessionId));

            if (dt == null || dt.Rows.Count == 0)
            {
                return new { status = "fail", message = "No invoices found." };
            }

            // convert datatable to list of objects
            var invoices = dt.AsEnumerable().Select(r => new
            {
                InvoiceId = r["InvoiceId"].ToString(),
                CreatedAt = Convert.ToDateTime(r["CreatedAt"]).ToString("yyyy-MM-dd HH:mm"),
                TotalAmount = Convert.ToDecimal(r["TotalAmount"])
            }).ToList();

            return new
            {
                status = "success",
                data = invoices
            };
        }
        public object HandleLoadInvoiceDetail(string invoiceId)
        {
            string detailsQuery = @"
        SELECT f.FoodName, id.Quantity, id.Price, (id.Quantity * id.Price) AS Total, id.Note
        FROM InvoiceDetails id
        INNER JOIN FoodAndDrink f ON id.FoodId = f.FoodId
        WHERE id.InvoiceId = @InvoiceId
    ";

            DataTable detailsDt = db.ExecuteQuery(detailsQuery, new SqlParameter("@InvoiceId", invoiceId));

            if (detailsDt == null || detailsDt.Rows.Count == 0)
            {
                return new { status = "fail", message = "No details found for this invoice." };
            }

            var details = detailsDt.AsEnumerable().Select(r => new
            {
                FoodName = r["FoodName"].ToString(),
                Quantity = Convert.ToInt32(r["Quantity"]),
                Price = Convert.ToDecimal(r["Price"]).ToString("F2"),
                Total = Convert.ToDecimal(r["Total"]).ToString("F2"),
                Note = r["Note"] == DBNull.Value ? "" : r["Note"].ToString()
            }).ToList();

            return new
            {
                status = "success",
                data = details
            };
        }

        public object HandleCreateInvoiceDetailTopUp(JObject data, ServerHandler.ServerHandler server)
        {
            string detailId = data["detailId"].ToString();
            string invoiceId = data["invoiceId"].ToString();
            int quantity = data["quantity"].ToObject<int>();
            decimal price = data["totalAmount"].ToObject<decimal>();
            string note = data["note"].ToString();
            string serviceId = data["serviceId"].ToString();

            string query = $@"
                INSERT INTO InvoiceDetails
                (InvoiceDetailId, InvoiceId, ServiceId, Quantity, Price, Status, Note)
                VALUES
                ('{detailId}', '{invoiceId}', '{serviceId}', {quantity}, {price}, 'PENDING', N'{note}')";


            int result = db.ExecuteNonQuery(query);

            string query_info = $@"
                SELECT DISTINCT s.CustomerId, s.SessionId
                FROM Invoices i JOIN Sessions s ON i.SessionId = s.SessionId
                WHERE InvoiceId = '{invoiceId}'";


            DataTable result_info = db.ExecuteQuery(query_info);
            server.notifyToStaff(new { type = "accept_paid", data = new { accountName = result_info.Rows[0]["CustomerId"], amount = price, addInfo = invoiceId, session = result_info.Rows[0]["SessionId"] } });

            return new { status = result > 0 ? "success" : "fail" };
        }
    }
}
