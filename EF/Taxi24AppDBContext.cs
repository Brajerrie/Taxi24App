using Microsoft.EntityFrameworkCore;
using Taxi24App.Models;

namespace Taxi24App.EF
{
    public class Taxi24AppDBContext : DbContext
    {
        public Taxi24AppDBContext(DbContextOptions<Taxi24AppDBContext> options)
            : base(options)
        {
        }

        public DbSet<Configuration> Configurations { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Rider> Riders { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Trip>(entity =>
            {
                entity.Property(e => e.Discount)
                    .HasColumnType("decimal(18, 2)");
                entity.Property(e => e.SubTotal)
                    .HasColumnType("decimal(18, 2)");
                entity.Property(e => e.TaxAmount)
                    .HasColumnType("decimal(18, 2)");
                entity.Property(e => e.TotalAmount)
                    .HasColumnType("decimal(18, 2)");
                entity.Property(e => e.TotalPaid)
                    .HasColumnType("decimal(18, 2)");
            });
            
            base.OnModelCreating(modelBuilder);
            // Add your custom model configurations here
        }
    }
}
