using Database.Converters;

namespace DataBase.Repositories;

using System.Collections.Generic;
using domain.Models;
using domain.Logic;
using System.Linq;

public class UserRepository : IUserRepository
{
  private readonly ApplicationContext _context;

  public UserRepository(ApplicationContext context)
  {
    _context = context;
  }

  public IEnumerable<User> GetAll()
  {
    return _context.Users.Select(user => user.ToDomain());
  }

  public bool Create(User item)
  {
    try
    {
      _context.Users.Add(item.ToModel());
    }
    catch (Exception)
    {
      return false;
    }

    return true;
  }

  public bool Update(User item)
  {
    try
    {
      _context.Users.Update(item.ToModel());
    }
    catch (Exception)
    {
      return false;
    }

    return true;
  }

  public bool Delete(int id)
  {
    var user = GetItem(id);
    if (user == default)
      return false;
    _context.Users.Remove(user.ToModel());
    return true;
  }

  public void Save()
  {
    _context.SaveChanges();
  }

  public bool IsUserExists(string username)
  {
    return _context.Users.Any(user => user.Username == username);
  }

  public User? GetItem(string username)
  {
    return _context.Users.FirstOrDefault(user => user.Username == username)?.ToDomain();
  }

  public User? GetItem(int id)
  {
    return _context.Users.FirstOrDefault(user => user.Id == id)?.ToDomain();
  }
}