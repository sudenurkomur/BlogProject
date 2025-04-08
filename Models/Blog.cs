namespace BlogProject.Models
{
    public class Blog
    {
        public Guid Id { get; set; } = Guid.NewGuid(); // otomatik üret
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime PublishDate { get; set; }

        public string? ImageBase64 { get; set; } // 🔥 Base64 image verisi

        public Guid UserId { get; set; }
        public User? User { get; set; }

        public Guid CategoryId { get; set; }
        public Category? Category { get; set; }


        public ICollection<Comment> Comments { get; set; }
    }



}
