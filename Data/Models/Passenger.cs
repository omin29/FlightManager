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
    /// The class represents the passengers who will have reservation for the flights.
    /// </summary>
    public class Passenger
    {
        /// <summary>
        /// The primary key for the database.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Each passenger has unique personal identification number (PIN) which consists of only 10 digits.
        /// The personal identification number is the equivalent of the bulgarian "ЕГН". This property is required.
        /// </summary>
        [Required]
        [RegularExpression(@"\d{10}", ErrorMessage = "PIN (ЕГН) must be 10 digits")]
        [UniquePassengerPIN]
        public string PersonalIdentificationNumber { get; set; }

        /// <summary>
        /// The first name of the passenger. This property is required. It cannot exceed 50 characters and contains only letters.
        /// </summary>
        [Required]
        [MaxLength(50, ErrorMessage = "{0} must be  maximum {1} characters")]
        [RegularExpression(@"[A-Za-z]+", ErrorMessage = "Names contain only letters")]
        public string FirstName { get; set; }

        /// <summary>
        /// The middle name of the passenger. This property is required. It cannot exceed 50 characters and contains only letters.
        /// </summary>
        [Required]
        [MaxLength(50, ErrorMessage = "{0} must be  maximum {1} characters")]
        [RegularExpression(@"[A-Za-z]+", ErrorMessage = "Names contain only letters")]
        public string MiddleName { get; set; }

        /// <summary>
        /// The last name of the passenger. This property is required. It cannot exceed 50 characters and contains only letters.
        /// </summary>
        [Required]
        [MaxLength(50, ErrorMessage = "{0} must be  maximum {1} characters")]
        [RegularExpression(@"[A-Za-z]+", ErrorMessage = "Names contain only letters")]
        public string LastName { get; set; }

        /// <summary>
        /// The phone number of the passenger. It consists of 10 digits and it is required.
        /// </summary>
        [Required]
        [Phone]
        [RegularExpression(@"\d{10}", ErrorMessage = "Phone number must be 10 digits")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// The nationality of the passenger. This property is required. It cannot exceed 50 characters and contains only letters.
        /// </summary>
        [Required]
        [MaxLength(50, ErrorMessage = "{0} must be  maximum {1} characters")]
        [RegularExpression(@"[A-Za-z]+", ErrorMessage = "Nationality contains only letters")]
        public string Nationality { get; set; }

        /// <summary>
        /// The ticket type which the passenger has. It can be either normal (economy) class or business class.
        /// </summary>
        [RegularExpression(@"\b(Normal|Business)\b", ErrorMessage = "TicketType can be only Normal or Business")]
        public string TicketType { get; set; }

        /// <summary>
        /// A navigation property used for configuring the relation to the  <see cref="Models.Reservation"/> entities.
        /// </summary>
        public int ReservationId { get; set; }

        /// <summary>
        /// A navigation property used for configuring the relation to the  <see cref="Models.Reservation"/> entities.
        /// </summary>
        public virtual Reservation Reservation { get; set; }
    }
}
