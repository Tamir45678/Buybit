using Marketplace.Models;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
    }
}
