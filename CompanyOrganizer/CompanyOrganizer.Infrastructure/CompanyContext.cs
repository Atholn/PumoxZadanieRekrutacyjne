using CompanyOrganizer.Core.Models;
using CompanyOrganizer.Core.Models.Enums;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyOrganizer.Infrastructure
{
    public class CompanyContext :IdentityDbContext
    {
        public DbSet<Company> Companies { get; set; }
        public DbSet<Worker> Workers { get; set; }
        public CompanyContext(DbContextOptions options): base (options)
        {
            var temp = Database;
            Database.Migrate();
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>()
                .HasMany(c => c.Workers)
                .WithOne(w => w.Company);

            modelBuilder.Entity<Worker>()
                .Property(e => e.PositionTitle)
                .HasConversion(
                v => v.ToString(),
                v => (Position)Enum.Parse(typeof(Position), v));
            base.OnModelCreating(modelBuilder);          
        }
    }
}
