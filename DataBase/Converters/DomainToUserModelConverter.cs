namespace DataBase.Converters;

using DataBase.Models;
using domain.Models;

public static class DomainToUserModelConverter
{
    public static UserModel ToUserModel(this User model)
    {
        return new UserModel
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