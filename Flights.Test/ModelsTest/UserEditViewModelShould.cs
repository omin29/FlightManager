
using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Data.Context;
using Data.Models;

namespace Flights.Test
{
    [TestFixture]
    public class UserEditViewModelShould
    {
        private UserEditViewModel userEdit;
        [SetUp]
        public void Setup()
        {
            this.userEdit = new UserEditViewModel();
        }

        [Test]
        public void CorrectUserEditViewModel()
        {

            Assert.That(userEdit.FirstName, Is.EqualTo(expected: userEdit.FirstName));
            Assert.That(userEdit.LastName, Is.EqualTo(expected: userEdit.LastName));
            Assert.That(userEdit.PhoneNumber, Is.EqualTo(expected: userEdit.PhoneNumber));
            Assert.That(userEdit.PersonalIdentificationNumber, Is.EqualTo(expected: userEdit.PersonalIdentificationNumber));
            Assert.That(userEdit.Address, Is.EqualTo(expected: userEdit.Address));
            Assert.That(userEdit.Role, Is.EqualTo(expected: userEdit.Role));
        }
        
        
    }
}
