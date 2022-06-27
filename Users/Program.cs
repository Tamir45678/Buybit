using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Users.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NServiceBus;
using Commands.Messages;

namespace Users
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
                var endpointConfiguration = new EndpointConfiguration("Users");
                var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
                transport.UseConventionalRoutingTopology();
                transport.ConnectionString("host = rabbitmq; port = 5672");
                endpointConfiguration.EnableInstallers();
                var routing = transport.Routing();
                //routing.RouteToEndpoint(typeof(UserCreated), "Balance");
                return endpointConfiguration;
            })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();

                })
                .ConfigureLogging(logging=>logging.AddConsole());
    }
}
