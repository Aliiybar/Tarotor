using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Org.BouncyCastle.Asn1.X509;

namespace Tarotor.Models
{
    public class TarotPageModel
    {
        public TarotPageModel(string lang)
        {
            TarotTypes = new Dictionary<int, string>();
            YesNo = new Dictionary<int, string>();
            Selection = new Dictionary<int, string>();
            
            if (lang == "tr")
            {
                TarotTypes.Add(1, "Aşk");
                TarotTypes.Add(2, "Genel");
                Selection.Add(1, "Ben Seçeceğim");
                Selection.Add(2, "Yorumcu Seçsin");
                YesNo.Add(1, "Evet");
                YesNo.Add(2, "Hayir");
            }
            else
            {
                TarotTypes.Add(1, "Love");
                TarotTypes.Add( 2, "General");   
                Selection.Add(1, "I will choose");
                Selection.Add(2, "Commentator wıll choose"); 
                YesNo.Add(1, "Yes");
                YesNo.Add(2, "No");
            }
        }
        public Dictionary<int, string> TarotTypes { get; set; }
         public Dictionary<int, string> YesNo { get; set; }
         public Dictionary<int, string> Selection { get; set; }

         [Display(Name="Please select what type of tarot you want")]
        public int TarotTypeSelected  { get; set; }
        [Display(Name="Do you want to ask questions after the comments?")]
        public int QuestionSelected { get; set; }
        [Display(Name="Who do you want to choose the cards?")]
        public int CardsSelectedBy { get; set; }
    }

    
}