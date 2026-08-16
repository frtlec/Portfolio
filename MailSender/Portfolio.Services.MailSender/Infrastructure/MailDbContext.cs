using Microsoft.EntityFrameworkCore;
using Portfolio.Services.MailSender.Models;

namespace Portfolio.Services.MailSender.Infrastructure
{
    public class MailDbContext : DbContext
    {
        public const string DEFAULT_SCHEMA = "mail";

        public MailDbContext(DbContextOptions<MailDbContext> options) : base(options)
        {
        }

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<MailSetting> MailSettings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contact>(entity =>
            {
                entity.ToTable("Contacts", DEFAULT_SCHEMA);
                entity.Property(x => x.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<MailSetting>(entity =>
            {
                entity.ToTable("MailSettings", DEFAULT_SCHEMA);
                entity.Property(x => x.Id).ValueGeneratedNever();
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
