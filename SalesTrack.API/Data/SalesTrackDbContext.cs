using Microsoft.EntityFrameworkCore;
using SalesTrack.API.Models;
using System;

namespace SalesTrack.API.Data
{
    public class SalesTrackDbContext : DbContext
    {
        public SalesTrackDbContext(DbContextOptions<SalesTrackDbContext> options) : base(options) { }

        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<SalesUser> Users { get; set; }
        public DbSet<Sale> Sales { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Table mapping
            modelBuilder.Entity<Country>().ToTable("Countries");
            modelBuilder.Entity<City>().ToTable("Cities");
            modelBuilder.Entity<Product>().ToTable("Products");
            modelBuilder.Entity<SalesUser>().ToTable("Users");
            modelBuilder.Entity<Sale>().ToTable("Sales");

            // Column types & constraints
            modelBuilder.Entity<Sale>().Property(s => s.Amount).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Sale>().HasCheckConstraint("CK_Sales_Quantity", "[Quantity] > 0");
            modelBuilder.Entity<Sale>().HasCheckConstraint("CK_Sales_Amount", "[Amount] >= 0");

            // Seed: Countries
            modelBuilder.Entity<Country>().HasData(
                new Country { CountryId = 1, CountryName = "India" },
                new Country { CountryId = 2, CountryName = "China" },
                new Country { CountryId = 3, CountryName = "UAE" },
                new Country { CountryId = 4, CountryName = "USA" }
            );

            // Seed: Cities
            modelBuilder.Entity<City>().HasData(
                new City { CityId = 1, CityName = "Mumbai", CountryId = 1 },
                new City { CityId = 2, CityName = "Delhi", CountryId = 1 },
                new City { CityId = 3, CityName = "Beijing", CountryId = 2 },
                new City { CityId = 4, CityName = "Shanghai", CountryId = 2 },
                new City { CityId = 5, CityName = "Dubai", CountryId = 3 },
                new City { CityId = 6, CityName = "Abu Dhabi", CountryId = 3 },
                new City { CityId = 7, CityName = "New York", CountryId = 4 },
                new City { CityId = 8, CityName = "San Francisco", CountryId = 4 }
            );

            // Seed: Products (10)
            modelBuilder.Entity<Product>().HasData(
                new Product { ProductId = 1, ProductName = "Laptop" },
                new Product { ProductId = 2, ProductName = "Smartphone" },
                new Product { ProductId = 3, ProductName = "Tablet" },
                new Product { ProductId = 4, ProductName = "Smartwatch" },
                new Product { ProductId = 5, ProductName = "Desktop PC" },
                new Product { ProductId = 6, ProductName = "Gaming Console" },
                new Product { ProductId = 7, ProductName = "Wireless Earbuds" },
                new Product { ProductId = 8, ProductName = "Bluetooth Speaker" },
                new Product { ProductId = 9, ProductName = "Smart TV" },
                new Product { ProductId = 10, ProductName = "Camera" }
            );

            // Seed: Users
            modelBuilder.Entity<SalesUser>().HasData(
                new SalesUser { UserId = 1, UserName = "Diljith" },
                new SalesUser { UserId = 2, UserName = "Akshay" },
                new SalesUser { UserId = 3, UserName = "Rajeev" }
            );

            // Optional: Seed a few Sales (example)
            modelBuilder.Entity<Sale>().HasData(
                new Sale { SaleId = 1, CountryId = 1, CityId = 1, ProductId = 1, Quantity = 5, Amount = 350000m, UserId = 1, SaleDate = new DateTime(2025, 8, 10) },
                new Sale { SaleId = 2, CountryId = 1, CityId = 2, ProductId = 2, Quantity = 10, Amount = 500000m, UserId = 2, SaleDate = new DateTime(2025, 8, 11) }
                // add more sample rows if you like
            );

            // Configure foreign key delete behaviors to avoid cascade path issues
            modelBuilder.Entity<Sale>()
                .HasOne(s => s.Country)
                .WithMany()
                .HasForeignKey(s => s.CountryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Sale>()
                .HasOne(s => s.City)
                .WithMany()
                .HasForeignKey(s => s.CityId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Sale>()
                .HasOne(s => s.Product)
                .WithMany()
                .HasForeignKey(s => s.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Sale>()
                .HasOne(s => s.SalesUser)
                .WithMany()
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
