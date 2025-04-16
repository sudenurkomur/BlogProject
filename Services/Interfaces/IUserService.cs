using BlogProject.Models;

namespace BlogProject.Services.Interfaces
{
    public interface IUserService
    {
        List<User> GetAll();
        Guid GetFirstUserId();
        User GetByUsername(string username);
        Task MakeUserPremiumAsync(string userId);
    }
}