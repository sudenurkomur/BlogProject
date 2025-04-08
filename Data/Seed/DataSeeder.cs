using BlogProject.Data.BlogProject.Data;
using BlogProject.Models;

namespace BlogProject.Data.Seed
{
    public static class DataSeeder
    {
        public static void Seed(IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<BlogContext>();

            // Veritabanı varsa oluştur
            context.Database.EnsureCreated();

            // Kategoriler
            if (!context.Categories.Any())
            {
                context.Categories.AddRange(
                    new Category { Name = "Teknoloji" },
                    new Category { Name = "Yazılım" },
                    new Category { Name = "Genel" }
                );
                context.SaveChanges();
            }

            // Kullanıcı
            if (!context.Users.Any())
            {
                var user = new User
                {
                    Username = "admin",
                    Email = "admin@mail.com",
                    PasswordHash = "1234", 
                    Role = "Admin"
                };
                context.Users.Add(user);
                context.SaveChanges();
            }

            // Blog
            if (!context.Blogs.Any())
            {
                context.Blogs.Add(new Blog
                {
                    Title = "Hoş geldiniz!",
                    Content = "Bu bir test blog yazısıdır.",
                    PublishDate = DateTime.Now,
                    CategoryId = context.Categories.First().Id,
                    UserId = context.Users.First().Id
                });
                context.SaveChanges();
            }
        }
    }
}