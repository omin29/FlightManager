using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Data.Context;
using Data.Models;

namespace Web.Controllers
{
    public class UsersController : Controller
    {
        private readonly FlightManagerDbContext _context;

        public UsersController(FlightManagerDbContext context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.ToListAsync());
        }

        //GET: Users/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: UsersController/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: UsersController/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            UserEditViewModel model = new UserEditViewModel()
            {
                Id = user.Id,
                UserName = user.UserName,
                NormalizedUserName = user.NormalizedUserName,
                Email = user.Email,
                NormalizedEmail = user.NormalizedEmail,
                EmailConfirmed = user.EmailConfirmed,
                PasswordHash = user.PasswordHash,
                SecurityStamp = user.SecurityStamp,
                ConcurrencyStamp = user.ConcurrencyStamp,
                PhoneNumber = user.PhoneNumber,
                TwoFactorEnabled = user.TwoFactorEnabled,
                LockoutEnd = user.LockoutEnd,
                LockoutEnabled = user.LockoutEnabled,
                AccessFailedCount = user.AccessFailedCount,
                Address = user.Address,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PersonalIdentificationNumber = user.PersonalIdentificationNumber,
                Role = user.Role
            };

            return View(model);
        }

        // POST: Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,PersonalIdentificationNumber,FirstName,LastName,Email,PhoneNumber,Address,Role")] UserEditViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            User editedUser = _context.Users.FirstOrDefault(u => u.Id == model.Id);

            editedUser.Id = model.Id;
            editedUser.UserName = model.UserName;
            editedUser.NormalizedUserName = model.NormalizedUserName;
            editedUser.Email = model.Email;
            editedUser.NormalizedEmail = model.NormalizedEmail;
            editedUser.EmailConfirmed = model.EmailConfirmed;
            editedUser.PasswordHash = model.PasswordHash;
            editedUser.SecurityStamp = model.SecurityStamp;
            editedUser.ConcurrencyStamp = model.ConcurrencyStamp;
            editedUser.PhoneNumber = model.PhoneNumber;
            editedUser.TwoFactorEnabled = model.TwoFactorEnabled;
            editedUser.LockoutEnd = model.LockoutEnd;
            editedUser.LockoutEnabled = model.LockoutEnabled;
            editedUser.AccessFailedCount = model.AccessFailedCount;
            editedUser.Address = model.Address;
            editedUser.FirstName = model.FirstName;
            editedUser.LastName = model.LastName;
            editedUser.PersonalIdentificationNumber = model.PersonalIdentificationNumber;
            editedUser.Role = model.Role;           

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Users.Update(editedUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(editedUser.Id))
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
            return View(model);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var user = await _context.Users.FindAsync(id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(string id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
