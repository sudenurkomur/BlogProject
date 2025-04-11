using BlogProject.Requests;
using FluentValidation;

namespace BlogProject.Validators
{
    public class CreateCommentRequestValidator : AbstractValidator<CreateCommentRequest>
    {
        public CreateCommentRequestValidator()
        {
            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("Yorum içeriği boş bırakılamaz.")
                .MaximumLength(1000).WithMessage("Yorum en fazla 1000 karakter olabilir.");

            RuleFor(x => x.BlogId)
                .NotEmpty().WithMessage("Blog ID gereklidir.");
        }
    }
}