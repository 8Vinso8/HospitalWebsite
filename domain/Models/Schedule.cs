namespace domain.Models;

using Logic;

public class Schedule
{
  public int Id;
  public int DoctorId;
  public DateTime StartTime;
  public DateTime EndTime;

  public Schedule()
  {
  }

  public Schedule(int id, int doctorId, DateTime startTime, DateTime endTime)
  {
    DoctorId = doctorId;
    StartTime = startTime;
    EndTime = endTime;
  }

  public Result IsValid()
  {
    if (DoctorId < 0)
      return Result.Fail("Invalid doctor id");
    if (EndTime <= StartTime)
      return Result.Fail("Invalid time");
    if (Id < 0)
      return Result.Fail("Invalid id");
    return Result.Ok();
  }
}