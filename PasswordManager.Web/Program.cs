using System.Net;
using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using PasswordManager.Web.Data;
using PasswordManager.Web.Infrastructure.Security;
using PasswordManager.Web.Validators;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddAuthentication();
builder.Services.AddControllersWithViews();
builder.Services.AddSession();
builder.Services.AddSingleton<IPasswordHasher, PasswordHasher>();
builder.Services.AddDbContext<PasswordManagerContext>(option => option.UseInMemoryDatabase("PasswordManager"));
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddValidatorsFromAssemblyContaining<AccountCreateValidator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();


app.UseRouting();
app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Password}/{action=Index}/{id?}");

app.Run();