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

            return View();
        }

        [Authorize]
        public async Task<IActionResult> Privacy()
        {
            //return View();
            var result = await userManager.GetRolesAsync(await userManager.GetUserAsync(this.User));
            return this.Json(result);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
