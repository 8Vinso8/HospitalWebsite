namespace domain.Logic;

using Models;

public interface IUserRepository : IRepository<User>
{
  bool CreateUser(User user);
  bool IsUserExists(string username);
  User GetUserByUsername(string username);
}