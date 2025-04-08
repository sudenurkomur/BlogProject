namespace BlogProject.Models
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        // Navigation
        public ICollection<Blog> Blogs { get; set; }
    }

}
