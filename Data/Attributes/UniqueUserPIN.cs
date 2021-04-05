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
    public class UniqueUserPIN : ValidationAttribute //PIN == ЕГН
    {
        protected override ValidationResult IsValid(object value,
            ValidationContext validationContext)
        {
            //User user = (User)validationContext.ObjectInstance;
            ErrorMessage = "Personal Identification Number must be unique";

            if (CheckUserPIN(value).Result == false )
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }

        /*public override bool IsValid(object value)
        {
            return CheckUserPIN(value).Result;
        }*/

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
