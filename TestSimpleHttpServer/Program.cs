using SimpleHttpServer;
using System;
using System.Threading;

namespace TestSimpleHttpServer
{
    /// <summary>
    /// メイン
    /// </summary>
    /// <remarks>bin\Release (or bin\Debug) に Resources\Pages\404.html, 500.html を作成しておかないとエラーになる。</remarks>
    class Program
    {
        static void Main(string[] args)
        {
            const int PORT = 5411;
            Console.WriteLine($"開始 SimpleHttpServer port={PORT} ...");
            try
            {
                HttpServer httpServer = new HttpServer(PORT, Routes.GET);

                Thread thread = new Thread(new ThreadStart(httpServer.Listen));
                thread.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("バックグランドで動作中。");
            Console.ReadKey();
        }
    }
}
