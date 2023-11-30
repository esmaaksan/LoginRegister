using DataAccess.Layer.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Layer.AddressesValidation
{
    public class AddressesValidators: AbstractValidator<Adresses>
    {
        public AddressesValidators()
        {
            RuleFor(c => c.UserAddress).NotEmpty().NotNull().WithMessage("Adres başlığını boş geçemezsin");
            RuleFor(c => c.City).NotEmpty().NotNull().WithMessage("Şehiri boş geçemezsin");
            RuleFor(c => c.Town).NotEmpty().NotNull().WithMessage("İlçeyi boş geçemezsin");
            RuleFor(c => c.Street).NotEmpty().NotNull().WithMessage("sokağı boş geçemezsin");
            RuleFor(c => c.Neighbourhood).NotEmpty().NotNull().WithMessage("mahalleyi boş geçemezsin");
            RuleFor(c => c.No).NotEmpty().NotNull().WithMessage("kapı numaranızı giriniz.");
        }

    }
}
