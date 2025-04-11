using FluentValidation;
using BlogProject.Requests;

namespace BlogProject.Validators
{
    public class UpdateCommentRequestValidator : AbstractValidator<UpdateCommentRequest>
    {
        public UpdateCommentRequestValidator()
        {

            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("Yorum içeriği boş bırakılamaz.")
                .MaximumLength(500).WithMessage("Yorum 500 karakterden uzun olamaz.");

        }
    }
}