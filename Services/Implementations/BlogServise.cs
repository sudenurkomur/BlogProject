using BlogProject.Data.BlogProject.Data;
using BlogProject.Models;
using BlogProject.Repositories.Interfaces;
using BlogProject.Repositories.Interfaces.BlogProject.Repositories.Interfaces;
using BlogProject.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlogProject.Services.Implementations
{
    public class BlogService : IBlogService
    {
        private readonly IBlogRepository _blogRepository;
        private readonly BlogContext _context;

        public BlogService(IBlogRepository blogRepository , BlogContext context)
        {
            _blogRepository = blogRepository;
            _context = context;
        }

        public List<Blog> GetAllBlogs()
        {
            return _blogRepository.GetAll();
        }

        public List<Blog> GetPagedBlogs(int page, int pageSize)
        {
            return _blogRepository.GetAll()
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public int GetTotalBlogCount()
        {
            return _blogRepository.GetAll().Count;
        }
        public List<Blog> GetBlogsByCategoryId(Guid categoryId)
        {
            return _context.Blogs
                .Where(b => b.CategoryId == categoryId)
                .Include(b => b.Category)
                .Include(b => b.User)
                .ToList();
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

        public async Task AddBlogAsync(Blog blog)
        {
            await _blogRepository.AddBlogAsync(blog);
        }
    }
}