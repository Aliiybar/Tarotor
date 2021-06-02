using System;

namespace Tarotor.Entities
{
    public class Response
    {
        public string Id { get; set; }
        public string RequestId { get; set; }
        public DateTime ResponseDate { get; set; }
        public string Commentator { get; set; }
        public string Comment { get; set; }
    }
}