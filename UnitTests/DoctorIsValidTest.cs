namespace UnitTests;

public class DoctorIsValidTest
{
  [Fact] public void Empty_Fullname_Returns_Error()
  {
    var doctor = new Doctor(0, "", new Specialization(0, "0"));
    var check = doctor.IsValid();

    Assert.True(check.IsFailure);
    Assert.Equal("Invalid fullname", check.Error);
  }

  [Fact] public void Negative_Id_Returns_Error()
  {
    var doctor = new Doctor(-1, "a", new Specialization(0, "0"));
    var check = doctor.IsValid();

    Assert.True(check.IsFailure);
    Assert.Equal("Invalid id", check.Error);
  }

  [Fact] public void Invalid_Specialization_Returns_Error()
  {
    var doctor = new Doctor(0, "a", new Specialization(-1, ""));
    var check = doctor.IsValid();

    Assert.True(check.IsFailure);
    Assert.Contains("Invalid specialization: ", check.Error);
  }

  [Fact] public void Valid_Doctor_Returns_Success()
  {
    var doctor = new Doctor(0, "a", new Specialization(0, "0"));
    var check = doctor.IsValid();

    Assert.True(check.Success);
  }
}
