﻿using Marketplace.Data;
using Marketplace.Models;
using Microsoft.EntityFrameworkCore;

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
            context.Database.Migrate();

            if (!context.Products.Any())
            {
                context.Products.AddRange(
                    new Product("Carrot", 5, "Vegetables", "data:image/jpeg;base64,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"),
                    new Product("Tomato", 5, "Vegetables", "data:image/jpeg;base64,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"),
                    new Product("TV", 1000, "Electronics", "https://images.samsung.com/is/image/samsung/levant-fhd-t5300-ua43t5300auxtw-frontblack-229857917?$2160_1728_PNG$"),
                    new Product("PS5", 1500, "Electronics", "https://pcmaster.co.il/wp-content/uploads/2022/02/ps5-credit-sie@2000x1270.jpg"),
                    new Product("Beef", 80, "Food", "https://cdn.britannica.com/68/143268-050-917048EA/Beef-loin.jpg"),
                    new Product("Tuna", 5, "Food", "data:image/jpeg;base64,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"),
                    new Product("Choclate", 5, "Candy", "https://upload.wikimedia.org/wikipedia/commons/7/70/Chocolate_%28blue_background%29.jpg"));
                context.SaveChanges();
            }
        }
    }
}
