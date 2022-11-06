using FluentValidation;
using PasswordManager.Web.Models;
using PasswordManager.Web.Models.Account;

namespace PasswordManager.Web.Validators;

public class AccountRegisterValidator : AbstractValidator<AccountRegisterModel>
{
    public AccountRegisterValidator()
    {
        RuleFor(x => x.Email).NotNull().EmailAddress();
        RuleFor(x => x.Password).NotNull().MinimumLength(3);
        RuleFor(x => x.PasswordRepeat).Equal(x => x.Password).NotNull().MinimumLength(3);
    }
}