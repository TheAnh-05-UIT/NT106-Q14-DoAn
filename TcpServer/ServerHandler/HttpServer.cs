using Guna.UI2.WinForms;
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
curl -X POST -H "Content-Type: application/json" -H "X-Api-Key: NET56784516723" -d "{\"action\":\"paid\",\"type\":\"123\",\"data\":{\"amount\":1000,\"accountName\":\"NhatAnh\",\"addInfo\":\"Số hóa đơn\"}}" http://localhost:5000/*/
    public class HttpServer
    {
        private readonly ServerHandler serverHandler;
        private readonly int port;
        private readonly HttpServerOptions options;

        public HttpServer(ServerHandler serverHandler, int port) : this(serverHandler, port, new HttpServerOptions()) { }

        public HttpServer(ServerHandler serverHandler, int port, HttpServerOptions options)
        {
            this.serverHandler = serverHandler ?? throw new ArgumentNullException(nameof(serverHandler));
            this.port = port;
            this.options = options ?? new HttpServerOptions();
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

            string scheme = options.UseHttps ? "https" : "http";
            string prefix = $"{scheme}://+:{port}/";
            HttpListener http = new HttpListener();
            http.Prefixes.Add(prefix);

            try
            {
                http.Start();
                Console.WriteLine($"{scheme}://{localAddress}:{port}/");
                Console.WriteLine($"HTTP server started on port {port} (prefix: {prefix})");
                if (!string.IsNullOrEmpty(options.CertificateThumbprint))
                {
                    Console.WriteLine($"HTTPS certificate thumbprint (configured): {options.CertificateThumbprint}");
                }
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

        // Simple in-memory rate limiting (per-IP)
        private readonly System.Collections.Concurrent.ConcurrentDictionary<string, (int count, DateTime windowStart)> rateLimits
            = new System.Collections.Concurrent.ConcurrentDictionary<string, (int, DateTime)>();

        private void HandleHttpContext(HttpListenerContext ctx)
        {
            HttpListenerRequest req = ctx.Request;
            HttpListenerResponse resp = ctx.Response;

            void ResponseSend(string d)
            {
                byte[] data = Encoding.UTF8.GetBytes(d);

                resp.ContentType = "application/json";
                resp.ContentEncoding = Encoding.UTF8;
                resp.ContentLength64 = data.Length;
                resp.OutputStream.Write(data, 0, data.Length);
                resp.OutputStream.Close();
            }

            try
            {
                if (req.HttpMethod != "POST")
                {
                    resp.StatusCode = (int)HttpStatusCode.MethodNotAllowed;
                    resp.Close();
                    return;
                }

                // Rate limiting by remote endpoint
                string remote = ctx.Request.RemoteEndPoint?.Address.ToString() ?? "unknown";
                var now = DateTime.UtcNow;
                var rl = rateLimits.GetOrAdd(remote, _ => (0, now));
                if (now - rl.windowStart > options.RateLimitPeriod)
                {
                    rl = (0, now);
                }
                rl.count++;
                rateLimits[remote] = rl;
                if (rl.count > options.RateLimitRequests)
                {
                    // HttpStatusCode.TooManyRequests not available in older frameworks; use numeric 429
                    resp.StatusCode = 429; // Too Many Requests
                    ResponseSend("{\"status\":\"error\",\"message\":\"Rate limit exceeded\"}");
                    resp.Close();
                    Console.WriteLine($"Rate limit exceeded for {remote}");
                    return;
                }

                // Content-Type validation
                if (options.RequireContentTypeJson)
                {
                    string contentType = req.ContentType ?? string.Empty;
                    if (!contentType.StartsWith("application/json", StringComparison.OrdinalIgnoreCase))
                    {
                        resp.StatusCode = (int)HttpStatusCode.UnsupportedMediaType;
                        ResponseSend("{\"status\":\"error\",\"message\":\"Content-Type must be application/json\"}");
                        resp.Close();
                        return;
                    }
                }

                // API Key validation
                if (!string.IsNullOrEmpty(options.ApiKey))
                {
                    string apiKey = req.Headers["X-Api-Key"];
                    if (string.IsNullOrEmpty(apiKey))
                    {
                        string auth = req.Headers["Authorization"];
                        if (!string.IsNullOrEmpty(auth) && auth.StartsWith("ApiKey ", StringComparison.OrdinalIgnoreCase))
                        {
                            apiKey = auth.Substring(7).Trim();
                        }
                    }

                    if (apiKey != options.ApiKey)
                    {
                        resp.StatusCode = (int)HttpStatusCode.Unauthorized;
                        ResponseSend("{\"status\":\"error\",\"message\":\"Invalid API key\"}");
                        resp.Close();
                        return;
                    }
                }

                // Read body with size limit
                string body;
                using (var ms = new MemoryStream())
                {
                    req.InputStream.CopyTo(ms);
                    if (ms.Length > options.MaxRequestBodyBytes)
                    {
                        resp.StatusCode = (int)HttpStatusCode.RequestEntityTooLarge;
                        ResponseSend("{\"status\":\"error\",\"message\":\"Request body too large\"}");
                        resp.Close();
                        return;
                    }
                    ms.Position = 0;
                    using (var reader = new StreamReader(ms, req.ContentEncoding))
                    {
                        body = reader.ReadToEnd();
                    }
                }

                Console.WriteLine($"HTTP Received from {remote}: {body}");

                object responseObj = ProcessRequestString(body);

                string json = JsonConvert.SerializeObject(responseObj);

                ResponseSend(json);
            }
            catch (JsonException jex)
            {
                Console.WriteLine($"JSON error: {jex.Message}");
                try
                {
                    var err = JsonConvert.SerializeObject(new { status = "error", message = "Invalid JSON" });
                    byte[] d = Encoding.UTF8.GetBytes(err);
                    resp.StatusCode = (int)HttpStatusCode.BadRequest;
                    resp.ContentType = "application/json";
                    resp.ContentEncoding = Encoding.UTF8;
                    resp.ContentLength64 = d.Length;
                    resp.OutputStream.Write(d, 0, d.Length);
                    resp.OutputStream.Close();
                }
                catch { }
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
                            object handle = serverHandler.notifyToStaff(obj);
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
    }
}
