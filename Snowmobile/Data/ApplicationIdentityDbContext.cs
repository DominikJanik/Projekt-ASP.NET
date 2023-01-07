using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SnowmobileShop.Models;

namespace SnowmobileShop.Data
{
    public interface IApplicationIdentityDbContext
    {
        DbSet<User> Users { get; set; }
    }

    public class ApplicationIdentityDbContext : IdentityDbContext<IdentityUser>, IApplicationIdentityDbContext
    {
        public ApplicationIdentityDbContext(DbContextOptions<ApplicationIdentityDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public DbSet<User> Users { get; set; }
    }
}
