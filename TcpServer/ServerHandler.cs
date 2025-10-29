using Newtonsoft.Json;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace TcpServer
{
    public class ServerHandler
    {
        private TcpListener listener;
        private DatabaseHelper db;

        public ServerHandler(string connStr)
        {
            db = new DatabaseHelper(connStr);
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
                NetworkStream ns = client.GetStream();
                byte[] buffer = new byte[4096];
                int bytesRead = ns.Read(buffer, 0, buffer.Length);

                if (bytesRead == 0)
                {
                    Console.WriteLine("Client disconnected before sending data.");
                    return;
                }

                string request = Encoding.UTF8.GetString(buffer, 0, bytesRead).Trim();
                Console.WriteLine($"Received raw data: {request}");

                dynamic obj = null;
                try
                {
                    obj = JsonConvert.DeserializeObject(request);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"JSON parse error: {ex.Message}");
                }

                if (obj == null)
                {
                    Console.WriteLine("Received invalid or empty JSON!");
                    SendResponse(ns, new { status = "error", message = "Invalid JSON format" });
                    return;
                }

                // Kiểm tra có tồn tại action không
                if (obj.action == null)
                {
                    SendResponse(ns, new { status = "error", message = "Missing 'action' field" });
                    return;
                }

                string action = obj.action.ToString();
                Console.WriteLine($"Action: {action}");

                string response = "";

                if (action == "login")
                {
                    string username = (string)obj.username;
                    string password = (string)obj.password;

                    string query = "SELECT * FROM Users WHERE Username=@u AND [Password]=@p";
                    SqlParameter[] prms = {
                        new SqlParameter("@u", username),
                        new SqlParameter("@p", password)
                    };

                    DataTable dt = db.ExecuteQuery(query, prms);
                    if (dt.Rows.Count > 0)
                    {
                        string role = dt.Rows[0]["Role"].ToString();
                        response = JsonConvert.SerializeObject(new { status = "success", role = role });
                    }
                    else
                    {
                        response = JsonConvert.SerializeObject(new { status = "fail" });
                    }
                }
                else
                {
                    response = JsonConvert.SerializeObject(new { status = "error", message = "Unknown action" });
                }

                SendResponse(ns, response);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                client.Close();
            }
        }

        private void SendResponse(NetworkStream ns, object obj)
        {
            string json = obj is string ? (string)obj : JsonConvert.SerializeObject(obj);
            byte[] sendData = Encoding.UTF8.GetBytes(json);
            ns.Write(sendData, 0, sendData.Length);
        }
    }
}