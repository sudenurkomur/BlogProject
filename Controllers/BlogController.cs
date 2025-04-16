using BlogProject.Models;
using BlogProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using BlogProject.Requests;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using BlogProject.Data.BlogProject.Data;
using BlogProject.Helpers;




namespace BlogProject.Controllers
{

    [Authorize]
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;
        private readonly ICategoryService _categoryService;
        private readonly IUserService _userService;
        private readonly BlogContext _context;
        private readonly GeminiService _geminiService;

        public BlogController(IBlogService blogService, ICategoryService categoryService, IUserService userService , BlogContext context, GeminiService geminiService)
        {
            _blogService = blogService;
            _categoryService = categoryService;
            _userService = userService;
            _context = context;
            _geminiService = geminiService;
        }

        [HttpPost]
        [Authorize(Roles = "Premium")]
        public async Task<IActionResult> FixGrammar([FromBody] FixGrammarRequest request)
        {
            var fixedText = await _geminiService.FixGrammarAsync(request.Text);
            return Content(fixedText);
        }

        [AllowAnonymous]
        [Route("Blog")]
        [Route("Blog/Page/{page?}")]
        public IActionResult Index(Guid? categoryId, int page = 1)
        {
            int pageSize = 5;

            var blogs = _blogService.GetAllBlogs();

            if (categoryId.HasValue && categoryId != Guid.Empty)
            {
                blogs = blogs.Where(b => b.CategoryId == categoryId).ToList();
                ViewBag.SelectedCategoryId = categoryId.Value;
            }
            else
            {
                ViewBag.SelectedCategoryId = "";
            }

            var pagedBlogs = blogs
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling((double)blogs.Count / pageSize);
            ViewBag.Categories = _categoryService.GetAll();

            return View(pagedBlogs);
        }

        // Detay
        [AllowAnonymous]
        public IActionResult Details(Guid id)
        {
            var blog = _context.Blogs
                .Include(b => b.User)
                .Include(b => b.Category)
                .Include(b => b.Comments)
                    .ThenInclude(c => c.User) // yorum yazan kişiyi de çek
                .FirstOrDefault(b => b.Id == id);

            if (blog == null)
                return NotFound();

            // Yorumları parent-child hiyerarşisine göre grupla (nested yorum için)
            blog.Comments = blog.Comments
                .Where(c => c.ParentCommentId == null)
                .OrderByDescending(c => c.CreatedDate)
                .ToList();

            return View(blog);
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
            Console.WriteLine("Gelen Kategori ID: " + request.CategoryId);

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


        public IActionResult Delete(Guid id)
        {
            var blog = _blogService.GetBlogById(id);
            if (blog == null)
                return NotFound();

            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (blog.UserId.ToString() != currentUserId)
                return Forbid();

            // 1. Nested yorumları sil
            var comments = _context.Comments
                .Where(c => c.BlogId == blog.Id)
                .ToList();

            if (comments.Any())
            {
                _context.Comments.RemoveRange(comments);
                _context.SaveChanges(); // yorumlar silinsin
            }

            // 2. Blogu sil
            _blogService.DeleteBlog(blog);
            return RedirectToAction("Index");
        }



        [HttpPost]
        [Authorize]
        public IActionResult UpdateBlog(Guid id, string content)
        {
            var blog = _context.Blogs.FirstOrDefault(b => b.Id == id);
            if (blog == null)
                return NotFound();

            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (blog.UserId.ToString() != currentUserId)
                return Forbid();

            blog.Content = content;
            blog.PublishDate = DateTime.Now;

            _context.SaveChanges();
            return RedirectToAction("Details", new { id });
        }
    }
}