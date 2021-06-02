using System;

namespace Tarotor.Entities
{
    public class ResponseQuestionAnswer
    {
        public string Id { get; set; }
        public string RequestId { get; set; }
        public string ResponseQuestionId { get; set; }
        public DateTime AnswerDate { get; set; }
        public string Answer { get; set; }
    }
}