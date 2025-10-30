using System;
using System.Configuration;

namespace TcpServer
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Starting TCP Server...");
            string connStr = ConfigurationManager.ConnectionStrings["QuanLyQuanNet"].ConnectionString;
            ServerHandler.ServerHandler server = new ServerHandler.ServerHandler(connStr);
            server.Start(8080);
        }
    }
}