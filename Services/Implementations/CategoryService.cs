using BlogProject.Data.BlogProject.Data;
using BlogProject.Models;
using BlogProject.Repositories.Interfaces;
using BlogProject.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlogProject.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly BlogContext _context;

        public CategoryService(ICategoryRepository categoryRepository, BlogContext context)
        {
            _categoryRepository = categoryRepository;
            _context = context;
        }

        public List<Category> GetAll()
        {
            return _categoryRepository.GetAll();
        }

      

    }
}