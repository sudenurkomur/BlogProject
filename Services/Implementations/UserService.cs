using BlogProject.Models;
using BlogProject.Repositories.Interfaces;
using BlogProject.Services.Interfaces;

namespace BlogProject.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public List<User> GetAll()
        {
            return _userRepository.GetAll();
        }

        public Guid GetFirstUserId()
        {
            return _userRepository.GetFirstUserId();
        }
    }
}