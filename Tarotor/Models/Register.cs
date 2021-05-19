using System;
using System.ComponentModel.DataAnnotations;
using LazZiya.ExpressLocalization.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Tarotor.Models
{
    public class Register
    {
        [ExRequired]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [ExRequired]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [ExRequired]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [ExRequired]
        [Display(Name = "Birth Date")]
        public DateTime Birthdate { get; set; }
        
        [Display(Name = "Your Photo")]
        public IFormFile File { get; set; }
    }
}