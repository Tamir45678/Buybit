using Balance.Entities;
using Microsoft.EntityFrameworkCore;

namespace Balance.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<UserBalance> UserBalances { get; set; }
    }
}
