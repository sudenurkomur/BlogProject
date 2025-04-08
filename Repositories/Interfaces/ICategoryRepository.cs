using BlogProject.Models;

namespace BlogProject.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        List<Category> GetAll();
    }
}