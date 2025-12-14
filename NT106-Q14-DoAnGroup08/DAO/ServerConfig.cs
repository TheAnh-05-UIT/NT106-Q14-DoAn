public static class ServerConfig
{
<<<<<<< HEAD
    private static string host = "192.168.112.171";
=======
    private static string host = "192.168.1.121";
>>>>>>> efaf4b3304b09576067750da2a83b92e63fab767
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
