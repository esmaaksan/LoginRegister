using DataAccess.Layer.DTOS;
using DataAccess.Layer.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DataAccess.Layer.AddressesValidation
{
    public class UsersValidators:AbstractValidator<UsersDTO>
    {
        public UsersValidators()
        {
            RuleFor(a => a.UserName).NotEmpty().NotNull().WithMessage("Kullanıcı adını boş geçemezsin");
            RuleFor(u => u.Email).EmailAddress().WithMessage("Mail adresini doldurunuz.");
            RuleFor(a=>a.Password).Must(IsPasswordValid).WithMessage("Parolanız en az sekiz karakter, en az bir harf ve bir sayı içermelidir!");
        }
        private bool IsPasswordValid(string arg)
        {
            Regex regex = new Regex(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$");
            return regex.IsMatch(arg);
        }
    }
}
