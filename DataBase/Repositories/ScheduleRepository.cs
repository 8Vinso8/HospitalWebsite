using Database.Converters;
using domain.Logic;
using domain.Models;

namespace DataBase.Repositories;

public class ScheduleRepository : IScheduleRepository
{
  private readonly ApplicationContext _context;

  public ScheduleRepository(ApplicationContext context)
  {
    _context = context;
  }

  public IEnumerable<Schedule> GetAll()
  {
    return _context.Schedules.Select(s => s.ToDomain());
  }

  public Schedule? GetItem(int id)
  {
    return _context.Schedules.FirstOrDefault(s => s.Id == id)?.ToDomain();
  }

  public bool Create(Schedule item)
  {
    try
    {
      _context.Schedules.Add(item.ToModel());
    }
    catch (Exception)
    {
      return false;
    }

    return true;
  }

  public bool Update(Schedule item)
  {
    try
    {
      _context.Schedules.Update(item.ToModel());
    }
    catch (Exception)
    {
      return false;
    }

    return true;
  }

  public bool Delete(int id)
  {
    var schedule = GetItem(id);
    if (schedule == default)
      return false;

    _context.Schedules.Remove(schedule.ToModel());
    return true;
  }

  public void Save()
  {
    _context.SaveChanges();
  }

  public IEnumerable<Schedule> GetSchedule(Doctor doctor)
  {
    return _context.Schedules.Where(s => s.DoctorId == doctor.Id).Select(s => s.ToDomain());
  }
}