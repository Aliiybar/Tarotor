using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Tarotor.Controllers
{
    public class _BaseController : Controller
    {
        public string GetCulture()
        {
            return HttpContext.Request.RouteValues.Values.First().ToString();
        }

        protected string GetCurrentPage()
        {
            return HttpContext.Request.Path;
        }

        public IActionResult SwitchLang(string language = "", string page = "")
        {
            var curPage = page;
            if(curPage == "")
                curPage = GetCurrentPage();
            var curLang = language;
            if (curLang == "")
            {
                curLang = GetCulture();
                return Ok(curLang == "tr" ? curPage.Replace("/tr/", "/en/") : curPage.Replace("/en/", "/tr/"));
            }
            return Ok(curLang == "tr" ? curPage.Replace("/en/", "/tr/") : curPage.Replace("/tr/", "/en/"));
        }
        
        
    }
    
 
}