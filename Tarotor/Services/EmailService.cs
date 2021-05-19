using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using Tarotor.DAL.Repositories.Smtp;
using Tarotor.DAL.Repositories.Template;
using Tarotor.Entities;
using Tarotor.Models;
using Tarotor.Util;

namespace Tarotor.Services
{
    public class EmailService : IEmailService
    {
        private readonly ITemplateRepository _templateRepository;
        private readonly ISmtpRepository _smtpRepository;
        private readonly IMapper _mapper;
        private readonly Secrets _secrets;
        public EmailService(ITemplateRepository templateRepository,
            IOptions<Secrets> secrets,
            ISmtpRepository smtpRepository,
            IMapper mapper)
        {
            _templateRepository = templateRepository;
            _smtpRepository = smtpRepository;
            _mapper = mapper;
            _secrets = secrets.Value;
        }
        public async Task<string> SaveTemplateAsync(TemplateVM templateVm)
        {
            var template = new Template();
            var id = templateVm.Id;
            if (string.IsNullOrWhiteSpace(templateVm.Id))
            {
                id = Guid.NewGuid().ToString();
            }

            template.Id = id;
            template.TemplateName = templateVm.TemplateName;
            template.TemplateBody = templateVm.TemplateBody.Replace("'", "&apos;");
            template.Language = templateVm.Language;
            if (string.IsNullOrWhiteSpace(templateVm.Id))
            {
                _templateRepository.Add(template);
                await _templateRepository.SaveAsync();
            }
            else
            {
               await _templateRepository.UpdateAsync(template);
            }

            return template.Id;
        }

        public async Task<TemplateVM> GetTemplateAsync(string templateId)
        {
            var template = await _templateRepository.GetAsync(templateId);
            return template != null
                ? _mapper.Map<TemplateVM>(template)
                : null;
        }

        public  List<TemplateVM> GetTemplates()
        {
            var templates =  _templateRepository.GetAll();
            return _mapper.Map<List<TemplateVM>>(templates);
        }
        public TemplateVM GetTemplateByName(string templateName)
        {
            var templateCollection =  _templateRepository.FindBy(k=>k.TemplateName == templateName);
            Template template = null;
            if (templateCollection.Count() > 0)
            {
                template = templateCollection.First();
            }
            return template != null
                ? _mapper.Map<TemplateVM>(template)
                : null;
        }
        public string Text2Template(string template, Dictionary<string, string> parameters)
        {
            foreach (var parameter in parameters)
            {
                template = template.Replace(parameter.Key, parameter.Value);
            }
            return template;
        }

        public async Task<string> SaveSmtpSettings(SmtpVm smtpVm)
        {
            var id = smtpVm.Id;
            var result = 0;
            if (string.IsNullOrWhiteSpace(smtpVm.Id))
            {
                id = Guid.NewGuid().ToString();
            }
            var smtp = new Smtp()
            {
                Address = smtpVm.Address,
                Port = smtpVm.Port,
                Id = id,
                SecureSocketOptions = smtpVm.SecureSocketOptions,
                UserName = smtpVm.UserName,
                Password = Encryption.EncryptString(smtpVm.Password, _secrets.HashSecret)
            };
            if (string.IsNullOrWhiteSpace(smtpVm.Id))
            {
              _smtpRepository.Add(smtp);  
                result = await _smtpRepository.SaveAsync();
            }
            else
            {
                result = await _smtpRepository.UpdateAsync(smtp);
            }
             
            return result > 0 ? smtp.Id : null;
        }
        public SmtpVm GetSmtpSettings()
        {
            var smtps = _smtpRepository.GetAll();
            if (smtps.Count() > 0)
            {
                var smtp = smtps.First();
                if (smtp != null)
                {
                    return new SmtpVm()
                    {
                        Address = smtp.Address,
                        Port = smtp.Port,
                        Id = smtp.Id,
                        SecureSocketOptions = smtp.SecureSocketOptions,
                        UserName = smtp.UserName,
                        Password = Encryption.DecryptString(smtp.Password, _secrets.HashSecret)
                    };
                }
            }

            return null;
        }
        
        public async Task<MimeMessage> PrepareEmail(Email emailObj)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(emailObj.From));
            email.To.Add(MailboxAddress.Parse(emailObj.To));
            email.Subject = emailObj.Subject;
            var mailBody = emailObj.Body;
            if (!string.IsNullOrWhiteSpace(emailObj.TemplateId))
            {
                var template = await GetTemplateAsync(emailObj.TemplateId);
                mailBody = Text2Template(template.TemplateBody, emailObj.Parameters);
            }
            else if (!string.IsNullOrWhiteSpace(emailObj.TemplateName))
            {
                var template = GetTemplateByName(emailObj.TemplateName);
                mailBody = Text2Template(template.TemplateBody, emailObj.Parameters);
            }
           
            email.Body = new TextPart(TextFormat.Html)
            {
                Text = mailBody
            };
            return email;
        }
        public async Task Send(MimeMessage email)
        {
            using var smtp = new SmtpClient();
            var smtpSetting = GetSmtpSettings();
             
            await smtp.ConnectAsync(smtpSetting.Address, 
                            smtpSetting.Port, 
                            smtpSetting.SecureSocketOptions);
            await smtp.AuthenticateAsync(smtpSetting.UserName,  smtpSetting.Password);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
    }
}