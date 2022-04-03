using Microsoft.EntityFrameworkCore;
using Portfolio.Services.WorkItems.Domain.WorkAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Services.WorkItems.Infrastructure
{
    public class WorkItemsDbContext:DbContext
    {
        public const string DEFAULT_SCHEMA = "works";
        public WorkItemsDbContext(DbContextOptions<WorkItemsDbContext> option) : base(option)
        {

        }
        public DbSet<Work> Works { get; set; }
        public DbSet<WorkItem> WorkItems { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<About> Abouts { get; set; }
        public DbSet<GeneralSetting> GeneralSettings { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Work>().ToTable("Works", DEFAULT_SCHEMA);
            modelBuilder.Entity<WorkItem>().ToTable("WorkItems", DEFAULT_SCHEMA);
            modelBuilder.Entity<Category>().ToTable("Categories", DEFAULT_SCHEMA);
            modelBuilder.Entity<About>().ToTable("Abouts", DEFAULT_SCHEMA);
            modelBuilder.Entity<GeneralSetting>().ToTable("GeneralSettings", DEFAULT_SCHEMA);

            base.OnModelCreating(modelBuilder);
        }
    }
}
