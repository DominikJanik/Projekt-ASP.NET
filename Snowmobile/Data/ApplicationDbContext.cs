using Microsoft.EntityFrameworkCore;
using SnowmobileShop.Models;

namespace SnowmobileShop.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Snowmobile> Snowmobiles { get; set; }
        public DbSet<SnowmobileType> SnowmobileTypes { get; set; }
    }
}
