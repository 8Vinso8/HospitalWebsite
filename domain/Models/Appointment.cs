namespace domain.Models;

using Logic;

public class Appointment
{
  public int DoctorId { get; set; }
  public int PatientId { get; set; }
  public DateTime StartTime { get; set; }
  public DateTime EndTime { get; set; }
  public int Id { get; set; }

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
      return Result.Fail("Invalid id");
    return Result.Ok();
  }
}