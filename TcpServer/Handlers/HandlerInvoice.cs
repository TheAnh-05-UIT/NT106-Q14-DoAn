using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace TcpServer.Handlers
{
    public class HandlerInvoice
    {
        private readonly DatabaseHelper db;
        public HandlerInvoice(DatabaseHelper database)
        {
            db = database;
        }

        private JArray ConvertDataTableToJson(DataTable dt)
        {
            string json = JsonConvert.SerializeObject(dt);
            return JArray.Parse(json);
        }

        public object HandleGetAllInvoices()
        {
            // Select explicit columns from Invoices and InvoiceDetails and include product/service names
            string query = @"SELECT i.InvoiceId,
                                    i.SessionId,
                                    i.CustomerId,
                                    i.CreatedAt,
                                    i.TotalAmount,
                                    d.InvoiceDetailId,
                                    d.FoodId,
                                    f.FoodName AS FoodName,
                                    d.ServiceId,
                                    s.ServiceName AS ServiceName,
                                    d.Quantity,
                                    d.Price,
                                    d.Status AS DetailStatus,
                                    d.Note
                             FROM Invoices i
                             LEFT JOIN InvoiceDetails d ON i.InvoiceId = d.InvoiceId
                             LEFT JOIN FoodAndDrink f ON d.FoodId = f.FoodId
                             LEFT JOIN Services s ON d.ServiceId = s.ServiceId";

            DataTable dt = db.ExecuteQuery(query);
            return new { status = "success", data = ConvertDataTableToJson(dt) };
        }
    }
}
