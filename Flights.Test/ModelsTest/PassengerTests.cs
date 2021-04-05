using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Data.Context;
using Data.Models;
using System.ComponentModel.DataAnnotations;

namespace Flights.Test
{
    [TestFixture]
    public class PassengerTests
    {
        private Passenger passenger;

        [SetUp]
        public void PrepareValidPassangerForTesting()
        {
            this.passenger = new Passenger()
            {
                Id = 1,
                PersonalIdentificationNumber = "7777777777",
                FirstName = "Ivan",
                MiddleName = "Mihail",
                LastName = "Georgiev",
                PhoneNumber = "1111111111",
                Nationality = "Bulgarian",
                TicketType = "Normal"
            };
        }

        private void ValidatePassenger()
        {
            ValidationContext entityValidationContext = new ValidationContext(passenger);
            Validator.ValidateObject(passenger, entityValidationContext, true);
        }

        [Test]
        public void CheckCompletelyValidPassenger()
        {
            Assert.DoesNotThrow(ValidatePassenger, "The passenger is not valid when it is supposed to be.");
        }
        
        [Test]
        public void CheckInvalidPersonalIdentificationNumberWhichContainsLetters()
        {
            passenger.PersonalIdentificationNumber = "777777777b";
            Assert.Throws<ValidationException>(ValidatePassenger, "The personal identification number allows letters.");
        }

        [Test]
        public void CheckPersonalIdentificationNumberWhichContainsInvalidNumberOfDigits()
        {
            passenger.PersonalIdentificationNumber = "777";
            Assert.Throws<ValidationException>(ValidatePassenger, "The personal identification number allows different number of digits than 10.");
        }
    }
}
