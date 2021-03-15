using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Models;
using System.Text;
using Data.Context;
using System.Runtime;
using Microsoft.EntityFrameworkCore;
using MailKit;//for email
using MailKit.Net.Smtp;//for email
using MimeKit;//for email

namespace Data.Static
{
    public static class ReservationAssistant
    {
        public static Reservation PendingReservation { get; set; }

        public static List<Passenger> PendingPassengers { get; set; } = new List<Passenger>();

        public static void ClearPendingPassengers()
        {
            //PendingReservation = null;
            PendingPassengers.Clear();
        }

        public static int GetUniqueId()
        {
            int id = new Random().Next(int.MaxValue);

            if (PendingReservation != null)
            {
                while (id == PendingReservation.Id)
                {
                    id = new Random().Next(int.MaxValue);
                }
            }           

            return id;
        }

        public static MimeMessage PrepareEmail(StringBuilder emailBody)
        {
            MimeMessage message = new MimeMessage();

            MailboxAddress from = new MailboxAddress("Server", @"flightmanagerserver@gmail.com");
            message.From.Add(from);

            MailboxAddress to = new MailboxAddress("User", PendingReservation.Email/*@"flightmanagerserver@gmail.com"*/);

            message.To.Add(to);

            message.Subject = "Flight reservation status";

            BodyBuilder bodyBuilder = new BodyBuilder();
            bodyBuilder.TextBody = emailBody.ToString();

            message.Body = bodyBuilder.ToMessageBody();

            return message;
        }

        private static Flight GetFlightByUniquePlaneNumber(int uniquePlaneNumber)
        {
            Flight flight = null;

            using (var db = new FlightManagerDbContext(new DbContextOptions<FlightManagerDbContext>()))
            {
                flight = db.Flights.Where(x => x.UniquePlaneNumber == uniquePlaneNumber).FirstOrDefault();
            }

            return flight;
        }

        private static StringBuilder GetSuccessfulReservationInformation(StringBuilder emailBody, Flight flight)
        {
            emailBody.AppendLine("Your reservation was successful.");
            emailBody.AppendFormat("Number of passengers for whom the reservation is made: {0}\n", PendingPassengers.Count);
            emailBody.AppendLine(@"Passengers' personal identification numbers:");

            foreach (Passenger passenger  in PendingPassengers)
            {
                emailBody.AppendFormat("- {0}\n", passenger.PersonalIdentificationNumber);
            }

            emailBody.AppendLine(new string('-', 30));

            emailBody.AppendLine("Flight information:");
            emailBody.AppendFormat("General information - {0}\n", flight.FlightGeneralInfo);
            emailBody.AppendFormat("Departure time - {0}\n", flight.DepartureTime);
            emailBody.AppendFormat("Landing time - {0}\n", flight.LandingTime);
            emailBody.AppendLine(new string('-', 30));

            return emailBody;
        }

        private static StringBuilder GetFailedReservationInformation(StringBuilder emailBody, Flight flight)
        {
            emailBody.AppendLine("Reservation failed due to lack of free seats.");
            emailBody.AppendLine("The number of seats you wanted to register:");
            emailBody.AppendFormat("Normal seats - {0}\n", PendingPassengers.Where(x => x.TicketType == "Normal").Count());
            emailBody.AppendFormat("Business seats - {0}\n", PendingPassengers.Where(x => x.TicketType == "Business").Count());
            emailBody.AppendLine(new string('-', 30));

            return emailBody;
        }

        public static StringBuilder PrepareTheWholeEmailBody(bool isSuccessfulReservation)
        {
            StringBuilder emailBody = new StringBuilder();
            Flight flight = GetFlightByUniquePlaneNumber(PendingReservation.FlightUniquePlaneNumber);

            if (isSuccessfulReservation)
            {
                emailBody = GetSuccessfulReservationInformation(emailBody, flight);
            }
            else
            {
                emailBody = GetFailedReservationInformation(emailBody, flight);
            }

            using (var db = new FlightManagerDbContext(new DbContextOptions<FlightManagerDbContext>()))
            {
                flight = db.Flights.Find(flight.UniquePlaneNumber);
            }

            emailBody.AppendLine("Remaining flight seats:");
            emailBody.AppendFormat("Normal seats - {0}\n", flight.FreePassengerSeats);
            emailBody.AppendFormat("Business seats - {0}", flight.FreeBusinessSeats);

            return emailBody;
        }

        public static void SendEmail(MimeMessage email)
        {
            SmtpClient client = new SmtpClient();
            client.Connect(@"smtp.gmail.com", 587);
            //client.AuthenticationMechanisms.Remove("XOAUTH2");
            client.Authenticate(@"flightmanagerserver@gmail.com", "flightmanager123");
            
            client.Send(email);
            client.Disconnect(true);
            client.Dispose();
        }
    }
}
