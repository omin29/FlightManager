using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Data.Attributes;
using Microsoft.AspNetCore.Identity;


namespace Data.Models
{
    public class Reservation
    {
        [Key]
        public int Id { get; set; }

        //[ForeignKey("UniquePlaneNumber")]
        public int FlightUniquePlaneNumber { get; set; }
        public virtual Flight Flight { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public virtual ICollection<Passenger> Passengers { get; set; }

        public Reservation()
        {
            Passengers = new HashSet<Passenger>();
        }
    }
}
