using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using TcpServer.Handlers;

namespace TcpServer.ServerHandler
{
    public class ServerHandler
    {
        private TcpListener listener;
        private readonly DatabaseHelper db;
        public static ConcurrentDictionary<string, TcpClient> ClientConnections =
            new ConcurrentDictionary<string, TcpClient>();
        private readonly HandlerLogin handlerLogin;
        private readonly HandlerAdminCustomerAcc handlerAdminCustomerAcc;
        private readonly HandlerAdminCustomer handlerCustomerHandler;
        private readonly HandlerFood handlerFood;
        private readonly HandlerCustomer handlerCustomer;
        private readonly HandlerComputerManagement computerHandler;
        private readonly HandlerAdminComputerManagementcs adminComputerHandler;
        private readonly HandlerNotification handlerNotification;
        public ServerHandler(string connStr)
        {
            db = new DatabaseHelper(connStr);
            handlerLogin = new HandlerLogin(db);
            handlerAdminCustomerAcc = new HandlerAdminCustomerAcc(db);
            handlerCustomerHandler = new HandlerAdminCustomer(db);
            handlerFood = new HandlerFood(db);
            handlerCustomer = new HandlerCustomer(db);
            computerHandler = new HandlerComputerManagement(db);
            adminComputerHandler = new HandlerAdminComputerManagementcs(db);
            handlerNotification = new HandlerNotification(db);
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
            string clientName = string.Empty;
            try
            {
                using (NetworkStream ns = client.GetStream())
                {
                    byte[] buffer = new byte[4096];
                    int bytesRead;
                    ns.ReadTimeout = 500;

                    try
                    {
                        bytesRead = ns.Read(buffer, 0, buffer.Length);
                        if (bytesRead == 0) return;

                        string firstData = Encoding.UTF8.GetString(buffer, 0, bytesRead).Trim();
                        if (firstData.StartsWith("{") && firstData.EndsWith("}"))
                        {
                            HandleBusinessRequest(ns, firstData);
                        }
                        else
                        {
                            clientName = firstData;
                            RegisterClientStaff(clientName, client);
                            ListenForClientStaffCommands(clientName, client, ns);
                        }
                    }
                    catch (IOException)
                    {
                        Console.WriteLine("Client ket noi nhung khong gui du lieu đang ky/yeu cau kip thoi.");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in HandleClient: {ex.Message}");
            }
            finally
            {
                if (!string.IsNullOrEmpty(clientName))
                {
                    DeregisterClientStaff(clientName, client);
                }
                else
                {
                    client.Close();
                }
            }
        }

        private object HandleComputerControl(dynamic data)
        {
            string computerId = data.ComputerId?.ToString();
            string actionType = data.ActionType?.ToString();
            string clientName = string.Empty;

            if (string.IsNullOrEmpty(computerId) || string.IsNullOrEmpty(actionType))
            {
                return new { status = "error", message = "Missing ComputerId or ActionType." };
            }
            clientName = computerId;

            if (string.IsNullOrEmpty(clientName))
            {
                return new { status = "error", message = $"Khong tim thay may tram đa đang ky cho ComputerId: {computerId}" };
            }


            switch (actionType.ToUpper())
            {
                case "LOCK":

                    if (SendCommandToClientStaff(clientName, "LOCK_PC"))
                    {

                        Console.WriteLine($"[CONTROL] Đa khoa may {computerId} ({clientName}).");
                        return new { status = "success", message = $"Đa gui lenh khoa đen may {computerId}" };
                    }
                    return new { status = "error", message = $"Khong the gui lenh khoa đen may {clientName}. May khong ket noi." };

                case "END_SESSION":
                    SendCommandToClientStaff(clientName, "LOGOUT_PC");

                    Console.WriteLine($"[CONTROL] Đa ket thuc phien may {computerId}.");
                    return new { status = "success", message = $"Đa ket thuc phien may {computerId}" };

                default:
                    return new { status = "error", message = $"ActionType khong hop le: {actionType}" };
            }
        }
        private void HandleBusinessRequest(NetworkStream ns, string request)
        {
            try
            {
                Console.WriteLine($"Received Business Request: {request}");

                dynamic obj = JsonConvert.DeserializeObject(request);
                if (obj?.action == null)
                {
                    SendResponse(ns, new { status = "error", message = "Missing 'action' field" });
                    return;
                }

                string action = obj.action.ToString();
                Console.WriteLine($"Action: {action}");
                object response;

                switch (action)
                {
                    case "login": response = handlerLogin.HandleLogin(obj); break;
                    case "get_all_employees": response = handlerAdminCustomerAcc.HandleGetAllEmployees(); break;
                    case "add_employee": response = handlerAdminCustomerAcc.HandleAddEmployee(obj.data); break;
                    case "update_employee": response = handlerAdminCustomerAcc.HandleUpdateEmployee(obj.data); break;
                    case "delete_employee": response = handlerAdminCustomerAcc.HandleDeleteEmployee(obj.data); break;
                    case "GET_ALL_COMPUTERS_ADMIN": response = adminComputerHandler.HandleGetAllComputers(); break;
                    case "GET_ALL_CUSTOMERS": response = handlerCustomerHandler.HandleGetAllCustomers(); break;
                    case "ADD_CUSTOMER": response = handlerCustomerHandler.HandleAddCustomer(obj.data); break;
                    case "UPDATE_CUSTOMER": response = handlerCustomerHandler.HandleUpdateCustomer(obj.data); break;
                    case "DELETE_CUSTOMER": response = handlerCustomerHandler.HandleDeleteCustomer(obj.data); break;
                    case "DEPOSIT_FUNDS": response = handlerCustomerHandler.HandleDeposit(obj.data); break;
                    case "SEARCH_CUSTOMER": response = handlerCustomerHandler.HandleSearchCustomer(obj.data); break;
                    case "get_all_categories": response = handlerFood.HandleGetAllCategories(); break;
                    case "get_all_food": response = handlerFood.HandleGetAllFood(); break;
                    case "create_invoice": response = handlerFood.HandleCreateInvoice(obj.data); break;
                    case "create_invoice_detail": response = handlerFood.HandleCreateInvoiceDetail(obj.data); break;
                    case "get_max_invoice_id": response = handlerFood.HandleGetMaxInvoiceId(); break;
                    case "get_max_invoice_detail_id": response = handlerFood.HandleGetMaxInvoiceDetailId(); break;
                    case "get_invoices_in_session": response = handlerFood.HandleLoadInvoiceInSession(); break;
                    case "get_invoices_details": response = handlerFood.HandleLoadInvoiceDetail(); break;
                    case "CONTROL_PC": response = HandleComputerControl(obj.data); break;
                    case "END_SESSION": response = HandleComputerControl(obj.data); break;
                    case "create_invoice_detail_top_up": response = handlerFood.HandleCreateInvoiceDetailTopUp(obj.data); break;
                    case "start_session":
                        response = handlerCustomer.HandleStartSession(obj);
                        break;
                    case "update_session":
                        response = handlerCustomer.HandleUpdateSession(obj);
                        break;
                    case "end_session":
                        response = handlerCustomer.HandleEndSession(obj);
                        break;
                    case "GET_ALL_COMPUTERS":
                        response = computerHandler.HandleGetAllComputers();
                        break;
                    case "UPDATE_COMPUTER_STATUS":
                        response = computerHandler.HandleUpdateStatus(obj.data);
                        break;
                    case "DELETE_COMPUTER":
                        response = adminComputerHandler.HandleDeleteComputer(obj.data);
                        break;
                    case "ADD_COMPUTER":
                        response = adminComputerHandler.HandleAddComputer(obj.data);
                        break;
                    case "UPDATE_COMPUTER": 
                        response = adminComputerHandler.HandleUpdateComputer(obj.data);
                        break;
                    case "GET_COMPUTER_DETAILS": 
                        response = adminComputerHandler.HandleGetComputerDetails(obj.data);
                        break;
                    default: response = new { status = "error", message = $"Unknown action: {action}" }; break;
                }

                SendResponse(ns, response);
            }
            catch (JsonReaderException jsonEx)
            {
                Console.WriteLine($"JSON parse error in business request: {jsonEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing business request: {ex.Message}");
            }
        }
        private void RegisterClientStaff(string clientName, TcpClient client)
        {
            if (ClientConnections.ContainsKey(clientName))
            {
                Console.WriteLine($"[REGISTER] WARNING: Ten may {clientName} da ton tai. Thay the ket noi cu.");
                if (ClientConnections.TryRemove(clientName, out TcpClient oldClient))
                {
                    try { oldClient.Close(); } catch { }
                }
            }

            ClientConnections.TryAdd(clientName, client);
            Console.WriteLine($"[REGISTER] May tram {clientName} đa đang ky. Tong so: {ClientConnections.Count}");
        }

        private void DeregisterClientStaff(string clientName, TcpClient client)
        {
            if (ClientConnections.TryRemove(clientName, out TcpClient removedClient))
            {
                try { removedClient.Close(); } catch { }
                Console.WriteLine($"[DEREGISTER] May tram {clientName} đa ngat ket noi. Tong so: {ClientConnections.Count}");
            }
            else
            {
                try { client.Close(); } catch { }
            }
        }
        private void ListenForClientStaffCommands(string clientName, TcpClient client, NetworkStream ns)
        {
            ns.ReadTimeout = System.Threading.Timeout.Infinite;
            byte[] buffer = new byte[256];
            int bytesRead;

            try
            {
                while (client.Connected)
                {
                    if (ns.DataAvailable)
                    {
                        bytesRead = ns.Read(buffer, 0, buffer.Length);
                        if (bytesRead == 0) break;

                        string data = Encoding.UTF8.GetString(buffer, 0, bytesRead).Trim();
                        Console.WriteLine($"Client Staff {clientName} gui: {data}");
                        HandleBusinessRequest(ns, data);
                    }
                    else
                    {
                        Thread.Sleep(500);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Loi trong qua trinh lang nghe may tram {clientName}: {ex.Message}");
            }
        }
        public bool SendCommandToClientStaff(string clientName, string command)
        {
            if (ClientConnections.TryGetValue(clientName, out TcpClient client))
            {
                try
                {
                    NetworkStream ns = client.GetStream();
                    byte[] msg = Encoding.UTF8.GetBytes(command);
                    ns.Write(msg, 0, msg.Length);
                    Console.WriteLine($"Đã gửi lệnh '{command}' đến máy trạm: {clientName}");
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Lỗi gửi lệnh đến {clientName}: {ex.Message}. Ngắt kết nối.");

                    DeregisterClientStaff(clientName, client);
                    return false;
                }
            }
            Console.WriteLine($"Không tìm thấy máy trạm với Tên máy: {clientName}");
            return false;
        }

        private void SendResponse(NetworkStream ns, object obj)
        {
            string json = JsonConvert.SerializeObject(obj);
            byte[] sendData = Encoding.UTF8.GetBytes(json);
            ns.Write(sendData, 0, sendData.Length);
        }

        // Chia ra project khác sau

        /* Test bằng
curl -X POST -H "Content-Type: application/json" -d "{\"action\":\"paid\",\"data\":{\"amount\":1000,\"accountName\":\"NhatAnh\",\"addInfo\":\"Số hóa đơn\"}}" http://localhost:5000/
        */

        public void StartHttp(int httpPort)
        {
            IPAddress localAddress = null;
            foreach (IPAddress address in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
            {
                if (address.AddressFamily == AddressFamily.InterNetwork)
                {
                    localAddress = address;
                    break;
                }
            }
            string prefix = $"http://+:{httpPort}/";
            HttpListener http = new HttpListener();
            http.Prefixes.Add(prefix);

            try
            {
                http.Start();
                Console.WriteLine($"http://{localAddress}:{httpPort}/");
                Console.WriteLine($"HTTP server started on port {httpPort} (prefix: {prefix})");
                Console.WriteLine("Để test HTTP notification ta xài thử lệnh như sau:");
                Console.WriteLine("curl -X POST -H \"Content-Type: application/json\" -d \"{\\\"action\\\":\\\"paid\\\",\\\"data\\\":{\\\"amount\\\":1000,\\\"accountName\\\":\\\"NhatAnh\\\",\\\"addInfo\\\":\\\"Số hóa đơn\\\"}}\" http://localhost:5000/");
            }
            catch (HttpListenerException hlex)
            {
                Console.WriteLine($"HttpListener start failed: {hlex.Message}");
                Console.WriteLine($"Lỗi thì mở port bằng quyền admin bằng lệnh sau trên cmd:\nnetsh http add urlacl url=http://+:{httpPort}/ user=Everyone"
            );
                throw;
            }

            while (true)
            {
                HttpListenerContext ctx = http.GetContext();
                ThreadPool.QueueUserWorkItem(_ => HandleHttpContext(ctx));
            }
        }

        private void HandleHttpContext(HttpListenerContext ctx)
        {
            try
            {
                HttpListenerRequest req = ctx.Request;
                HttpListenerResponse resp = ctx.Response;

                if (req.HttpMethod != "POST")
                {
                    resp.StatusCode = (int)HttpStatusCode.MethodNotAllowed;
                    resp.Close();
                    return;
                }

                string body;
                using (var reader = new StreamReader(req.InputStream, req.ContentEncoding))
                {
                    body = reader.ReadToEnd();
                }

                Console.WriteLine($"HTTP Received: {body}");

                object responseObj = ProcessRequestString(body);

                string json = JsonConvert.SerializeObject(responseObj);
                byte[] data = Encoding.UTF8.GetBytes(json);

                resp.ContentType = "application/json";
                resp.ContentEncoding = Encoding.UTF8;
                resp.ContentLength64 = data.Length;
                resp.OutputStream.Write(data, 0, data.Length);
                resp.OutputStream.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in HandleHttpContext: {ex.Message}");
                try
                {
                    ctx.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    ctx.Response.Close();
                }
                catch { }
            }
        }

        private object ProcessRequestString(string request)
        {
            if (string.IsNullOrWhiteSpace(request))
                return new { status = "error", message = "Empty request" };

            try
            {
                dynamic obj = JsonConvert.DeserializeObject(request);
                if (obj?.action == null)
                {
                    return new { status = "error", message = "Missing 'action' field" };
                }

                string action = obj.action.ToString();
                object response;

                switch (action)
                {
                    case "paid":
                        {
                            object handle = handlerNotification.HandleNotification(obj);
                            try
                            {
                                dynamic handleMessage = handle;
                                var notificationObj = handleMessage.notification;
                                string notificationJson = JsonConvert.SerializeObject(notificationObj);
                                string message = "NOTIFICATION|" + notificationJson;

                                bool sentToAny = false;

                                if (ClientConnections.ContainsKey("Staff"))
                                {
                                    sentToAny = SendCommandToClientStaff("Staff", message);
                                }

                                if (!sentToAny)
                                {
                                    bool any = BroadcastToAllStaff(message);
                                    if (!any)
                                    {
                                        Console.WriteLine("No connected staff clients to deliver notification.");
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error while sending notification to clients: {ex.Message}");
                            }

                            response = handle;
                        }
                        break;
                    default:
                        response = new { status = "error", message = $"Unknown HTTP action: {action}" };
                        break;
                }

                return response;
            }
            catch (Exception ex)
            {
                return new { status = "error", message = $"JSON Error: {ex.Message}" };
            }
        }

        private bool BroadcastToAllStaff(string command)
        {
            bool sentAny = false;
            foreach (var kv in ClientConnections)
            {
                string clientName = kv.Key;
                try
                {
                    if (SendCommandToClientStaff(clientName, command))
                    {
                        sentAny = true;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Broadcast error to {clientName}: {ex.Message}");
                }
            }
            return sentAny;
        }
    }
}
