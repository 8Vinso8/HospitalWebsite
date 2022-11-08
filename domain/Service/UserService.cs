namespace domain.Service;

using Logic;
using Models;

public class UserService
{
  private IUserRepository _db;

  public UserService(IUserRepository db)
  {
    _db = db;
  }

  public Result<User> GetUserByUsername(string username)
  {
    if (string.IsNullOrEmpty(username))
      return Result.Fail<User>("Invalid username");

    return _db.IsUserExists(username)
      ? Result.Ok(_db.GetUserByUsername(username))
      : Result.Fail<User>("Cant find user");
  }


  public Result IsUserExists(string username)
  {
    if (string.IsNullOrEmpty(username))
      return Result.Fail("Invalid username");

    return _db.IsUserExists(username)
      ? Result.Ok()
      : Result.Fail("Cant find user");
  }

  public Result<User> Register(User user)
  {
    var validity = user.IsValid();
    if (validity.IsFailure)
      return Result.Fail<User>("Invalid user: " + validity.Error);

    if (_db.IsUserExists(user.Username))
      return Result.Fail<User>("Username is already taken");

    return _db.Create(user)
      ? Result.Ok(user)
      : Result.Fail<User>("Cant create user");
  }
}
