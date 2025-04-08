using BlogProject.Models;

namespace BlogProject.Services.Interfaces
{
    public interface ICategoryService
    {
        List<Category> GetAll();
    }
}