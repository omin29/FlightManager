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
    /// A custom attribute which ensures that the flight's departure time is before its landing time.
    /// </summary>
    public class ValidFlightTimesAttribute:ValidationAttribute
    {
        /// <summary>
        /// Performs a validation check which attempts to confirm that the flight's departure time is before its landing time.
        /// </summary>
        /// <param name="value">The flight's departure time.</param>
        /// <param name="validationContext">Describes the context in which a validation check is performed.</param>
        /// <returns>A container for the results of a validation request.</returns>
        protected override ValidationResult IsValid(object value,
            ValidationContext validationContext)
        {
            Flight flight = (Flight)validationContext.ObjectInstance;
            ErrorMessage = "The departure time should be before the landing time.";

            if (flight.DepartureTime.CompareTo(flight.LandingTime) < 0)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(ErrorMessage);
        }
    }
}
