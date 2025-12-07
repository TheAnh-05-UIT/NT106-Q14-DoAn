using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class NotifyClient
{
    private static NotifyClient _instance;

    public static NotifyClient Instance
    {
        get
        {
            if (_instance == null)
                _instance = new NotifyClient(ServerConfig.Host, ServerConfig.Port);
            return _instance;
        }
    }

    private readonly string host;
    private readonly int port;

    private Thread workerThread;
    private volatile bool running;
    private TcpClient client;
    private string staffId;

    public event Action<dynamic> NotificationReceived;
    public event Action<string, string> MessageReceived;

    public NotifyClient(string host, int port)
    {
        this.host = host;
        this.port = port;
    }

    internal static void Reconfigure()
    {
        _instance = new NotifyClient(ServerConfig.Host, ServerConfig.Port);
    }

    public void Start(string staffId)
    {
        if (workerThread != null && workerThread.IsAlive) return;
        this.staffId = staffId ?? "";
        running = true;
        workerThread = new Thread(Run) { IsBackground = true };
        workerThread.Start();
    }

    public void Stop()
    {
        running = false;
        try { client?.Close(); } catch { }
        try
        {
            if (workerThread != null && workerThread.IsAlive)
            {
                if (!workerThread.Join(2000))
                    workerThread.Abort();
            }
        }
        catch { }
    }

    private void Run()
    {
        while (running)
        {
            try
            {
                client = new TcpClient();
                client.Connect(host, port);

                using (var ns = client.GetStream())
                {
                    byte[] nameBytes = Encoding.UTF8.GetBytes(staffId);
                    ns.Write(nameBytes, 0, nameBytes.Length);

                    byte[] buffer = new byte[8192];
                    while (running && client.Connected)
                    {
                        int bytesRead = 0;
                        try
                        {
                            bytesRead = ns.Read(buffer, 0, buffer.Length);
                            if (bytesRead == 0) break;
                        }
                        catch (Exception)
                        {
                            break;
                        }

                        string msg = Encoding.UTF8.GetString(buffer, 0, bytesRead).Trim();
                        if (string.IsNullOrEmpty(msg)) continue;

                        if (msg.StartsWith("NOTIFICATION|"))
                        {
                            string payload = msg.Substring("NOTIFICATION|".Length);
                            try
                            {
                                dynamic notif = JsonConvert.DeserializeObject(payload);
                                NotificationReceived?.Invoke(notif);

                                try
                                {
                                    string content = notif.content != null ? notif.content.ToString() : "";
                                }
                                catch { }
                            }
                            catch (Exception ex)
                            {
                                Debug.WriteLine("Failed parse notification payload: " + ex.Message);
                            }
                        }
                        else
                        {
                            Debug.WriteLine("NotifyClient received: " + msg);
                        }
                    }
                }
            }
            catch (SocketException ex)
            {
                Debug.WriteLine("NotifyClient socket error: " + ex.Message);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("NotifyClient error: " + ex.Message);
            }
            finally
            {
                try { client?.Close(); } catch { }
            }

            if (running)
            {
                Thread.Sleep(3000);
            }
        }
    }
}
