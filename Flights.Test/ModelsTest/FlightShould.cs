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
            Assert.That(flights.UniquePlaneNumber, Is.EqualTo(expected: flights.UniquePlaneNumber));
            Assert.That(flights.PilotName, Is.EqualTo(expected: flights.PilotName));
            Assert.That(flights.LocationFrom, Is.EqualTo(expected: flights.LocationFrom));
            Assert.That(flights.LocationTo, Is.EqualTo(expected: flights.LocationTo));
            Assert.That(flights.PlaneType, Is.EqualTo(expected: flights.PlaneType));
            Assert.That(flights.FreePassengerSeats, Is.EqualTo(expected: flights.FreePassengerSeats));
            Assert.That(flights.FreeBusinessSeats, Is.EqualTo(expected: flights.FreeBusinessSeats));
            Assert.That(flights.Reservations, Is.EqualTo(expected: flights.Reservations));
            Assert.That(flights.FlightGeneralInfo, Is.EqualTo(expected: flights.FlightGeneralInfo));
            Assert.That(flights.DepartureTime, Is.EqualTo(expected: flights.DepartureTime));
            Assert.That(flights.LandingTime, Is.EqualTo(expected: flights.LandingTime));
        }
        
        [Test]
        [TestCase(-10)]
        [TestCase(-20)]
        [TestCase(-30)]
        public void TestFreePassengerSeats(int result) 
        {
            //вярно е ако е грешно
            this.flights.FreeSeats(result);
            Assert.IsFalse(this.flights.FreeSeats(result),"The number of free seats must be a positive number or 0");
        }

        [Test]
        [TestCase(10)]
        [TestCase(20)]
        [TestCase(30)]
        public void TestFreePassengerSeatsCorrect(int result)
        {
            //вярно е ако е вярно
            this.flights.FreeSeats(result);
            Assert.IsTrue(this.flights.FreeSeats(result), "The number of free seats must be a positive number or 0");
        }
        [Test]
        [TestCase(-10)]
        [TestCase(-20)]
        [TestCase(-30)]
        public void TestFreeBusinessSeats(int result)
        {
            //вярно е ако е грешно
            this.flights.FreeSeats(result);
            Assert.IsFalse(this.flights.FreeSeats(result), "The number of free seats must be a positive number or 0");
        }

        [Test]
        [TestCase(10)]
        [TestCase(20)]
        [TestCase(30)]
        public void TestFreeBusinessSeatsCorrect(int result)
        {
            //вярно е ако е вярно
            this.flights.FreeSeats(result);
            Assert.IsTrue(this.flights.FreeSeats(result), "The number of free seats must be a positive number or 0");
        }
    }
}
