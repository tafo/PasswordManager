namespace PasswordManager.Web.Models.Password;

public class PasswordDeleteModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string URL { get; set; }
    public string CategoryName { get; set; }
    public string Username { get; set; }
    // ToDo : Password field is removed 
}