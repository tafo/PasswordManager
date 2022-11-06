using FluentValidation;
using PasswordManager.Web.Models;
using PasswordManager.Web.Models.Account;

namespace PasswordManager.Web.Validators;

public class AccountCreateValidator : AbstractValidator<AccountCreateModel>
{
    public AccountCreateValidator()
    {
        RuleFor(x => x.Email).NotNull().EmailAddress();
        RuleFor(x => x.Password).NotNull().MinimumLength(3);
    }
}