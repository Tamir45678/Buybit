using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Marketplace.Data;
using Marketplace.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
namespace Marketplace
{
    public class Startup
    {

        private IConfiguration _config { get; }
        public Startup(IConfiguration config)
        {
            _config = config;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var server = _config["MYSQL_SERVER"] ?? "localhost";
            var mysql_password = _config["MYSQL_ROOT_PASSWORD"];
            var mysql_user = _config["MYSQL_USER"];
            var mysql_port = _config["MYSQL_PORT"] ;
            var mysql_database = _config["MYSQL_MARKET_DATABASE"];

            string connectionString = $"Server={server};port={mysql_port} ;User Id={mysql_user};Password={mysql_password};Database={mysql_database}";
            services.AddDbContext<DataContext>(options => {
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            });
            services.AddControllers();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //PrepDB.PrepPopulation(app);

            app.UseHttpsRedirection();
            
            app.UseRouting();
            
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


        }
    }
}


