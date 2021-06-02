using System;
using System.Globalization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Tarotor.DAL;
using Tarotor.Entities;
using LazZiya.ExpressLocalization;
using Microsoft.AspNetCore.Localization;
using Tarotor.LocalizationResources;
using Tarotor.Models;


namespace Tarotor
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var cultures = new[]
            {
                new CultureInfo("tr"),
                new CultureInfo("en"),
            };
            services.AddRazorPages()
                .AddExpressLocalization<ExpressLocalizationResource, ViewLocalizationResource>(
                    ops =>
                    {
                        ops.ResourcesPath = "LocalizationResources";
                        ops.RequestLocalizationOptions = o =>
                        {
                            o.SupportedCultures = cultures;
                            o.SupportedUICultures = cultures;
                            o.DefaultRequestCulture = new RequestCulture("en");
                        };
                    });
            string mySqlConnectionStr = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<AppDbContext>(options => options.UseMySql(mySqlConnectionStr, ServerVersion.AutoDetect(mySqlConnectionStr), b => b.MigrationsAssembly("Tarotor")));
           
            services.AddIdentity<User, Role>(options =>
            {
                options.Password.RequiredLength = 5;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1d);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = true;
            })
               .AddEntityFrameworkStores<AppDbContext>()
               .AddDefaultTokenProviders();
            services
                .AddAuthentication()
                .AddCookie(options =>
                {
                    options.LoginPath = "/Account/login";
                    options.LogoutPath = "Account/logout";
                });
            services.AddControllersWithViews();
            services.Configure<DataProtectionTokenProviderOptions>(opt =>
                opt.TokenLifespan = TimeSpan.FromHours(2));
            services.Configure<Secrets>(Configuration.GetSection("Secrets"));
            // Dependencies
            ConfigureIOT.ConfigureDependencies(services);

            //Auto Mapper
            var autoMapperConfig = new AutoMapper.MapperConfiguration(c =>
            {
                c.AddProfile(new MappingProfile());
            });
            var mapper = autoMapperConfig.CreateMapper();
            services.AddSingleton(mapper);

        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRequestLocalization();
            app.UseRouting();
            
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "tarot",
                    pattern: "{culture=en}/{controller=Home}/{action}");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{culture=en}/{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
