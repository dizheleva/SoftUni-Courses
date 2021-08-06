using MyWebServer.Server;
using System.Threading.Tasks;

namespace MyWebServer
{
    public class StartUp
    {
        public static async Task Main()
        {
            var address = "127.0.0.1";
            var port = 90;

            var server = new HttpServer(address, port);

            await server.Start();
        }
    }
}
