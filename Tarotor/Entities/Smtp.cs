using MailKit.Security;

namespace Tarotor.Entities
{
    public class Smtp
    {
        public string Id { get; set; }
        public string Address { get; set; }
        public int Port { get; set; }
        public SecureSocketOptions SecureSocketOptions { get; set; }
        public string FromEmail { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        
    }
}