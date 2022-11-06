using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PasswordManager.Web.Data;
using PasswordManager.Web.Infrastructure.Security;
using PasswordManager.Web.Mappers;

namespace PasswordManager.Web.Controllers;

[SessionAuthorizationFilter]
public class HomeController : Controller
{
    private readonly PasswordManagerContext _context;
    public HomeController(PasswordManagerContext context)
    {
        _context = context;
    }
    
    public IActionResult Index(string filter)
    {
        var categories = _context.Categories.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name });
        if (!categories.Any())
        {
            ViewBag.Message = "You have to create a category";
            return View(null);
        }
        if (string.IsNullOrEmpty(filter))
        {
            var passwords = _context.Passwords.Select(x => x.ToModel()).ToList();
            ViewBag.Categories = categories;
            return View(passwords);
        }
        
        return View();
    }

    public IActionResult Create()
    {
        return View();
    }

    public IActionResult Edit()
    {
        return View();
    }

    public IActionResult Delete()
    {
        return View();
    }
}