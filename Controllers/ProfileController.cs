using BlogProject.Data.BlogProject.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace BlogProject.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly BlogContext _context;

        public ProfileController(BlogContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var user = _context.Users
                .Include(u => u.Blogs)
                .FirstOrDefault(u => u.Id.ToString() == currentUserId);

            if (user == null)
                return NotFound();

            return View(user);
        }
    }
}