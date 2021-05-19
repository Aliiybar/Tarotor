using System;

namespace Tarotor.Models
{
    public class Message
    {
        public DateTime MessageDate { get; set; }
        public string From { get; set; }
        public string MessageSubject { get; set; }
        public string MessageBody { get; set; }
    }
}