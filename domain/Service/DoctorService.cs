namespace domain.Service;

using Logic;
using Models;

public class DoctorService
{
  private readonly IDoctorRepository _db;

  public DoctorService(IDoctorRepository db)
  {
    _db = db;
  }

  public Result<Doctor> AddDoctor(Doctor doctor)
  {
    var check = doctor.IsValid();
    if (check.IsFailure)
      return Result.Fail<Doctor>("Invalid doctor: " + check.Error);
    return _db.AddDoctor(doctor) ? Result.Ok(doctor) : Result.Fail<Doctor>("Cant add doctor");
  }

  public Result<Doctor> RemoveDoctor(int id, IAppointmentRepository repository)
  {
    var apps = repository.GetAppointments(id);
    if (apps.Any())
      return Result.Fail<Doctor>("Doctor has appointments");
    var check = GetDoctor(id);
    if (check.IsFailure)
      return Result.Fail<Doctor>(check.Error);
    return _db.RemoveDoctor(id) ? check : Result.Fail<Doctor>("Cant remove doctor");
  }

  public Result<IEnumerable<Doctor>> GetAllDoctors()
  {
    return Result.Ok(_db.GetAllDoctors());
  }
  public Result<Doctor> GetDoctor(int id)
  {
    if (id < 0)
      return Result.Fail<Doctor>("Invalid id");
    var doctor = _db.GetDoctor(id);
    return doctor == null ? Result.Fail<Doctor>("Cant get doctor") : Result.Ok(doctor);
  }

  public Result<IEnumerable<Doctor>> GetDoctor(Specialization specialization)
  {
    var check = specialization.IsValid();
    if (check.IsFailure)
      return Result.Fail<IEnumerable<Doctor>>("Invalid specialization: " + check.Error);
    var doctors = _db.GetDoctor(specialization);
    return Result.Ok(doctors);
  }
}
