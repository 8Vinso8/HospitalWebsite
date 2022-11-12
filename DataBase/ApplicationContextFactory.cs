﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DataBase;

public class ApplicationContextFactory : IDesignTimeDbContextFactory<ApplicationContext>
{
  public ApplicationContext CreateDbContext(string[] args)
  {
    var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
    optionsBuilder.UseNpgsql(
      $"Host=localhost;Port=5432;Database=db;Username=user;Password=password");

    return new ApplicationContext(optionsBuilder.Options);
  }
}