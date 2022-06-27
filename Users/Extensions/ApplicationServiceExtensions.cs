using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Users.Interfaces;
using Users.Services;
using Users.Data;
using Microsoft.EntityFrameworkCore;

namespace Users.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config){
            services.AddScoped<ITokenService,TokenService>();
            var server = config["MYSQL_SERVER"] ?? "localhost";
            var mysql_password = config["MYSQL_ROOT_PASSWORD"];
            var mysql_user = config["MYSQL_USER"];
            var mysql_port = config["MYSQL_PORT"];
            var mysql_database = config["MYSQL_USERS_DATABASE"];

            string connectionString = $"Server={server};port={mysql_port} ;User Id={mysql_user};Password={mysql_password};Database={mysql_database}";
            services.AddDbContext<DataContext>(options => {
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            });
            return services;
        }
    }
}