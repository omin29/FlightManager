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

namespace Web.Controllers
{
    public class PassengersController : Controller
    {
        private readonly FlightManagerDbContext _context;

        public PassengersController(FlightManagerDbContext context)
        {           
            _context = context;
        }

        // GET: Passengers
        public async Task<IActionResult> Index()
        {
            var flightManagerDbContext = _context.Passengers.Include(p => p.Reservation);
            return View(await flightManagerDbContext.ToListAsync());
        }

        // GET: Passengers/Details/5
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

        // GET: Passengers/Create
        /*public IActionResult Create()
        {
            ViewData["ReservationId"] = new SelectList(_context.Reservations, "Id", "Email");
            return View();
        }*/

        public IActionResult Create(string reservationId)
        {
            //ViewData["ReservationId"] = new SelectList(_context.Reservations, "Id", "Email");
            ViewData["ReservationId"] = int.Parse(reservationId);
            return View();
        }

        // POST: Passengers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PersonalIdentificationNumber,FirstName,MiddleName,LastName,PhoneNumber,Nationality,TicketType,ReservationId")] Passenger passenger,
            string finishAdding)
        {
            if (ModelState.IsValid)
            {
                /*_context.Add(passenger);
                await _context.SaveChangesAsync();*/
                /*if(ReservationAssistant.PendingPassengers.Count> 0 && ReservationAssistant.PendingReservation.Id != passenger.ReservationId)
                {
                    ReservationAssistant.PendingPassengers.Clear();
                }*/

                if(ReservationAssistant.PendingPassengers.Any(x=>x.ReservationId != passenger.ReservationId))
                {
                    ReservationAssistant.ClearPendingPassengers();
                }

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

        public async Task<IActionResult> FinishReservation()
        {
            /*Reservation pendingReservation = _context.Reservations.Where(x => x.Id == _context.Passengers.OrderByDescending(x=>x.Id).First().ReservationId).FirstOrDefault();
            Flight flight = _context.Flights.Where(x => x.UniquePlaneNumber == pendingReservation.FlightUniquePlaneNumber).FirstOrDefault();
            int normalSeatsForReservation = _context.Passengers.Where(x => x.ReservationId == pendingReservation.Id).Where(x => x.TicketType == "Normal").Count();
            int businessSeatsForReservation = _context.Passengers.Where(x => x.ReservationId == pendingReservation.Id).Where(x => x.TicketType == "Business").Count();*/
            
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

        // GET: Passengers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var passenger = await _context.Passengers.FindAsync(id);
            if (passenger == null)
            {
                return NotFound();
            }
            ViewData["ReservationId"] = new SelectList(_context.Reservations, "Id", "Email", passenger.ReservationId);
            return View(passenger);
        }

        // POST: Passengers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PersonalIdentificationNumber,FirstName,MiddleName,LastName,PhoneNumber,Nationality,TicketType,ReservationId")] Passenger passenger)
        {
            if (id != passenger.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(passenger);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PassengerExists(passenger.Id))
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
            ViewData["ReservationId"] = new SelectList(_context.Reservations, "Id", "Email", passenger.ReservationId);
            return View(passenger);
        }

        // GET: Passengers/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

        // POST: Passengers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var passenger = await _context.Passengers.FindAsync(id);
            _context.Passengers.Remove(passenger);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PassengerExists(int id)
        {
            return _context.Passengers.Any(e => e.Id == id);
        }
    }
}
