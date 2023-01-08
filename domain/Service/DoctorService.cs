namespace domain.Service;

using Logic;
using Models;

public class DoctorService
{
  private readonly IDoctorRepository _db;
  private readonly IAppointmentRepository _apps;

  public DoctorService(IDoctorRepository db, IAppointmentRepository apps)
  {
    _db = db;
    _apps = apps;
  }

  public Result<Doctor> AddDoctor(Doctor doctor)
  {
    var check = doctor.IsValid();
    if (check.IsFailure)
      return Result.Fail<Doctor>("Invalid doctor: " + check.Error);
    if (!_db.Create(doctor))
      return Result.Fail<Doctor>("Cant create doctor");
    _db.Save();
    return Result.Ok(doctor);
  }

  public Result<Doctor> RemoveDoctor(int id)
  {
    var apps = _apps.GetAppointments(id);
    if (apps.Any())
      return Result.Fail<Doctor>("Doctor has appointments");
    var check = GetDoctor(id);
    if (check.IsFailure)
      return Result.Fail<Doctor>(check.Error);
    return _db.Delete(id) ? check : Result.Fail<Doctor>("Cant remove doctor");
  }

  public Result<IEnumerable<Doctor>> GetAllDoctors()
  {
    return Result.Ok(_db.GetAll());
  }

  public Result<Doctor> GetDoctor(int id)
  {
    if (id < 0)
      return Result.Fail<Doctor>("Invalid id");
    var doctor = _db.GetItem(id);
    return doctor == null ? Result.Fail<Doctor>("Cant get doctor") : Result.Ok(doctor);
  }

  public Result<IEnumerable<Doctor>> GetDoctor(Specialization specialization)
  {
    var check = specialization.IsValid();
    if (check.IsFailure)
      return Result.Fail<IEnumerable<Doctor>>("Invalid specialization: " + check.Error);
    var doctors = _db.GetDoctors(specialization);
    return Result.Ok(doctors);
  }
}