namespace BlogProject.Requests
{
    public class CreateCommentRequest
    {
        public Guid BlogId { get; set; }
        public string Content { get; set; } = string.Empty;
        public Guid? ParentCommentId { get; set; } // nested için
    }
}