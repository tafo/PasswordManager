using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PasswordManager.Web.Data;
using PasswordManager.Web.Infrastructure.Security;
using PasswordManager.Web.Mappers;
using PasswordManager.Web.Models.Category;

namespace PasswordManager.Web.Controllers;

[SessionAuthorizationFilter]
public class CategoryController : Controller
{
    private readonly PasswordManagerContext _context;
    private readonly IValidator<CategoryCreateModel> _categoryCreateValidator;
    private readonly IValidator<CategoryModel> _categoryEditValidator;

    public CategoryController(
        PasswordManagerContext context, 
        IValidator<CategoryCreateModel> categoryCreateValidator,
        IValidator<CategoryModel> categoryEditValidator)
    {
        _context = context;
        _categoryCreateValidator = categoryCreateValidator;
        _categoryEditValidator = categoryEditValidator;
    }
    
    public IActionResult Index()
    {
        var categories = _context.Categories.Select(x => x.ToModel());
        return View(categories);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CategoryCreateModel model)
    {
        var validationResult = await _categoryCreateValidator.ValidateAsync(model);
        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState);
            return View("Create", model);
        }

        try
        {
            var category = model.ToEntity();
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        catch (Exception)
        {
            ModelState.AddModelError("", "Unable to save changes. Try again");
        }
        
        return View(model);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var entity = await _context.Categories.SingleOrDefaultAsync(x => x.Id == id.Value);
        if (entity == null)
        {
            return NotFound();
        }
        return View(entity.ToModel());
    }

    [HttpPost]
    public async Task<IActionResult> Edit(CategoryModel model)
    {
        var validationResult = await _categoryEditValidator.ValidateAsync(model);
        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState);
            return View("Edit", model);
        }

        try
        {
            var entity = model.ToEntity();
            _context.Update(entity);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        catch (Exception)
        {
            ModelState.AddModelError("", "Unable to save changes. Try again");
        }

        return View(model);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (!id.HasValue)
        {
            // ToDo : Test NotFound
            return NotFound();
        }

        var entity = await _context.Categories.SingleOrDefaultAsync(x => x.Id == id.Value);
        if (entity == null)
        {
            return NotFound();
        }
        
        return View(entity.ToModel());
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var entity = await _context.Categories.SingleOrDefaultAsync(x => x.Id == id);
        if (entity == null)
        {
            return NotFound();
        }
        try
        {
            _context.Categories.Remove(entity);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        catch (Exception)
        {
            ModelState.AddModelError("", "Unable to delete the record. Try again");
        }

        return View(entity.ToModel());
    }
}