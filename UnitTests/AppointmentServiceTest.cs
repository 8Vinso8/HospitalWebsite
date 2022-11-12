namespace UnitTests;

using System;
using System.Collections.Generic;

public class AppointmentServiceTests
{
  private readonly AppointmentService _appointmentService;
  private readonly Mock<IAppointmentRepository> _appointmentRepositoryMock;

  public AppointmentServiceTests()
  {
    _appointmentRepositoryMock = new Mock<IAppointmentRepository>();
    _appointmentService = new AppointmentService(_appointmentRepositoryMock.Object);
  }

  [Fact]
  public void Creating_Invalid_Appointment_Returns_Error()
  {
    var appointment = new Appointment(-1, -2, -2, DateTime.MinValue, DateTime.MinValue.AddMinutes(1));
    var schedule = new Schedule(0, 0, DateTime.MinValue, DateTime.MaxValue);
    var check = _appointmentService.CreateAppointment(appointment, schedule);

    Assert.True(check.IsFailure);
    Assert.Contains("Invalid appointment: ", check.Error);
  }

  [Fact]
  public void Creating_Appointment_With_Invalid_Schedule_Returns_Error()
  {
    var appointment = new Appointment(0, 0, 0, DateTime.MinValue, DateTime.MaxValue);
    var schedule = new Schedule(-1, -1, DateTime.MinValue, DateTime.MinValue);
    var check = _appointmentService.CreateAppointment(appointment, schedule);

    Assert.True(check.IsFailure);
    Assert.Contains("Invalid schedule: ", check.Error);
  }

  [Fact]
  public void Creating_Appointment_Out_Of_Schedule_Returns_Error()
  {
    var appointment = new Appointment(0, 0, 0, DateTime.MinValue.AddMinutes(10), DateTime.MaxValue);
    var schedule = new Schedule(0, 0, DateTime.MinValue, DateTime.MinValue.AddMinutes(1));
    var res = _appointmentService.CreateAppointment(appointment, schedule);

    Assert.True(res.IsFailure);
    Assert.Equal("Appointment out of schedule", res.Error);
  }

  [Fact]
  public void Creating_Appointment_With_Occupied_Time_Returns_Error()
  {
    List<Appointment> appointments = new()
    {
      new Appointment(0, 0, 0, DateTime.MinValue, DateTime.MaxValue)
    };
    _appointmentRepositoryMock.Setup(x => x.GetAppointments(It.IsAny<int>())).Returns(() => appointments);

    var appointment = new Appointment(1, 0, 0, DateTime.MinValue, DateTime.MaxValue);
    var schedule = new Schedule(0, 0, DateTime.MinValue, DateTime.MaxValue);
    var check = _appointmentService.CreateAppointment(appointment, schedule);

    Assert.True(check.IsFailure);
    Assert.Equal("Time is occupied", check.Error);
  }

  [Fact]
  public void Error_When_Cant_Create_Appointment()
  {
    List<Appointment> appointments = new();
    _appointmentRepositoryMock.Setup(x => x.GetAppointments(It.IsAny<int>())).Returns(() => appointments);
    _appointmentRepositoryMock.Setup(x => x.Create(It.IsAny<Appointment>())).Returns(() => false);

    var appointment = new Appointment(0, 0, 0, DateTime.MinValue, DateTime.MaxValue);
    var schedule = new Schedule(0, 0, DateTime.MinValue, DateTime.MaxValue);
    var check = _appointmentService.CreateAppointment(appointment, schedule);

    Assert.True(check.IsFailure);
    Assert.Equal("Cant create appointment", check.Error);
  }

  [Fact]
  public void Create_Valid_Appointment_Success()
  {
    List<Appointment> appointments = new();
    _appointmentRepositoryMock.Setup(x => x.GetAppointments(It.IsAny<int>())).Returns(() => appointments);
    _appointmentRepositoryMock.Setup(x => x.Create(It.IsAny<Appointment>())).Returns(() => true);

    var appointment = new Appointment(0, 0, 0, DateTime.MinValue, DateTime.MaxValue);
    var schedule = new Schedule(0, 0, DateTime.MinValue, DateTime.MaxValue);
    var check = _appointmentService.CreateAppointment(appointment, schedule);

    Assert.True(check.Success);
  }

  [Fact]
  public void GetFreeAppointments_Invalid_Specialization_Returns_Error()
  {
    var specialization = new Specialization(0, "");
    var check = _appointmentService.GetFreeAppointments(specialization);

    Assert.True(check.IsFailure);
    Assert.Contains("Invalid specialization: ", check.Error);
  }

  [Fact]
  public void GetFreeAppointments_Specialization_Valid_Success()
  {
    List<List<DateTime>> appointments = new()
    {
      new List<DateTime>()
    };
    IEnumerable<IEnumerable<DateTime>> a = appointments;
    _appointmentRepositoryMock.Setup(repository => repository.GetFreeAppointments(It.IsAny<Specialization>()))
      .Returns(() => a);

    var specialization = new Specialization(0, "a");
    var check = _appointmentService.GetFreeAppointments(specialization);

    Assert.True(check.Success);
  }

  [Fact]
  public void GetAppointments_Id_Valid_Success()
  {
    List<Appointment> appointments = new()
    {
      new Appointment(0, 0, 0, DateTime.MinValue, DateTime.MaxValue)
    };
    IEnumerable<Appointment> a = appointments;
    _appointmentRepositoryMock.Setup(repository => repository.GetAppointments(It.IsAny<int>())).Returns(() => a);

    var specialization = new Specialization(0, "a");
    var check = _appointmentService.GetAppointments(0);

    Assert.True(check.Success);
  }

  [Fact]
  public void GetAppointments_Invalid_Id_Returns_Error()
  {
    var check = _appointmentService.GetAppointments(-1);

    Assert.True(check.IsFailure);
    Assert.Contains("Invalid id", check.Error);
  }

  [Fact]
  public void GetFreeAppointments_Id_Valid_Success()
  {
    List<List<DateTime>> appointments = new()
    {
      new List<DateTime>()
    };
    IEnumerable<IEnumerable<DateTime>> a = appointments;
    _appointmentRepositoryMock.Setup(repository => repository.GetFreeAppointments(It.IsAny<int>()))
      .Returns(() => a);
    var check = _appointmentService.GetFreeAppointments(0);

    Assert.True(check.Success);
  }

  [Fact]
  public void GetFreeAppointments_Id_Invalid_Failure()
  {
    var check = _appointmentService.GetFreeAppointments(-1);

    Assert.True(check.IsFailure);
  }
}