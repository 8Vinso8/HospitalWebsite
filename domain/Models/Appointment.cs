namespace domain.Models;

using Logic;

public class Appointment
{
  public int DoctorId;
  public int PatientId;
  public DateTime StartTime;
  public DateTime EndTime;
  public int Id;

  public Appointment()
  {
  }

  public Appointment(int id, int doctorId, int patientId, DateTime startTime, DateTime endTime)
  {
    Id = id;
    DoctorId = doctorId;
    PatientId = patientId;
    StartTime = startTime;
    EndTime = endTime;
  }

  public Result IsValid()
  {
    if (DoctorId < 0)
      return Result.Fail("Invalid doctor id");
    if (PatientId < 0)
      return Result.Fail("Invalid patient id");
    if (EndTime <= StartTime)
      return Result.Fail("Invalid time");
    if (Id < 0)
      return Result.Fail("Invalid Id");
    return Result.Ok();
  }
}