using Database.Converters;
using domain.Logic;
using domain.Models;

namespace DataBase.Repositories;

public class DoctorRepository : IDoctorRepository
{
  private readonly ApplicationContext _context;

  public DoctorRepository(ApplicationContext context)
  {
    _context = context;
  }

  public IEnumerable<Doctor> GetAll()
  {
    return _context.Doctors.Select(d => d.ToDomain());
  }

  public Doctor? GetItem(int id)
  {
    return _context.Doctors.FirstOrDefault(d => d.Id == id)?.ToDomain();
  }

  public bool Create(Doctor item)
  {
    try
    {
      _context.Doctors.Add(item.ToModel());
    }
    catch (Exception)
    {
      return false;
    }

    return true;
  }

  public bool Update(Doctor item)
  {
    try
    {
      _context.Doctors.Update(item.ToModel());
    }
    catch (Exception)
    {
      return false;
    }

    return true;
  }

  public bool Delete(int id)
  {
    var app = GetItem(id);
    if (app == default)
      return false;

    _context.Doctors.Remove(app.ToModel());
    return true;
  }

  public void Save()
  {
    _context.SaveChanges();
  }

  public IEnumerable<Doctor> GetDoctors(Specialization specialization)
  {
    return _context.Doctors.Where(d => d.Specialization == specialization.ToModel()).Select(d => d.ToDomain());
  }
}