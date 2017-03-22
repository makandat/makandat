using System;
using SimpleHttpServer.Models;
using SimpleHttpServer.RouteHandlers;
using System.Collections.Generic;
using System.IO;
using MySettings = global::HttmpServer1.Properties.Settings;

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
                // Home
                new Route()
                {
                    Callable = HomeIndex,
                    UrlRegex = "^\\/$",
                    Method = "GET"
                },
                // スタティックなコンテンツ
                new Route()
                {
                    // http://localhost:5411/Static/test.html
                    Callable = new FileSystemRouteHandler() { BasePath = MySettings.Default.StaticPath }.Handle, // 変更:設定から取得するようにした。
                    UrlRegex = "^\\/Static\\/(.*)$",
                    Method = "GET"
                },
                // 追加：
                new Route()
                {
                    Callable = Test,
                    UrlRegex = "^\\/Test\\/(.*)$",
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

    /// <summary>
    /// Test ハンドラ
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    private static HttpResponse Test(HttpRequest request)
    {
        var view = MySettings.Default.ViewPath + "\\Test.html";
        return new TestHandler().Handle(request, view);
    }
}

