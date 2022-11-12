using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DataBase;

public class ApplicationContextFactory : IDesignTimeDbContextFactory<ApplicationContext>
{
  public ApplicationContext CreateDbContext(string[] args)
  {
    var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
    optionsBuilder.UseNpgsql(
      $"Host=localhost;Port=5432;Database=db;Username=postgres;Password=9564");

    return new ApplicationContext(optionsBuilder.Options);
  }
}