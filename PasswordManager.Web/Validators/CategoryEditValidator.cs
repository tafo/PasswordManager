using FluentValidation;
using PasswordManager.Web.Models;
using PasswordManager.Web.Models.Category;

namespace PasswordManager.Web.Validators;

public class CategoryEditValidator : AbstractValidator<CategoryModel>
{
    public CategoryEditValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MinimumLength(3).MaximumLength(50);
    }
}