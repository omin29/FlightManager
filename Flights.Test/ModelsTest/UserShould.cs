using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Data.Context;
using Data.Models;

namespace Flights.Test
{
    [TestFixture]
    public class UserShould
    {
        private User user;
        [SetUp]
        public void Setup()
        {
            this.user = new User();
        }


        [Test]
        public void CorrectUser()
        {

            Assert.That(user.FirstName, Is.EqualTo(expected: user.FirstName));
            Assert.That(user.LastName, Is.EqualTo(expected: user.LastName));
            Assert.That(user.PhoneNumber, Is.EqualTo(expected: user.PhoneNumber));
            Assert.That(user.PersonalIdentificationNumber, Is.EqualTo(expected: user.PersonalIdentificationNumber));
        }
        [Test]
        [TestCase(12345678)]
        public void ErrorEGN(int result)
        {
            //проверява EGN e с по-малко от 10 символа ако EGN е с по-малко от 10символа теста работи
            this.user.CheckEGN(result);
            Assert.IsFalse(this.user.CheckEGN(result), "PIN (ЕГН) must be 10 digits");
        }
        [Test]
        [TestCase(1524223490)]
        public void CorrectEGn(int result)
        {
            //проверява EGN дали е с 10символа
            this.user.CheckEGN(result);
            Assert.IsTrue(this.user.CheckEGN(result), "PIN (ЕГН) must be 10 digits");
        }
    }
}
