using System.Data;
using System.Security.Claims;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PasswordManager.Web.Data;
using PasswordManager.Web.Infrastructure.Security;
using PasswordManager.Web.Mappers;
using PasswordManager.Web.Models;
using PasswordManager.Web.Models.Account;

namespace PasswordManager.Web.Controllers;

[AllowAnonymous]
public class AccountController : Controller
{
    private readonly PasswordManagerContext _context;
    private readonly IPasswordHasher _passwordHasher;

    private readonly IValidator<AccountCreateModel> _accountCreateValidator;
    private readonly IValidator<AccountRegisterModel> _accountRegisterValidator;

    public AccountController(
        PasswordManagerContext context,
        IPasswordHasher passwordHasher,
        IValidator<AccountCreateModel> accountValidator, 
        IValidator<AccountRegisterModel> accountRegisterValidator
        )
    {
        _context = context;
        _passwordHasher = passwordHasher;
        _accountCreateValidator = accountValidator;
        _accountRegisterValidator = accountRegisterValidator;
        _context.Database.EnsureCreated();
    }
    
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(AccountCreateModel model)
    {
        var validationResult = await _accountCreateValidator.ValidateAsync(model);
        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState);
            return View("Login", model);
        }

        var account = await _context.Accounts.SingleOrDefaultAsync(x => x.Email == model.Email && _passwordHasher.VerifyHashedPassword(model.Password, x.PasswordHash));
        if (account != null)
        {
            HttpContext.Session.SetString("email", account.Email);
            return RedirectToAction("Index", "Password");
        }

        ViewBag.msg = "Invalid credentials";
        return View("Login");
    }
    
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(AccountRegisterModel model)
    {
        var validationResult = await _accountRegisterValidator.ValidateAsync(model);
        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState);
            return View("Register", model);
        }

        try
        {
            var account = model.ToEntity(_passwordHasher);
            await _context.Accounts.AddAsync(account);
            await _context.SaveChangesAsync();
            HttpContext.Session.SetString("email", account.Email);
            return RedirectToAction("Index", "Password");
        }
        catch (DataException)
        {
            ModelState.AddModelError("", "Unable to save changes. Try again");
        }

        return View(model);
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _context.Dispose();
        }
        base.Dispose(disposing);
    }
}