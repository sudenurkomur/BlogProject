using BlogProject.Data;
using BlogProject.Data.BlogProject.Data;
using BlogProject.Models;
using BlogProject.Repositories.Interfaces;

namespace BlogProject.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly BlogContext _context;

        public UserRepository(BlogContext context)
        {
            _context = context;
        }

        public List<User> GetAll()
        {
            return _context.Users.ToList();
        }

        // Login sistemi gelene kadar test için kullanılabilir
        public Guid GetFirstUserId()
        {
            return _context.Users.Select(u => u.Id).FirstOrDefault();
        }

        public User? GetByUsername(string username)
        {
            return _context.Users.FirstOrDefault(u => u.Username == username);
        }
    }
}