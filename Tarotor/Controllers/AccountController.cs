using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Tarotor.Entities;
using Tarotor.Models;
using Tarotor.Services;

namespace Tarotor.Controllers
{
    public class AccountController : _BaseController
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IEmailService _emailService;

        public AccountController(SignInManager<User> signInManager,
                                    UserManager<User> userManager,
                                    IEmailService emailService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _emailService = emailService;
        }
        // GET
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(Register model)
        {
            IdentityResult result = null;
            if(!ModelState.IsValid)
            {
                return View(model);
            }
            var user = new User();
            user.FirstName = model.FirstName;
            user.Email = model.Email;
            user.EmailConfirmed = false;
            user.LockoutEnabled = false;
            user.UserName = model.Email;
            result = await _userManager.CreateAsync(user, model.Password);
            if(!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return View(model);
            }
            
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var confirmationLink = Url.Action(nameof(ConfirmEmail), "Account", new { token, email = user.Email }, Request.Scheme);

            var variables = new Dictionary<string, string>();
            variables.Add("#firstname#", model.FirstName);
            variables.Add("#token#",  token);
            variables.Add("#confirmationUrl#", confirmationLink);
            
            var emailObj = new Email();
            emailObj.Subject = "Email confirmation of Enki account";
            emailObj.To = model.Email;
            emailObj.From = "test@iybar.com";
            emailObj.Parameters = variables;
            emailObj.TemplateName = "Registration Validation Email";
            var email = await _emailService.PrepareEmail(emailObj);
            await _emailService.Send(email);
            ViewBag.Message = "Email Sent";
            return View(model);
        }

        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return View("Error");
            var result = await _userManager.ConfirmEmailAsync(user, token);
            return View(result.Succeeded ? nameof(ConfirmEmail) : "Error");
        }
        
        [HttpGet]
        public IActionResult SuccessRegistration()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            
            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, 
                // set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(model.UserEmail,
                    model.UserPassword, model.RememberMe, lockoutOnFailure: true);
                if (result.Succeeded)
                {
                 //   _logger.LogInformation("User logged in.");
                    return LocalRedirect("/index");
                }
                
                if (result.IsLockedOut)
                {
                 //   _logger.LogWarning("User account locked out.");
                    return RedirectToPage("./Lockout");
                }
                ModelState.AddModelError(string.Empty, "Invalid login attempt."); 
                return View();
            }

            return null;
        }
    }
}