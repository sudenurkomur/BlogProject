using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace BlogProject.DTOs
{
    public class CreateBlogDTO
    {
        [Required(ErrorMessage = "Başlık boş geçilemez.")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "İçerik boş geçilemez.")]
        public string Content { get; set; } = string.Empty;

        [Required(ErrorMessage = "Kategori seçilmelidir.")]
        public Guid CategoryId { get; set; }

        public IFormFile? ImageFile { get; set; }
    }
}