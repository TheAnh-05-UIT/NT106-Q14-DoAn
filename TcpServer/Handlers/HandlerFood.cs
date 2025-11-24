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
            string query = @"SELECT f.*, c.CategoryName 
                             FROM FoodAndDrink f 
                             INNER JOIN Category c ON f.CategoryId = c.CategoryId";

            DataTable dt = db.ExecuteQuery(query);
            return new { status = "success", data = ConvertDataTableToJson(dt) };
        }

        public object HandleCreateInvoice(JObject data)
        {
            string invoiceId = data["invoiceId"].ToString();
            string customerId = data["customerId"].ToString();
            decimal totalAmount = data["totalAmount"].ToObject<decimal>();

            string query = $@"
                INSERT INTO Invoices (InvoiceId, CustomerId, TotalAmount)
                VALUES ('{invoiceId}', '{customerId}', {totalAmount})";

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

        public object HandleLoadInvoiceInSession()
        {
            string machineId = Environment.MachineName;

            string sessionQuery = "SELECT TOP 1 SessionId FROM Sessions WHERE MachineId = @MachineId AND Status='Active'";
            DataTable sessionDt = db.ExecuteQuery(sessionQuery, new SqlParameter("@MachineId", machineId));

            if (sessionDt.Rows.Count == 0)
            {
                return new { status = "fail", message = "No active session for this machine." };
            }

            string sessionId = sessionDt.Rows[0]["SessionId"].ToString();


            string query = "SELECT InvoiceId, CreatedAt, TotalAmount FROM Invoices WHERE SessionId=@SessionId ORDER BY CreatedAt DESC";

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
        public object HandleLoadInvoiceDetail()
        {
            // 1. Detect current machine
            string machineId = Environment.MachineName; // or MAC address for uniqueness

            // 2. Get active session for this machine
            string sessionQuery = @"
        SELECT TOP 1 SessionId 
        FROM Sessions 
        WHERE MachineId = @MachineId AND Status = 'Active'
        ORDER BY StartTime DESC
    ";
            DataTable sessionDt = db.ExecuteQuery(sessionQuery, new SqlParameter("@MachineId", machineId));

            if (sessionDt == null || sessionDt.Rows.Count == 0)
            {
                return new { status = "fail", message = "No active session for this machine." };
            }

            string sessionId = sessionDt.Rows[0]["SessionId"].ToString();

            // 3. Get invoice for the session
            string invoiceQuery = @"
        SELECT TOP 1 InvoiceId 
        FROM Invoices 
        WHERE SessionId = @SessionId 
        ORDER BY CreatedAt DESC
    ";
            DataTable invoiceDt = db.ExecuteQuery(invoiceQuery, new SqlParameter("@SessionId", sessionId));

            if (invoiceDt == null || invoiceDt.Rows.Count == 0)
            {
                return new { status = "fail", message = "No invoice found for this session." };
            }

            string invoiceId = invoiceDt.Rows[0]["InvoiceId"].ToString();

            // 4. Get invoice details
            string detailsQuery = @"
        SELECT f.FoodName, id.Quantity, id.Price, (id.Quantity * id.Price) AS Total
        FROM InvoiceDetails id
        INNER JOIN Food f ON id.FoodId = f.FoodId
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
                Total = Convert.ToDecimal(r["Total"]).ToString("F2")
            }).ToList();

            return new
            {
                status = "success",
                data = details
            };
        }
        public object HandleCreateInvoiceDetailTopUp(JObject data)
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
            return new { status = result > 0 ? "success" : "fail" };
        }
    }
}
