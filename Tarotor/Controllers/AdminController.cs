using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tarotor.Models;
using Tarotor.Services;

namespace Tarotor.Controllers
{
    public class AdminController : _BaseController
    {
        private readonly IEmailService _emailService;
        private readonly IContentManager _contentManager;
        private readonly ISocialMediaManager _socialMediaManager;

        public AdminController(IEmailService emailService,
            IContentManager contentManager,
            ISocialMediaManager socialMediaManager)
        {
            _emailService = emailService;
            _contentManager = contentManager;
            _socialMediaManager = socialMediaManager;
        }
        private void SetAdminLayoutData()
        {
            
            var user = new UserInfo();
            user.UserName = "Ali Iybar"  ;
            user.IconImg = "/admin/img/undraw_profile_1.svg";

            var alerts = new List<Alert>();
            alerts.Add(new Alert(){AlertDate = new DateTime(2021,04,22), AlertMessage = "Yeni bir seyler oldu"});
            alerts.Add(new Alert(){AlertDate = new DateTime(2021,04,28), AlertMessage = "Yeni bir seyler daha oldu"});

            var messages = new List<Message>();
            messages.Add(new Message(){MessageDate = new DateTime(2021,04,22), From = "Test", MessageSubject = "Test Message"});
            var model = new AdminLayoutModel()
            {
                UserInfo = user,
                Messages =  messages,
                Alerts = alerts
            };
            ViewBag.AdminLayoutModel = model;
        }
       // [Authorize]
        public IActionResult Dashboard()
        {
            SetAdminLayoutData();
            return View();
        }
        
        // [Authorize]
        public IActionResult Settings()
        {
            SetAdminLayoutData();
            var model = new Settings();
            return View(model);
        }

        #region SocialMediaLinks
        public IActionResult Social()
        {
            SetAdminLayoutData();
             
            var model = new SocialMediaVm();
            var mediaLinks = _socialMediaManager.GetSocialMedia();
            if (mediaLinks != null) model = mediaLinks;
            return View(model);
        }
       
        [HttpPost]
        public async Task<IActionResult> Social(SocialMediaVm model)
        {
            SetAdminLayoutData();
            if (ModelState.IsValid)
            {
                await _socialMediaManager.SaveSocialMedia(model);
            }
            ViewBag.Success = true;
            return View(model);
        }
        

        #endregion
        
        #region Content
        public IActionResult SiteContents()
        {
            SetAdminLayoutData();
            var model = _contentManager.GetContents();
            return View(model);
        }
        
        public  async Task<IActionResult> SiteContent(string id)
        {
            SetAdminLayoutData();
            var model = new ContentVm();
            if (string.IsNullOrWhiteSpace(id)) return View(model);
            var content = await _contentManager.GetContentAsync(id);
            if (content != null)
            {
                model = content;
            }
            return View(model);
        }
        
        [HttpPost]
        public async Task<IActionResult> SiteContent(ContentVm model)
        {
            var retVal = "";
            SetAdminLayoutData();
            if (ModelState.IsValid)
            {
                retVal = await  _contentManager.SaveContentAsync(model );
            }
            ViewBag.Success = true;
            return RedirectToAction("SiteContent", "Admin", retVal);
        }
        #endregion
        
        #region SMTP
        public IActionResult Smtp()
        {
            SetAdminLayoutData();
             
            var model = new SmtpVm();
            var smtp = _emailService.GetSmtpSettings();
            if (smtp != null) model = smtp;
            return View(model);
        }
       
        [HttpPost]
        public IActionResult Smtp(SmtpVm model)
        {
            SetAdminLayoutData();
            if (ModelState.IsValid)
            {
                _emailService.SaveSmtpSettings(model);
            }
            ViewBag.Success = true;
            return View(model);
        }
        #endregion

        #region Template

 
        public  IActionResult Templates()
        {
            SetAdminLayoutData();
            var model =_emailService.GetTemplates();
            return View(model);
        }

        public  async Task<IActionResult> Template(string id)
        {
            SetAdminLayoutData();
            var model = new TemplateVM();
            if (string.IsNullOrWhiteSpace(id)) return View(model);
            var template = await _emailService.GetTemplateAsync(id);
            if (template != null)
            {
                model = template;
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Template(TemplateVM model)
        {
            var retVal = "";
            SetAdminLayoutData();
            if (ModelState.IsValid)
            {
               retVal = await  _emailService.SaveTemplateAsync(model );
            }
            ViewBag.Success = true;
            return RedirectToAction("Template", "Admin", retVal);
        }
        #endregion

    }
}