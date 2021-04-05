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
    /// <summary>
    /// A static class which provides methods for the creation of reservations. It also stores data of pending passengers and their reservation which are not
    /// ready to be added to the database.
    /// </summary>
    public static class ReservationAssistant
    {
        /// <summary>
        /// The pending reservation which cannot be completed because the amount of free seats for the flight are not checked and the user has not finished adding passengers
        /// to the reservation. This static property stores data temporarily.
        /// </summary>
        public static Reservation PendingReservation { get; set; }

        /// <summary>
        /// The pending passengers which cannot be added to the database because the amount of free seats for the flight are not checked and the user has not finished adding passengers
        /// to the reservation. This static property stores data temporarily.
        /// </summary>
        public static List<Passenger> PendingPassengers { get; set; } = new List<Passenger>();

        /// <summary>
        /// Removes the pending passengers from the static <see cref="ReservationAssistant"/> class because they will be replaced by new ones for future reservations.
        /// It is used when the pending reservation has changed or been finished.
        /// </summary>
        public static void ClearPendingPassengers()
        {
            PendingPassengers.Clear();
        }

        /// <summary>
        /// Generates a random unique identifier for the pending reservation. It is done to differentiate the different pending reservations which the user creates.
        /// </summary>
        /// <returns>An Id for the pending reservation.</returns>
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

        /// <summary>
        /// Prepares the whole email by configuring its sender, receiver, subject and body. A dedicated gmail account exists as the sender.
        /// The receiver is the email which the user specifies while creating the reservation. The email is about the status of the reservation
        /// and notifies the user if it is successful or not.
        /// </summary>
        /// <param name="emailBody">The completed email body which is formatted text. The email body is a StringBuilder object.<see cref="StringBuilder"/></param>
        /// <returns>A <see cref="MimeMessage"/> object representing an email which is ready to be sent.</returns>
        public static MimeMessage PrepareEmail(StringBuilder emailBody)
        {
            MimeMessage message = new MimeMessage();

            MailboxAddress from = new MailboxAddress("Server", @"flightmanagerserver@gmail.com");
            message.From.Add(from);

            MailboxAddress to = new MailboxAddress("User", PendingReservation.Email);

            message.To.Add(to);

            message.Subject = "Flight reservation status";

            BodyBuilder bodyBuilder = new BodyBuilder();
            bodyBuilder.TextBody = emailBody.ToString();

            message.Body = bodyBuilder.ToMessageBody();

            return message;
        }

        /// <summary>
        /// Extracts a flight from the database using its unique plane number.
        /// </summary>
        /// <param name="uniquePlaneNumber">The unique plane number of the flight.</param>
        /// <returns>A <see cref="Models.Flight"/> object.</returns>
        private static Flight GetFlightByUniquePlaneNumber(int uniquePlaneNumber)
        {
            Flight flight = null;

            using (var db = new FlightManagerDbContext(new DbContextOptions<FlightManagerDbContext>()))
            {
                flight = db.Flights.Where(x => x.UniquePlaneNumber == uniquePlaneNumber).FirstOrDefault();
            }

            return flight;
        }

        /// <summary>
        /// Prepares a part of the email body when the reservation is successful. It includes information
        /// about the passengers and the flight related to the reservation.
        /// </summary>
        /// <param name="emailBody">A <see cref="StringBuilder"/> object which contains the email body.</param>
        /// <param name="flight">The fight for which the reservation is being made.<see cref="Models.Flight"/></param>
        /// <returns>The modified email body which is formatted text. The email body is still a <see cref="StringBuilder"/> object.</returns>
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

        /// <summary>
        /// Prepares a part of the email body when the reservation is unsuccessful. It includes information
        /// about the passengers related to the reservation.
        /// </summary>
        /// <param name="emailBody">A <see cref="StringBuilder"/> object which contains the email body.</param>
        /// <returns>The modified email body which is formatted text. The email body is still a <see cref="StringBuilder"/> object.</returns>
        private static StringBuilder GetFailedReservationInformation(StringBuilder emailBody)
        {
            emailBody.AppendLine("Reservation failed due to lack of free seats.");
            emailBody.AppendLine("The number of seats you wanted to register:");
            emailBody.AppendFormat("Normal seats - {0}\n", PendingPassengers.Where(x => x.TicketType == "Normal").Count());
            emailBody.AppendFormat("Business seats - {0}\n", PendingPassengers.Where(x => x.TicketType == "Business").Count());
            emailBody.AppendLine(new string('-', 30));

            return emailBody;
        }

        /// <summary>
        /// Prepares the whole email body depending on the status of the reservation. It can either create an email body for successful or failed
        /// reservation.
        /// </summary>
        /// <param name="isSuccessfulReservation">The status of the reservation. A bool which indicates whether or not the reservation is successful.</param>
        /// <returns>The completed email body which is formatted text. The email body is a <see cref="StringBuilder"/> object.</returns>
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
                emailBody = GetFailedReservationInformation(emailBody);
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

        /// <summary>
        /// Connects to a dedicated gmail account which sends an email to the user.
        /// </summary>
        /// <param name="email">A <see cref="MimeMessage"/> object representing an email which is ready to be sent.</param>
        public static void SendEmail(MimeMessage email)
        {
            SmtpClient client = new SmtpClient();
            client.Connect(@"smtp.gmail.com", 587);
            client.Authenticate(@"flightmanagerserver@gmail.com", "flightmanager123");
            
            client.Send(email);
            client.Disconnect(true);
            client.Dispose();
        }
    }
}
