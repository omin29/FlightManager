using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Data.Attributes;
using Microsoft.AspNetCore.Identity;

namespace Data.Models
{
    public class Flight
    {
        [Key]
        public int UniquePlaneNumber { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "{0} must be  maximum {1} characters")]
        public string LocationFrom { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "{0} must be  maximum {1} characters")]
        [Unlike("LocationFrom",ErrorMessage = "LocationFrom and LocationTo cannot match")]
        public string LocationTo { get; set; }

        [Required]
        [ValidFlightTimes]
        public DateTime DepartureTime { get; set; }

        [Required]
        public DateTime LandingTime { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "{0} must be  maximum {1} characters")]
        public string PlaneType { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "{0} must be  maximum {1} characters")]
        [RegularExpression(@"[A-Za-z]+", ErrorMessage = "Names contain only letters")]
        public string PilotName { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "The number of free seats must be a positive number or 0")]
        public int FreePassengerSeats { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "The number of free seats must be a positive number or 0")]
        public int FreeBusinessSeats { get; set; }

        [NotMapped]
        public string FlightGeneralInfo
        {
            get
            {
                return string.Format("Unique plane number:{0} | {1} - {2}", UniquePlaneNumber, LocationFrom, LocationTo);
            }
        }

        public virtual ICollection<Reservation> Reservations { get; set; }

        public Flight()
        {
            Reservations = new HashSet<Reservation>();
        }
        public bool FreeSeats(int seats)
        {
            if (seats < 0)
            {
                return false;
            }
            return true;
        }
       
    }
}
