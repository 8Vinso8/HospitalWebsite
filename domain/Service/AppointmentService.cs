namespace domain.Service;

using Logic;
using Models;

public class AppointmentService
{
  private readonly IAppointmentRepository _db;

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
    appointments.Sort((a, b) => a.StartTime < b.StartTime ? -1 : 1);
    var index = appointments.FindLastIndex(a => a.EndTime <= appointment.StartTime);
    if (appointments.Count > index + 1 && appointments[index + 1].StartTime < appointment.EndTime)
      return Result.Fail<Appointment>("Time is occupied");

    return _db.CreateAppointment(appointment)
      ? Result.Ok(appointment)
      : Result.Fail<Appointment>("Cant create appointment");
  }

  public Result<IEnumerable<Appointment>> GetAppointments(Specialization specialization)
  {
    var result = specialization.IsValid();
    if (result.IsFailure)
      return Result.Fail<IEnumerable<Appointment>>("Invalid specialization: " + result.Error);

    return Result.Ok(_db.GetAppointments(specialization));
  }

  public Result<IEnumerable<Appointment>> GetAppointments(int id)
  {
    if (id < 0)
      return Result.Fail<IEnumerable<Appointment>>("Invalid id");
    return Result.Ok(_db.GetAppointments(id));
  }
}
