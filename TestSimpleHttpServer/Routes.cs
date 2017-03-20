using SimpleHttpServer.Models;
using SimpleHttpServer.RouteHandlers;
using System.Collections.Generic;

namespace TestSimpleHttpServer
{
    static class Routes
    {
        public static List<Route> GET
        {
            get
            {
                return new List<Route>()
                {
                    new Route()
                    {
                        Callable = HomeIndex,
                        UrlRegex = "^\\/$",
                        Method = "GET"
                    },
                    new Route()
                    {
                        Callable = new FileSystemRouteHandler() { BasePath = @"C:\Users\Barend.Erasmus\Desktop\Test"}.Handle,
                        UrlRegex = "^\\/Static\\/(.*)$",
                        Method = "GET"
                    }
                };

            }
        }

        private static HttpResponse HomeIndex(HttpRequest request)
        {
            return new HttpResponse()
            {
                ContentAsUTF8 = "Hello",
                ReasonPhrase = "OK",
                StatusCode = "200"
            };

        }
    }
}
