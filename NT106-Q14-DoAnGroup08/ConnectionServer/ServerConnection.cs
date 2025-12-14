using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace NT106_Q14_DoAnGroup08.ConnectionServser
{
    public static class ServerConnection
    {
        public static string SendRequest(string jsonRequest)
        {
            try
            {
                using (TcpClient client = new TcpClient(ServerConfig.Host.ToString(), ServerConfig.Port))
                {
                    using (NetworkStream stream = client.GetStream())
                    {
                        // 1. Gửi dữ liệu đi
                        byte[] requestData = Encoding.UTF8.GetBytes(jsonRequest);
                        stream.Write(requestData, 0, requestData.Length);

                        // 2. Đọc phản hồi từ server
                        using (MemoryStream ms = new MemoryStream())
                        {
                            byte[] buffer = new byte[4096];
                            int bytesRead;
                            while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
                            {
                                ms.Write(buffer, 0, bytesRead);
                            }

    
                            return Encoding.UTF8.GetString(ms.ToArray());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ServerConnection Error]: {ex.Message}");
                var errorResponse = new
                {
                    status = "error",
                    message = "Không thể kết nối hoặc giao tiếp với server."
                };
                return JsonConvert.SerializeObject(errorResponse);
            }
        }
    }
}
