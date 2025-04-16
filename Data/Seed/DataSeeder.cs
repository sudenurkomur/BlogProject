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
                    new Category { Name = "Gezilecek Yerler" },
                    new Category { Name = "Yazılım" },
                    new Category { Name = "Genel" }
                );
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