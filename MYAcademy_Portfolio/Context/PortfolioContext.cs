using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MYAcademy_Portfolio.Entities;

namespace MYAcademy_Portfolio.Context
{
    public class PortfolioContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS01;Database=PortfolioDbNight;integrated security=true;trust server certificate=true");

        }
        public DbSet<About> Abouts { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<Experience> Experiences { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<HeroSection> HeroSections { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<SkillCategory> SkillCategories { get; set; }
        public DbSet<Skill> Skills{ get; set; }
        public DbSet<Certificate> Certificates { get; set; }
        public DbSet<SocialMedia> SocialMedias { get; set; }
        
        
        

    }
}
