using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Context
{
    public class FlightManagerDbContext : IdentityDbContext<User>
    {
        public DbSet<Flight> Flights { get; set; }

        public DbSet<Reservation> Reservations { get; set; }

        public DbSet<Passenger> Passengers { get; set; }
        
        public FlightManagerDbContext(DbContextOptions<FlightManagerDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<User>()
                .HasIndex(x => x.PersonalIdentificationNumber)
                .IsUnique();

            builder.Entity<Passenger>()
                .HasIndex(x => x.PersonalIdentificationNumber)
                .IsUnique();

            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=FlightManager;Trusted_Connection=True;MultipleActiveResultSets=true");
            optionsBuilder.UseLazyLoadingProxies();
        }
    }
}
