using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Data.Context;
using Data.Models;
using Web.Models;

namespace Flights.Test
{
    [TestFixture]
    public class ErrorViewModelShould
    {
        private ErrorViewModel error;
        [SetUp]
        public void Setup()
        {
            this.error = new ErrorViewModel();
        }


        [Test]
        public void CorrectErrorViewModel()
        {
            Assert.That(error.RequestId, Is.EqualTo(expected: error.RequestId));

        }
        [Test]
        public void CorrectErrorViewModel1()
        {

            Assert.That(error.RequestId, Is.Null);


        }
    }
}
