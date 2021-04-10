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
using Microsoft.AspNetCore.Authorization;
using Data.Shared;
using Web.Pagers;
namespace Web.Controllers
{
    /// <summary>
    /// The controller which is responsible for user CRUD operations. Accessible only by the administrator.
    /// </summary>
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly FlightManagerDbContext _context;

        /// <summary>
        /// Standard constructor which initializes a context used by the controller.
        /// </summary>
        /// <param name="context">The flight manager context.</param>
        public UsersController(FlightManagerDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// The Users controller for the Index page. Supports pagination and filtration
        /// </summary>
        /// <param name="searchString">string for filtering users with given UserName,FirstName,LastName,Email</param>
        /// <param name="model">UserIndexViewModel object used to display filtered or paged records from Users</param>
        /// <param name="pages">int for the amount of records to be shown on a single page</param>
        /// <returns>Returns Users Index View with the filtered Users(if entered search string) otherwise pages all records</returns>
        // GET: Users
        public async Task<IActionResult> Index(string searchString, UserIndexViewModel model, int pages)
        {
            if (pages < 1) pages = UserPager.currentAmount;
            else UserPager.currentAmount = pages;
            model.Pager ??= new PagerViewModel();
            model.Pager.CurrentPage = model.Pager.CurrentPage <= 0 ? 1 : model.Pager.CurrentPage;
            List<User> items = new List<User>();

            //check if there is searchy string and filter all records without pagination
            if (!string.IsNullOrEmpty(searchString))
            {
                UserPager.search = searchString;
                items = await _context.Users.Select(u => new User()
                {
                    Id=u.Id,
                    PersonalIdentificationNumber = u.PersonalIdentificationNumber,
                    UserName = u.UserName,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email
                }).Where(s => (s.LastName.Contains(searchString) || s.FirstName.Contains(searchString) || s.Email.Contains(searchString))).ToListAsync();
                model.Items = items;
                model.Pager.CurrentPage = 1;
                model.Pager.PagesCount = 1;
            }
            //if not searching use pagination
            else 
            {
                UserPager.search = "";
                items = await _context.Users.Skip((model.Pager.CurrentPage - 1) * pages).Take(pages).Select(u => new User() { 
                Id = u.Id,
                 PersonalIdentificationNumber = u.PersonalIdentificationNumber,
                 UserName = u.UserName,
                 FirstName = u.FirstName,
                 LastName = u.LastName,
                 Email = u.Email
                }).ToListAsync();
                model.Items = items;
                model.Pager.PagesCount = (int)Math.Ceiling(await _context.Users.CountAsync() / (double)pages);
                //if you change the show count and there are not enough pages e.g. you have 6 pages for show X &
                //change the count to Y ur current page is empty therefore set the current page to 1 and redo pagination
                if (!model.Items.Any())
                {
                    model.Pager.CurrentPage = 1;
                    items = await _context.Users.Skip((model.Pager.CurrentPage - 1) * pages).Take(pages).Select(u => new User()
                    {
                        Id = u.Id,
                        PersonalIdentificationNumber = u.PersonalIdentificationNumber,
                        UserName = u.UserName,
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                        Email = u.Email
                    }).ToListAsync();
                    model.Items = items;
                }
            }
            model.Pager.ShowRecords = UserPager.currentAmount;
            return View(model);
        }

        //GET: Users/Details/5
        /// <summary>
        /// Returns a detailed view for a user.
        /// </summary>
        /// <param name="id">Primary key of the user</param>
        /// <returns>User detail view.</returns>
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
        /// <summary>
        /// Redirects to the user registration form.
        /// </summary>
        /// <returns>User registration form.</returns>
        public ActionResult Create()
        {
            return Redirect(@"https://localhost:44370/Identity/Account/Register");
        }

        // GET: Users/Edit/5
        /// <summary>
        /// Finds an user from the database using its primary key and prepares it for edit.
        /// </summary>
        /// <param name="id">Primary key of the user</param>
        /// <returns>User edit view.</returns>
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
        /// <summary>
        /// Takes an edited <see cref="User"/> object and saves its changes to the database.
        /// </summary>
        /// <param name="id">Primary key of the user</param>
        /// <param name="model">The edited user.</param>
        /// <returns>The index page for users.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,PersonalIdentificationNumber,UserName,FirstName,LastName,Email,PhoneNumber,Address,Role,"+
            "NormalizedUserName,NormalizedEmail,EmailConfirmed,PasswordHash,SecurityStamp,ConcurrencyStamp,TwoFactorEnabled,LockoutEnd,"+
            "LockoutEnabled,AccessFailedCount")] UserEditViewModel model)
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
        /// <summary>
        /// Extracts an user from the database and prepares it for delete.
        /// </summary>
        /// <param name="id">Primary key of user.</param>
        /// <returns>User delete view.</returns>
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
        /// <summary>
        /// Deletes an user from the database after receiving a confirmation. The administrator account cannot be deleted.
        /// </summary>
        /// <param name="id">Primary key of user</param>
        /// <returns>The index page for users.</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var user = await _context.Users.FindAsync(id);

            if(user.Role!= "Admin")
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
            
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Checks if an user exists in the database by searching for it with primary key.
        /// </summary>
        /// <param name="id">Primary key of user</param>
        /// <returns>A bool value which indicates if the user exists.</returns>
        private bool UserExists(string id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
