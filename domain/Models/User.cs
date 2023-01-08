using domain.Logic;

namespace domain.Models
{
  public class User
  {
    public int Id { get; set; }
    public string PhoneNumber { get; set; }
    public string FullName { get; set; }
    public Role Role { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }

    public User(int id, string phoneNumber, string fullName, Role role,
      string username, string password)
    {
      Id = id;
      PhoneNumber = phoneNumber;
      FullName = fullName;
      Role = role;
      Username = username;
      Password = password;
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