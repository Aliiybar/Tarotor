using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Tarotor.Entities;

namespace Tarotor.DAL
{
    public class AppDbContext : IdentityDbContext<User, Role, Guid>
    {
        public DbSet<Content> Content { get; set; } 
        public DbSet<Template> Template { get; set; } 
        public DbSet<Smtp> Smtp { get; set; } 
        public DbSet<Request> Request { get; set; } 
        public DbSet<RequestProfile> RequestProfile { get; set; } 
        public DbSet<Response> Response { get; set; } 
        public DbSet<ResponseQuestion> ResponseQuestion { get; set; } 
        public DbSet<ResponseQuestionAnswer> ResponseQuestionAnswer { get; set; } 
        public DbSet<SelectedCard> SelectedCard { get; set; } 
     
        
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
 
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
