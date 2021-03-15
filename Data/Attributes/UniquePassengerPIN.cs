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
    public class UniquePassengerPIN: ValidationAttribute //PIN == ЕГН
    {
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
