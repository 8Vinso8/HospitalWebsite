namespace DataBase.Models;

using domain.Models;

public class UserModel
{
  public int Id;
  public string? PhoneNumber;
  public string? FullName;
  public Role Role;
  public string? Username;
  public string? Password;
}