using Database.Models;

namespace DataBase;

using Models;
using Microsoft.EntityFrameworkCore;

public class ApplicationContext : DbContext
{
  public DbSet<UserModel>? Users { get; set; }
  public DbSet<AppointmentModel>? Appointments { get; set; }
  public DbSet<DoctorModel>? Doctors { get; set; }
  public DbSet<ScheduleModel>? Schedules { get; set; }
  public DbSet<SpecializationModel>? Specializations { get; set; }

  public ApplicationContext(DbContextOptions options) : base(options)
  {
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);
    modelBuilder.Entity<UserModel>().HasIndex(model => model.Username);
  }
}