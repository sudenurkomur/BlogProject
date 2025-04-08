using BlogProject.Models;
using global::BlogProject.Models;


namespace BlogProject.Repositories.Interfaces
{

    namespace BlogProject.Repositories.Interfaces
    {
        public interface IBlogRepository
        {
            List<Blog> GetAll();
            Blog GetById(Guid id);
            void Add(Blog blog);
            void Update(Blog blog);
            void Delete(Blog blog);
        }
    }
}
