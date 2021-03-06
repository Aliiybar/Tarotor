using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Tarotor.Util;

namespace Tarotor.Models
{
    public class TemplateVM
    {
        public TemplateVM()
        {
            LanguageList = Lang.GetLanguages();
        }
        public string Id { get; set; }
        [Required]
        [Display(Name = "Template Name")]
        public string TemplateName { get; set; }
        [Display(Name = "Subject")]
        public string TemplateSubject { get; set; }
        [Display(Name = "Language")]
        public string Language { get; set; }
        public IEnumerable<SelectListItem> LanguageList { get;  }
        [Display(Name = "Template Body")]
        public string TemplateBody { get; set; }
    }
}