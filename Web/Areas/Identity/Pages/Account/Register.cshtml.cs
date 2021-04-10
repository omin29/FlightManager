using System;
using Data.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;

namespace Web.Areas.Identity.Pages.Account
{
    #pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        public RegisterModel(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [EmailUserUnique]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [MaxLength(20, ErrorMessage = "{0} must be  maximum {1} characters")]
            [Display(Name = "User Name")]
            public string UserName { get; set; } //custom added

            [Required]
            [MaxLength(50, ErrorMessage = "{0} must be  maximum {1} characters")]
            [RegularExpression(@"[A-Za-z]+", ErrorMessage = "Names contain only letters")]
            [Display(Name = "First Name")]
            public string FirstName { get; set; } //custom added

            [Required]
            [MaxLength(50, ErrorMessage = "{0} must be  maximum {1} characters")]
            [RegularExpression(@"[A-Za-z]+", ErrorMessage = "Names contain only letters")]
            [Display(Name = "Last Name")]
            public string LastName { get; set; } //custom added

            [Required]
            [MaxLength(50, ErrorMessage = "{0} must be  maximum {1} characters")]
            [Display(Name = "Address")]
            public string Address { get; set; } //custom added

            [Required]
            [RegularExpression(@"\d{10}", ErrorMessage = "PIN (ЕГН) must be 10 digits")]
            [UniqueUserPIN]
            [Display(Name = "Personal Identification Number (ЕГН)")]
            public string PersonalIdentificationNumber { get; set; } // custom added

            [Required]
            [Phone]
            [RegularExpression(@"\d{10}", ErrorMessage = "Phone number must be 10 digits")]
            [Display(Name = "Phone Number")]
            public string PhoneNumber { get; set; } // custom added

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = new User {
                    UserName = Input.UserName,
                    Email = Input.Email,
                    PhoneNumber = Input.PhoneNumber,
                    FirstName = Input.FirstName,
                    LastName = Input.LastName,
                    PersonalIdentificationNumber = Input.PersonalIdentificationNumber,
                    Address = Input.Address,
                    Role = (_userManager.Users.Count() == 0)?"Admin":"User"}; // modified here
                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    if (_userManager.Users.Count() == 1)
                    {
                        await _userManager.AddToRoleAsync(await _userManager.FindByEmailAsync(user.Email), "Admin");
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(await _userManager.FindByEmailAsync(user.Email), "User");
                    }

                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        //await _signInManager.SignInAsync(user, isPersistent: false); // auto sign in
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
