public static class ServerConfig
{
    private static string host = "192.168.112.171";
    private static int port = 8080;

    public static string Host => host;
    public static int Port => port;

    public static void Configure(string newHost, int newPort)
    {
        host = newHost ?? host;
        port = newPort;

        ApiClient.Reconfigure();
        NotifyClient.Reconfigure();
    }
}
