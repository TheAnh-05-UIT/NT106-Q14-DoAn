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
            string query = @"SELECT *
                             FROM Invoices i 
                             INNER JOIN InvoiceDetails d ON i.InvoiceId = d.InvoiceId";

            DataTable dt = db.ExecuteQuery(query);
            return new { status = "success", data = ConvertDataTableToJson(dt) };
        }
    }
}
