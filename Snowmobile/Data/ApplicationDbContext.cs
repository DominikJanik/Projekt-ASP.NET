using Microsoft.EntityFrameworkCore;
using SnowmobileShop.Models;

namespace SnowmobileShop.Data
{
    public interface IApplicationDbContext
    {
        DbSet<Order> Orders { get; set; }
        DbSet<ProductLine> ProductLines { get; set; }
        DbSet<Product> Products { get; set; }
        DbSet<RentalDay> RentalDays { get; set; }
        DbSet<RentalTime> RentalHours { get; set; }
        DbSet<ShoppingCart> ShoppingCarts { get; set; }
        DbSet<Snowmobile> Snowmobiles { get; set; }
        DbSet<SnowmobileType> SnowmobileTypes { get; set; }
        DbSet<Trip> Trips { get; set; }

        public int SaveChanges();
    }

    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Snowmobile> Snowmobiles { get; set; }
        public virtual DbSet<SnowmobileType> SnowmobileTypes { get; set; }
        public virtual DbSet<Trip> Trips { get; set; }
        public virtual DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public virtual DbSet<ProductLine> ProductLines { get; set; }
        public virtual DbSet<RentalDay> RentalDays { get; set; }
        public virtual DbSet<RentalTime> RentalHours { get; set; }
        public virtual DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Snowmobile>().ToTable("Snowmobiles");
            modelBuilder.Entity<Trip>().ToTable("Trips");
        }
    }
}
