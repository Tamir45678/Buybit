using Stock.Data;
using Stock.Entities;
using Microsoft.EntityFrameworkCore;

namespace Stock.Extensions
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
            System.Console.WriteLine("Appling Stock Migrations");
            context.Database.Migrate();

            if (!context.Products.Any())
            {
                System.Console.WriteLine("Adding Data - seeding...");
                context.Products.AddRange(
                    new Product(1, 5000),
                    new Product(2, 5000),
                    new Product(3, 30),
                    new Product(4, 40),
                    new Product(5, 500),
                    new Product(6, 600),
                    new Product(7, 5));
                context.SaveChanges();
            }
        }
    }
}
