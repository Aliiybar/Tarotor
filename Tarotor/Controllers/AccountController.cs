using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IContentManager _contentManager;

        public AccountController(SignInManager<User> signInManager,
                                    UserManager<User> userManager,
                                    IEmailService emailService,
                                    IContentManager contentManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _emailService = emailService;
            _contentManager = contentManager;
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
            var smtpSettings = _emailService.GetSmtpSettings();
            
            var emailObj = new Email();
            emailObj.Subject = "Email confirmation of Enki account";
            emailObj.To = model.Email;
            emailObj.From = smtpSettings.FromEmail;
            emailObj.Parameters = variables;
            emailObj.TemplateName = "Registration Validation Email";
            emailObj.Language = GetCulture();
            var email = await _emailService.PrepareEmail(emailObj);
            await _emailService.Send(email);
            ViewBag.Message = "Email Sent";
            return RedirectToAction("Confirmation", "Account");
        }

        public IActionResult ForgotPassword()
        {
            var model = new ForgotPassword();
            return View(model);
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPassword model)
        {
            IdentityResult result = null;
            if(!ModelState.IsValid)
            {
                return View(model);
            }
            
            
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return RedirectToAction(nameof(ForgotPasswordConfirmation));
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var confirmationLink = Url.Action(nameof(ResetPassword), "Account", new { token, email = user.Email }, Request.Scheme);
           // var message = new Message(new string[] { user.Email }, "Reset password token", callback, null);
           
            
             var variables = new Dictionary<string, string>();
             variables.Add("#firstname#", user.FirstName);
             variables.Add("#token#",  token);
             variables.Add("#confirmationUrl#", confirmationLink);

            var smtpSettings = _emailService.GetSmtpSettings();
             
            var emailObj = new Email();
            emailObj.Subject = "Password Reset Link of Enki account";
            emailObj.To = model.Email;
            emailObj.From = smtpSettings.UserName;
            emailObj.Parameters = variables;
            emailObj.TemplateName = "Reset Password Email";
            emailObj.Language = GetCulture();
            var email = await _emailService.PrepareEmail(emailObj);
            await _emailService.Send(email);
            ViewBag.Message = "Email Sent";
            
            return RedirectToAction(nameof(ForgotPasswordConfirmation));
        }
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }  
        [HttpGet]
        public IActionResult ResetPassword(string token, string email)
        {
            var model = new ResetPassword { Token = token, Email = email };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPassword resetPasswordModel)
        {
            if (!ModelState.IsValid)
                return View(resetPasswordModel);
            var user = await _userManager.FindByEmailAsync(resetPasswordModel.Email);
            if (user == null)
                RedirectToAction(nameof(ResetPasswordConfirmation));
            var resetPassResult = await _userManager.ResetPasswordAsync(user, resetPasswordModel.Token, resetPasswordModel.Password);
            if(!resetPassResult.Succeeded)
            {
                foreach (var error in resetPassResult.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return View();
            }
            return RedirectToAction(nameof(ResetPasswordConfirmation));
        }
        [HttpGet]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }
        
        public IActionResult Confirmation()
        {
          
            var retVal = new ContentVm();
            
            retVal =  _contentManager.GetContentByName("E-mail Confirmation Sent", GetCulture());
            ViewBag.Title = retVal.ContentTitle;
            return View("Page", retVal);
        }

        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            var retVal = new ContentVm();
             
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return View("Error");
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                retVal =  _contentManager.GetContentByName("Registration Confirmed", GetCulture());
            }
            else
            {
                retVal = _contentManager.GetContentByName("Registration Not Confirmed", GetCulture());
            }
            return View("Page",retVal);
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