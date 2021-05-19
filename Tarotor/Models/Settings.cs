using System.ComponentModel.DataAnnotations;
using LazZiya.ExpressLocalization.DataAnnotations;
namespace Tarotor.Models
{
    public class Settings
    {
        [ExRequired]
        [Display(Name = "Daily Limit")]
        
        public int DailyLimit { get; set; }
        [ExRequired]
        [Display(Name = "Question Limit")]
        public int QuestionLimit { get; set; }
        [ExRequired]
        [Display(Name = "Tarot Price")]
        public decimal TarotPrice { get; set; }
        [ExRequired]
        [Display(Name = "Question Price")]
        public decimal QuestionsPrice { get; set; }
        [ExRequired]
        [Display(Name = "Selection Card Count")]
        public int SelectionCardCount { get; set; }
        
    }
}