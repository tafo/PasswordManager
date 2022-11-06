using PasswordManager.Web.Models;

namespace PasswordManager.Web.Domain;

public class PasswordEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string URL { get; set; }
    public int? CategoryId { get; set; }
    public CategoryEntity Category { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
}