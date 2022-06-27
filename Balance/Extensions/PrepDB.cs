using Balance.Data;
using Balance.Entities;
using Microsoft.EntityFrameworkCore;

namespace Balance.Extensions
{
    public class PrepDB
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
            System.Console.WriteLine("Appling Balance Migrations");
            context.Database.Migrate();
            if (!context.UserBalances.Any())
            {
                System.Console.WriteLine("Adding Balance Data - seeding...");
                context.UserBalances.AddRange(
                    new UserBalance(1, 1000),
                    new UserBalance(2, 100));
                context.SaveChanges();
            }
        }
    }
}
