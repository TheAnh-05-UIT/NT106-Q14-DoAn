using System;

namespace TcpServer.ServerHandler
{
    // netsh http add sslcert ipport=0.0.0.0:5000 certhash= appid={}
    // netsh http add urlacl url=https://+:5000/ user=Everyone
    public class HttpServerOptions
    {
        // If true, use "https" prefix. Note: certificate must be bound at OS level (netsh) for HttpListener to serve HTTPS.
        public bool UseHttps { get; set; } = false;

        // Optional certificate thumbprint used for documentation/logging. Binding must be done externally (netsh http add sslcert ...)
        public string CertificateThumbprint { get; set; } = null;

        // Optional API key required in header "X-Api-Key" (or "Authorization: ApiKey <key>"). If null or empty, API key is not required.
        public string ApiKey { get; set; } = null;

        // Maximum request body size in bytes (default 10KB)
        public int MaxRequestBodyBytes { get; set; } = 10 * 1024;

        // Require Content-Type application/json
        public bool RequireContentTypeJson { get; set; } = true;

        // Simple per-IP rate limiting
        public int RateLimitRequests { get; set; } = 60;
        public TimeSpan RateLimitPeriod { get; set; } = TimeSpan.FromMinutes(1);
    }
}
