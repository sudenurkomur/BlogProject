using BlogProject.Models;
using BlogProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using BlogProject.Requests;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using BlogProject.Data.BlogProject.Data;




namespace BlogProject.Controllers
{

    [Authorize]
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;
        private readonly ICategoryService _categoryService;
        private readonly IUserService _userService;
        private readonly BlogContext _context;
        private BlogContext context;

        public BlogController(IBlogService blogService, ICategoryService categoryService, IUserService userService , BlogContext _context)
        {
            _blogService = blogService;
            _categoryService = categoryService;
            _userService = userService;
            _context = context;
        }


        [AllowAnonymous]
        // Blogları listele
        public IActionResult Index()
        {
            var blogs = _blogService.GetAllBlogs();
            return View(blogs); // Views/Blog/Index.cshtml
        }

        // Detay
        [AllowAnonymous]
        public IActionResult Details(Guid id)
        {
            var blog = _blogService.GetBlogById(id);
            if (blog == null)
                return NotFound();

            return View(blog); // Views/Blog/Details.cshtml
        }

        // Yeni blog (form)
        // GET: Blog/Create
        public IActionResult Create()
        {
            ViewBag.Categories = _categoryService.GetAll();
            return View();
        }

        // POST: Blog/Create
        [HttpPost]
        public IActionResult Create(CreateBlogRequest request)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = _categoryService.GetAll();
                return View(request);
            }

            var username = User.Identity?.Name;
            var user = _userService.GetByUsername(username); 

            var blog = new Blog
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                Content = request.Content,
                PublishDate = DateTime.Now,
                CategoryId = request.CategoryId,
                UserId = user.Id
            };

            if (request.ImageFile != null && request.ImageFile.Length > 0)
            {
                using var ms = new MemoryStream();
                request.ImageFile.CopyTo(ms);
                blog.ImageBase64 = Convert.ToBase64String(ms.ToArray());
            }

            _blogService.AddBlog(blog);
            return RedirectToAction("Index");
        }


        // Sil
        public IActionResult Delete(Guid id)
        {
            var blog = _blogService.GetBlogById(id);
            if (blog == null)
                return NotFound();

            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (blog.UserId.ToString() != currentUserId)
            {
                return Forbid(); // başka birinin blogunu silemez
            }

            _blogService.DeleteBlog(blog);
            return RedirectToAction("Index");
        }
    }
}