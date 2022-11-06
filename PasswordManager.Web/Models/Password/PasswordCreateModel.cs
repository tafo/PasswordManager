namespace PasswordManager.Web.Models.Password;

public class PasswordCreateModel
{
    public string Name { get; set; }
    public string URL { get; set; }
    public int CategoryId { get; set; }
    public int Username { get; set; }
    public string Password { get; set; }
}