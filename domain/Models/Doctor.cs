namespace domain.Models;

using Logic;

public class Doctor
{
  public int Id { get; set; }
  public string FullName { get; set; }
  public Specialization Specialization { get; set; }

  public Doctor(int id, string fullname, Specialization specialization)
  {
    Id = id;
    FullName = fullname;
    Specialization = specialization;
  }

  public Result IsValid()
  {
    if (Id < 0)
      return Result.Fail("Invalid id");
    if (string.IsNullOrEmpty(FullName))
      return Result.Fail("Invalid fullname");
    var check = Specialization.IsValid();
    if (check.IsFailure)
      return Result.Fail("Invalid specialization: " + check.Error);
    return Result.Ok();
  }
}