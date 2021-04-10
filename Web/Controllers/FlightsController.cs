using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Data.Context;
using Data.Models;
using Microsoft.AspNetCore.Authorization;
using Data.Shared;
using Web.Pagers;
namespace Web.Controllers
{
    
    /// <summary>
    /// The controller which is responsible for flight CRUD operations.
    /// </summary>
    public class FlightsController : Controller
    {
        private readonly FlightManagerDbContext _context;

        /// <summary>
        /// Standard constructor which initializes a context used by the controller.
        /// </summary>
        /// <param name="context">The flight manager context.</param>
        public FlightsController(FlightManagerDbContext context)
        {
            _context = context;
        }

        // GET: Flights
        /// <summary>
        /// The Flights controller for the Index page. Supports pagination and filtration
        /// </summary>
        /// <param name="searchString">string for filtering Flights with given from location & to location</param>
        /// <param name="model">FlightIndexViewModel object used to display filtered or paged records from Flights</param>
        /// <param name="pages">int for the amount of records to be shown on a single page</param>
        /// <returns>Returns Flights Index View with the filtered Flights(if entered search string) otherwise pages all records</returns>
        public async Task<IActionResult> Index(string searchString, FlightIndexViewModel model, int pages)
        {
            if (pages < 1) pages = FlightPager.currentAmount;
            else FlightPager.currentAmount = pages;
            model.Pager ??= new PagerViewModel();
            model.Pager.CurrentPage = model.Pager.CurrentPage <= 0 ? 1 : model.Pager.CurrentPage;
            List<Flight> items = new List<Flight>();

            //check if there is searchy string and filter all records without pagination
            if (!string.IsNullOrEmpty(searchString))
            {
                FlightPager.search = searchString;
                items = await _context.Flights.Select(u => new Flight()
                {
                    UniquePlaneNumber=u.UniquePlaneNumber,
                    LocationFrom=u.LocationFrom,
                    LocationTo=u.LocationTo,
                    DepartureTime=u.DepartureTime,
                    LandingTime=u.LandingTime,
                    PlaneType=u.PlaneType,
                    PilotName=u.PilotName,
                    FreePassengerSeats=u.FreePassengerSeats,
                    FreeBusinessSeats=u.FreeBusinessSeats
                }).Where(s => (s.LocationFrom.Contains(searchString)|| s.LocationTo.Contains(searchString))).ToListAsync();
                model.Items = items;
                model.Pager.CurrentPage = 1;
                model.Pager.PagesCount = 1;
            }
            //if not searching use pagination
            else
            {
                FlightPager.search = "";
                items = await _context.Flights.Skip((model.Pager.CurrentPage - 1) * pages).Take(pages).Select(u => new Flight()
                {
                    UniquePlaneNumber = u.UniquePlaneNumber,
                    LocationFrom = u.LocationFrom,
                    LocationTo = u.LocationTo,
                    DepartureTime = u.DepartureTime,
                    LandingTime = u.LandingTime,
                    PlaneType = u.PlaneType,
                    PilotName = u.PilotName,
                    FreePassengerSeats = u.FreePassengerSeats,
                    FreeBusinessSeats = u.FreeBusinessSeats
                }).ToListAsync();
                model.Items = items;
                model.Pager.PagesCount = (int)Math.Ceiling(await _context.Flights.CountAsync() / (double)pages);
                //if you change the show count and there are not enough pages e.g. you have 6 pages for show X &
                //change the count to Y ur current page is empty therefore set the current page to 1 and redo pagination
                if (!model.Items.Any())
                {
                    model.Pager.CurrentPage = 1;
                    items = await _context.Flights.Skip((model.Pager.CurrentPage - 1) * pages).Take(pages).Select(u => new Flight()
                    {
                        UniquePlaneNumber = u.UniquePlaneNumber,
                        LocationFrom = u.LocationFrom,
                        LocationTo = u.LocationTo,
                        DepartureTime = u.DepartureTime,
                        LandingTime = u.LandingTime,
                        PlaneType = u.PlaneType,
                        PilotName = u.PilotName,
                        FreePassengerSeats = u.FreePassengerSeats,
                        FreeBusinessSeats = u.FreeBusinessSeats
                    }).ToListAsync();
                    model.Items = items;
                }
            }
            model.Pager.ShowRecords = FlightPager.currentAmount;
            return View(model);
            
        }
        /// <summary>
        /// The Flights controller for the Details page. Shows details about the Flight and Passengers in selected Flight
        /// </summary>
        /// <param name="id">int for Flight id to find Passengers</param>
        /// <returns>Returns Flight Detail page showing information for both the Flight and its Passengers</returns>
        // GET: Flights/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var flight = await _context.Flights
                .FirstOrDefaultAsync(m => m.UniquePlaneNumber == id);

            if (flight == null)
            {
                return NotFound();
            }

            List<Reservation> reservations = _context.Reservations.Where(x => x.FlightUniquePlaneNumber == flight.UniquePlaneNumber).ToList();
            List<Passenger> passengers = new List<Passenger>();

            foreach (var reservation in reservations)
            {
                passengers.AddRange(_context.Passengers.Where(x => x.ReservationId == reservation.Id));
            }

            ViewData["Passengers"] = passengers;


            return View(flight);
        }

        // GET: Flights/Create
        /// <summary>
        /// Returns the form for flight creation which the user will fill. Accessible only by the administrator.
        /// </summary>
        /// <returns>Flight creation view.</returns>
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Flights/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Creates a <see cref="Flight"/> object with the information provided by the user and adds it to the database. Accessible only by the administrator.
        /// </summary>
        /// <param name="flight">The flight which will be added to the database.</param>
        /// <returns>The index page for flights.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("UniquePlaneNumber,LocationFrom,LocationTo,DepartureTime,LandingTime,PlaneType,PilotName,FreePassengerSeats,FreeBusinessSeats")] Flight flight)
        {
            if (ModelState.IsValid)
            {
                _context.Add(flight);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(flight);
        }

        // GET: Flights/Edit/5
        /// <summary>
        /// Finds a flight from the database by using its primary key and prepares it for edit. Accessible only by the administrator.
        /// </summary>
        /// <param name="id">The id of the flight which will be edited.</param>
        /// <returns>Flight edit view</returns>
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flight = await _context.Flights.FindAsync(id);
            if (flight == null)
            {
                return NotFound();
            }
            return View(flight);
        }

        // POST: Flights/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Edits a flight and saves the changes to the database. Accessible only by the administrator.
        /// </summary>
        /// <param name="id">The primary key of the flight</param>
        /// <param name="flight">The modified flight which will be saved.</param>
        /// <returns>The index page for flights.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("UniquePlaneNumber,LocationFrom,LocationTo,DepartureTime,LandingTime,PlaneType,PilotName,FreePassengerSeats,FreeBusinessSeats")] Flight flight)
        {
            if (id != flight.UniquePlaneNumber)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(flight);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FlightExists(flight.UniquePlaneNumber))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(flight);
        }

        // GET: Flights/Delete/5
        /// <summary>
        /// Prepares a flight for deletion. Accessible only by the administrator.
        /// </summary>
        /// <param name="id">Primary key of the flight which will be deleted</param>
        /// <returns>Flight delete view</returns>
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flight = await _context.Flights
                .FirstOrDefaultAsync(m => m.UniquePlaneNumber == id);
            if (flight == null)
            {
                return NotFound();
            }

            return View(flight);
        }

        // POST: Flights/Delete/5
        /// <summary>
        /// Deletes the flight after receiving confirmation from the user. Accessible only by the administrator.
        /// </summary>
        /// <param name="id">Primary key of the deleted flight.</param>
        /// <returns>The index page for flights.</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var flight = await _context.Flights.FindAsync(id);
            _context.Flights.Remove(flight);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Checks if a flight exists in the database.
        /// </summary>
        /// <param name="id">Primary key of the flight.</param>
        /// <returns>A bool indicator which shows if the flight exists.</returns>
        private bool FlightExists(int id)
        {
            return _context.Flights.Any(e => e.UniquePlaneNumber == id);
        }
    }
}
