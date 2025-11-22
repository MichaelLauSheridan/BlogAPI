using Blog.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Data
{
    public class BlogDbContext : DbContext
    {
        public BlogDbContext(DbContextOptions<BlogDbContext> options)
            : base(options)
        {
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // POST
            modelBuilder.Entity<Post>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Title).IsRequired().HasMaxLength(200);
                entity.Property(p => p.Content).IsRequired();
                entity.Property(p => p.Author).IsRequired();
                entity.Property(p => p.CreatedDate).IsRequired();

                entity.HasMany(p => p.Comments)
                      .WithOne(c => c.Post)
                      .HasForeignKey(c => c.PostId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // COMMENT
            modelBuilder.Entity<Comment>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Name).IsRequired().HasMaxLength(100);
                entity.Property(c => c.Email).IsRequired().HasMaxLength(150);
                entity.Property(c => c.Content).IsRequired().HasMaxLength(1000);
                entity.Property(c => c.CreatedDate).IsRequired();
            });
        }
    }
}
