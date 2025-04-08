using BlogProject.Models;

namespace BlogProject.Repositories.Interfaces
{
    public interface IUserRepository
    {
        List<User> GetAll();
        Guid GetFirstUserId(); // test için geçici
    }
}