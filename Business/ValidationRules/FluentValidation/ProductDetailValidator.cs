using Entities.DTOs.Product;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class ProductDetailValidator : AbstractValidator<ProductDetailDto>
    {
        public ProductDetailValidator()
        {
            RuleFor(c => c.ProductName).NotEmpty().NotNull().WithMessage("Ürün ismi boş geçilemez");
            RuleFor(c => c.ProductName).MinimumLength(2);
        }
    }
}
