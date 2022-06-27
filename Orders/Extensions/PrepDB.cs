﻿using Microsoft.EntityFrameworkCore;
using Orders.Data;

namespace Marketplace.Extensions
{
    public static class PrepDB
    {
        public static void PrepPopulation(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<DataContext>());
            }
        }

        public static void SeedData(DataContext context)
        {
            System.Console.WriteLine("Appling Orders Migrations");
            context.Database.Migrate();
        }
    }
}
