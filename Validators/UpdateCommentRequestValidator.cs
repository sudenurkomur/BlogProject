using FluentValidation;
using BlogProject.Requests;

namespace BlogProject.Validators
{
    public class UpdateCommentRequestValidator : AbstractValidator<UpdateCommentRequest>
    {
        public UpdateCommentRequestValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Yorum ID bilgisi eksik.");

            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("Yorum içeriği boş bırakılamaz.")
                .MaximumLength(500).WithMessage("Yorum 500 karakterden uzun olamaz.");

            RuleFor(x => x.BlogId)
                .NotEmpty().WithMessage("Blog bilgisi eksik.");
        }
    }
}