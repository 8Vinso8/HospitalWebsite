namespace UnitTests;

public class TestUserIsValid
{
  [Fact] public void Empty_Username_Returns_Error()
  {
    var user = new User(1, "0", "0", Role.Administrator, "", "0");
    var check = user.IsValid();

    Assert.True(check.IsFailure);
    Assert.Equal("Invalid username", check.Error);
  }

  [Fact] public void Empty_Password_Returns_Error()
  {
    var user = new User(2, "0", "0", Role.Administrator, "0", "");
    var check = user.IsValid();

    Assert.True(check.IsFailure);
    Assert.Equal("Invalid password", check.Error);
  }

  [Fact] public void Empty_Phone_Number_Returns_Error()
  {
    var user = new User(3, "", "0", Role.Administrator, "0", "0");
    var check = user.IsValid();

    Assert.True(check.IsFailure);
    Assert.Equal("Invalid phone number", check.Error);
  }

  [Fact] public void Empty_Full_Name_Returns_Error()
  {
    var user = new User(4, "0", "", Role.Administrator, "0", "0");
    var check = user.IsValid();

    Assert.True(check.IsFailure);
    Assert.Equal("Invalid full name", check.Error);
  }

  [Fact] public void Valid_User_Returns_Success()
  {
    var user = new User(5, "0", "0", Role.Administrator, "0", "0");
    var check = user.IsValid();

    Assert.True(check.Success);
  }
}
