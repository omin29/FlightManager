using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Data.Context;
using Data.Models;
using System.Text;
using System.Threading.Tasks;
using Data.Static;
using System.Linq;

namespace Data.Attributes
{
    /// <summary>
    /// A custom attribute which ensures that the passenger's personal identification number is unique to the database.
    /// It also ensures that the passenger's personal identification number is unique to the list which holds pending passengers.<see cref="Data.Static.ReservationAssistant"/>
    /// </summary>
    public class UniquePassengerPIN: ValidationAttribute
    {
        /// <summary>
        /// Performs a validation check on the value of the passenger's personal identification number to ensure that it is unique.
        /// </summary>
        /// <param name="value">The value of the passenger's personal identification number.</param>
        /// <param name="validationContext">Describes the context in which a validation check is performed.</param>
        /// <returns>A container for the results of a validation request.</returns>
        protected override ValidationResult IsValid(object value,
            ValidationContext validationContext)
        {
            ErrorMessage = "Personal Identification Number must be unique";
            Passenger passenger = (Passenger)validationContext.ObjectInstance;

            if (CheckPassengerPIN(value).Result == false || ReservationAssistant.PendingPassengers.Any(x=>x.PersonalIdentificationNumber == passenger.PersonalIdentificationNumber))
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }

        /// <summary>
        /// Checks if the passenger's personal identification number is unique to the database.
        /// </summary>
        /// <param name="value">The personal identification number of the passenger.</param>
        /// <returns>A bool value which indicates whether the passenger's personal identification number is unique to the database.</returns>
        private async Task<bool> CheckPassengerPIN(object value)
        {
            using (var db = new FlightManagerDbContext(new DbContextOptions<FlightManagerDbContext>()))
            {
                List<Passenger> passengers = await db.Passengers.ToListAsync();
                bool result = !passengers.Exists(x => x.PersonalIdentificationNumber == (string)value);
                return result;
            }
        }

    }
}
