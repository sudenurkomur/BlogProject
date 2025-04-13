using BlogProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BlogProject.Models;
using BlogProject.Requests;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using BlogProject.Data.BlogProject.Data;

namespace BlogProject.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogApiController : ControllerBase
    {
        private readonly IBlogService _blogService;
        private readonly IUserService _userService;
        private readonly BlogContext _context;
        public BlogApiController(IBlogService blogService, IUserService userService , BlogContext context)
        {
            _blogService = blogService;
            _userService = userService;
            _context = context;
        }


        // get /api/blogapi/{id} seçileni
        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult GetById(Guid id)
        {
            var blog = _blogService.GetBlogById(id);
            if (blog == null)
                return NotFound();

            return Ok(blog);
        }

        [HttpPost("create")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> AddBlog([FromBody] CreateBlogRequest request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var blog = new Blog
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                Content = request.Content,
                CategoryId = request.CategoryId,
                UserId = Guid.Parse(userId),
                PublishDate = DateTime.Now
            };

            await _blogService.AddBlogAsync(blog);

            return Ok(new { message = "Blog başarıyla eklendi." });
        }
        

        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult GetBlogById(Guid id)
        {
            var blog = _context.Blogs
                .Include(b => b.Category)
                .Include(b => b.User)
                .FirstOrDefault(b => b.Id == id);

            if (blog == null)
                return NotFound();

            return Ok(new
            {
                blog.Id,
                blog.Title,
                blog.Content,
                Category = blog.Category?.Name,
                Author = blog.User?.Username,
                blog.PublishDate
            });
        }

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> DeleteBlog(Guid id)
        {
            var blog = await _context.Blogs.FindAsync(id);
            if (blog == null)
                return NotFound();

            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (blog.UserId.ToString() != currentUserId)
                return Forbid();

            _context.Blogs.Remove(blog);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> UpdateBlog(Guid id, [FromBody] CreateBlogRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var blog = await _context.Blogs.FindAsync(id);
            if (blog == null)
                return NotFound();

            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (blog.UserId.ToString() != currentUserId)
                return Forbid();

            blog.Title = request.Title;
            blog.Content = request.Content;
            blog.CategoryId = request.CategoryId;

            await _context.SaveChangesAsync();
            return Ok(new
            {
                blog.Id,
                blog.Title,
                blog.Content
            });
        }
    }
}