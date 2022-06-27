using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Commands.Messages;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NServiceBus;

namespace Marketplace
{
    public static class Program
    {

        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }


        public static IHostBuilder CreateHostBuilder(string[] args) =>

            Host.CreateDefaultBuilder(args)
             .UseNServiceBus(context =>
             {
                 var endpointConfiguration = new EndpointConfiguration("Marketplace");
                 var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
                 transport.UseConventionalRoutingTopology();
                 transport.ConnectionString("host = rabbitmq; port=5672");
                 endpointConfiguration.EnableInstallers();
                 endpointConfiguration.EnableDurableMessages();
                 var routing = transport.Routing();
                 routing.RouteToEndpoint(typeof(OrderRequest), "Orders");
                 return endpointConfiguration;
             })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();

                })
                 .ConfigureLogging(logging => logging.AddConsole());
                  



    }
}
