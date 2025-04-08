using BlogProject.Models;

namespace BlogProject.Services.Interfaces
{
    public interface IBlogService
    {
        List<Blog> GetAllBlogs();
        Blog GetBlogById(Guid id);
        void AddBlog(Blog blog);
        void UpdateBlog(Blog blog);
        void DeleteBlog(Blog blog);

    }
}