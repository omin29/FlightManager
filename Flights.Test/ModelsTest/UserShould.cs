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
        public void CorrectUser1()
        {


            Assert.That(user.FirstName, Is.Null);
            Assert.That(user.LastName, Is.Null);
            Assert.That(user.PersonalIdentificationNumber, Is.Null);
            Assert.That(user.PhoneNumber, Is.Null);



        }
    }
}
