using BlogProject.Data;
using BlogProject.Data.BlogProject.Data;
using BlogProject.Models;
using BlogProject.Repositories.Interfaces;
using BlogProject.Repositories.Interfaces.BlogProject.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlogProject.Repositories.Implementations
{
    public class BlogRepository : IBlogRepository
    {
        private readonly BlogContext _context;

        public BlogRepository(BlogContext context)
        {
            _context = context;
        }

        public List<Blog> GetAll()
        {
            return _context.Blogs
                .Include(b => b.Category)
                .Include(b => b.User)
                .Include(b => b.Comments)
                    .ThenInclude(c => c.User)
                .ToList();
        }

        public Blog GetById(Guid id)
        {
            return _context.Blogs
                           .Include(b => b.Category)
                           .Include(b => b.User)
                           .FirstOrDefault(b => b.Id == id);
        }

        public void Add(Blog blog)
        {
            _context.Blogs.Add(blog);
            _context.SaveChanges();
        }


        public void Update(Blog blog)
        {
            _context.Blogs.Update(blog);
            _context.SaveChanges();
        }

        public void Delete(Blog blog)
        {
            _context.Blogs.Remove(blog);
            _context.SaveChanges();
        }

        public async Task AddBlogAsync(Blog blog)
        {
            await _context.Blogs.AddAsync(blog);
            await _context.SaveChangesAsync();
        }
    }
}