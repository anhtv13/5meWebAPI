using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.SelfHost;

namespace WebApi
{
    class Program
    {
        static void Main(string[] args)
        {
            //web api selfhost using console app
            string baseAddress = ConfigurationManager.AppSettings["baseAddress"];
            baseAddress = "http://localhost:8888";
            var config = new HttpSelfHostConfiguration(baseAddress);
            config.MaxReceivedMessageSize = 2147483647;
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
            config.Routes.MapHttpRoute(
              name: "API",
              routeTemplate: "{controller}/{action}/{id}",
              defaults: new { id = RouteParameter.Optional }
                );
            HttpSelfHostServer server = new HttpSelfHostServer(config);
            server.OpenAsync().Wait();
            Console.WriteLine("Starts successfully: " + baseAddress);
            Console.WriteLine("Press Enter to quit.");
            Console.ReadLine();
            server.CloseAsync().Wait();
        }
    }
}
