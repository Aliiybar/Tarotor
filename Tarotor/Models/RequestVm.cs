using System;
using System.Collections.Generic;
using Tarotor.Entities;

namespace Tarotor.Models
{ 
    public class RequestVm
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string Email { get; set; }
        public int TarotType { get; set; }
        public string Reference { get; set; }
        public int Questions { get; set; }
        public int CardsChosenBy { get; set; }
        public DateTime RequestDate { get; set; }
        public decimal Price { get; set; }
        public int PaymentType { get; set; }
        public bool IsPaid { get; set; }
        public string CommentId { get; set; }
        public bool QuestionsAsked { get; set; }
        public bool QuestionsAnswered { get; set; }
        public List<SelectedCard> SelectedCards { get; set; }
        
    }
}