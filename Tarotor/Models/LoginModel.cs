namespace Tarotor.Models
{
    public class LoginModel
    {
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }
        public bool RememberMe { get; set; }
    }
}