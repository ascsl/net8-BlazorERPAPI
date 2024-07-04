using FluentValidation;

namespace Blazorcrud.Shared.Models;

public class ProductValidator : AbstractValidator<Product>
{
    public ProductValidator()
    {
        //CascadeMode = CascadeMode.Stop;
        RuleLevelCascadeMode = CascadeMode.Stop;        

        RuleFor(register => register.Title).NotEmpty().WithMessage("Name is a required field.")
            .Length(3, 50).WithMessage("Name must be between 3 and 50 characters.");
    }
}