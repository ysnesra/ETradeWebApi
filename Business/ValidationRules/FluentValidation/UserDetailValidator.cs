﻿using Entities.Concrete;
using Entities.DTOs.User;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class UserDetailValidator : AbstractValidator<UserDetailDto>
    {
        public UserDetailValidator()
        {
            RuleFor(u => u.FirstName).NotNull().NotEmpty().WithMessage("İsim alanı boş geçilemez!")
                 .MinimumLength(2).WithMessage("İsim alanı en az 2 karakter olmalıdır!");

            RuleFor(u => u.LastName).NotNull().NotEmpty().WithMessage("Soyad alanı boş geçilemez!").MinimumLength(2);

            RuleFor(u => u.Email).NotNull().NotEmpty().WithMessage("Mail alanı boş geçilemez");
            RuleFor(u => u.Email).EmailAddress().WithMessage("Geçerli bir e-Mail adresi giriniz! ");

            RuleFor(u => u.Password).NotNull().NotEmpty().WithMessage("Parola alanı boş geçilemez!");
            RuleFor(u => u.Password).Matches(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$").WithMessage("Parolanız en az sekiz karakter, en az bir harf ve bir sayı içermelidir!");

            RuleFor(u => u.RePassword).NotNull().NotEmpty().WithMessage("Parola alanı boş geçilemez!").Equal(u => u.Password).WithMessage("Şifreler eşleşmiyor");
        }
    }
}
