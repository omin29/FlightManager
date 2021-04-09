using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Data.Context;
using Data.Models;
using System.ComponentModel.DataAnnotations;

namespace Flights.Test
{
    /// <summary>
    /// This class runs tests on class <see cref="Passenger"/>
    /// </summary>
    [TestFixture]
    public class PassengerTests
    {
        private Passenger passenger;
        /// <summary>
        ///  Prepares valid data for the <see cref="Passenger"/> object in this class before every test.
        /// </summary>
        [SetUp]
        public void Setup()
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
        /// <summary>
        /// This is the method which checks if the passenger is valid. 
        /// </summary>
        private void ValidatePassenger()
        {
            ValidationContext entityValidationContext = new ValidationContext(passenger);
            Validator.ValidateObject(passenger, entityValidationContext, true);
        }
        /// <summary>
        /// Check completely valid passenger which should not throw <see cref="ValidationException"/>.
        /// </summary>
        [Test]
        public void CheckCompletelyValidPassenger()
        {
            Assert.DoesNotThrow(ValidatePassenger, "The passenger is not valid when it is supposed to be.");
        }
        /// <summary>
        /// Checks invalid first name which does not contain only letters.
        /// </summary>
        [Test]
        public void CheckInvalidFirstName()
        {
            passenger.FirstName = "IVANN@123412";
            Assert.Throws<ValidationException>(ValidatePassenger, "The personal name can contains only letters .");
        }
        /// <summary>
        /// Check invalid first name which exceeds the 50 character length limit.
        /// </summary>
        [Test]
        public void CheckInvalidFirstNameSize()
        {
            passenger.FirstName = new string('a', 51);
            Assert.Throws<ValidationException>(ValidatePassenger, "The personal name cannot be longer than 50 characters .");
        }
        /// <summary>
        /// Checks an invalid personal identification number which contains letters.
        /// </summary>
        [Test]
        public void CheckInvalidPersonalIdentificationNumberWhichContainsLetters()
        {
            passenger.PersonalIdentificationNumber = "777777777b";
            Assert.Throws<ValidationException>(ValidatePassenger, "The personal identification number allows letters.");
        }
        /// <summary>
        /// Checks a personal identification number which contains invalid number of digits.
        /// </summary>
        [Test]
        public void CheckPersonalIdentificationNumberWhichContainsInvalidNumberOfDigits()
        {
            passenger.PersonalIdentificationNumber = "777";
            Assert.Throws<ValidationException>(ValidatePassenger, "The personal identification number allows different number of digits than 10.");
        }
        /// <summary>
        /// Checks an invalid phone number  which contains letters.
        /// </summary>
        [Test]
        public void CheckInvalidPhoneNumberWhichContainsLetters()
        {
            passenger.PersonalIdentificationNumber = "5674g25125g";
            Assert.Throws<ValidationException>(ValidatePassenger, "The personal phone number allows letters.");
        }
        /// <summary>
        /// Checks an invalid phone number which contains invalid number of digits.
        /// </summary>
        [Test]
        public void CheckInvalidPhoneNumberLength()
        {
            passenger.PersonalIdentificationNumber = "112";
            Assert.Throws<ValidationException>(ValidatePassenger, "The personal Phone number of digits than 10.");
        }
    }
}
