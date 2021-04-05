using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Data.Attributes;
using Microsoft.AspNetCore.Identity;

namespace Data.Models
{
    public class User : IdentityUser
    {
        [Required]
        [MaxLength(50, ErrorMessage = "{0} must be  maximum {1} characters")]
        [RegularExpression(@"[A-Za-z]+", ErrorMessage = "Names contain only letters")]
        public string FirstName { get; set; }

        [Required]
        [Phone]
        [RegularExpression(@"\d{10}", ErrorMessage = "Phone number must be 10 digits")]
        public override string PhoneNumber { get => base.PhoneNumber; set => base.PhoneNumber = value; }

        [Required]
        [MaxLength(50, ErrorMessage = "{0} must be  maximum {1} characters")]
        [RegularExpression(@"[A-Za-z]+", ErrorMessage = "Names contain only letters")]
        public string LastName { get; set; }
        [Required]
        [RegularExpression(@"\d{10}", ErrorMessage = "PIN (ЕГН) must be 10 digits")]
        [UniqueUserPIN]
        public virtual string PersonalIdentificationNumber { get; set; } //ЕГН

        [Required]
        [MaxLength(50, ErrorMessage = "{0} must be  maximum {1} characters")]
        public string Address { get; set; }

        [RegularExpression(@"\b(Admin|User)\b")]
        public string Role { get; set; }
        public bool CheckEGN(int number)
        {
            short count = 1;
            while (number / 10 > 0)
            {
                number /= 10;
                ++count;
            }
            return count >= 10;
        }
    }
}
