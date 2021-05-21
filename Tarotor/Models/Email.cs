using System.Collections.Generic;

namespace Tarotor.Models
{
    public class Email
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string TemplateId { get; set; }
        public string TemplateName { get; set; }
        public  string Language { get; set; }
        public Dictionary<string, string>  Parameters { get; set; }
    }
}