using System.Collections.Generic;

namespace MyWebServer.Server.Http
{
    public class HttpRequest
    {
        public HttpMethod Method { get; set; }

        public string Url { get; private set; }

        public Dictionary<string, HttpHeader> Headers { get; } = new Dictionary<string, HttpHeader>();

        public string Body { get; private set; }
    }
}
