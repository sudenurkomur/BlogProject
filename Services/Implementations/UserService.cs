using BlogProject.Models;
using BlogProject.Repositories.Interfaces;
using BlogProject.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

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
        public User GetByUsername(string username)
        {
            return _userRepository.GetByUsername(username);
        }
        public Guid GetFirstUserId()
        {
            return _userRepository.GetFirstUserId();
        }
    }
}