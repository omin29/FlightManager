using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Data.Models;
using Web.Models;
using Microsoft.AspNetCore.Authorization;

namespace Web.Controllers
{
    /// <summary>
    /// Responsible for controlling the start page and successful start of the project
    /// </summary>
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public readonly RoleManager<IdentityRole> roleManager;
        public readonly UserManager<User> userManager;

        public HomeController(ILogger<HomeController> logger, RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            _logger = logger;
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        /// <summary>
        /// Ensures that roles and administrator account exist on project start. The administrator has a default password which can be changed in the future.
        /// </summary>
        /// <returns>The home page</returns>
        public async Task<IActionResult> Index()
        {
            if(roleManager.Roles.Count() == 0)
            {
                await roleManager.CreateAsync(new IdentityRole
                {
                    Name = "Admin"
                });

                await roleManager.CreateAsync(new IdentityRole
                {
                    Name = "User"
                });
            }

            if (userManager.Users.Count() == 0)
            {
                var user = new User
                {
                    UserName = "Admin",
                    Email = "admin@abv.bg",
                    PhoneNumber = "9999999999",
                    FirstName = "Admin",
                    LastName = "Adminov",
                    PersonalIdentificationNumber = "9999999999",
                    Address = "Admin street",
                    Role = "Admin"
                };

                await userManager.CreateAsync(user, "admin");

                await userManager.AddToRoleAsync(await userManager.FindByEmailAsync(user.Email), "Admin");
            }

            return View();
        }
        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
