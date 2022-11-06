namespace PasswordManager.Web.Domain;

public class AccountEntity
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
}