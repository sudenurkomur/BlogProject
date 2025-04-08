using BlogProject.Models;
using BlogProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using BlogProject.DTOs;



namespace BlogProject.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;
        private readonly ICategoryService _categoryService;
        private readonly IUserService _userService;


        public BlogController(IBlogService blogService, ICategoryService categoryService, IUserService userService)
        {
            _blogService = blogService;
            _categoryService = categoryService;
            _userService = userService;
        }



        // Blogları listele
        public IActionResult Index()
        {
            var blogs = _blogService.GetAllBlogs();
            return View(blogs); // Views/Blog/Index.cshtml
        }

        // Detay
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
        public IActionResult Create(CreateBlogDTO dto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = _categoryService.GetAll();
                return View(dto);
            }

            var blog = new Blog
            {
                Id = Guid.NewGuid(),
                Title = dto.Title,
                Content = dto.Content,
                PublishDate = DateTime.Now,
                CategoryId = dto.CategoryId,
                UserId = _userService.GetFirstUserId() // ileride login sonrası değişecek
            };

            if (dto.ImageFile != null && dto.ImageFile.Length > 0)
            {
                using var ms = new MemoryStream();
                dto.ImageFile.CopyTo(ms);
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

            _blogService.DeleteBlog(blog);
            return RedirectToAction("Index");
        }
    }
}