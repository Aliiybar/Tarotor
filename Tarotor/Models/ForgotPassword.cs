using System.ComponentModel.DataAnnotations;
using LazZiya.ExpressLocalization.DataAnnotations;

namespace Tarotor.Models
{
    public class ForgotPassword
    {
        [ExRequired]
        [Display(Name = "E-Mail Address")]
        public string Email { get; set; }
    }
}