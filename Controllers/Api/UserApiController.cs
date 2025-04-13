using BlogProject.Data.BlogProject.Data;
using BlogProject.Helpers;
using BlogProject.Models;
using BlogProject.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlogProject.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserApiController : ControllerBase
    {
        private readonly BlogContext _context;
        private readonly JwtTokenHelper _jwtTokenHelper;

        public UserApiController(BlogContext context, JwtTokenHelper jwtTokenHelper)
        {
            _context = context;
            _jwtTokenHelper = jwtTokenHelper;
        }

        // Kullanıcı bilgilerini getir (token'dan)
        [HttpGet("me")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public IActionResult GetMyInfo()
        {
            var username = User.FindFirstValue(ClaimTypes.Name);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            return Ok(new
            {
                Id = userId,
                Username = username
            });
        }

        //Refresh token endpoint
        [HttpPost("refresh")]
        public IActionResult RefreshToken([FromBody] RefreshRequest request)
        {
            var refreshToken = request.RefreshToken;

            if (string.IsNullOrEmpty(refreshToken))
                return BadRequest("Refresh token eksik.");

            var user = _context.Users.FirstOrDefault(u => u.RefreshToken == refreshToken);

            if (user == null || user.RefreshTokenExpires < DateTime.Now)
                return Unauthorized("Geçersiz veya süresi dolmuş refresh token.");

            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, user.Username),
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
    };

            var newAccessToken = _jwtTokenHelper.GenerateAccessToken(claims);
            var newRefreshToken = _jwtTokenHelper.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpires = DateTime.Now.AddDays(7);
            _context.SaveChanges();

            return Ok(new
            {
                Token = newAccessToken,
                RefreshToken = newRefreshToken
            });
        }
    }
}