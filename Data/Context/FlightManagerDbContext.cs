using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Data.Context
{
    /// <summary>
    /// The database context for the "FlightManager" project. It inherits the IdentityDbContext class with an extended version of IdentityUser class.
    /// The database contains all tables needed for implementing authentication and authorization. As an addition, it contains the "Flights", "Reservations" and "Passengers" tables
    /// which exist for providing the rest of the functionalities of the project.
    /// </summary>
    public class FlightManagerDbContext : IdentityDbContext<User>
    {
        /// <summary>
        /// A <see cref="DbSet{TEntity}"/> for the <see cref="Models.Flight"/> class.
        /// </summary>
        public DbSet<Flight> Flights { get; set; }

        /// <summary>
        /// A <see cref="DbSet{TEntity}"/> for the <see cref="Models.Reservation"/> class.
        /// </summary>
        public DbSet<Reservation> Reservations { get; set; }

        /// <summary>
        /// A <see cref="DbSet{TEntity}"/> for the <see cref="Models.Passenger"/> class.
        /// </summary>
        public DbSet<Passenger> Passengers { get; set; }
        
        /// <summary>
        /// A constructor for the FlightManagerDbContext with default options.
        /// </summary>
        /// <param name="options"><see cref="DbContextOptions{TContext}"/></param>
        public FlightManagerDbContext(DbContextOptions<FlightManagerDbContext> options)
            : base(options)
        {
        }

<<<<<<< Updated upstream

        /// <summary>
        /// A Fluent API which lets you modify how your database entities are mapped. In this case, there are added restrictions which make
        /// the personal identification numbers of both the users and the passengers unique columns.
        /// </summary>
        /// <param name="builder"><see cref=" ModelBuilder"/></param>
=======
        


>>>>>>> Stashed changes
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<User>()
                .HasIndex(x => x.PersonalIdentificationNumber)
                .IsUnique();

            builder.Entity<Passenger>()
                .HasIndex(x => x.PersonalIdentificationNumber)
<<<<<<< Updated upstream
                .IsUnique();          
=======
                .IsUnique();
>>>>>>> Stashed changes
        }

        /// <summary>
        /// A method which lets you configure the database to be used for this context. The connection string for the database is set here.
        /// Lazy-loading proxies are enabled.
        /// </summary>
        /// <param name="optionsBuilder"><see cref="DbContextOptionsBuilder"/></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=FlightManager;Trusted_Connection=True;MultipleActiveResultSets=true");
            optionsBuilder.UseLazyLoadingProxies();
        }
    }
}
