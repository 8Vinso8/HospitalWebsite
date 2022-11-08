namespace DataBase.Repositories;

using System.Collections.Generic;
using domain.Models;
using domain.Logic;
using Models;
using Converters;
using System.Linq;


public class UserRepository : IUserRepository
{
    private readonly ApplicationContext _context;

    public UserRepository(ApplicationContext context)
    {
        _context = context;
    }

    public User? GetUserByUsername(string username)
    {
        var user = _context.Users.FirstOrDefault(u => u.Username == username);
        return user?.ToDomain();
    }

    public User? GetUserByID(int id)
    {
        var user = _context.Users.FirstOrDefault(u => u.Id == id);
        return user?.ToDomain();
    }

    public bool IsUserExists(string username)
    {
        var flag = _context.Users.FirstOrDefault(u => u.Username == username);
        return flag != null;
    }
    
    public bool Create(User user)
    {
        UserModel newuser = new()
        {
            Username = user.Username,
            Id = user.Id,
            PhoneNumber = user.PhoneNumber,
            FullName = user.FullName,
            Role = user.Role,
            Password = user.Password,
        };
        try
        {
            _context.Users?.Add(newuser);
        }
        catch
        {
            return false;
        }

        _context.SaveChanges();
        return true;
    }

    public bool Delete(int id)
    {
        var user = _context.Users.SingleOrDefault(u => u.Id == id);
        if (user == null) return false;
        var flag = _context.Users.Remove(user);
        _context.SaveChanges();
        return true;
    }

    public bool Update(User user)
    {
        var new_user = user.ToUserModel();
        var _user = _context.Users.SingleOrDefault(u => u.Id == user.Id);
        if (_user == null) return false;
        _user = new_user;
        _context.SaveChanges();
        return true;
    }

    public void Save()
    {
        _context.SaveChanges();
    }

    public IEnumerable<User> GetAll()
    {
        var _users = _context.Users.ToList();
        var users = _users.Select(x => x.ToDomain()).ToList();
        return users;
    }
}