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
                }
            );
        }
    }
}
