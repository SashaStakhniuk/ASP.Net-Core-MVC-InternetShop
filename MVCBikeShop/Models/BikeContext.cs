using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCBikeShop.Models
{
    public class BikeContext:DbContext
    {
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
        public DbSet<User> Users { get; set; }

    }
}
