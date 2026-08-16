using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Portfolio.Api.Identity
{
    public class IdentityDataContext : IdentityDbContext<ApplicationUser>
    {
        public const string DEFAULT_SCHEMA = "identity";

        public IdentityDataContext(DbContextOptions<IdentityDataContext> options) : base(options)
        {
        }

        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                entityType.SetSchema(DEFAULT_SCHEMA);
            }

            builder.Entity<RefreshToken>(entity =>
            {
                entity.HasIndex(x => x.Token).IsUnique();
            });
        }
    }
}
