using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Data.Context;
using Data.Models;
using System.Text;
using System.Threading.Tasks;

namespace Data.Attributes
{
    /// <summary>
    /// A custom attribute which ensures that the user's personal identification number is unique to the database.
    /// </summary>
    public class UniqueUserPIN : ValidationAttribute 
    {
        /// <summary>
        /// Performs a validation check on the value of the user's personal identification number to ensure that it is unique.
        /// </summary>
        /// <param name="value">The value of the user's personal identification number.</param>
        /// <param name="validationContext">Describes the context in which a validation check is performed.</param>
        /// <returns>A container for the results of a validation request.</returns>
        protected override ValidationResult IsValid(object value,
            ValidationContext validationContext)
        {
            ErrorMessage = "Personal Identification Number must be unique";

            if (CheckUserPIN(value).Result == false )
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }

        /// <summary>
        /// Checks if the user's personal identification number is unique to the database.
        /// </summary>
        /// <param name="value">The personal identification number of the user.</param>
        /// <returns>A bool value which indicates whether the user's personal identification number is unique to the database.</returns>
        private async Task<bool> CheckUserPIN(object value)
        {
            using (var db = new FlightManagerDbContext(new DbContextOptions<FlightManagerDbContext>()))
            {
                List<User> users = await db.Users.ToListAsync();
                bool result = !users.Exists(x => x.PersonalIdentificationNumber == (string)value);
                return result;
            }
        }

    }
}
