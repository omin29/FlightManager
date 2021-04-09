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
    /// This class runs tests on class <see cref="UserEditViewModel"/>
    /// </summary>
    [TestFixture]
    public class UserEditViewModelTests
    {
        private UserEditViewModel userEdit;
        /// <summary>
        ///  Prepares valid data for the <see cref="UserEditViewModel"/> object in this class before every test.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            this.userEdit = new UserEditViewModel() 
            { 
             FirstName="Hasan",
             PhoneNumber="0899871234",
             LastName="Petkov",
             Email="Petko@abv.bg",
             PersonalIdentificationNumber="1122334455",
             Address="Aprilsko wustanie №22",
             Role="User"
            };
        }
        /// <summary>
        /// This is the method which checks if the user is valid.
        /// </summary>
        private void ValidateUserEdit()
        {
            ValidationContext entityValidationContext = new ValidationContext(userEdit);
            Validator.ValidateObject(userEdit, entityValidationContext, true);
        }
        /// <summary>
        /// Checks a completely valid user which should not throw <see cref="ValidationException"/>.
        /// </summary>
        [Test]
        public void CheckCompletelyValidUserEdit()
        {
            Assert.DoesNotThrow(ValidateUserEdit, "The user is not valid when it is supposed to be.");
        }
        /// <summary>
        /// Checks an invalid personal identification number which contains letters.
        /// </summary>
        [Test]
        public void CheckInvalidPersonalIdentificationNumberWhichContainsLetters()
        {
            userEdit.PersonalIdentificationNumber = "777777777b";
            Assert.Throws<ValidationException>(ValidateUserEdit, "The userEdit identification number allows letters.");
        }
        /// <summary>
        /// Checks an invalid first name which does not contain only letters.
        /// </summary>
        [Test]
        public void CheckInvalidFirstName()
        {
            userEdit.FirstName = "dimitur@123412";
            Assert.Throws<ValidationException>(ValidateUserEdit, "The first name can contains only letters .");
        }
        /// <summary>
        /// Checks an invalid first name which exceeds the 50 character length limit.
        /// </summary>
        [Test]
        public void CheckInvalidFirstNameSize()
        {
            userEdit.FirstName = new string('a', 51);
            Assert.Throws<ValidationException>(ValidateUserEdit, "The First name cannot be longer than 50 characters .");
        }
        /// <summary>
        /// Checks an invalid last name which does not contain only letters.
        /// </summary>
        [Test]
        public void CheckInvalidLastName()
        {
            userEdit.LastName = "Hasan@412";
            Assert.Throws<ValidationException>(ValidateUserEdit, "The last name can contains only letters .");
        }
        /// <summary>
        /// Checks an invalid last name which exceeds the 50 character length limit.
        /// </summary>
        [Test]
        public void CheckInvalidLastNameSize()
        {
            userEdit.LastName = new string('a', 51);
            Assert.Throws<ValidationException>(ValidateUserEdit, "The last name cannot be longer than 50 characters .");
        }
        /// <summary>
        /// Checks a personal identification number which contains invalid number of digits
        /// </summary>
        [Test]
        public void CheckPersonalIdentificationNumberWhichContainsInvalidNumberOfDigits()
        {
           userEdit.PersonalIdentificationNumber = "777";
            Assert.Throws<ValidationException>(ValidateUserEdit, "The personal identification number allows different number of digits than 10.");
        }
        /// <summary> 
        /// Checks an invalid phone number  which contains letters.
        /// </summary>
        [Test]
        public void CheckInvalidPhoneNumberWhichContainsLetters()
        {
            userEdit.PersonalIdentificationNumber = "5674g25125g";
            Assert.Throws<ValidationException>(ValidateUserEdit, "The user phone number allows letters.");
        }
        /// <summary>
        /// Checks an invalid phone number which contains invalid number of digits.
        /// </summary>
        [Test]
        public void CheckInvalidPhoneNumberWithInvalidLength()
        {
            userEdit.PersonalIdentificationNumber = "112";
            Assert.Throws<ValidationException>(ValidateUserEdit, "The user Phone number of digits than 10.");
        }
    }
}
