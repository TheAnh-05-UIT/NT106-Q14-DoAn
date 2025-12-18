using System;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.Remoting.Contexts;
using TcpServer.Handlers; // Add this at the top

namespace TcpServer.Handlers
{
    public class HandlerNotification
    {
        private readonly DatabaseHelper db;
        private readonly HandlerInvoice invoiceHandler;

        public HandlerNotification(DatabaseHelper database)
        {
            db = database;
            invoiceHandler = new HandlerInvoice(db);
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

        private object acceptPaidNotificationHandler(dynamic data)
        {
            string title = "Yêu cầu nạp tiền";
            string content = string.Empty;
            string time = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            string messages = string.Empty;

            decimal amount = 0;
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

            return new
            {
                status = "success",
                message = messages,
                notification = new { title = title, content = content + "\n" + messages, time = time, actionType = "accept_paid", addInfo = addInfo }
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

            dynamic paidCheck = new { status = "", messages = "" };
            if (!string.IsNullOrEmpty(addInfo) && amount > 0)
            {
                dynamic obj = new { data = new { invoiceId = addInfo } };
                paidCheck = invoiceHandler.HandleAcceptPayment(obj.data);
            }
            if (paidCheck != null && paidCheck.status == "success")
            {
                messages += paidCheck.messages;
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
                            case "accept_paid":
                                message = acceptPaidNotificationHandler(data.data);
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