using BlogProject.Models;
using BlogProject.Data.BlogProject.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using BlogProject.Requests;

namespace BlogProject.Controllers
{
    [Authorize] // Giriş yapmamış kullanıcı bu controllera erişemez
    public class CommentController : Controller
    {
        private readonly BlogContext _context;

        public CommentController(BlogContext context)
        {
            _context = context;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(UpdateCommentRequest request)
        {
            var comment = _context.Comments.FirstOrDefault(c => c.Id == request.Id);

            if (comment == null)
                return NotFound();

            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (comment.UserId.ToString() != currentUserId)
                return Forbid();

            comment.Content = request.Content;
            comment.ModifiedDate = DateTime.Now;
            _context.SaveChanges();

            return RedirectToAction("Details", "Blog", new { id = comment.BlogId });
        }

        [HttpPost]
        public IActionResult Add(CreateCommentRequest request)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Details", "Blog", new { id = request.BlogId });
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var comment = new Comment
            {
                Id = Guid.NewGuid(),
                BlogId = request.BlogId,
                Content = request.Content,
                ParentCommentId = request.ParentCommentId,
                CreatedDate = DateTime.Now,
                UserId = Guid.Parse(userId)
            };

            _context.Comments.Add(comment);
            _context.SaveChanges();

            return RedirectToAction("Details", "Blog", new { id = request.BlogId });
        }

        // GET: Yorum Düzenleme Sayfası
        public IActionResult Edit(Guid id)
        {
            var comment = _context.Comments.FirstOrDefault(c => c.Id == id);

            if (comment == null)
                return NotFound();

            // Sadece yorumu yazan kullanıcı düzenleyebilsin
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (comment.UserId.ToString() != currentUserId)
                return Forbid(); // Yetkisi yok

            var model = new UpdateCommentRequest
            {
                Id = comment.Id,
                Content = comment.Content
            };

            return View(comment); // Views/Comment/Edit.cshtml
        }

        // POST: Yorum Güncelleme
        [HttpPost]
        public IActionResult Edit(UpdateCommentRequest updatedComment)
        {
            var existing = _context.Comments.FirstOrDefault(c => c.Id == updatedComment.Id);

            if (existing == null)
                return NotFound();

            // Yine sadece yorumu yazan kişi değiştirebilsin
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (existing.UserId.ToString() != currentUserId)
                return Forbid();

            existing.Content = updatedComment.Content;
            existing.ModifiedDate = DateTime.Now;

            _context.SaveChanges();

            return RedirectToAction("Details", "Blog", new { id = existing.BlogId });
        }

        [HttpPost]
        [Authorize]
        public IActionResult AddReply(CreateCommentRequest request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(request.Content) || string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Details", "Blog", new { id = request.BlogId });
            }

            var comment = new Comment
            {
                Id = Guid.NewGuid(),
                Content = request.Content,
                BlogId = request.BlogId,
                UserId = Guid.Parse(userId),
                ParentCommentId = request.ParentCommentId,
                CreatedAt = DateTime.Now
            };

            _context.Comments.Add(comment);
            _context.SaveChanges();

            return RedirectToAction("Details", "Blog", new { id = request.BlogId, anchor = "yorumlar" });
        }
    }
}