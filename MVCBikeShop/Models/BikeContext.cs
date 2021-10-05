using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCBikeShop.Models
{
    public class BikeContext:DbContext
    {
        //public BikeContext()
        //{

        //}

        public BikeContext(DbContextOptions<BikeContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Bike> Bikes { get; set; }
        public DbSet<BreakType> BreakTypes { get; set; }
        public DbSet<Manufactury> Manufacturies { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Type> Types { get; set; }
        public DbSet<Base64File> Base64Files { get; set; }




        //public DbSet<User> Users { get; set; }
        //public DbSet<Role> Roles { get; set; }

        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    Role mainAdminRole = new Role { RoleId = 1, Name = "mainAdmin" };
        //    Role adminRole = new Role {RoleId=2, Name = "admin" };
        //    Role userRole = new Role { RoleId = 3,Name = "user" };

        //    User mainAdmin = new User {UserId=1,Name="Main",LastName="Admin", Email= "MainAdmin@gmail.com", Password="1",RoleId= mainAdminRole.RoleId};
        //    User admin = new User { UserId = 2, Name = "NeMain", LastName = "Admin", Email = "admin@gmail.com", Password = "1", RoleId = adminRole.RoleId };
        //    User user = new User { UserId = 3, Name = "User", LastName = "User", Email = "user@gmail.com", Password = "1", RoleId = userRole.RoleId };

        //    builder.Entity<Role>().HasData(new Role[] { mainAdminRole, adminRole, userRole });
        //    builder.Entity<User>().HasData(new User[] { mainAdmin, admin, user });
        //    base.OnModelCreating(builder);

        //}
    }
}
