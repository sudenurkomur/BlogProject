using BlogProject.Data.BlogProject.Data;
using BlogProject.Models;
using BlogProject.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlogProject.Services.Implementations
{
    public class CommentService : ICommentService
    {
        private readonly BlogContext _context;

        public CommentService(BlogContext context)
        {
            _context = context;
        }

        public List<Comment> GetCommentsByBlog(Guid blogId)
        {
            var comments = _context.Comments
                .Where(c => c.BlogId == blogId)
                .Include(c => c.User)
                .ToList();

            // Nested yapı kur
            var commentDict = comments.ToDictionary(c => c.Id);
            foreach (var comment in comments)
            {
                if (comment.ParentCommentId.HasValue &&
                    commentDict.ContainsKey(comment.ParentCommentId.Value))
                {
                    commentDict[comment.ParentCommentId.Value].Children.Add(comment);
                }
            }

            return comments.Where(c => c.ParentCommentId == null).ToList();
        }

        public void AddComment(Comment comment)
        {
            _context.Comments.Add(comment);
            _context.SaveChanges();
        }
    }
}
