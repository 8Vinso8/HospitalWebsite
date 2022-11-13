using Database.Models;
using domain.Models;

namespace Database.Converters;

public static class ScheduleConverter
{
  public static ScheduleModel ToModel(this Schedule model)
  {
    return new ScheduleModel
    {
      Id = model.Id,
      StartTime = model.StartTime,
      EndTime = model.EndTime,
      DoctorId = model.DoctorId
    };
  }

  public static Schedule ToDomain(this ScheduleModel model)
  {
    return new Schedule(model.Id, model.DoctorId, model.StartTime, model.EndTime);
  }
}