using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;

namespace MusalaGatewayProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                            .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>());
        }
    }
}
