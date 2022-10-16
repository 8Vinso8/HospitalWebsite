namespace UnitTests
{

  public class TestUserIsValid
  {
    [Fact] public void UsernameError()
    {
      var user = new User(1, "0", "0", Role.Administrator, "", "0");
      var check = user.IsValid();

      Assert.True(check.IsFailure);
      Assert.Equal("Invalid username", check.Error);
    }

    [Fact] public void PassError()
    {
      var user = new User(2, "0", "0", Role.Administrator, "0", "");
      var check = user.IsValid();

      Assert.True(check.IsFailure);
      Assert.Equal("Invalid password", check.Error);
    }

    [Fact] public void PhoneNumberError()
    {
      var user = new User(3, "", "0", Role.Administrator, "0", "0");
      var check = user.IsValid();

      Assert.True(check.IsFailure);
      Assert.Equal("Invalid phone number", check.Error);
    }

    [Fact] public void FullnameError()
    {
      var user = new User(4, "0", "", Role.Administrator, "0", "0");
      var check = user.IsValid();

      Assert.True(check.IsFailure);
      Assert.Equal("Invalid full name", check.Error);
    }
  }
}
