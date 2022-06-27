using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NServiceBus;
using Commands.Messages;


namespace Shipping
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
                    var endpointConfiguration = new EndpointConfiguration("Shipping");
                    var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
                    transport.UseConventionalRoutingTopology();
                    transport.ConnectionString("host = rabbitmq;port = 5672");
                    endpointConfiguration.EnableInstallers();
                    endpointConfiguration.EnableDurableMessages();
                    var persistance = endpointConfiguration.UsePersistence<LearningPersistence>();
                    var routing = transport.Routing();

                    return endpointConfiguration;
                })
                 .ConfigureLogging(logging => logging.AddConsole());



    }
}
