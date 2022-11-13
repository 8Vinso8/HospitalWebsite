namespace domain.Logic;

using Models;

public interface IUserRepository : IRepository<User>
{
  bool IsUserExists(string username);
  User? GetItem(string username);
}