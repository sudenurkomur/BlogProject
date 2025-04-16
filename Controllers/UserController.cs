using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BlogProject.Data.BlogProject.Data;
using System.Security.Claims;
using BlogProject.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;

namespace BlogProject.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly BlogContext _context;

        private readonly IUserService _userService;

        public UserController(BlogContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        public IActionResult BecomePremium()
        {
            return View();
        }

        [HttpPost]
        [HttpPost]
        [HttpPost]
        public async Task<IActionResult> ConfirmPremium()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _context.Users.FirstOrDefault(u => u.Id.ToString() == userId);

            if (user != null)
            {
                user.Role = "Premium";
                await _context.SaveChangesAsync();

                // Yeni claim'leri oluştur, böylece premium rolü yansın
                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Role, user.Role)
        };

                var identity = new ClaimsIdentity(claims, "UserCookie");
                var principal = new ClaimsPrincipal(identity);

                // Mevcut kullanıcı kimlik bilgilerini yeni cookie ile güncelle
                await HttpContext.SignInAsync("UserCookie", principal);

                // Artık kullanıcı, cookie içinde "Premium" rolüne sahip
                return RedirectToAction("Login", "Auth");
            }

            return NotFound();
        }
    }
}