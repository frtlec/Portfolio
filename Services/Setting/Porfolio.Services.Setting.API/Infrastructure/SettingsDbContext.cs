using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Porfolio.Services.Setting.API.Models.DbModels;
using System.Collections.Generic;
using System.Text.Json;

namespace Porfolio.Services.Setting.API.Infrastructure
{
    public class SettingsDbContext : DbContext
    {
        public const string DEFAULT_SCHEMA = "settings";

        public SettingsDbContext(DbContextOptions<SettingsDbContext> options) : base(options)
        {
        }

        public DbSet<AboutPage> AboutPages { get; set; }
        public DbSet<Localization> Localizations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AboutPage>(entity =>
            {
                entity.ToTable("AboutPages", DEFAULT_SCHEMA);
                entity.Property(x => x.Id).ValueGeneratedNever();

                ConfigureJsonListColumn(entity.Property(x => x.Softwares));
                ConfigureJsonListColumn(entity.Property(x => x.Businesses));
                ConfigureJsonListColumn(entity.Property(x => x.Educations));
                ConfigureJsonListColumn(entity.Property(x => x.Certifacates));
                ConfigureJsonListColumn(entity.Property(x => x.Projects));
            });

            modelBuilder.Entity<Localization>(entity =>
            {
                entity.ToTable("Localizations", DEFAULT_SCHEMA);
                entity.Property(x => x.Id).ValueGeneratedNever();
            });

            base.OnModelCreating(modelBuilder);
        }

        private static void ConfigureJsonListColumn<T>(PropertyBuilder<List<T>> property)
        {
            property
                .HasConversion(
                    v => JsonSerializer.Serialize(v ?? new List<T>(), (JsonSerializerOptions)null),
                    v => string.IsNullOrEmpty(v) ? new List<T>() : JsonSerializer.Deserialize<List<T>>(v, (JsonSerializerOptions)null))
                .Metadata.SetValueComparer(new ValueComparer<List<T>>(
                    (a, b) => JsonSerializer.Serialize(a, (JsonSerializerOptions)null) == JsonSerializer.Serialize(b, (JsonSerializerOptions)null),
                    v => v == null ? 0 : JsonSerializer.Serialize(v, (JsonSerializerOptions)null).GetHashCode(),
                    v => JsonSerializer.Deserialize<List<T>>(JsonSerializer.Serialize(v, (JsonSerializerOptions)null), (JsonSerializerOptions)null)));

            property.HasColumnType("jsonb");
        }
    }
}
