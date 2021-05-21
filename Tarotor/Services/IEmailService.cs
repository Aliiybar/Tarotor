using System.Collections.Generic;
using System.Threading.Tasks;
using MimeKit;
using Tarotor.Entities;
using Tarotor.Models;

namespace Tarotor.Services
{
    public interface IEmailService
    {
        Task<string> SaveTemplateAsync(TemplateVM templateVm);
        Task<TemplateVM> GetTemplateAsync(string templateId);
        TemplateVM GetTemplateByName(string templateName, string language);
        List<TemplateVM> GetTemplates();
        string Text2Template(string template, Dictionary<string, string> parameters);
        Task<string> SaveSmtpSettings(SmtpVm smtpVm);
        SmtpVm GetSmtpSettings();
        Task<MimeMessage> PrepareEmail(Email emailObj);
        Task Send(MimeMessage email);
    }
}