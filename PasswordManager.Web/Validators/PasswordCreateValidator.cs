using FluentValidation;
using PasswordManager.Web.Models.Password;

namespace PasswordManager.Web.Validators;

public class PasswordCreateValidator : AbstractValidator<PasswordCreateModel>
{
    public PasswordCreateValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MinimumLength(3).MaximumLength(50);
        RuleFor(x => x.URL).NotEmpty().Must(x => Uri.TryCreate(x, UriKind.Absolute, out _)).When(x => !string.IsNullOrEmpty(x.URL));
        RuleFor(x => x.CategoryId).GreaterThan(0);
        RuleFor(x => x.Username).NotEmpty().MinimumLength(3).MaximumLength(50);
        RuleFor(x => x.Password).NotEmpty().MinimumLength(3).MaximumLength(50);
    }
}