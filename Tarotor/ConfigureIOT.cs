using Microsoft.Extensions.DependencyInjection;
using Tarotor.DAL.Repositories.Content;
using Tarotor.DAL.Repositories.Request;
using Tarotor.DAL.Repositories.RequestProfile;
using Tarotor.DAL.Repositories.Response;
using Tarotor.DAL.Repositories.ResponseQuestion;
using Tarotor.DAL.Repositories.ResponseQuestionAnswer;
using Tarotor.DAL.Repositories.SelectedCard;
using Tarotor.DAL.Repositories.Smtp;
using Tarotor.DAL.Repositories.SocialMedia;
using Tarotor.DAL.Repositories.Template;
using Tarotor.Entities;
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
            services.AddScoped<ISocialMediaRepository, SocialMediaRepository>();
            services.AddScoped<IRequestRepository, RequestRepository>();
            services.AddScoped<IRequestProfileRepository, RequestProfileRepository>();
            services.AddScoped<IResponseRepository, ResponseRepository>();
            services.AddScoped<IResponseQuestionRepository, ResponseQuestionRepository>();
            services.AddScoped<IResponseQuestionAnswerRepository, ResponseQuestionAnswerRepository>();
            services.AddScoped<ISelectedCardRepository, SelectedCardRepository>();
            
            services.AddScoped<IContentManager, ContentManager>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<ISocialMediaManager, SocialMediaManager>();
        }
    }
}