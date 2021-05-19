using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Tarotor.Models;

namespace Tarotor.Util
{
    public static class Lang
    {
        public static IEnumerable<SelectListItem> GetLanguages()
        {
            var lang = new List<SelectListItem>();
            lang.Add(new SelectListItem("Türkçe", "tr"));
            lang.Add(new SelectListItem("English", "en"));
            return lang;
        }
    }
}