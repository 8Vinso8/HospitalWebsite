namespace domain.Models;

using Logic;

public class Schedule
{
  public int DoctorId;
  public DateTime StartTime;
  public DateTime EndTime;

  public Schedule(int doctorId, DateTime startTime, DateTime endTime)
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
    return Result.Ok();
  }
}
