using SimpleHttpServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SimpleHttpServer.RouteHandlers
{
    public class TestHandler
    {
        public HttpResponse Handle(HttpRequest request, string view)
        {
            var response = new HttpResponse();
            response.StatusCode = "200";
            response.ReasonPhrase = "Ok";
            response.Headers["Content-Type"] = "text/html;charset=UTF8";
            var viewdata = File.ReadAllText(view);
            var buff = viewdata.Replace("{Method}", request.Method); //, request.Url, request.Path, request.Content, request.Route.Name);
            buff = buff.Replace("{Url}", request.Url);
            buff = buff.Replace("{Path}", request.Path);
            buff = buff.Replace("{Content}", request.Content==null?"Null":request.Content);
            buff = buff.Replace("{Route.Name}", request.Route.Name==null?"Null":request.Route.Name);
            response.Content = Encoding.UTF8.GetBytes(buff);
            return response;
        }
    }
}
