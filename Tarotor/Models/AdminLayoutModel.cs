using System.Collections.Generic;

namespace Tarotor.Models
{
    public class AdminLayoutModel
    {
        public UserInfo UserInfo { get; set; }
        public List<Alert> Alerts { get; set; }
        public List<Message> Messages { get; set; }
        
    }
}