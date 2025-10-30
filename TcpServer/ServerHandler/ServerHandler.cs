using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using TcpServer.Handlers;

namespace TcpServer.ServerHandler
{
    public class ServerHandler
    {
        private TcpListener listener;
        private readonly DatabaseHelper db;

        private readonly HandlerLogin handlerLogin;
        private readonly HandlerAdminCustomerAcc handlerAdminCustomerAcc;

        public ServerHandler(string connStr)
        {
            db = new DatabaseHelper(connStr);
            handlerLogin = new HandlerLogin(db);
            handlerAdminCustomerAcc = new HandlerAdminCustomerAcc(db);
        }

        public void Start(int port)
        {
            listener = new TcpListener(IPAddress.Any, port);
            listener.Start();
            Console.WriteLine($"Server started on port {port}");

            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();
                new Thread(() => HandleClient(client)).Start();
            }
        }

        private void HandleClient(TcpClient client)
        {
            try
            {
                using (NetworkStream ns = client.GetStream())
                {
                    byte[] buffer = new byte[4096];
                    int bytesRead = ns.Read(buffer, 0, buffer.Length);
                    if (bytesRead == 0) return;

                    string request = Encoding.UTF8.GetString(buffer, 0, bytesRead).Trim();
                    Console.WriteLine($"Received: {request}");

                    dynamic obj = JsonConvert.DeserializeObject(request);
                    if (obj?.action == null)
                    {
                        SendResponse(ns, new { status = "error", message = "Missing 'action' field" });
                        return;
                    }

                    string action = obj.action.ToString();
                    Console.WriteLine($"Action: {action}");
                    object response;

                    // Dựa vào action để gọi handler tương ứng
                    switch (action)
                    {
                        // Login
                        case "login":
                            response = handlerLogin.HandleLogin(obj);
                            break;

                        // lấy ra nhân viên
                        case "get_all_employees":
                            response = handlerAdminCustomerAcc.HandleGetAllEmployees();
                            break;
                        // thêm nhân viên
                        case "add_employee":
                            response = handlerAdminCustomerAcc.HandleAddEmployee(obj.data);
                            break;
                        // cập nhật nhân viên
                        case "update_employee":
                            response = handlerAdminCustomerAcc.HandleUpdateEmployee(obj.data);
                            break;
                        // xóa nhân viên
                        case "delete_employee":
                            response = handlerAdminCustomerAcc.HandleDeleteEmployee(obj.data);
                            break;
                        // ngoại lệ
                        default:
                            response = new { status = "error", message = $"Unknown action: {action}" };
                            break;
                    }

                    SendResponse(ns, response);
                }
            }
            catch (JsonReaderException jsonEx)
            {
                Console.WriteLine($"JSON parse error: {jsonEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in HandleClient: {ex.Message}");
            }
            finally
            {
                client.Close();
            }
        }
        // RESPONSE WRITER
        private void SendResponse(NetworkStream ns, object obj)
        {
            string json = JsonConvert.SerializeObject(obj);
            byte[] sendData = Encoding.UTF8.GetBytes(json);
            ns.Write(sendData, 0, sendData.Length);
        }
    }
}