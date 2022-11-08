using domain.Models;

namespace DataBase.Converters;

using Models;

public static class UserModelToDomainConverter
{
    public static User? ToDomain(this UserModel model)
    {
        return new User
        {
            Id = model.Id,
            Username = model.Username,
            Role = model.Role,
            FullName = model.FullName,
            Password = model.Password,
            PhoneNumber = model.PhoneNumber
        };
    }
}