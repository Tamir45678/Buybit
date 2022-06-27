using Microsoft.EntityFrameworkCore;
using Stock.Entities;

namespace Stock.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
    }
}
