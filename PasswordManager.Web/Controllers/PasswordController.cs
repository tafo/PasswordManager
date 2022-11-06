using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PasswordManager.Web.Data;
using PasswordManager.Web.Infrastructure.Security;
using PasswordManager.Web.Mappers;
using PasswordManager.Web.Models.Password;

namespace PasswordManager.Web.Controllers;

[SessionAuthorizationFilter]
public class PasswordController : Controller
{
    private readonly PasswordManagerContext _context;
    private readonly IValidator<PasswordCreateModel> _passwordCreateValidator;
    private readonly IValidator<PasswordEditModel> _passwordEditValidator;

    public PasswordController(
        PasswordManagerContext context,
        IValidator<PasswordCreateModel> passwordCreateValidator,
        IValidator<PasswordEditModel> passwordEditValidator)
    {
        _context = context;
        _passwordCreateValidator = passwordCreateValidator;
        _passwordEditValidator = passwordEditValidator;
    }

    public IActionResult Index(string filter)
    {
        if (! _context.Categories.Any())
        {
            ViewBag.Message = "You have to create a category";
            return View(null);
        }

        var passwords = from p in _context.Passwords.Include(x => x.Category) select p;
        if (!string.IsNullOrEmpty(filter))
        {
            passwords = passwords.Where(x => x.Name.Contains(filter) || x.URL.Contains(filter) || x.Category.Name.Contains(filter) || x.Username.Contains(filter));
        }

        return View(passwords.Select(x => x.ToModel()).ToList());
    }

    public IActionResult Create()
    {
        PopulateCategories();
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(PasswordCreateModel model)
    {
        PopulateCategories();
        var validationResult = await _passwordCreateValidator.ValidateAsync(model);
        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState);

            return View("Create", model);
        }

        try
        {
            var entity = model.ToEntity();
            await _context.Passwords.AddAsync(entity);
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
        if (!id.HasValue)
        {
            return NotFound();
        }

        var entity = await _context.Passwords.SingleOrDefaultAsync(x => x.Id == id);
        if (entity == null)
        {
            return NotFound();
        }
        
        PopulateCategories();
        return View(entity.ToEditModel());
    }

    [HttpPost]
    public async Task<IActionResult> Edit(PasswordEditModel model)
    {
        PopulateCategories();
        var validationResult = await _passwordEditValidator.ValidateAsync(model);
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
            return NotFound();
        }

        var entity = await _context.Passwords.Include(x => x.Category).SingleOrDefaultAsync(x => x.Id == id.Value);
        if (entity == null)
        {
            return NotFound();
        }
        
        return View(entity.ToDeleteModel());
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var entity = await _context.Passwords.Include(x => x.Category).SingleOrDefaultAsync(x => x.Id == id);
        if (entity == null)
        {
            return NotFound();
        }

        try
        {
            _context.Passwords.Remove(entity);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        catch (Exception)
        {
            ModelState.AddModelError("", "Unable to delete the record. Try again");
        }

        return View(entity.ToDeleteModel());
    }

    public void PopulateCategories()
    {
        var categories = _context.Categories.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList();
        ViewBag.Categories = categories;
    }
}