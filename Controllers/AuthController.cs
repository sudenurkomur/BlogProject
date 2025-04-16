using Microsoft.AspNetCore.Mvc;
using BlogProject.Requests;
using BlogProject.Models;
using BlogProject.Data.BlogProject.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using BCrypt.Net;
using BlogProject.Helpers;

namespace BlogProject.Controllers
{
    [AllowAnonymous] // Controller genelinde AllowAnonymous (sadece Login/Register için erişim)
    public class AuthController : Controller
    {
        private readonly BlogContext _context;
        private readonly JwtTokenHelper _jwtHelper;

        public AuthController(BlogContext context, JwtTokenHelper jwtHelper)
        {
            _context = context;
            _jwtHelper = jwtHelper;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterRequest request)
        {
            if (!ModelState.IsValid || request.Password != request.ConfirmPassword)
            {
                ModelState.AddModelError("", "Şifreler uyuşmuyor.");
                return View(request);
            }

            var user = new User
            {
                Id = Guid.NewGuid(),
                Username = request.Username,
                Email = request.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password)
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginAsync(LoginRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);

            var user = _context.Users.FirstOrDefault(x => x.Email == request.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                ModelState.AddModelError("", "Email veya şifre hatalı");
                return View(request);
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role ?? "User")
            };

            //  Access ve Refresh Token oluştur
            var accessToken = _jwtHelper.GenerateAccessToken(claims);
            var refreshToken = _jwtHelper.GenerateRefreshToken();

            //  RefreshToken bilgisini kaydet
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpires = DateTime.Now.AddDays(7);
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            // cookie login işlemi 
            var identity = new ClaimsIdentity(claims, "UserCookie");
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync("UserCookie", principal);

            // geçici olarak göstermek için TempData
            TempData["JwtToken"] = accessToken;

            //  Eğer View değil de REST API endpoint'iyse
            /*
            return Ok(new
            {
                Token = accessToken,
                RefreshToken = refreshToken               // kontrol etmem gerekirse
            });
            */

            return RedirectToAction("Index", "Home");
        }

        [Authorize] // Çıkış yapmak sadece giriş yapanlara açık
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("UserCookie");
            return RedirectToAction("Login", "Auth");
        }


    }
}