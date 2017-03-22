using System;
using SimpleHttpServer.Models;
using SimpleHttpServer.RouteHandlers;
using System.Collections.Generic;

namespace TestSimpleHttpServer
{
    public static class Routes
    {
        /// <summary>
        /// ルートの定義
        /// </summary>
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
                        // http://localhost:5411/Satic/test.html
                        Callable = new FileSystemRouteHandler() { BasePath = @"C:\workspace\project2017\SimpleHttpServer"}.Handle,
                        UrlRegex = "^\\/Static\\/(.*)$",
                        Method = "GET"
                    }
                };

            }
        }

        /// <summary>
        /// ホーム (http://localhost:5411)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private static HttpResponse HomeIndex(HttpRequest request)
        {
            return new HttpResponse()
            {
                ContentAsUTF8 = "Hello " + DateTime.Now.ToString("HH:mm:ss"),
                ReasonPhrase = "OK",
                StatusCode = "200"
            };

        }
    }
}
