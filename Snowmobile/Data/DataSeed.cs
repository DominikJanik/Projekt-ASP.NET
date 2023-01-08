using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SnowmobileShop.Models;
using System.Configuration;

namespace SnowmobileShop.Data
{
    public static class DataSeed
    {
        public static void Seed(IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var dbContext = scope.ServiceProvider.GetService<ApplicationDbContext>();
            var identityDbContext = scope.ServiceProvider.GetService<ApplicationIdentityDbContext>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            
            var pendingMigrations = dbContext.Database.GetPendingMigrations();
            if (pendingMigrations.Any())
            {
                dbContext.Database.Migrate();
            }

            dbContext.Database.EnsureCreated();

            var identityPendingMigrations = identityDbContext.Database.GetPendingMigrations();
            if (identityPendingMigrations.Any())
            {
                identityDbContext.Database.Migrate();
            }

            identityDbContext.Database.EnsureCreated();

            roleManager.CreateAsync(new IdentityRole("Admin"));
            roleManager.CreateAsync(new IdentityRole("Customer"));

            var users = userManager.Users;
            if (!users.Any())
            {
                var admin = new User()
                {
                    UserName = "admin",
                    Email = "admin@snowmobileshop.pl",
                    Forename = "admin",
                    Surname = "admin",
                    PhoneNumber = "999888777",
                    EmailConfirmed = true
                };
                var adminResult = userManager.CreateAsync(admin, "Password$1");

                if (adminResult.Result.Succeeded)
                    userManager.AddToRoleAsync(admin, "Admin");

                var customer = new User()
                {
                    UserName = "customer",
                    Email = "customer@snowmobileshop.pl",
                    Forename = "customer",
                    Surname = "customer",
                    PhoneNumber = "111222333",
                    EmailConfirmed = true
                };
                var customerResult = userManager.CreateAsync(customer, "Password$1");

                if (customerResult.Result.Succeeded)
                    userManager.AddToRoleAsync(admin, "Admin");
            }

            var snowmobileTypes = dbContext.SnowmobileTypes.ToList();
            if (!snowmobileTypes.Any())
            {
                var seed = new List<SnowmobileType>()
                {
                    new SnowmobileType { Name = "Touristic" },
                    new SnowmobileType { Name = "Mountain" },
                    new SnowmobileType { Name = "Freestyle" },
                    new SnowmobileType { Name = "Racing" }
                };

                dbContext.SnowmobileTypes.AddRange(seed);
                dbContext.SaveChanges();
            }

            var snowmobiles = dbContext.Snowmobiles.ToList();
            if (!snowmobiles.Any())
            {
                var seed = new List<Snowmobile>()
                {
                    new Snowmobile()
                    {
                        Name = "ARCTIC CAT BLAST M 4000",
                        ListPrice = 200,
                        Price = 180,
                        Description = "Aliquam et ipsum non dolor tempus fringilla. Nam purus odio, mollis quis vehicula in, blandit sit amet nibh. Phasellus mi velit, vestibulum a nisi vel, ornare semper lectus. Nunc commodo orci ut tellus pharetra posuere non id massa. Ut lacus diam, lacinia id posuere in, lacinia ac quam. Phasellus id odio bibendum, hendrerit ante nec, fermentum risus. Phasellus ac diam ac mauris posuere viverra. Cras vitae diam ac lacus pulvinar commodo ultrices eget magna. Donec ut finibus sem.",
                        Brand = "Arctic Cat",
                        Model = "BLAST M 4000",
                        Horsepower = 65,
                        EngineCapacity = "397cc",
                        YearOfProduction = 2022,
                        ImageUrl = "/img/arctic_cat.png",
                        SnowmobileTypeId = dbContext.SnowmobileTypes.Single(x => x.Name == "Freestyle").Id
                    },
                    new Snowmobile()
                    {
                        Name = "Arctic Cat M 8000 Hardcore Alpha One",
                        ListPrice = 250,
                        Price = 220,
                        Description = "Nullam ac aliquam felis. Curabitur fringilla dignissim elit eget venenatis. Ut lacinia nunc odio. Maecenas molestie iaculis neque, non porttitor ex semper sed. Donec pellentesque erat nec massa mollis gravida vitae fringilla odio. Aliquam nec nibh eu elit porta rhoncus. Quisque non consequat lorem. In sit amet vestibulum massa, vel tristique dui. Etiam id imperdiet velit, bibendum pulvinar turpis. Morbi quam nibh, aliquam sed odio ut, scelerisque tincidunt purus. Vivamus placerat elementum arcu, fermentum rutrum magna lacinia at. Sed ex elit, feugiat a gravida quis, elementum non urna.",
                        Brand = "Arctic Cat",
                        Model = "M 8000 Hardcore Alpha One",
                        Horsepower = 165,
                        EngineCapacity = "794cc",
                        YearOfProduction = 2020,
                        ImageUrl = "/img/hardcore.jpg",
                        SnowmobileTypeId = dbContext.SnowmobileTypes.Single(x => x.Name == "Mountain").Id
                    },
                };

                dbContext.Snowmobiles.AddRange(seed);
                dbContext.SaveChanges();
            }

            var trips = dbContext.Trips.ToList();
            if (!trips.Any())
            {
                var seed = new List<Trip>()
                {
                    new Trip()
                    {
                        Name = "One hour long guided snowmobile trip!",
                        Price = 200,
                        Hours = 1,
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi auctor, leo consequat faucibus iaculis, turpis libero ultricies arcu, scelerisque facilisis nisi neque nec mauris. Etiam consequat dolor a enim mattis, a varius ligula sodales. Nunc a ante dui. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Praesent aliquam interdum sem, quis lobortis lacus pellentesque non. Nulla facilisi. Quisque quis auctor nibh. Donec quis odio dignissim, faucibus felis porttitor, rutrum mi. Cras bibendum tincidunt lacinia.",
                        ImageUrl = "/img/one_hour_trip.jpg"
                    },
                    new Trip()
                    {
                        Name = "Three hours long guided snowmobile trip followed by a bonfire!",
                        Price = 500,
                        Hours = 3,
                        Description = "Proin molestie risus quis neque fermentum, non semper metus luctus. Sed facilisis convallis semper. Praesent sit amet euismod est, at sodales libero. Vestibulum molestie, leo sit amet tristique facilisis, ipsum risus mollis velit, a gravida dui odio auctor velit. Ut at nisl feugiat, iaculis sapien ut, tempus justo. Praesent ac massa ut quam efficitur vestibulum. Nullam ut felis ligula. Cras et eros fermentum, lobortis leo eget, suscipit erat. Nam ante ipsum, pulvinar ut est ac, mattis auctor erat. Sed imperdiet aliquam est. Fusce commodo nisi in erat blandit posuere. Fusce a dui erat. Donec auctor ullamcorper dui et semper. Nullam finibus fringilla odio. Suspendisse hendrerit risus a metus efficitur ullamcorper. Nulla ut ex nibh.",
                        ImageUrl = "/img/three_hour_trip.jpg"
                    },
                };

                dbContext.Trips.AddRange(seed);
                dbContext.SaveChanges();
            }
        }
    }
}
