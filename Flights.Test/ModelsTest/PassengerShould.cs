using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Data.Context;
using Data.Models;

namespace Flights.Test
{
    [TestFixture]
    public class PassengerShould
    {
        private Passenger passenger;
        [SetUp]
        public void Setup()
        {
            this.passenger = new Passenger();
        }


        [Test]
        public void CorrectPassenger()
        {
            Assert.That(passenger.Reservation, Is.EqualTo(expected: passenger.Reservation));
            Assert.That(passenger.TicketType, Is.EqualTo(expected: passenger.TicketType));
            Assert.That(passenger.ReservationId, Is.EqualTo(expected: passenger.ReservationId));
            Assert.That(passenger.Nationality, Is.EqualTo(expected: passenger.Nationality));
            Assert.That(passenger.MiddleName, Is.EqualTo(expected: passenger.MiddleName));
            Assert.That(passenger.Id, Is.EqualTo(expected: passenger.Id));
            Assert.That(passenger.FirstName, Is.EqualTo(expected: passenger.FirstName));
            Assert.That(passenger.LastName, Is.EqualTo(expected: passenger.LastName));
            Assert.That(passenger.PhoneNumber, Is.EqualTo(expected: passenger.PhoneNumber));
            Assert.That(passenger.PersonalIdentificationNumber, Is.EqualTo(expected: passenger.PersonalIdentificationNumber));
        }
        [Test]
        [TestCase(12345678)]
        public void ErrorNumber(int result)
        {
            //проверява тел. номер с по-малко от 10 символа ако тел. номер е с по-малко от 10символа теста работи
            this.passenger.CheckNumber(result);
            Assert.IsFalse(this.passenger.CheckNumber(result), "Phone number must be 10 digits");
        }

        [Test]
        [TestCase(1987868643)]
        public void CorrectNumber(int result)
        {
            //проверява телефонен номер с 10символа
            this.passenger.CheckNumber(result);
            Assert.IsTrue(this.passenger.CheckNumber(result), "Phone number must be 10 digits");
        }
    }
}
