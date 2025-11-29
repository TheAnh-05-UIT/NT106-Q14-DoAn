using System;
using System.Configuration;
using System.Threading;

namespace TcpServer
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Starting TCP Server...");
            string connStr = ConfigurationManager.ConnectionStrings["QuanLyQuanNet"].ConnectionString;
            ServerHandler.ServerHandler server = new ServerHandler.ServerHandler(connStr);
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
                server.StartHttp(5000);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error starting HTTP listener: {ex.Message}");
            }
        }
    }
}