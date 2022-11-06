using PasswordManager.Web.Domain;
using PasswordManager.Web.Models;
using PasswordManager.Web.Models.Category;

namespace PasswordManager.Web.Mappers;

public static class CategoryMapper
{
    public static CategoryModel ToModel(this CategoryEntity entity)
    {
        var model = new CategoryModel
        {
            Name = entity.Name,
            Id = entity.Id
        };
        return model;
    }

    public static CategoryEntity ToEntity(this CategoryCreateModel model)
    {
        var entity = new CategoryEntity
        {
            Name = model.Name
        };
        return entity;
    }

    public static CategoryEntity ToEntity(this CategoryModel model)
    {
        var entity = new CategoryEntity
        {
            Id = model.Id,
            Name = model.Name
        };
        return entity;
    }
}