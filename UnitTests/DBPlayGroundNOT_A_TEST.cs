using System.Linq;
using DataBase;
using DataBase.Models;
using DataBase.Repositories;
using Microsoft.EntityFrameworkCore;

namespace UnitTests
{
  public class EfPlayground
  {
    private readonly DbContextOptionsBuilder<ApplicationContext> _optionsBuilder;

    public EfPlayground()
    {
      var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
      optionsBuilder.UseNpgsql(
        $"Host=localhost;Port=5432;Database=db;Username=postgres;Password=9564");
      _optionsBuilder = optionsBuilder;
    }

    // [Fact]
    public void PlaygroundMethod4()
    {
      var context = new ApplicationContext(_optionsBuilder.Options);

      var userRep = new UserRepository(context);

      userRep.Create(new User(0, "123123", "fiofio", Role.Patient, "Name", "Pass"));

      context.SaveChanges();

      Assert.True(context.Users.Any(u => u.Username == "Name"));
    }

    // [Fact]
    public void PlaygroundMethod1()
    {
      using var context = new ApplicationContext(_optionsBuilder.Options);
      context.Users.Add(new UserModel
      {
        Id = 123,
        Username = "TEST"
      });
      context.SaveChanges();

      Assert.True(context.Users.Any(u => u.Username == "TEST"));
    }

    // [Fact]
    public void PlaygroundMethod2()
    {
      using var context = new ApplicationContext(_optionsBuilder.Options);
      var u = context.Users.FirstOrDefault(u => u.Username == "Name");
      context.Users.Remove(u);
      context.SaveChanges();

      Assert.True(!context.Users.Any(u => u.Username == "Name"));
    }

    // [Fact]
    public void PlaygroundMethod3()
    {
      #region подготовили сервис

      using var context = new ApplicationContext(_optionsBuilder.Options);
      var userRepository = new UserRepository(context);
      var userService = new UserService(userRepository);

      #endregion

      var res = userService.GetUserByUsername("TEST");

      Assert.NotNull(res.Value);
    }
  }
}