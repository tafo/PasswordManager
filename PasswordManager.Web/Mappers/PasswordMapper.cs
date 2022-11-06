using PasswordManager.Web.Domain;
using PasswordManager.Web.Models;
using PasswordManager.Web.Models.Password;

namespace PasswordManager.Web.Mappers;

public static class PasswordMapper
{
    public static PasswordModel ToModel(this PasswordEntity entity)
    {
        var model = new PasswordModel
        {
            Id = entity.Id,
            Name = entity.Name,
            URL = entity.URL,
            CategoryName = entity.Category.Name,
            Username = entity.Username,
            Password = entity.Password
        };

        return model;
    }
}