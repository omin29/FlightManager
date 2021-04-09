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
    /// This class runs tests on class <see cref="Flight"/>
    /// </summary>
    [TestFixture]
    public class FlightsTests
    {
        private Flight flights;

        /// <summary>
        /// Prepares valid data for the <see cref="Flight"/> object in this class before every test.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            this.flights = new Flight()
            {
                UniquePlaneNumber = 1,
                LocationFrom = "Sofia",
                LocationTo = "Rome",
                DepartureTime = DateTime.Now,
                LandingTime = DateTime.Now.AddDays(1),
                PlaneType = "Тип Б",
                PilotName = "Kostantin",
                FreePassengerSeats = 50,
                FreeBusinessSeats = 25,
            };
        }
        /// <summary>
        /// This is the method which checks if the flight is valid.
        /// </summary>
        private void ValidateFlight()
        {
            ValidationContext entityValidationContext = new ValidationContext(flights);
            Validator.ValidateObject(flights, entityValidationContext, true);
        }

        /// <summary>
        /// Checks an invalid pilot name which does not contain only letters.
        /// </summary>
        [Test]
        public void CheckInvalidPilotName()
        {
            flights.PilotName = "Kostantin@123412";
            Assert.Throws<ValidationException>(ValidateFlight, "The flight name can contains only letters.");
        }
        /// <summary>
        /// Checks an invalid pilot name which exceeds the 50 character length limit.
        /// </summary>
        [Test]
        public void CheckInvalidPilotNameSize()
        {
            flights.PilotName = new string('a', 51);
            Assert.Throws<ValidationException>(ValidateFlight, "The flight name cannot be longer than 50 characters.");
        }
        /// <summary>
        /// Check a completely valid flight which should not throw <see cref="ValidationException"/>.
        /// </summary>
        [Test]
        public void CheckCompletelyValidFlight()
        {
            Assert.DoesNotThrow(ValidateFlight, "The flight is not valid when it is supposed to be.");
        }
        /// <summary>
        /// Checks an invalid free passenger seats value which is negative.
        /// </summary>
        [Test]
        public void CheckInvalidFreePassengerSeats()
        {
            flights.FreePassengerSeats = -10;
            Assert.Throws<ValidationException>(ValidateFlight, "The number of free seats must be a positive number or 0.");
        }
        /// <summary>
        /// Checks an invalid free business seats value which is negative.
        /// </summary>
        [Test]
        public void CheckInvalidFreeBusinessSeats()
        {
            flights.FreeBusinessSeats = -10;
            Assert.Throws<ValidationException>(ValidateFlight, "The number of free seats must be a positive number or 0.");
        }
        /// <summary>
        /// Check an invalid flight which has the same departure location as destination.
        /// </summary>
        [Test]
        public void CheckInvalidLocacionFrom() 
        {
            flights.LocationFrom ="Rome";
            Assert.Throws<ValidationException>(ValidateFlight,
                "The departure location cannot the same as the destination.");
        }
       
    }
}
