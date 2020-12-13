using CompanyOrganizer.Core.Models;
using CompanyOrganizer.Core.Models.Enums;
using Microsoft.EntityFrameworkCore;
using System;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyOrganizer.Infrastructure
{
    public class CompanyContext :DbContext
    {
        private string _connectionString = "Server =.; Database = CompanyDataBase; Trusted_Connection = True;";
        public DbSet<Company> Companies { get; set; }
        public DbSet<Worker> Workers { get; set; }

        public CompanyContext(DbContextOptions options): base (options)
        {
            var temp = Database;
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
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}
