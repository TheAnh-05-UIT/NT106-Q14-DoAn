using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;

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

            string query = $@"
                INSERT INTO InvoiceDetails
                (InvoiceDetailId, InvoiceId, FoodId, Quantity, Price, Status, Note)
                VALUES
                ('{detailId}', '{invoiceId}', '{foodId}', {quantity}, {price}, 'PENDING', '{note}')";

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
    }
}
