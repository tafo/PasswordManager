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

    public static PasswordEntity ToEntity(this PasswordCreateModel model)
    {
        var entity = new PasswordEntity
        {
            Name = model.Name,
            URL = model.URL,
            CategoryId = model.CategoryId,
            Username = model.Username,
            Password = model.Password
        };

        return entity;
    }

    public static PasswordEditModel ToEditModel(this PasswordEntity entity)
    {
        var model = new PasswordEditModel
        {
            Id = entity.Id,
            Name = entity.Name,
            URL = entity.URL,
            CategoryId = entity.CategoryId,
            Username = entity.Username,
            Password = entity.Password
        };
        return model;
    }

    public static PasswordEntity ToEntity(this PasswordEditModel model)
    {
        var entity = new PasswordEntity
        {
            Id = model.Id,
            Name = model.Name,
            URL = model.URL,
            CategoryId = model.CategoryId,
            Username = model.Username,
            Password = model.Password
        };
        return entity;
    }

    public static PasswordDeleteModel ToDeleteModel(this PasswordEntity entity)
    {
        var model = new PasswordDeleteModel
        {
            Id = entity.Id,
            Name = entity.Name,
            URL = entity.URL,
            CategoryName = entity.Category.Name,
            Username = entity.Username
        };
        return model;
    }
}