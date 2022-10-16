namespace domain.Models;

using Logic;

public class Appointment
{
  public int DoctorId;
  public int PatientId;
  public DateTime StartTime;
  public DateTime EndTime;

  public Appointment(int doctorId, int patientId, DateTime startTime, DateTime endTime)
  {
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
    return Result.Ok();
  }
}
