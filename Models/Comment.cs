namespace BlogProject.Models
{
    public class Comment
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedDate { get; set; }

        // Foreign Keys
        public Guid UserId { get; set; }
        public User? User { get; set; }

        public Guid BlogId { get; set; }
        public Blog? Blog { get; set; }
    }

}
