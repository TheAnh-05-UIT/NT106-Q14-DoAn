using System;
using System.Configuration;
using System.Threading;
using TcpServer.ServerHandler;

namespace TcpServer
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Starting TCP Server...");
            string connStr = ConfigurationManager.ConnectionStrings["QuanLyQuanNet"].ConnectionString;
            ServerHandler.ServerHandler server = new ServerHandler.ServerHandler(connStr);
            // Example options: enable HTTPS (requires cert bound with netsh), set an API key, and tighten limits.
            var httpOptions = new HttpServerOptions
            {
                UseHttps = false, // set to true if you have bound a certificate for the port
                ApiKey = "NET56784516723", // set your API key here to require clients to send X-Api-Key
                MaxRequestBodyBytes = 16 * 1024,
                RequireContentTypeJson = true
            };
            var http = new TcpServer.ServerHandler.HttpServer(server, 5000, httpOptions);
            Thread tcpThread = new Thread(() =>
            {
                try
                {
                    server.Start(8080);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error starting TCP listener: {ex.Message}");
                }
            })
            { IsBackground = true };
            tcpThread.Start();

            // Lỗi thì mở port bằng quyền admin bằng lệnh sau trên cmd:
            // netsh http add urlacl url=http://+:5000/ user=Everyone
            try
            {
               new Thread(() => http.Start()).Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error starting HTTP listener: {ex.Message}");
            }
        }
    }
}