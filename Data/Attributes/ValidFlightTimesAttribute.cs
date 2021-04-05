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
    public class ValidFlightTimesAttribute:ValidationAttribute
    {
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
