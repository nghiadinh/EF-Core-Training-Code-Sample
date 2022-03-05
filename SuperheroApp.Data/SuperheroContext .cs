using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SuperheroApp.Domain;
using System;

namespace SuperheroApp.Data
{
    public class SuperheroContext : DbContext
    {
        public DbSet<Hero> Heroes { get; set; }
        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Battle> Battles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Data Source=HSSSC1LAP03043\\SQLEXPRESS;Initial Catalog=SuperheroAppDb;Integrated Security=True")
                .UseLazyLoadingProxies()
                .LogTo(Console.WriteLine,new[] {
                    DbLoggerCategory.Model.Name,
                    DbLoggerCategory.Database.Command.Name,
                    DbLoggerCategory.Database.Transaction.Name,
                    DbLoggerCategory.Query.Name,
                    DbLoggerCategory.ChangeTracking.Name,
                },
                       LogLevel.Information)
                .EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Hero>()
                .Property(q => q.RealName)
                .HasColumnName("FullName");
        }
    }
}
