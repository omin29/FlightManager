using System;
using Data.Models;
using Data.Context;
using Data.Static;

namespace Data
{
    class Program
    {
        static void Main(string[] args)
        {
            /*try
            {
                using (var db = new FlightManagerDbContext(new Microsoft.EntityFrameworkCore.DbContextOptions<FlightManagerDbContext>()))
                {
                    var User = new User();
                    User.PersonalIdentificationNumber = "1111111111";
                    db.Add(User){ };
                    db.SaveChanges();
                }
            }
            catch(Exception ex)
            {
                if(ex.Message.Equals("Cannot insert duplicate key row in object 'dbo.AspNetUsers' with unique index 'IX_AspNetUsers_PersonalIdentificationNumber'."))
                {
                    Console.WriteLine(true);
                }
            }*/

            ReservationAssistant.SendEmail(ReservationAssistant.PrepareEmail(new System.Text.StringBuilder()));
        }
    }
}
