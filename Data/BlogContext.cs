using Microsoft.EntityFrameworkCore;
using BlogProject.Models;
using global::BlogProject.Models;
using System.Collections.Generic;
using System.Reflection.Emit;


namespace BlogProject.Data
{
    
    namespace BlogProject.Data
    {
        public class BlogContext : DbContext
        {
            public BlogContext(DbContextOptions<BlogContext> options)
                : base(options) { }

            public DbSet<User> Users { get; set; }
            public DbSet<Blog> Blogs { get; set; }
            public DbSet<Category> Categories { get; set; }
            public DbSet<Comment> Comments { get; set; }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);

                // Comment → Blog
                modelBuilder.Entity<Comment>()
                    .HasOne(c => c.Blog)
                    .WithMany(b => b.Comments)
                    .HasForeignKey(c => c.BlogId)
                    .OnDelete(DeleteBehavior.NoAction); // Veya DeleteBehavior.NoAction

                // Comment → User
                modelBuilder.Entity<Comment>()
                    .HasOne(c => c.User)
                    .WithMany(u => u.Comments)
                    .HasForeignKey(c => c.UserId)
                    .OnDelete(DeleteBehavior.NoAction); // Veya DeleteBehavior.NoAction
            }
        }
    }
}
