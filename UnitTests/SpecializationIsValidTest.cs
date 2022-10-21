namespace UnitTests;

public class SpecializationIsValidTest
{
  [Fact] public void Negative_Id_Returns_Error()
  {
    var specialization = new Specialization(-1, "0");
    var check = specialization.IsValid();

    Assert.True(check.IsFailure);
    Assert.Equal("Invalid id", check.Error);
  }

  [Fact] public void Empty_Name_Returns_Error()
  {
    var specialization = new Specialization(0, "");
    var check = specialization.IsValid();

    Assert.True(check.IsFailure);
    Assert.Equal("Invalid name", check.Error);
  }

  [Fact] public void Valid_Specialization_Returns_Success()
  {
    var specialization = new Specialization(0, "0");
    var check = specialization.IsValid();
    
    Assert.True(check.Success);
  }
}
