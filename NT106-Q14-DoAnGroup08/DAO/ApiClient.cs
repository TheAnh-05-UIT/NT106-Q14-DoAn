using Newtonsoft.Json;
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
        using (var client = new TcpClient())
        {
            client.Connect(host, port);
            using (var ns = client.GetStream())
            {
                string json = JsonConvert.SerializeObject(obj);
                byte[] data = Encoding.UTF8.GetBytes(json);
                ns.Write(data, 0, data.Length);

                // read response
                byte[] buffer = new byte[8192];
                int read = ns.Read(buffer, 0, buffer.Length);
                if (read == 0) return null;
                string response = Encoding.UTF8.GetString(buffer, 0, read);
                return JsonConvert.DeserializeObject(response);
            }
        }
    }
}
