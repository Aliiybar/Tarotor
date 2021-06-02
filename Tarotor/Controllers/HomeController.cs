using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Tarotor.Models;
using Tarotor.Services;

namespace Tarotor.Controllers
{
    public class HomeController : _BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISocialMediaManager _socialMediaManager;
        private readonly IContentManager _contentManager;

        public HomeController(ILogger<HomeController> logger,
            ISocialMediaManager socialMediaManager,
            IContentManager contentManager)
        {
            _logger = logger;
            _socialMediaManager = socialMediaManager;
            _contentManager = contentManager;
        }

        public IActionResult GetSocialMediaLinks()
        {
           return  Ok(_socialMediaManager.GetSocialMedia());
        }
        
        public IActionResult Index()
        {
            var model = new HomePageModel();
            model.HowItWorks = _contentManager.GetContentByName("How It Works", GetCulture());
            return View(model);
        }

        public IActionResult Tarot()
        {
            var model = new TarotPageModel(GetCulture());
            return View(model);
        }
        [HttpPost]
        public IActionResult Tarot(TarotPageModel model)
        {
            if (ModelState.IsValid)
            {
                var requestVm = new RequestVm();
                
                // _emailService.SaveSmtpSettings(model);
            }

            return RedirectToAction("Payment");
        }
        public IActionResult Payment()
        {
           
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
