using domain.Logic;

namespace domain.Models
{
  public class User
  {
    public int Id;
    public string PhoneNumber;
    public string FullName;
    public Role Role;
    public string Username;
    public string Password;

    public User() { }
    public User(int _Id, string _PhoneNumber, string _FullName, Role _Role,
                string _Username, string _Password)
    {
      Id = _Id;
      PhoneNumber = _PhoneNumber;
      FullName = _FullName;
      Role = _Role;
      Username = _Username;
      Password = _Password;
    }

    public Result IsValid()
    {
      if (string.IsNullOrEmpty(Username))
        return Result.Fail("Invalid username");

      if (string.IsNullOrEmpty(Password))
        return Result.Fail("Invalid password");

      if (string.IsNullOrEmpty(PhoneNumber))
        return Result.Fail("Invalid phone number");

      if (string.IsNullOrEmpty(FullName))
        return Result.Fail("Invalid full name");

      return Result.Ok();
    }
  }
}
