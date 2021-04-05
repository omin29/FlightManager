using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Data.Attributes;
using Microsoft.AspNetCore.Identity;


namespace Data.Models
{
    /// <summary>
    /// The class which represents the flight reservations. One reservation can be made for more than one passenger. The only limit
    /// to how many passengers a reservation can have is the amount of free seats for the flight.
    /// </summary>
    public class Reservation
    {
        /// <summary>
        /// The primary key for the database.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// A navigation property used for configuring the relation to the <see cref="Models.Flight"/> entities.
        /// </summary>
        public int FlightUniquePlaneNumber { get; set; }
        /// <summary>
        /// A navigation property used for configuring the relation to the <see cref="Models.Flight"/> entities.
        /// </summary>
        public virtual Flight Flight { get; set; }

        /// <summary>
        /// The email address on which the reservation is made. It will receive an email whenever a reservation is made. This email will 
        /// notify the user if the reservation is successful and give him extra information about the flight for which the reservation is
        /// made. This property is required.
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// A navigation property used for configuring the relation to the <see cref="Models.Passenger"/> entities.
        /// </summary>
        public virtual ICollection<Passenger> Passengers { get; set; }

        /// <summary>
        /// A constructor which sets an empty HashSet as a value to the <see cref="Passengers"/> navigation property.
        /// </summary>
        public Reservation()
        {
            Passengers = new HashSet<Passenger>();
        }
    }
}
