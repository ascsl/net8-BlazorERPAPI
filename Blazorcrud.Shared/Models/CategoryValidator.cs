using FluentValidation;

namespace Blazorcrud.Shared.Models;

public class CategoryValidator : AbstractValidator<Category>
{
    public CategoryValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;        

        RuleFor(register => register.Name).NotEmpty().WithMessage("Name is a required field.")
            .Length(3, 50).WithMessage("Name must be between 3 and 50 characters.");
    }
}
