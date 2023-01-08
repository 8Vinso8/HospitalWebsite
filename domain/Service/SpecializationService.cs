namespace domain.Service;

using Logic;
using Models;
using System.Collections.Generic;

public class SpecializationService
{
  private readonly ISpecializationRepository _db;

  public SpecializationService(ISpecializationRepository db)
  {
    _db = db;
  }

  public Result<bool> IsSpecializationExists(string name)
  {
    return string.IsNullOrEmpty(name)
      ? Result.Fail<bool>("Invalid specialization name")
      : Result.Ok(_db.IsSpecializationExists(name));
  }

  public Result<Specialization> Create(Specialization spec)
  {
    var result = spec.IsValid();
    if (result.IsFailure)
      return Result.Fail<Specialization>("Invalid specialization: " + result.Error);

    if (_db.IsSpecializationExists(spec.Id) || _db.IsSpecializationExists(spec.Name))
      return Result.Fail<Specialization>("Specialization already exists");

    return _db.Create(spec) ? Result.Ok(spec) : Result.Fail<Specialization>("Cant create specialization");
  }

  public Result<Specialization> Delete(Specialization spec)
  {
    if (!_db.IsSpecializationExists(spec.Id))
      return Result.Fail<Specialization>("Specialization does not exist");
    return _db.Delete(spec.Id) ? Result.Ok(spec) : Result.Fail<Specialization>("Cant delete specialization");
  }

  public Result<Specialization> GetSpecialization(string name)
  {
    if (name == string.Empty)
      return Result.Fail<Specialization>("Invalid specialization name");
    var specialization = _db.GetItem(name);
    return specialization != null ? Result.Ok(specialization) : Result.Fail<Specialization>("Cant find specialization");
  }

  public Result<Specialization> GetSpecialization(int id)
  {
    if (id < 1)
      return Result.Fail<Specialization>("Invalid specialization id");
    var specialization = _db.GetItem(id);
    return specialization != null ? Result.Ok(specialization) : Result.Fail<Specialization>("Cant find specialization");
  }

  public Result<IEnumerable<Specialization>> GetAllSpecializations()
  {
    return Result.Ok(_db.GetAll());
  }


  public void Save()
  {
    _db.Save();
  }
}