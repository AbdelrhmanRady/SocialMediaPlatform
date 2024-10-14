using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Social_Media_Platform.Models
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { 
        } 

        public DbSet<Posts> Posts { get; set; }
        public DbSet<Comments> Comments { get; set; }
        public DbSet<PostReactions> PostReactions { get; set; }

        public DbSet<PostMedia> Media { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PostMedia>().HasKey("PostId", "MediaLocation");
            modelBuilder.Entity<PostReactions>().HasKey("UserId", "PostId");

            modelBuilder.Entity<PostReactions>()
                .HasOne(r => r.User)
                .WithMany()
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ApplicationUser>().HasMany(u => u.Posts)
                .WithOne()
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Posts>()
                .HasOne(p => p.User)
                .WithMany(u=>u.Posts)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Posts>().HasMany(p => p.Comments)
                .WithOne(c => c.Post)
                .HasForeignKey(c => c.PostId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Posts>().HasMany(p => p.Media)
                .WithOne()
                .HasForeignKey(m => m.PostId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Posts>().HasMany(p => p.Reactions)
                .WithOne()
                .HasForeignKey(r => r.PostId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Comments>().HasMany(c => c.SubComments)
                .WithOne()
                .HasForeignKey(c => c.ParentCommentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Comments>().HasOne(c=>c.User)
                .WithMany()
                .HasForeignKey(c => c.CommenterId)
                .OnDelete(DeleteBehavior.Restrict);





        }
    }
}
