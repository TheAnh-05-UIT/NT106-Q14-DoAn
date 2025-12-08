using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace TcpServer.ServerHandler
{
    /* Test bằng
curl -X POST -H "Content-Type: application/json" -d "{\"action\":\"paid\",\"data\":{\"amount\":1000,\"accountName\":\"NhatAnh\",\"addInfo\":\"Số hóa đơn\"}}" http://localhost:5000/
*/
    public class HttpServer
    {
        private readonly ServerHandler serverHandler;
        private readonly int port;

        public HttpServer(ServerHandler serverHandler, int port)
        {
            this.serverHandler = serverHandler ?? throw new ArgumentNullException(nameof(serverHandler));
            this.port = port;
        }

        public void Start()
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

            string prefix = $"http://+:{port}/";
            HttpListener http = new HttpListener();
            http.Prefixes.Add(prefix);

            try
            {
                http.Start();
                Console.WriteLine($"http://{localAddress}:{port}/");
                Console.WriteLine($"HTTP server started on port {port} (prefix: {prefix})");
            }
            catch (HttpListenerException hlex)
            {
                Console.WriteLine($"HttpListener start failed: {hlex.Message}");
                Console.WriteLine($"Run as admin or reserve URL: netsh http add urlacl url=http://+:{port}/ user=Everyone");
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
                            var handlerNotification = serverHandler.NotificationHandler;
                            object handle = handlerNotification.HandleNotification(obj);
                            try
                            {
                                dynamic handleMessage = handle;
                                var notificationObj = handleMessage.notification;
                                string notificationJson = JsonConvert.SerializeObject(notificationObj);
                                string message = "NOTIFICATION|" + notificationJson;

                                bool sentToAny = false;

                                // Try sending to named Staff connection first
                                if (ServerHandler.ClientConnections.ContainsKey("Staff"))
                                {
                                    sentToAny = serverHandler.SendCommandToClientStaff("Staff", message);
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
            foreach (var kv in ServerHandler.ClientConnections)
            {
                string clientName = kv.Key;
                try
                {
                    if (serverHandler.SendCommandToClientStaff(clientName, command))
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
