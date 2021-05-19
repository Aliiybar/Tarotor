using System.ComponentModel.DataAnnotations;
using LazZiya.ExpressLocalization.DataAnnotations;
using MailKit.Security;

namespace Tarotor.Models
{
    public class SmtpVm
    {
        public string Id { get; set; }
        [ExRequired]
        [Display(Name = "SMTP Address")]
        public string Address { get; set; }
        
        [ExRequired]
        [Display(Name = "Port")]
        public int Port { get; set; }
        [ExRequired]
        [Display(Name = "Secure Socket Options")]

        public SecureSocketOptions SecureSocketOptions { get; set; }
        [ExRequired]
        [Display(Name = "UserName")]

        public string UserName { get; set; }
        [ExRequired]
        [Display(Name = "Password")]

        public string Password { get; set; }
    }
}