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

  public Result CreateSchedule(Doctor doctor, Schedule schedule)
  {
    var result = doctor.IsValid();
    if (result.IsFailure)
      return Result.Fail("Invalid doctor: " + result.Error);

    var result1 = schedule.IsValid();
    if (result1.IsFailure)
      return Result.Fail("Invalid schedule: " + result1.Error);

    return _db.CreateSchedule(doctor, schedule) ? Result.Ok() : Result.Fail<Schedule>("Cant create schedule");
  }

  public Result UpdateSchedule(Doctor doctor, Schedule schedule)
  {
    var check = doctor.IsValid();
    if (check.IsFailure)
      return Result.Fail("Invalid doctor: " + check.Error);

    check = schedule.IsValid();
    if (check.IsFailure)
      return Result.Fail("Invalid schedule: " + check.Error);

    return _db.UpdateSchedule(doctor, schedule)
      ? Result.Ok()
      : Result.Fail("Cant update schedule");
  }
}
