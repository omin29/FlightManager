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
    /// The class contains information about plane flights. Reservations can be made for these flights.
    /// </summary>
    public class Flight
    {
        /// <summary>
        /// An unique plane number for the flight. It serves the purpose of a primary key for the database.
        /// </summary>
        [Key]
        public int UniquePlaneNumber { get; set; }

        /// <summary>
        /// The location from where the plane departs. The property is required and its length cannot exceed 50 characters.
        /// </summary>
        [Required]
        [MaxLength(50, ErrorMessage = "{0} must be  maximum {1} characters")]
        [RegularExpression(@"[A-Za-z]+", ErrorMessage = "Departure locations contain only letters")]
        public string LocationFrom { get; set; }

        /// <summary>
        /// The destination where the plane will arrive. The property is required and its length cannot exceed 50 characters.
        /// Also it must be different from the departure location.
        /// </summary>
        [Required]
        [MaxLength(50, ErrorMessage = "{0} must be  maximum {1} characters")]
        [Unlike("LocationFrom",ErrorMessage = "LocationFrom and LocationTo cannot match")]
        [RegularExpression(@"[A-Za-z]+", ErrorMessage = "Destinations contain only letters")]
        public string LocationTo { get; set; }

        /// <summary>
        /// The time when the plane will depart. This property is required. It must be before the landing time.
        /// </summary>
        [Required]
        [ValidFlightTimes]
        public DateTime DepartureTime { get; set; }

        /// <summary>
        /// The time when the plane will arrive at its destination. This property is required.
        /// </summary>
        [Required]
        public DateTime LandingTime { get; set; }

        /// <summary>
        /// The type of the plane. The property is required and its length cannot exceed 50 characters.
        /// </summary>
        [Required]
        [MaxLength(50, ErrorMessage = "{0} must be  maximum {1} characters")]
        public string PlaneType { get; set; }

        /// <summary>
        /// The name of the person who pilots the plane. The property is required and its length cannot exceed 50 characters.
        /// Also it can contain only letters.
        /// </summary>
        [Required]
        [MaxLength(50, ErrorMessage = "{0} must be  maximum {1} characters")]
        [RegularExpression(@"[A-Za-z]+", ErrorMessage = "Names contain only letters")]
        public string PilotName { get; set; }

        /// <summary>
        /// The amount of free passenger seats for the economy class. This property is required.
        /// </summary>
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "The number of free seats must be a positive number or 0")]
        public int FreePassengerSeats { get; set; }

        /// <summary>
        /// The amount of free passenger seats for the business class. This property is required.
        /// </summary>
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "The number of free seats must be a positive number or 0")]
        public int FreeBusinessSeats { get; set; }

        /// <summary>
        /// A property which is not mapped to the database. It returns general information about the flight which includes
        /// its unique plane number, departure location and destination.
        /// </summary>
        [NotMapped]
        public string FlightGeneralInfo
        {
            get
            {
                return string.Format("Unique plane number:{0} | {1} - {2}", UniquePlaneNumber, LocationFrom, LocationTo);
            }
        }

        /// <summary>
        /// A navigation property used for configuring relation to the <see cref=" Models.Reservation"/> entities.
        /// </summary>
        public virtual ICollection<Reservation> Reservations { get; set; }
<<<<<<< Updated upstream

        /// <summary>
        /// A constructor which sets an empty HashSet as a value to the <see cref=" Reservations"/> navigation property.
        /// </summary>
=======
        
>>>>>>> Stashed changes
        public Flight()
        {
            Reservations = new HashSet<Reservation>();
        }
    }
}
