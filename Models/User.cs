namespace BlogProject.Models
{
    public class User
    {
        public Guid Id { get; set; }  // Primary Key
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; } = "User";

        // Navigation
        public ICollection<Blog> Blogs { get; set; }
        public ICollection<Comment> Comments { get; set; }

        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpires { get; set; }
    }

}
