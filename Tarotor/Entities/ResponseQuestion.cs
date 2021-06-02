using System;

namespace Tarotor.Entities
{
    public class ResponseQuestion
    {
        public string Id { get; set; }
        public string RequestId { get; set; }
        public string Question { get; set; }
        public DateTime QuestionDate { get; set; }
    }
}