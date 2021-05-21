using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Tarotor.Util;

namespace Tarotor.Models
{
    public class ContentVm
    {
        public ContentVm()
        {
            LanguageList = Lang.GetLanguages();
        }
        public string Id { get; set; }
        [Required]
        [Display(Name = "Content Name")]
        public string ContentName { get; set; }
        [Display(Name = "Content Title")]
        public string ContentTitle { get; set; }
        [Display(Name = "Language")]
        public string Language { get; set; }
        public IEnumerable<SelectListItem> LanguageList { get;  }
        [Display(Name = "Content Body")]
        public string ContentBody { get; set; }
    }
}