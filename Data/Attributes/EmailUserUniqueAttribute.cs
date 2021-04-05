using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Data.Context;
using Data.Models;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Data.Attributes
{
    /// <summary>
    /// A custom attribute which ensures that the user's email address is unique to the database.
    /// </summary>
    public class EmailUserUniqueAttribute : ValidationAttribute
    {
        /// <summary>
        /// Performs a validation check on the value of the user's email address to ensure that it is unique.
        /// </summary>
        /// <param name="value">The value of the user's email address.</param>
        /// <param name="validationContext">Describes the context in which a validation check is performed.</param>
        /// <returns>A container for the results of a validation request.</returns>
        protected override ValidationResult IsValid(
            object value, ValidationContext validationContext)
        {
            var _context = (FlightManagerDbContext)validationContext.GetService(typeof(FlightManagerDbContext));
            var entity = _context.Users.SingleOrDefault(e => e.Email == value.ToString());

            if (entity != null)
            {
                return new ValidationResult(GetErrorMessage(value.ToString()));
            }
            return ValidationResult.Success;
        }

        /// <summary>
        /// Prepares an error message for duplicate email address.
        /// </summary>
        /// <param name="email">The duplicate email address.</param>
        /// <returns>An error message for duplicate email address as a string.</returns>
        public string GetErrorMessage(string email)
        {
            return $"Email {email} is already in use.";
        }
    }
}
