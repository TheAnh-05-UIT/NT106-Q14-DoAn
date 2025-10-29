using System;
using System.Net.Sockets;
using System.Text;

namespace NT106_Q14_DoAnGroup08.DAO
{
    public class RequestData
    {
        private string serverIP = "127.0.0.1";
        private int serverPort = 8080;

        public string SendRequest(string json)
        {
            using (TcpClient client = new TcpClient(serverIP, serverPort))
            {
                NetworkStream ns = client.GetStream();
                byte[] data = Encoding.UTF8.GetBytes(json);
                ns.Write(data, 0, data.Length);

                byte[] buffer = new byte[1024];
                int bytesRead = ns.Read(buffer, 0, buffer.Length);
                string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                return response;
            }
        }
    }
}