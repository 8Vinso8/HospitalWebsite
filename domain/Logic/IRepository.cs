using domain.Models;

namespace domain.Logic
{
  public interface IRepository<T> where T : class
  {
    IEnumerable<T> GetAll();
    T GetItem(int id);
    void Create(T item);
    void Update(T item);
    void Delete(int id);
    void Save();
  }

  public interface IUserRepository : IRepository<User>
  {
    bool CreateUser(User user);
    bool IsUserExists(string username);
    User GetUserByUsername(string username);
  }
}
