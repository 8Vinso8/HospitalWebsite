namespace domain.Logic;

using Models;

public interface IUserRepository : IRepository<User>
{
  bool IsUserExists(string username);
  bool IsUserExists(int id);
  User? GetItem(string username);
  User? GetItem(int id);
}