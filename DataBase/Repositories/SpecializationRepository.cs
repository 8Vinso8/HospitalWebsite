using Database.Converters;
using domain.Logic;
using domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DataBase.Repositories;

public class SpecializationRepository : ISpecializationRepository
{
  private readonly ApplicationContext _context;

  public SpecializationRepository(ApplicationContext context)
  {
    _context = context;
  }

  public IEnumerable<Specialization> GetAll()
  {
    return _context.Specializations.ToList().Select(x => x.ToDomain()).ToList();
  }

  public Specialization? GetItem(int id)
  {
    return _context.Specializations.FirstOrDefault(s => s.Id == id)?.ToDomain();
  }

  public Specialization? GetItem(string name)
  {
    return _context.Specializations.FirstOrDefault(s => s.Name == name)?.ToDomain();
  }

  public bool IsSpecializationExists(string name)
  {
    return _context.Specializations.FirstOrDefault(u => u.Name == name) != null;
  }

  public bool IsSpecializationExists(int id)
  {
    return _context.Specializations.FirstOrDefault(u => u.Id == id) != null;
  }

  public bool IsSpecializationExists(Specialization specialization)
  {
    return _context.Specializations.FirstOrDefault(u => u.Id == specialization.Id) != null;
  }

  public bool Create(Specialization item)
  {
    try
    {
      _context.Specializations.Add(item.ToModel());
    }
    catch (Exception)
    {
      return false;
    }

    return true;
  }

  public bool Update(Specialization item)
  {
    try
    {
      _context.Specializations.Update(item.ToModel());
    }
    catch (Exception)
    {
      return false;
    }

    return true;
  }

  public bool Delete(int id)
  {
    var specialization = GetItem(id);
    if (specialization == default)
      return false;
    _context.Specializations.Remove(specialization.ToModel());
    return true;
  }

  public void Save()
  {
    _context.SaveChanges();
  }
}