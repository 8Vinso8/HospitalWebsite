using Database.Converters;
using domain.Logic;
using domain.Models;

namespace DataBase.Repositories;

public class SpecializationRepository : IRepository<Specialization>
{
  private readonly ApplicationContext _context;

  public SpecializationRepository(ApplicationContext context)
  {
    _context = context;
  }

  public IEnumerable<Specialization> GetAll()
  {
    return _context.Specializations.Select(s => s.ToDomain());
  }

  public Specialization? GetItem(int id)
  {
    return _context.Specializations.FirstOrDefault(s => s.Id == id)?.ToDomain();
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