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
    /// This class runs tests on class <see cref="User"/>
    /// </summary>
    [TestFixture]
    public class UserTests
    {
        private User user;
        /// <summary>
        ///  Prepares valid data for the <see cref="User"/> object in this class before every test.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            this.user = new User() 
            {
                FirstName= "Kosta",
                LastName="Popov",
                PhoneNumber="0892321212",
                Email = "Kostov@abv.bg",
                PersonalIdentificationNumber="0150231234",
                Address="Iwan Rilski №12",
                Role="User"
            };
        }
        /// <summary>
        /// This is the method which checks if the user is valid.
        /// </summary>
        private void ValidateUser()
        {
            ValidationContext entityValidationContext = new ValidationContext(user);
            Validator.ValidateObject(user, entityValidationContext, true);
        }
        /// <summary>
        /// Check a completely valid user which should not throw <see cref="ValidationException"/>.
        /// </summary>
        [Test]
        public void CheckCompletelyValidUser()
        {
            Assert.DoesNotThrow(ValidateUser, "The user is not valid when it is supposed to be.");
        }
         /// <summary>
         /// Checks an invalid personal identification number which contains letters.
         /// </summary>
        [Test]
        public void CheckInvalidPersonalIdentificationNumberWhichContainsLetters()
        {
            user.PersonalIdentificationNumber = "123412555b";
            Assert.Throws<ValidationException>(ValidateUser, "The user identification number allows letters.");
        }
        /// <summary>
        /// Check a personal identification number which contains invalid number of digits.
        /// </summary>
        [Test]
        public void CheckPersonalIdentificationNumberWhichContainsInvalidNumberOfDigits()
        {
            user.PersonalIdentificationNumber = "213";
            Assert.Throws<ValidationException>(ValidateUser, "The user identification number allows different number of digits than 10.");
        }
        /// <summary>
        /// Checks an invalid phone number which contains letters.
        /// </summary>
        [Test]
        public void CheckInvalidPhoneNumberWhichContainsLetters()
        {
            user.PersonalIdentificationNumber = "9874125125g";
            Assert.Throws<ValidationException>(ValidateUser, "The user phone number allows letters.");
        }
        /// <summary>
        /// Checks an invalid phone number which contains invalid number of digits.
        /// </summary>
        [Test]
        public void CheckPersonalPhoneNumberWithInvalidLength()
        {
            user.PersonalIdentificationNumber = "911";
            Assert.Throws<ValidationException>(ValidateUser, "The user Phone number of digits than 10.");
        }
        /// <summary>
        /// Checks an invalid first name which does not contain only letters.
        /// </summary>
        [Test]
        public void CheckInvalidFirstName()
        {
            user.FirstName = "IVANN@123412";
            Assert.Throws<ValidationException>(ValidateUser, "The first name can contains only letters .");
        }
        /// <summary>
        /// Checks an invalid first name which exceeds the 50 character length limit.
        /// </summary>
        [Test]
        public void CheckInvalidFirstNameSize()
        {
            user.FirstName = new string('a', 51);
            Assert.Throws<ValidationException>(ValidateUser, "The First name cannot be longer than 50 characters .");
        }
        /// <summary>
        /// Checks an invalid last name which does not contain only letters.
        /// </summary>
        [Test]
        public void CheckInvalidLastName()
        {
            user.LastName = "Popa@412";
            Assert.Throws<ValidationException>(ValidateUser, "The last name can contains only letters .");
        }
        /// <summary>
        /// Checks an invalid last name which exceeds the 50 character length limit.
        /// </summary>
        [Test]
        public void CheckInvalidLastNameSize()
        {
            user.LastName = new string('a', 51);
            Assert.Throws<ValidationException>(ValidateUser, "The last name cannot be longer than 50 characters .");
        }
    }
}
