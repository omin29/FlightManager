using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Data.Context;
using Data.Models;
using Data.Static;
using MimeKit;
using Data.Shared;
using Web.Pagers;
namespace Web.Controllers
{
    /// <summary>
    /// The controller provides CRUD operations for the passengers in the database.
    /// </summary>
    public class PassengersController : Controller
    {
        private readonly FlightManagerDbContext _context;

        /// <summary>
        /// Standard constructor which initializes a context used by the controller.
        /// </summary>
        /// <param name="context">The flight manager context.</param>
        public PassengersController(FlightManagerDbContext context)
        {           
            _context = context;
        }
        /// <summary>
        /// The Passengers controller for the Index page. Supports pagination and filtration
        /// </summary>
        /// <param name="searchString">string for filtering flights with given FirstName,MiddleName,LastName,PersonalIdentificationNumber</param>
        /// <param name="model">PassengerIndexViewModel object used to display filtered or paged records from Passengers</param>
        /// <param name="pages">int for the amount of records to be shown on a single page</param>
        /// <returns>Returns Passengers Index View with the filtered Passengers(if entered search string) otherwise pages all records</returns>
        // GET: Passengers
        public async Task<IActionResult> Index(string searchString, PassengerIndexViewModel model, int pages)
        {
            if (pages < 1) pages = PassengerPager.currentAmount;
            else PassengerPager.currentAmount = pages;
            model.Pager ??= new PagerViewModel();
            model.Pager.CurrentPage = model.Pager.CurrentPage <= 0 ? 1 : model.Pager.CurrentPage;
            List<Passenger> items = new List<Passenger>();

            //check if there is searchy string and filter all records without pagination
            if (!string.IsNullOrEmpty(searchString))
            {
                PassengerPager.search = searchString;
                items = await _context.Passengers.Select(u => new Passenger()
                {
                    Id = u.Id,
                    PersonalIdentificationNumber = u.PersonalIdentificationNumber,
                    FirstName = u.FirstName,
                    MiddleName = u.MiddleName,
                    LastName = u.LastName,
                    PhoneNumber=u.PhoneNumber,
                    Nationality=u.Nationality,
                    TicketType=u.TicketType,
                    Reservation=u.Reservation,
                    ReservationId=u.ReservationId
                }).Where(s => (s.LastName.Contains(searchString) || s.FirstName.Contains(searchString) ||
                s.MiddleName.Contains(searchString) || s.PersonalIdentificationNumber.Contains(searchString))).ToListAsync();
                model.Items = items;
                model.Pager.CurrentPage = 1;
                model.Pager.PagesCount = 1;
            }
            //if not searching use pagination
            else
            {
                PassengerPager.search = "";
                items = await _context.Passengers.Skip((model.Pager.CurrentPage - 1) * pages).Take(pages).Select(u => new Passenger()
                {
                    Id = u.Id,
                    PersonalIdentificationNumber = u.PersonalIdentificationNumber,
                    FirstName = u.FirstName,
                    MiddleName = u.MiddleName,
                    LastName = u.LastName,
                    PhoneNumber = u.PhoneNumber,
                    Nationality = u.Nationality,
                    TicketType = u.TicketType,
                    Reservation = u.Reservation,
                    ReservationId = u.ReservationId
                }).ToListAsync();
                model.Items = items;
                model.Pager.PagesCount = (int)Math.Ceiling(await _context.Passengers.CountAsync() / (double)pages);
                //if you change the show count and there are not enough pages e.g. you have 6 pages for show X &
                //change the count to Y ur current page is empty therefore set the current page to 1 and redo pagination
                if (!model.Items.Any())
                {
                    model.Pager.CurrentPage = 1;
                    items = await _context.Passengers.Skip((model.Pager.CurrentPage - 1) * pages).Take(pages).Select(u => new Passenger()
                    {
                        Id = u.Id,
                        PersonalIdentificationNumber = u.PersonalIdentificationNumber,
                        FirstName = u.FirstName,
                        MiddleName = u.MiddleName,
                        LastName = u.LastName,
                        PhoneNumber = u.PhoneNumber,
                        Nationality = u.Nationality,
                        TicketType = u.TicketType,
                        Reservation = u.Reservation,
                        ReservationId = u.ReservationId
                    }).ToListAsync();
                    model.Items = items;
                }
            }
            model.Pager.ShowRecords = PassengerPager.currentAmount;
            
            return View(model);


            //var flightManagerDbContext = _context.Passengers.Include(p => p.Reservation);
            //return View(await flightManagerDbContext.ToListAsync());
        }

        // GET: Passengers/Details/5
        /// <summary>
        /// Shows a detailed view of a passenger.
        /// </summary>
        /// <param name="id">Primary key of passenger.</param>
        /// <returns>Passenger detail view.</returns>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var passenger = await _context.Passengers
                .Include(p => p.Reservation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (passenger == null)
            {
                return NotFound();
            }

            return View(passenger);
        }

        /// <summary>
        /// Prepares information about the reservation which is used in the creating form for passengers.
        /// </summary>
        /// <param name="reservationId">Primary key of the reservation for which the passenger is made.</param>
        /// <returns>Passenger creation view.</returns>
        public IActionResult Create(string reservationId)
        {
            //ViewData["ReservationId"] = new SelectList(_context.Reservations, "Id", "Email");
            ViewData["ReservationId"] = int.Parse(reservationId);
            return View();
        }

        // POST: Passengers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Creates a passenger which goes to a pending list. This process repeats until the user stops adding passengers to the reservation.
        /// After the users confirmation a final method for finishing the reservation is called.
        /// </summary>
        /// <param name="passenger">A passenger created by the user</param>
        /// <param name="finishAdding">A bool value which indicates if the user has finished adding passengers to the reservation.</param>
        /// <returns>The <see cref="FinishReservation"/> method.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PersonalIdentificationNumber,FirstName,MiddleName,LastName,PhoneNumber,Nationality,TicketType,ReservationId")] Passenger passenger,
            string finishAdding)
        {
            if (ModelState.IsValid)
            {               
                ReservationAssistant.PendingPassengers.Add(passenger);

                if (finishAdding == "false")
                {
                    return Redirect($"/Passengers/Create?reservationId={passenger.ReservationId}");
                }
                else
                {
                    return RedirectToAction(nameof(FinishReservation));
                }
            }
            //ViewData["ReservationId"] = new SelectList(_context.Reservations, "Id", "Email", passenger.ReservationId);
            ViewData["ReservationId"] = passenger.ReservationId;
            return View(passenger);
        }

        /// <summary>
        /// Finishes the reservation of passengers. It adds the pending passengers and reservation to the database if it is possible
        /// and updates the free flight seats number. An email is sent to the email address provided by the user for the reservation.
        /// Depending on the circumstances this email will notify the user if the reservation has been successful or not by providing
        /// information about the flight and passengers related to the reservation.
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> FinishReservation()
        {                 
            Flight flight = _context.Flights.Where(x => x.UniquePlaneNumber == ReservationAssistant.PendingReservation.FlightUniquePlaneNumber).First();
            int normalSeatsForReservation = ReservationAssistant.PendingPassengers.Where(x => x.TicketType == "Normal").Count();
            int businessSeatsForReservation = ReservationAssistant.PendingPassengers.Where(x => x.TicketType == "Business").Count();
            MimeMessage email = null;

            if (flight.FreePassengerSeats >= normalSeatsForReservation && flight.FreeBusinessSeats >= businessSeatsForReservation)
            {
                ReservationAssistant.PendingReservation.Id = 0;
                _context.Reservations.Add(ReservationAssistant.PendingReservation);
                await _context.SaveChangesAsync();

                int pendingReservationId = _context.Reservations.OrderByDescending(x => x.Id).First().Id;

                foreach (var passenger in ReservationAssistant.PendingPassengers)
                {
                    passenger.ReservationId = pendingReservationId;
                    _context.Passengers.Add(passenger);
                }

                //await _context.SaveChangesAsync();

                flight.FreePassengerSeats -= normalSeatsForReservation;
                flight.FreeBusinessSeats -= businessSeatsForReservation;
                _context.Flights.Update(flight);
                await _context.SaveChangesAsync();

                email = ReservationAssistant.PrepareEmail(ReservationAssistant.PrepareTheWholeEmailBody(true));
            }
            else
            {
                email = ReservationAssistant.PrepareEmail(ReservationAssistant.PrepareTheWholeEmailBody(false));
            }

            ReservationAssistant.SendEmail(email);
            ReservationAssistant.ClearPendingPassengers();

            return RedirectToAction(nameof(Index));
        }

        //IMPORTANT NOTE: The controller methods for editing and deleting passengers have been removed because they are not used in this application!
    }
}
