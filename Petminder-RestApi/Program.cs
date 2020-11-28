using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Petminder_RestApi
{
    public class Program
    {
        private static IConfigurationRoot configuration;
        public static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false);

            configuration = builder.Build();

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    //webBuilder.UseUrls($"{configuration["Http"]}:{configuration["Port"]};{configuration["Https"]}:{Convert.ToInt32(configuration["Port"])+1}");
                    //webBuilder.UseUrls($"http://192.168.1.90:3000;https://192.168.1.90:3001");
                });
    }
}
