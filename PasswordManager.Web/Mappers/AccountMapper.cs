using PasswordManager.Web.Domain;
using PasswordManager.Web.Infrastructure.Security;
using PasswordManager.Web.Models;
using PasswordManager.Web.Models.Account;

namespace PasswordManager.Web.Mappers;

public static class AccountMapper
{
    public static AccountEntity ToEntity(this AccountRegisterModel model, IPasswordHasher passwordHasher)
    {
        var entity = new AccountEntity
        {
            Email = model.Email,
            PasswordHash = passwordHasher.HashPassword(model.Password)
        };
        return entity;
    }
}