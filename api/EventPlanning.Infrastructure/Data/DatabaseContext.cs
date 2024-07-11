using EventPlanning.Core.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EventPlanning.Infrastructure.Data
{
    public class DatabaseContext : IdentityDbContext<UserEntity, IdentityRole<int>, int>
    {
        public DbSet<EventEntity> Events { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<EventEntity>(b =>
            {
                b.HasKey(e => e.Id);
                b.Property(e => e.Name).HasMaxLength(300);
                b.Property(e => e.Description).HasMaxLength(1000);
                b.Property(e => e.Venue).HasMaxLength(300);
                b.HasOne(e => e.Creator)
                    .WithMany()
                    .HasForeignKey(e => e.CreatorId)
                    .OnDelete(DeleteBehavior.NoAction);
                b.HasMany(c => c.Members).WithMany(u => u.Events);
            });
        }
    }
}
