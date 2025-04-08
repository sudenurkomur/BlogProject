using BlogProject.Data;
using BlogProject.Data.BlogProject.Data;
using BlogProject.Models;
using BlogProject.Repositories.Interfaces;

namespace BlogProject.Repositories.Implementations
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly BlogContext _context;

        public CategoryRepository(BlogContext context)
        {
            _context = context;
        }

        public List<Category> GetAll()
        {
            return _context.Categories.ToList();
        }
    }
}