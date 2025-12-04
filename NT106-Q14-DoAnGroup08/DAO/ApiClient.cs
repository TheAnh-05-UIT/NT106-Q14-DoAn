using Newtonsoft.Json;
using System;
using System.Net.Sockets;
using System.Text;

public class ApiClient
{
    public static readonly ApiClient Client = new ApiClient("127.0.0.1", 8080);

    private readonly string host;
    private readonly int port;

    public ApiClient(string host, int port)
    {
        this.host = host;
        this.port = port;
    }

    public dynamic Send(object obj)
    {
        try
        {
            using (var client = new TcpClient())
            {
                client.Connect(host, port);

                using (var ns = client.GetStream())
                {
                    // ✅ Gửi JSON + \n để server biết kết thúc gói tin
                    string json = JsonConvert.SerializeObject(obj) + "\n";
                    byte[] data = Encoding.UTF8.GetBytes(json);
                    ns.Write(data, 0, data.Length);

                    // ✅ Đọc cho tới khi hết dữ liệu trả về
                    byte[] buffer = new byte[8192];
                    StringBuilder sb = new StringBuilder();

                    int bytesRead;
                    do
                    {
                        bytesRead = ns.Read(buffer, 0, buffer.Length);
                        sb.Append(Encoding.UTF8.GetString(buffer, 0, bytesRead));
                    }
                    while (ns.DataAvailable);

                    string response = sb.ToString();
                    if (string.IsNullOrWhiteSpace(response)) return null;

                    return JsonConvert.DeserializeObject(response);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("API ERROR: " + ex.Message);
            return null;
        }
    }
}
