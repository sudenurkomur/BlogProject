using System.ComponentModel.DataAnnotations;

namespace BlogProject.Requests
{
    public class UpdateCommentRequest
    {
        public Guid Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public Guid BlogId { get; set; }
    }
}