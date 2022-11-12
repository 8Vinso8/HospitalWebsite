using Database.Converters;
using DataBase.Models;
using domain.Logic;
using domain.Models;

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

  public IEnumerable<Appointment> GetFreeAppointments(int doctorId)
  {
    return _context.Appointments.Where(a => a.DoctorId == doctorId && a.PatientId == -1).Select(a => a.ToDomain());
  }

  public IEnumerable<Appointment> GetFreeAppointments(Specialization specialization)
  {
    var allAppointments = new List<AppointmentModel>();
    var doctors = _context.Doctors.Where(u => u.Specialization != null && u.Specialization.Id == specialization.Id)
      .ToList();
    foreach (var appointment in doctors.Select(doctor =>
               _context.Appointments.Where(u => u.DoctorId == doctor.Id && u.PatientId == 0).ToList()))
    {
      appointment.ForEach(p => allAppointments.Add(p));
    }

    return allAppointments.Select(x => x.ToDomain());
  }
}