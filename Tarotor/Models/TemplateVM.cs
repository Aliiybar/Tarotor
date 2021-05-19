using System.Collections.Generic;
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
        public string TemplateName { get; set; }
        public string Language { get; set; }
        public IEnumerable<SelectListItem> LanguageList { get;  }
        public string TemplateBody { get; set; }
    }
}