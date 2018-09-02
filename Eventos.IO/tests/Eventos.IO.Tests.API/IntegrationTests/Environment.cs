using System;
using System.IO;
using System.Net.Http;
using Eventos.IO.Services.Api;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;

namespace Eventos.IO.Tests.API.Integration_Tests
{
    public class Environment
    {
        public static TestServer Server { get; set; }
        public static HttpClient Client { get; set; }

        public static void CriarServidor()
        {
            Server = new TestServer(
                new WebHostBuilder()
                    .UseEnvironment("Testing")
                    .UseContentRoot(Directory.GetCurrentDirectory())
                    .UseUrls("http://localhost:62263")
                    .UseStartup<StartupTests>());

            Client = Server.CreateClient();

            //BuildWebHost();
            //Client.BaseAddress = new Uri("http://localhost:62263/");
        }

        //public static IWebHost BuildWebHost() =>
        //    WebHost.CreateDefaultBuilder()
        //        .UseStartup<StartupTests>()
        //        .Build();
    }
}