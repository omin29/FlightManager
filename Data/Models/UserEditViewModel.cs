using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Data.Attributes;
using Microsoft.AspNetCore.Identity;

namespace Data.Models
{
    /// <summary>
    /// Almost identical to the <see cref="Models.User"/> class. The difference is that this model is used for
    /// the validation of user data which is being modified. The <see cref="PersonalIdentificationNumber"/> property does not contain
    /// the attribute which ensures that its value is unique to the database.
    /// </summary>
    public class UserEditViewModel : IdentityUser
    {
        /// <summary>
        /// <inheritdoc cref="User.FirstName"/>
        /// </summary>
        [Required]
        [MaxLength(50, ErrorMessage = "{0} must be  maximum {1} characters")]
        [RegularExpression(@"[A-Za-z]+", ErrorMessage = "Names contain only letters")]
        public string FirstName { get; set; }

        /// <summary>
        /// <inheritdoc cref="User.PhoneNumber"/>
        /// </summary>
        [Required]
        [Phone]
        [RegularExpression(@"\d{10}", ErrorMessage = "Phone number must be 10 digits")]
        public override string PhoneNumber { get => base.PhoneNumber; set => base.PhoneNumber = value; }

        /// <summary>
        /// <inheritdoc cref="User.LastName"/>
        /// </summary>
        [Required]
        [MaxLength(50, ErrorMessage = "{0} must be  maximum {1} characters")]
        [RegularExpression(@"[A-Za-z]+", ErrorMessage = "Names contain only letters")]
        public string LastName { get; set; }

        /// <summary>
        /// <inheritdoc cref="User.PersonalIdentificationNumber"/>
        /// </summary>
        [Required]
        [RegularExpression(@"\d{10}", ErrorMessage = "PIN (ЕГН) must be 10 digits")]
        public virtual string PersonalIdentificationNumber { get; set; } 

        /// <summary>
        /// <inheritdoc cref="User.Address"/>
        /// </summary>
        [Required]
        [MaxLength(50, ErrorMessage = "{0} must be  maximum {1} characters")]
        public string Address { get; set; }

        /// <summary>
        /// <inheritdoc cref="User.Role"/>
        /// </summary>
        [RegularExpression(@"\b(Admin|User)\b")]
        public string Role { get; set; }
    }
}
