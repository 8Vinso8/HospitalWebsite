using DataBase.Models;
using domain.Models;

namespace Database.Converters;

public static class DoctorConverter
{
  public static DoctorModel ToModel(this Doctor model)
  {
    return new DoctorModel
    {
      Id = model.Id,
      FullName = model.FullName,
      Specialization = model.Specialization.ToModel()
    };
  }

  public static Doctor ToDomain(this DoctorModel model)
  {
    return new Doctor(model.Id, model.FullName, model.Specialization.ToDomain());
  }
}