using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Data;
using System.Data.SqlClient;

namespace TcpServer.Handlers
{
    public class HandlerInvoice
    {
        private readonly DatabaseHelper db;
        private readonly HandlerCustomerBalance customerBalanceHandler;
        public HandlerInvoice(DatabaseHelper database)
        {
            db = database;
            customerBalanceHandler = new HandlerCustomerBalance(database);
        }

        private JArray ConvertDataTableToJson(DataTable dt)
        {
            string json = JsonConvert.SerializeObject(dt);
            return JArray.Parse(json);
        }

        public object HandleAcceptPayment(JObject data)
        {
            string messages = string.Empty;
            string addInfo = data["invoiceId"].ToString();
            Console.WriteLine(addInfo);
            try
            {
                string getInvoiceSql = "SELECT InvoiceId, CustomerId FROM Invoices WHERE InvoiceId = @invoiceId";
                var dt = db.ExecuteQuery(getInvoiceSql, new SqlParameter("@invoiceId", addInfo));
                if (dt != null && dt.Rows.Count > 0)
                {
                    string invoiceId = dt.Rows[0]["InvoiceId"].ToString();
                    string customerId = dt.Rows[0]["CustomerId"].ToString();
                    try
                    {
                        string checkPaid = "SELECT Status, Price FROM InvoiceDetails WHERE InvoiceId = @invoiceId";
                        var paidCheck = db.ExecuteQuery(checkPaid, new SqlParameter("@invoiceId", addInfo));
                        decimal amount = 0;
                        decimal.TryParse(paidCheck.Rows[0]["Price"].ToString(), out amount);
                        if (paidCheck.Rows[0]["Status"].ToString() == "PENDING")
                        {
                            string updateDetails = "UPDATE InvoiceDetails SET Status = 'PAID' WHERE InvoiceId = @invoiceId";
                            db.ExecuteNonQuery(updateDetails, new SqlParameter("@invoiceId", invoiceId));
                            messages = "Đơn hàng đã được cập nhật trạng thái 'ĐÃ THANH TOÁN'.";
                        }
                        string checkComplete = "SELECT Status FROM InvoiceDetails WHERE InvoiceId = @invoiceId";
                        var completeCheck = db.ExecuteQuery(checkComplete, new SqlParameter("@invoiceId", addInfo));
                        if (completeCheck.Rows[0]["Status"].ToString() == "PAID")
                        {
                            try
                            {
                                string balanceUpdateMessage = customerBalanceHandler.AddBalance(amount, customerId);
                                messages = balanceUpdateMessage;
                                string updateDetail = "UPDATE InvoiceDetails SET Status = 'COMPLETED' WHERE InvoiceId = @invoiceId";
                                db.ExecuteNonQuery(updateDetail, new SqlParameter("@invoiceId", invoiceId));
                                messages = "Đơn hàng đã được hoàn thành và cập nhật số dư khách hàng.";
                            }
                            catch (Exception exBal)
                            {
                                Console.WriteLine($"Warning: cannot update customer balance: {exBal.Message}");
                            }
                        }
                        else
                        {
                            messages += " Đơn hàng đã được hoàn thành trước đó rồi.";
                        }
                    }
                    catch (Exception exDet)
                    {
                        Console.WriteLine($"Warning: cannot update InvoiceDetails status: {exDet.Message}");
                        messages += " Không thể cập nhật thông tin hóa đơn";
                    }
                }
                else
                {
                    Console.WriteLine($"HandlerNotification: invoice '{addInfo}' not found in DB.");
                    messages += " Không tìm thấy mã hóa đơn";
                }

                return new { status = "success", data = ConvertDataTableToJson(dt), messages = messages };
            }
            catch (Exception ex)
            {
                messages = "Không xử lý được thông tin.";
                Console.WriteLine($"Error in HandleGetInvoiceDetails: {messages}");
                return new { status = "error", message = "An error occurred while fetching invoice details." };
            }
        }

        public object HandleGetAllInvoices()
        {
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

        public object HandleGetInvoicesByCustomer(JObject data)
        {
            try
            {
                string customerId = data["customerId"].ToString();

                string query = @"SELECT InvoiceId, SessionId, CreatedAt, TotalAmount
                                 FROM Invoices
                                 WHERE CustomerId = @CustomerId
                                 ORDER BY CreatedAt DESC";

                DataTable dt = db.ExecuteQuery(query, new SqlParameter("@CustomerId", customerId));

                if (dt.Rows.Count == 0)
                {
                    return new { status = "fail", message = "No invoices found for the specified customer." };
                }

                return new { status = "success", data = ConvertDataTableToJson(dt) };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in HandleGetInvoicesByCustomer: {ex.Message}");
                return new { status = "error", message = "An error occurred while fetching invoices." };
            }
        }

        public object HandleGetInvoicesByCustomerAndService(JObject data)
        {
            try
            {
                string customerId = data["customerId"].ToString();

                string query = @"
                    SELECT DISTINCT i.InvoiceId, i.CustomerId, i.CreatedAt, i.TotalAmount, d.Status
                    FROM Invoices i
                    LEFT JOIN InvoiceDetails d ON i.InvoiceId = d.InvoiceId
                    WHERE i.CustomerId = @customerId 
                    AND (d.ServiceId = 1 OR d.ServiceId IS NULL)
                    AND d.Status != 'PAID'";

                SqlParameter[] parameters = {
                    new SqlParameter("@customerId", customerId)
                };

                DataTable dt = db.ExecuteQuery(query, parameters);
                return new { status = "success", data = ConvertDataTableToJson(dt) };
            }
            catch (Exception ex)
            {
                return new { status = "error", message = ex.Message };
            }
        }

        public object HandleGetInvoiceDetails(JObject data)
        {
            try
            {
                string invoiceId = data["invoiceId"].ToString();

                string query = @"SELECT id.FoodId, f.FoodName, id.Quantity, id.Price, (id.Quantity * id.Price) AS Total
                                 FROM InvoiceDetails id
                                 INNER JOIN FoodAndDrink f ON id.FoodId = f.FoodId
                                 WHERE id.InvoiceId = @InvoiceId AND id.ServiceId = 1";

                DataTable dt = db.ExecuteQuery(query, new SqlParameter("@InvoiceId", invoiceId));

                if (dt.Rows.Count == 0)
                {
                    return new { status = "fail", message = "No details found for the specified invoice." };
                }

                return new { status = "success", data = ConvertDataTableToJson(dt) };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in HandleGetInvoiceDetails: {ex.Message}");
                return new { status = "error", message = "An error occurred while fetching invoice details." };
            }
        }

        public object HandleUpdateInvoiceStatus(JObject requestObj)
        {
            try
            {
                var data = requestObj["data"];
                if (data == null) return new { status = "error", message = "Dữ liệu không hợp lệ." };

                string invoiceId = data["invoiceId"]?.ToString();
                string newStatus = data["status"]?.ToString();
                decimal totalAmount = data["totalAmount"]?.Value<decimal>() ?? 0;

                string checkQuery = "SELECT TOP 1 Status FROM InvoiceDetails WHERE InvoiceId = @InvoiceId";
                SqlParameter[] checkParams = { new SqlParameter("@InvoiceId", invoiceId) };

                object currentStatusObj = db.ExecuteScalar(checkQuery, checkParams);

                if (currentStatusObj != null)
                {
                    string currentStatus = currentStatusObj.ToString().ToUpper();
                    if (currentStatus == "COMPLETED")
                    {
                        return new
                        {
                            status = "fail",
                            message = $"Hóa đơn này đã ở trạng thái '{currentStatus}', không thể thay đổi thêm."
                        };
                    }
                }

                string queryDetails = "UPDATE InvoiceDetails SET Status = @Status WHERE InvoiceId = @InvoiceId";
                SqlParameter[] paramsDetails = {
            new SqlParameter("@Status", newStatus),
            new SqlParameter("@InvoiceId", invoiceId)
        };

                string queryInvoice = "UPDATE Invoices SET TotalAmount = @TotalAmount WHERE InvoiceId = @InvoiceId";
                SqlParameter[] paramsInvoice = {
            new SqlParameter("@TotalAmount", totalAmount),
            new SqlParameter("@InvoiceId", invoiceId)
        };

                db.ExecuteNonQuery(queryDetails, paramsDetails);
                db.ExecuteNonQuery(queryInvoice, paramsInvoice);

                return new { status = "success", message = "Cập nhật thành công." };
            }
            catch (Exception ex)
            {
                return new { status = "error", message = ex.Message };
            }
        }
    }
}
