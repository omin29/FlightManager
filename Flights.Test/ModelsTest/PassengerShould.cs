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
        public void CorrectPassenger1()
        {

            Assert.That(passenger.Reservation, Is.Null);
            Assert.That(passenger.Id, Is.Not.Null);
            Assert.That(passenger.FirstName, Is.Null);
            Assert.That(passenger.LastName, Is.Null);
            Assert.That(passenger.MiddleName, Is.Null);
            Assert.That(passenger.PersonalIdentificationNumber, Is.Null);
            Assert.That(passenger.PhoneNumber, Is.Null);
            Assert.That(passenger.Nationality, Is.Null);
            Assert.That(passenger.ReservationId, Is.Not.Null);
            Assert.That(passenger.TicketType, Is.Null);


        }
    }
}
