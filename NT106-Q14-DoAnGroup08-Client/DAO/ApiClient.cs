using Newtonsoft.Json;
using System;
using System.Net.Sockets;
using System.Text;

public class ApiClient
{
    private static ApiClient _client;

    public static ApiClient Client
    {
        get
        {
            if (_client == null)
                _client = new ApiClient(ServerConfig.Host, ServerConfig.Port);
            return _client;
        }
    }

    private readonly string host;
    private readonly int port;

    public ApiClient(string host, int port)
    {
        this.host = host;
        this.port = port;
    }

    internal static void Reconfigure()
    {
        _client = new ApiClient(ServerConfig.Host, ServerConfig.Port);
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
                    string json = JsonConvert.SerializeObject(obj) + "\n";
                    byte[] data = Encoding.UTF8.GetBytes(json);
                    ns.Write(data, 0, data.Length);

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
