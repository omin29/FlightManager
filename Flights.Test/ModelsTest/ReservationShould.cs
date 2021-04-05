using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Data.Context;
using Data.Models;

namespace Flights.Test
{
    [TestFixture]
    public class ReservationShould
    {
        private Reservation reservation;
        [SetUp]
        public void Setup()
        {
            this.reservation = new Reservation();
        }

        [Test]
        public void CorrectReservation()
        {

            Assert.That(reservation.Id, Is.EqualTo(expected: reservation.Id));
            Assert.That(reservation.FlightUniquePlaneNumber, Is.EqualTo(expected: reservation.FlightUniquePlaneNumber));
            Assert.That(reservation.Flight, Is.EqualTo(expected: reservation.Flight));
            Assert.That(reservation.Email, Is.EqualTo(expected: reservation.Email));
            Assert.That(reservation.Passengers, Is.EqualTo(expected: reservation.Passengers));
        }
        
       
    }
}
