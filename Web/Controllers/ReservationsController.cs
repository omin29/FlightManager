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
using Data.Shared;
using Web.Pagers;
namespace Web.Controllers
{
    public class ReservationsController : Controller
    {
        private readonly FlightManagerDbContext _context;

        public ReservationsController(FlightManagerDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// The Reservations controller for the Index page. Supports pagination and filtration
        /// </summary>
        /// <param name="searchString">string for filtering Reservations with given Email</param>
        /// <param name="model">ReservationIndexViewModel object used to display filtered or paged records from Reservations</param>
        /// <param name="pages">int for the amount of records to be shown on a single page</param>
        /// <returns>Returns Reservations Index View with the filtered Reservations(if entered search string) otherwise pages all records</returns>
        // GET: Reservations
        public async Task<IActionResult> Index(string searchString, ReservationIndexViewModel model, int pages)
        {
            if (pages < 1) pages = ReservationPager.currentAmount;
            else ReservationPager.currentAmount = pages;
            model.Pager ??= new PagerViewModel();
            model.Pager.CurrentPage = model.Pager.CurrentPage <= 0 ? 1 : model.Pager.CurrentPage;
            List<Reservation> items = new List<Reservation>();
            //check if there is searchy string and filter all records without pagination
            if (!string.IsNullOrEmpty(searchString))
            {
                ReservationPager.search = searchString;
                items = await _context.Reservations.Select(u => new Reservation()
                {
                    Id = u.Id,
                    Flight = u.Flight,
                    Email = u.Email
                }).Where(s => s.Email.Contains(searchString)).ToListAsync();
                model.Items = items;
                model.Pager.CurrentPage = 1;
                model.Pager.PagesCount = 1;
            }
            //if not searching use pagination
            else
            {
                ReservationPager.search = "";
                items = await _context.Reservations.Skip((model.Pager.CurrentPage - 1) * pages).Take(pages).Select(u => new Reservation()
                {
                    Id = u.Id,
                    Flight = u.Flight,
                    Email = u.Email
                }).ToListAsync();
                model.Items = items;
                model.Pager.PagesCount = (int)Math.Ceiling(await _context.Reservations.CountAsync() / (double)pages);
                //if you change the show count and there are not enough pages e.g. you have 6 pages for show X &
                //change the count to Y ur current page is empty therefore set the current page to 1 and redo pagination
                if (!model.Items.Any())
                {
                    model.Pager.CurrentPage = 1;
                    items = await _context.Reservations.Skip((model.Pager.CurrentPage - 1) * pages).Take(pages).Select(u => new Reservation()
                    {
                        Id=u.Id,
                        Flight=u.Flight,
                        Email=u.Email
                    }).ToListAsync();
                    model.Items = items;
                }
            }
            model.Pager.ShowRecords = ReservationPager.currentAmount;
            return View(model);
        }
        /// <summary>
        /// The Reservation controller for the Details page. Shows details about the Reservation and the Passengers in it
        /// </summary>
        /// <param name="id">int id for the reserved Flight</param>
        /// <returns>Returns Reservation Detail page showing information for both the Reservation and the Passengers</returns>
        // GET: Reservations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations
                .Include(r => r.Flight)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // GET: Reservations/Create
        public IActionResult Create()
        {
            ViewData["FlightUniquePlaneNumber"] = new SelectList(_context.Flights, "UniquePlaneNumber", /*"LocationFrom"*/"FlightGeneralInfo");
            return View();
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FlightUniquePlaneNumber,Email")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                /*_context.Add(reservation);
                await _context.SaveChangesAsync();*/

                reservation.Id = ReservationAssistant.GetUniqueId();
                ReservationAssistant.PendingReservation = reservation;

                //return RedirectToAction(nameof(Index));
                return Redirect($"/Passengers/Create?reservationId={reservation.Id}");
            }
            ViewData["FlightUniquePlaneNumber"] = new SelectList(_context.Flights, "UniquePlaneNumber", /*"LocationFrom"*/"FlightGeneralInfo", reservation.FlightUniquePlaneNumber);
            return View(reservation);
        }

        // GET: Reservations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }
            ViewData["FlightUniquePlaneNumber"] = new SelectList(_context.Flights, "UniquePlaneNumber", /*"LocationFrom"*/"FlightGeneralInfo", reservation.FlightUniquePlaneNumber);
            return View(reservation);
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FlightUniquePlaneNumber,Email")] Reservation reservation)
        {
            if (id != reservation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reservation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservationExists(reservation.Id))
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
            ViewData["FlightUniquePlaneNumber"] = new SelectList(_context.Flights, "UniquePlaneNumber", /*"LocationFrom"*/"FlightGeneralInfo", reservation.FlightUniquePlaneNumber);
            return View(reservation);
        }

        // GET: Reservations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations
                .Include(r => r.Flight)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservationExists(int id)
        {
            return _context.Reservations.Any(e => e.Id == id);
        }
    }
}
