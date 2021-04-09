using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Data.Attributes;
using Microsoft.AspNetCore.Identity;

namespace Data.Models
{
    /// <summary>
    /// The class represents the users who are registered on the FlightManager website. This class is an extended version
    /// of the <see cref="IdentityUser"/> class. It is used for authentication and authorization.
    /// </summary>
    public class User : IdentityUser
    {
        /// <summary>
        /// The first name of the user. This property is required. It cannot exceed 50 characters and contains only letters.
        /// </summary>
        [Required]
        [MaxLength(50, ErrorMessage = "{0} must be  maximum {1} characters")]
        [RegularExpression(@"[A-Za-z]+", ErrorMessage = "Names contain only letters")]
        public string FirstName { get; set; }

        /// <summary>
        /// The phone number of the user. This property is required. It consists of only 10 digits.
        /// </summary>
        [Required]
        [Phone]
        [RegularExpression(@"\d{10}", ErrorMessage = "Phone number must be 10 digits")]
        public override string PhoneNumber { get => base.PhoneNumber; set => base.PhoneNumber = value; }

        /// <summary>
        /// The last name of the user. This property is required. It cannot exceed 50 characters and contains only letters.
        /// </summary>
        [Required]
        [MaxLength(50, ErrorMessage = "{0} must be  maximum {1} characters")]
        [RegularExpression(@"[A-Za-z]+", ErrorMessage = "Names contain only letters")]
        public string LastName { get; set; }

        /// <summary>
        /// Each user has unique personal identification number (PIN) which consists of only 10 digits.
        /// The personal identification number is the equivalent of the bulgarian "ЕГН". This property is required.
        /// </summary>
        [Required]
        [RegularExpression(@"\d{10}", ErrorMessage = "PIN (ЕГН) must be 10 digits")]
        [UniqueUserPIN]
        public virtual string PersonalIdentificationNumber { get; set; }

        /// <summary>
        /// The address where the user lives. This property is required. It cannot exceed 50 characters and contains only letters.
        /// </summary>
        [Required]
        [MaxLength(50, ErrorMessage = "{0} must be  maximum {1} characters")]
        public string Address { get; set; }

        /// <summary>
        /// The role of the user. In this application there are only 2 roles. The "User" role indicates that the user is an employee of the FlightManager company and it
        /// does not offer him special privileges on the website. The "Admin" role grants the user full access to the website. There can be only 1 administrator. They have
        /// access to all CRUD operations over the other users in the database. The administrator can also add flights to the database and modify them.
        /// </summary>
        [RegularExpression(@"\b(Admin|User)\b")]
        public string Role { get; set; }
        
    }
}
