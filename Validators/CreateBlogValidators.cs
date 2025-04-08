using BlogProject.DTOs;
using FluentValidation;

namespace BlogProject.Validators
{
    public class CreateBlogValidator : AbstractValidator<CreateBlogDTO>
    {
        public CreateBlogValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Başlık boş olamaz")
                .MinimumLength(5).WithMessage("Başlık en az 5 karakter olmalı");

            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("İçerik boş olamaz")
                .MinimumLength(30).WithMessage("İçerik en az 30 karakter olmalı");

            RuleFor(x => x.CategoryId)
                .NotEmpty().WithMessage("Kategori seçilmelidir");
        }
    }
}