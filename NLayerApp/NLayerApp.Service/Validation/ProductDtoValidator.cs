using FluentValidation;
using NLayerApp.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerApp.Service.Validation
{
    public class ProductDtoValidator : AbstractValidator<ProductDto>
    {

        public ProductDtoValidator()
        {
            RuleFor(x => x.productName).NotNull().WithMessage("{PropertyName} is Required")
                .NotEmpty().WithMessage("{PropertyName} is Empty");

            RuleFor(x => x.Price).InclusiveBetween(1, int.MaxValue).WithMessage("{PropertyName} must be greater than 0");

            RuleFor(x => x.Stock).InclusiveBetween(1, int.MaxValue).WithMessage("{PropertyName} must be greater than 0");


        }

    }
}
