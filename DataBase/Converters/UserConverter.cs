using DataBase.Models;
using domain.Models;

namespace Database.Converters;

public static class UserConverter
{
  public static UserModel ToModel(this User model)
  {
    return new UserModel
    {
      Id = model.Id,
      PhoneNumber = model.PhoneNumber,
      FullName = model.FullName,
      Role = model.Role,
      Username = model.Username,
      Password = model.Password,
    };
  }

  public static User ToDomain(this UserModel model)
  {
    return new User
    {
      Id = model.Id,
      PhoneNumber = model.PhoneNumber,
      FullName = model.FullName,
      Role = model.Role,
      Username = model.Username,
      Password = model.Password,
    };
  }
}