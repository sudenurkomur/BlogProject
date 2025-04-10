﻿using BlogProject.Requests;
using FluentValidation;
using global::BlogProject.Requests;

namespace BlogProject.Validators
{

    namespace BlogProject.Validators
    {
        public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
        {
            public RegisterRequestValidator()
            {
                RuleFor(x => x.Username)
                    .NotEmpty().WithMessage("Kullanıcı adı boş olamaz.")
                    .MinimumLength(3).WithMessage("Kullanıcı adı en az 3 karakter olmalıdır.");

                RuleFor(x => x.Email)
                    .NotEmpty().WithMessage("Email boş olamaz.")
                    .EmailAddress().WithMessage("Geçerli bir email adresi giriniz.");

                RuleFor(x => x.Password)
                    .NotEmpty().WithMessage("Şifre boş olamaz.")
                    .MinimumLength(6).WithMessage("Şifre en az 6 karakter olmalıdır.");

                RuleFor(x => x.ConfirmPassword)
                    .Equal(x => x.Password).WithMessage("Şifreler eşleşmiyor.");
            }
        }
    }
}
