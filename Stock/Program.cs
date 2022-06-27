using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Commands.Messages;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NServiceBus;


namespace Stock
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>

            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();

                })
                 .UseNServiceBus(context =>
                 {
                     var endpointConfiguration = new EndpointConfiguration("Stock");
                     var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
                     transport.UseConventionalRoutingTopology();    
                     transport.ConnectionString("host=rabbitmq;port=5672");
                     endpointConfiguration.EnableInstallers();
                     endpointConfiguration.EnableDurableMessages();
                     var routing = transport.Routing();
                     //routing.RouteToEndpoint(typeof(ProductRetrieved), "Orders");
                     routing.RouteToEndpoint(typeof(StockFailed), "Orders");
                     return endpointConfiguration;
                 })
                .ConfigureLogging(logging => logging.AddConsole());
    }
}
