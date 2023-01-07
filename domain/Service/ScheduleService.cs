namespace domain.Service;

using Logic;
using Models;

public class ScheduleService
{
  private readonly IScheduleRepository _db;

  public ScheduleService(IScheduleRepository db)
  {
    _db = db;
  }

  public Result<IEnumerable<Schedule>> GetSchedule(Doctor doctor)
  {
    var result = doctor.IsValid();
    if (result.IsFailure)
      return Result.Fail<IEnumerable<Schedule>>("Invalid doctor: " + result.Error);

    return Result.Ok(_db.GetSchedule(doctor));
  }

  public Result<Schedule> GetSchedule(int id)
  {
    return Result.Ok(_db.GetItem(id));
  }

  public Result CreateSchedule(Schedule schedule)
  {
    if (schedule.DoctorId < 0)
      return Result.Fail("Invalid doctor");

    var result1 = schedule.IsValid();
    if (result1.IsFailure)
      return Result.Fail("Invalid schedule: " + result1.Error);

    if (!_db.Create(schedule))
      return Result.Fail<Schedule>("Cant create schedule");
    _db.Save();
    return Result.Ok();
  }

  public Result UpdateSchedule(Schedule schedule)
  {
    if (schedule.DoctorId < 0)
      return Result.Fail("Invalid doctor");

    var check = schedule.IsValid();
    if (check.IsFailure)
      return Result.Fail("Invalid schedule: " + check.Error);

    if (!_db.Update(schedule))
      return Result.Fail("Cant update schedule");
    _db.Save();
    return Result.Ok();
  }
}