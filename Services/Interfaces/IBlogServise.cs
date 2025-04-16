using BlogProject.Models;
using System.Threading.Tasks;

namespace BlogProject.Services.Interfaces
{
    public interface IBlogService
    {
        List<Blog> GetAllBlogs();
        List<Blog> GetBlogsByCategoryId(Guid categoryId);
        Blog GetBlogById(Guid id);
        void AddBlog(Blog blog);
        void UpdateBlog(Blog blog);
        void DeleteBlog(Blog blog);
        Task AddBlogAsync(Blog blog);
        List<Blog> GetPagedBlogs(int page, int pageSize);
        int GetTotalBlogCount(); // Sayfa sayısını hesaplamak için

    }
}