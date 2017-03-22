using SimpleHttpServer.Models;
using System.IO;
using MyResources = global::SimpleHttpServer.Properties.Resources;

//  エラー表示元のHTMLをリソースへ置くようにした。
//
namespace SimpleHttpServer
{
    class HttpBuilder
    {
        public static HttpResponse InternalServerError()
        {
            string content = MyResources.InternalServerError; // 変更

            return new HttpResponse()
            {
                ReasonPhrase = "InternalServerError",
                StatusCode = "500",
                ContentAsUTF8 = content
            };
        }

        public static HttpResponse NotFound()
        {
            string content = MyResources.NotFound; // 変更

            return new HttpResponse()
            {
                ReasonPhrase = "NotFound",
                StatusCode = "404",
                ContentAsUTF8 = content
            };
        }
    }
}
