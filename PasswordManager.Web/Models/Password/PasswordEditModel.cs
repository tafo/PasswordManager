namespace PasswordManager.Web.Models.Password;

public class PasswordEditModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string URL { get; set; }
    public int CategoryId { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
}