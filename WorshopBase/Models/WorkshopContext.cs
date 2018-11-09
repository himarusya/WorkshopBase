using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WorshopBase.Models
{
    public class WorkshopContext : IdentityDbContext<User>
    {
        public WorkshopContext(DbContextOptions<WorkshopContext> options) : base(options)
        {

        }

        public DbSet<Breakdown> Breakdowns { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Part> Parts { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Worker> Workers { get; set; }
    }
}
