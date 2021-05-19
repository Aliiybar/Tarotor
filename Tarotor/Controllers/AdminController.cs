using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MailKit.Security;
using Microsoft.AspNetCore.Mvc;
using Tarotor.Models;
using Tarotor.Services;

namespace Tarotor.Controllers
{
    public class AdminController : _BaseController
    {
        private readonly IEmailService _emailService;

        public AdminController(IEmailService emailService)
        {
            _emailService = emailService;
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

        public IActionResult Content()
        {
            SetAdminLayoutData();
            var model = new ContentEntry();
            return View(model);
        }

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

 
        public async Task<IActionResult> Templates()
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