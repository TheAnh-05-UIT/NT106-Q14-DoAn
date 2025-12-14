using System;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.Remoting.Contexts;

namespace TcpServer.Handlers
{
    public class HandlerNotification
    {
        private readonly DatabaseHelper db;

        public HandlerNotification(DatabaseHelper database)
        {
            db = database;
        }

        private object genericNotificationHandler(dynamic data)
        {
            string title = string.Empty;
            string content = string.Empty;
            string time = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            string messages = string.Empty;
            string addInfo = null;

            if (data != null)
            {
                var d = data;

                if (d.title != null)
                {
                    title = d.title;
                }

                if (d.addInfo != null)
                {
                    addInfo = d.addInfo;
                }

                if (d.content != null)
                {
                    content = d.content;
                }


            }

            if (!string.IsNullOrEmpty(addInfo))
            {
                try
                {
                    messages = "Không có lỗi gì xảy ra.";
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error while processing HandlerNotification: {ex.Message}");
                    messages = "Không xử lý được thông tin.";
                }
            }

            return new
            {
                status = "success",
                message = messages,
                notification = new { title = title, content = content + "\n" + messages, time = time }
            };
        }

        private object bankNotificationHandler(dynamic data)
        {
            string title = "Bank payment";
            string content = "Thông báo thanh toán từ ngân hàng.";
            string time = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            string messages = "Thông báo từ ngân hàng đã được xử lý.";

            decimal amount = 0m;
            string accountName = null;
            string addInfo = null;
            if (data != null)
            {
                var d = data;

                if (d.amount != null)
                {
                    decimal.TryParse(d.amount.ToString(), out amount);
                }

                if (d.accountName != null)
                {
                    accountName = d.accountName.ToString();
                }

                if (d.addInfo != null)
                {
                    addInfo = d.addInfo;
                }

                var parts = new System.Text.StringBuilder();
                if (amount > 0) parts.AppendFormat("Số tiền: {0} ", amount);
                if (!string.IsNullOrEmpty(accountName)) parts.AppendFormat("Tài khoản: {0} ", accountName);
                if (parts.Length > 0)
                    content = parts.ToString();
            }

            if (!string.IsNullOrEmpty(addInfo) && amount > 0)
            {
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
                            string checkPaid = "SELECT Status FROM InvoiceDetails WHERE InvoiceId = @invoiceId";
                            var paidCheck = db.ExecuteQuery(checkPaid, new SqlParameter("@invoiceId", addInfo));
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
                                    string updateBalance = "UPDATE Customers SET Balance = ISNULL(Balance,0) + @amount WHERE CustomerId = @customerId";
                                    Console.WriteLine(customerId);
                                    Console.WriteLine(amount);
                                    db.ExecuteNonQuery(updateBalance,
                                        new SqlParameter("@amount", amount),
                                        new SqlParameter("@customerId", customerId));
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
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error while processing DB update in HandlerNotification: {ex.Message}");
                    messages = "Không xử lý được thông tin.";
                }
            }

            return new
            {
                status = "success",
                message = messages,
                notification = new { title = title, content = content + "\n" + messages, time = time }
            };
        }

        public object HandleNotification(dynamic data)
        {
            try
            {
                string type = string.Empty;
                object message = null;

                try
                {
                    if (data != null && data.type != null)
                    {
                        type = data.type;
                        switch (type)
                        {
                            case "paid":
                                message = bankNotificationHandler(data.data);
                                break;
                            default:
                                message = genericNotificationHandler(data.data);
                                break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }

                return message;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in HandlerNotification: {ex.Message}");
                return new { status = "error", message = "Lỗi hệ thống." };
            }
        }
    }
}