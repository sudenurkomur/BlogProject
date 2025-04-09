using Microsoft.AspNetCore.Http;

namespace BlogProject.Requests
{
    public class CreateBlogRequest
    {
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public Guid CategoryId { get; set; }
        public IFormFile? ImageFile { get; set; }
    }
}