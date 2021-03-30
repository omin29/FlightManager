using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Data.Context;
using Data.Models;

namespace Flights.Test
{
    [TestFixture]
    public class FlightsShould
    {
        private Flight flights;
        [SetUp]
        public void Setup()
        {
            this.flights = new Flight();
        }


        [Test]
        public void CorrectFlights()
        {

            Assert.That(flights.PilotName, Is.EqualTo(expected: flights.PilotName));
            Assert.That(flights.LocationFrom, Is.EqualTo(expected: flights.LocationFrom));
            Assert.That(flights.LocationTo, Is.EqualTo(expected: flights.LocationTo));
            Assert.That(flights.PlaneType, Is.EqualTo(expected: flights.PlaneType));

        }
        [Test]
        public void CorrectFlights1()
        {

            Assert.That(flights.PilotName, Is.Null);
            Assert.That(flights.LocationFrom, Is.Null);
            Assert.That(flights.LocationTo, Is.Null);
            Assert.That(flights.PlaneType, Is.Null);

        }
    }
}
