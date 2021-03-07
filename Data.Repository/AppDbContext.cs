using Data.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        DbSet<User> Users { get; set; }
        DbSet<Dfx24b_77_lol> Dfx24b_77_lol { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User()
                {
                    Id=1,
                    Username = "admin",
                    //TODO: never ever leave this admin user here. alwyas change this after a deploy
                    Password = Utilities.Crypt.CreateMD5("admin"),
                    IsAllowed = true
                },
                new User()
                {
                    Id = 2,
                    Username = "brown-candies",
                    Password = "1",
                    IsAllowed = false
                }
            );

            modelBuilder.Entity<Dfx24b_77_lol>().HasData(
                new Dfx24b_77_lol()
                {
                    start = DateTime.UtcNow,
                    stop = DateTime.UtcNow.Subtract(new TimeSpan(2007, 4, 1)),
                    key = "hjkhyioghkgjkhgjhg",
                    value = "KHkuYIUgHguYtgIGiGkjGi"
                }
            );

            
        }
    }
}
