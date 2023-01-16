namespace domain.Service;

using Logic;
using Models;

public class AppointmentService
{
  private readonly IAppointmentRepository _db;

  private readonly Dictionary<int, Mutex> _mutexes = new();

  public AppointmentService(IAppointmentRepository db)
  {
    _db = db;
  }

  public Result<Appointment> CreateAppointment(Appointment appointment, Schedule schedule)
  {
    var check_app = appointment.IsValid();
    if (check_app.IsFailure)
      return Result.Fail<Appointment>("Invalid appointment: " + check_app.Error);

    var check_sc = schedule.IsValid();
    if (check_sc.IsFailure)
      return Result.Fail<Appointment>("Invalid schedule: " + check_sc.Error);

    if (schedule.StartTime > appointment.StartTime || schedule.EndTime < appointment.EndTime)
      return Result.Fail<Appointment>("Appointment out of schedule");

    var appointments = _db.GetAppointments(appointment.DoctorId).ToList();
    if (appointments.Any(a => appointment.StartTime < a.EndTime && a.StartTime < appointment.EndTime))
      return Result.Fail<Appointment>("Time is occupied");

    if (!_mutexes.ContainsKey(appointment.DoctorId))
      _mutexes.Add(appointment.DoctorId, new Mutex());
    _mutexes.First(d => d.Key == appointment.DoctorId).Value.WaitOne();

    if (!_db.Create(appointment))
    {
      _mutexes.First(d => d.Key == appointment.DoctorId).Value.ReleaseMutex();
      return Result.Fail<Appointment>("Cant create appointment");
    }

    _db.Save();
    _mutexes.First(d => d.Key == appointment.DoctorId).Value.ReleaseMutex();
    return Result.Ok(appointment);
  }

  public Result<IEnumerable<Appointment>> GetAppointments(int doctorId)
  {
    return doctorId < 0
      ? Result.Fail<IEnumerable<Appointment>>("Invalid id")
      : Result.Ok(_db.GetAppointments(doctorId));
  }

  public Result<IEnumerable<Appointment>> GetFreeAppointments(Specialization specialization, DateOnly date)
  {
    var result = specialization.IsValid();
    if (result.IsFailure)
      return Result.Fail<IEnumerable<Appointment>>("Invalid specialization: " + result.Error);
    return Result.Ok(_db.GetFreeAppointments(specialization, date));
  }

  public Result<IEnumerable<Appointment>> GetFreeAppointments(int doctorId, DateOnly date)
  {
    if (doctorId < 0)
      return Result.Fail<IEnumerable<Appointment>>("Invalid id");
    return Result.Ok(_db.GetFreeAppointments(doctorId, date));
  }
}