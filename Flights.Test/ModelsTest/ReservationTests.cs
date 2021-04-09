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
    /// This class runs tests on class <see cref="Reservation"/>
    /// </summary>
    [TestFixture]
    public class ReservationTests
    {
        private Reservation reservation;
        /// <summary>
        ///  Prepares valid data for the <see cref="Reservation"/> object in this class before every test.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            this.reservation = new Reservation() 
            { 
             Id=3,
             FlightUniquePlaneNumber=12,
             Email="user01@abv.bg",
            };
        }
        /// <summary>
        /// This is the method which checks if the reservation is valid.
        /// </summary>
        private void ValidateReservation()
        {
            ValidationContext entityValidationContext = new ValidationContext(reservation);
            Validator.ValidateObject(reservation, entityValidationContext, true);
        }
        /// <summary>
        /// Checks a  completely valid reservation which should not throw <see cref="ValidationException"/>.
        /// </summary>
        [Test]
        public void CheckCompletelyValidReservation()
        {
            Assert.DoesNotThrow(ValidateReservation, "The reservation is not valid when it is supposed to be.");
        }
        /// <summary>
        /// Checks an invalid email address.
        /// </summary>
        [Test]
        public void CheckInvalidEmail() 
        {
           reservation.Email = "Gosho";
           Assert.Throws<ValidationException>(ValidateReservation, "A valid email address contains '@'.");
        }

    }
}
