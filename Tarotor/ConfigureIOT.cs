using Microsoft.Extensions.DependencyInjection;
using Tarotor.DAL.Repositories.Content;
using Tarotor.DAL.Repositories.Smtp;
using Tarotor.DAL.Repositories.Template;
using Tarotor.Services;

namespace Tarotor
{
    public static class ConfigureIOT
    {
        public static void ConfigureDependencies(IServiceCollection services)
        {
            services.AddScoped<IContentRepository, ContentRepository>();
            services.AddScoped<ITemplateRepository, TemplateRepository>();
            services.AddScoped<ISmtpRepository, SmtpRepository>();
            
            services.AddScoped<IContentManager, ContentManager>();
            services.AddScoped<IEmailService, EmailService>();
        }
    }
}