using Database.Converters;
using DataBase.Models;
using domain.Logic;
using domain.Models;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace DataBase.Repositories;

public class AppointmentRepository : IAppointmentRepository
{
  private readonly ApplicationContext _context;

  public AppointmentRepository(ApplicationContext context)
  {
    _context = context;
  }

  public IEnumerable<Appointment> GetAll()
  {
    return _context.Appointments.Select(a => a.ToDomain());
  }

  public Appointment? GetItem(int id)
  {
    return _context.Appointments.FirstOrDefault(a => a.Id == id)?.ToDomain();
  }

  public bool Create(Appointment item)
  {
    try
    {
      _context.Appointments.Add(item.ToModel());
    }
    catch (Exception)
    {
      return false;
    }

    return true;
  }

  public bool Update(Appointment item)
  {
    try
    {
      _context.Appointments.Update(item.ToModel());
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

    _context.Appointments.Remove(app.ToModel());
    return true;
  }

  public void Save()
  {
    _context.SaveChanges();
  }

  public IEnumerable<Appointment> GetAppointments(int doctorId)
  {
    return _context.Appointments.Where(a => a.DoctorId == doctorId).Select(a => a.ToDomain());
  }

  public IEnumerable<Appointment> GetFreeAppointments(int doctorId, DateOnly date)
  {
    var allApps =
      _context.Appointments.Where(a => a.DoctorId == doctorId && DateOnly.FromDateTime(a.StartTime.Date) == date);
    var sch = _context.Schedules.FirstOrDefault(sch => sch.DoctorId == doctorId);
    IQueryable<AppointmentModel> freeApps = Enumerable.Empty<AppointmentModel>().AsQueryable();
    TimeOnly cur = TimeOnly.FromDateTime(sch.StartTime);
    while (cur < TimeOnly.FromDateTime(sch.EndTime))
    {
      if (!allApps.Any(a => a.StartTime == date.ToDateTime(cur)))
        freeApps.Append(new AppointmentModel()
          { DoctorId = doctorId, StartTime = date.ToDateTime(cur), EndTime = date.ToDateTime(cur.AddMinutes(30)) });
      cur = cur.AddMinutes(30);
    }

    var result = freeApps.Select(a => a.ToDomain());
    return result.ToList();
  }

  public IEnumerable<Appointment> GetFreeAppointments(Specialization specialization, DateOnly date)
  {
    var doctors = _context.Doctors.Where(d => d.Specialization.Id == specialization.Id);
    IQueryable<Appointment> freeApps = Enumerable.Empty<Appointment>().AsQueryable();
    foreach (var doctor in doctors)
    {
      GetFreeAppointments(doctor.Id, date).ToList().ForEach(a => freeApps.Append(a));
    }
    return freeApps;
  }
}