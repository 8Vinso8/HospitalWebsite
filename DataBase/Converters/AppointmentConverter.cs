using DataBase.Models;
using domain.Models;

namespace Database.Converters;

public static class AppointmentConverter
{
  public static AppointmentModel ToModel(this Appointment model)
  {
    return new AppointmentModel
    {
      Id = model.Id,
      StartTime = model.StartTime,
      EndTime = model.EndTime,
      PatientId = model.PatientId,
      DoctorId = model.DoctorId
    };
  }

  public static Appointment ToDomain(this AppointmentModel model)
  {
    return new Appointment
    {
      Id = model.Id,
      StartTime = model.StartTime,
      EndTime = model.EndTime,
      PatientId = model.PatientId,
      DoctorId = model.DoctorId
    };
  }
}