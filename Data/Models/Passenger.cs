using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Data.Attributes;
using Microsoft.AspNetCore.Identity;

namespace Data.Models
{
    public class Passenger
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [RegularExpression(@"\d{10}", ErrorMessage = "PIN (ЕГН) must be 10 digits")]
        [UniquePassengerPIN]
        public string PersonalIdentificationNumber { get; set; } //ЕГН

        [Required]
        [MaxLength(50, ErrorMessage = "{0} must be  maximum {1} characters")]
        [RegularExpression(@"[A-Za-z]+", ErrorMessage = "Names contain only letters")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "{0} must be  maximum {1} characters")]
        [RegularExpression(@"[A-Za-z]+", ErrorMessage = "Names contain only letters")]
        public string MiddleName { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "{0} must be  maximum {1} characters")]
        [RegularExpression(@"[A-Za-z]+", ErrorMessage = "Names contain only letters")]
        public string LastName { get; set; }

        [Required]
        [Phone]
        [RegularExpression(@"\d{10}", ErrorMessage = "Phone number must be 10 digits")]
        public string PhoneNumber { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "{0} must be  maximum {1} characters")]
        [RegularExpression(@"[A-Za-z]+", ErrorMessage = "Nationality contains only letters")]
        public string Nationality { get; set; }

        [RegularExpression(@"\b(Normal|Business)\b", ErrorMessage = "TicketType can be only Normal or Business")]
        public string TicketType { get; set; }

        //[ForeignKey("ReservationId")]
        public int ReservationId { get; set; }
        public virtual Reservation Reservation { get; set; }
    }
}
