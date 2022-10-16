using domain;

namespace UnitTests
{
  public class TestUserService
  {
    private readonly UserService _userService;
    private readonly Mock<IUserRepository> _userRepositoryMock;

    public TestUserService()
    {
      _userRepositoryMock = new Mock<IUserRepository>();
      _userService = new UserService(_userRepositoryMock.Object);
    }

    [Fact] public void UsernameIsEmptyOrNull_ShouldFail()
    {
      var res = _userService.GetUserByUsername(string.Empty);

      Assert.True(res.IsFailure);
      Assert.Equal("Invalid username", res.Error);
    }

    [Fact] public void UserNotFound_ShouldFail()
    {
      _userRepositoryMock.Setup(repository => repository
        .GetUserByUsername(It.IsAny<string>())).Returns(() => null);

      var res = _userService.GetUserByUsername("0");

      Assert.True(res.IsFailure);
      Assert.Equal("Cant find user", res.Error);
    }

    [Fact] public void IsUserExists_ShouldFail()
    {
      var res = _userService.IsUserExists(string.Empty);

      Assert.True(res.IsFailure);
      Assert.Equal("Invalid username", res.Error);
    }

    [Fact] public void IsUserExists_NotFound_ShouldFail()
    {
      _userRepositoryMock.Setup(repository => repository
        .IsUserExists(It.IsAny<string>())).Returns(() => false);

      var res = _userService.IsUserExists("0");

      Assert.True(res.IsFailure);
      Assert.Equal("Cant find user", res.Error);
    }

    [Fact] public void Register_Empty_ShouldFail()
    {
      var res = _userService.Register(new User());

      Assert.True(res.IsFailure);
      Assert.Equal("Invalid username", res.Error);
    }

    [Fact] public void Register_AlreadyExists_ShouldFail()
    {
      _userRepositoryMock.Setup(repository => repository
        .IsUserExists(It.IsAny<string>())).Returns(() => true);

      var res = _userService.Register(new User(0, "0", "0", Role.Patient, "0", "0"));

      Assert.True(res.IsFailure);
      Assert.Equal("Username is already taken", res.Error);
    }

    [Fact] public void Register_Error_ShouldFail()
    {
      _userRepositoryMock.Setup(repository => repository
        .IsUserExists(It.IsAny<string>())).Returns(() => false);

      _userRepositoryMock.Setup(repository => repository
        .CreateUser(It.IsAny<User>())).Returns(() => false);

      var res = _userService.Register(new User(0, "0", "0", Role.Patient, "0", "0"));

      Assert.True(res.IsFailure);
      Assert.Equal("Cant create user", res.Error);
    }
  }
}
