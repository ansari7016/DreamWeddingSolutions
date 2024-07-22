using DreamWedding.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using DreamWedding.Data;
using System.Numerics;
using Microsoft.Extensions.Hosting;

namespace DreamWedding.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        //public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PostsComments>()
                .HasOne(pc => pc.Post)
                .WithMany(p => p.PostsComments)
                .HasForeignKey(pc => pc.PostId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PostsComments>()
                .HasOne(pc => pc.User)
                .WithMany(u => u.PostsComments)
                .HasForeignKey(pc => pc.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PostsLike>()
            .HasKey(pl => new { pl.LikeId });

            modelBuilder.Entity<PostsLike>()
                .HasOne(pl => pl.Post)
                .WithMany(p => p.Likes)
                .HasForeignKey(pl => pl.PostId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PostsLike>()
                .HasOne(pl => pl.User)
                .WithMany(u => u.Likes)
                .HasForeignKey(pl => pl.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure the relationship between ApplicationUser and Wedding
            modelBuilder.Entity<Post>()
                .HasOne(w => w.User)
                .WithMany() // Use WithMany() if the relationship is one-to-many
                .HasForeignKey(w => w.UserId);

            modelBuilder.Entity<ReelsComments>()
               .HasOne(pc => pc.Reels)
               .WithMany(p => p.ReelsComments)
               .HasForeignKey(pc => pc.ReelsId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ReelsComments>()
                .HasOne(pc => pc.User)
                .WithMany(u => u.ReelsComments)
                .HasForeignKey(pc => pc.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ReelsLikes>()
                .HasOne(pl => pl.Reels)
                .WithMany(p => p.ReelsLikes)
                .HasForeignKey(pl => pl.ReelsId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ReelsLikes>()
                .HasOne(pl => pl.User)
                .WithMany(u => u.ReelsLikes)
                .HasForeignKey(pl => pl.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SavePosts>()
           .HasKey(sp => new { sp.UserId, sp.PostId });

            modelBuilder.Entity<SavePosts>()
                .HasOne(sp => sp.User)
                .WithMany(u => u.SavePosts)
                .HasForeignKey(sp => sp.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SavePosts>()
                .HasOne(sp => sp.Post)
                .WithMany(p => p.SavePosts)
                .HasForeignKey(sp => sp.PostId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Chats>()
               .HasOne(m => m.Sender)
               .WithMany()
               .HasForeignKey(m => m.SenderId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Chats>()
                .HasOne(m => m.Receiver)
                .WithMany()
                .HasForeignKey(m => m.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Advertisment> Advertisments { get; set; }
        public DbSet<PostsComments> PostsComments { get; set; }
        public DbSet<PostsLike> PostsLikes { get; set; }
        public DbSet<Reels> Reels { get; set; }
        public DbSet<ReelsComments> ReelsComments { get; set; }
        public DbSet<ReelsLikes> ReelsLikes { get; set; }
        public DbSet<SavePosts> SavePosts { get; set; }
        public DbSet<Chats> Chats { get; set; }

    }
}