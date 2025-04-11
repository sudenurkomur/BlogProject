using BlogProject.Models;

namespace BlogProject.Services.Interfaces
{
    public interface ICommentService
    {
        List<Comment> GetCommentsByBlog(Guid blogId);
        void AddComment(Comment comment);
    }
}
