using BlogProject.Models;
using BlogProject.Repositories.Interfaces;
using BlogProject.Repositories.Interfaces.BlogProject.Repositories.Interfaces;
using BlogProject.Services.Interfaces;

namespace BlogProject.Services.Implementations
{
    public class BlogService : IBlogService
    {
        private readonly IBlogRepository _blogRepository;

        public BlogService(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        public List<Blog> GetAllBlogs()
        {
            return _blogRepository.GetAll();
        }

        public Blog GetBlogById(Guid id)
        {
            return _blogRepository.GetById(id);
        }

        public void AddBlog(Blog blog)
        {
            _blogRepository.Add(blog);
        }

        public void UpdateBlog(Blog blog)
        {
            _blogRepository.Update(blog);
        }

        public void DeleteBlog(Blog blog)
        {
            _blogRepository.Delete(blog);
        }
    }
}