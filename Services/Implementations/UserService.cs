using BlogProject.Models;
using BlogProject.Repositories.Interfaces;
using BlogProject.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using BlogProject.Data.BlogProject.Data;

namespace BlogProject.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly BlogContext _context;


        public UserService(IUserRepository userRepository, BlogContext context)
        {
            _userRepository = userRepository;
            _context = context;
        }

        public List<User> GetAll()
        {
            return _userRepository.GetAll();
        }
        public User GetByUsername(string username)
        {
            return _userRepository.GetByUsername(username);
        }

        public async Task MakeUserPremiumAsync(string userId)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id.ToString() == userId);
            if (user != null)
            {
                user.Role = "Premium";
                await _context.SaveChangesAsync();
            }
        }
        public Guid GetFirstUserId()
        {
            return _userRepository.GetFirstUserId();
        }
    }
}