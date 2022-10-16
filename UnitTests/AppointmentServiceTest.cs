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

  [Fact] public void Creating_Invalid_Appointment_Returns_Error()
  {
    var appointment = new Appointment(-2, -2, DateTime.MinValue, DateTime.MinValue.AddMinutes(1));
    var schedule = new Schedule(0, DateTime.MinValue, DateTime.MaxValue);
    var check = _appointmentService.CreateAppointment(appointment, schedule);

    Assert.True(check.IsFailure);
    Assert.Contains("Invalid appointment: ", check.Error);
  }

  [Fact] public void Creating_Appointment_With_Invalid_Schedule_Returns_Error()
  {
    var appointment = new Appointment(0, 0, DateTime.MinValue, DateTime.MaxValue);
    var schedule = new Schedule(-1, DateTime.MinValue, DateTime.MinValue);
    var check = _appointmentService.CreateAppointment(appointment, schedule);

    Assert.True(check.IsFailure);
    Assert.Contains("Invalid schedule: ", check.Error);
  }

  [Fact] public void Creating_Appointment_Out_Of_Schedule_Returns_Error()
  {
    var appointment = new Appointment(0, 0, DateTime.MinValue.AddMinutes(10), DateTime.MaxValue);
    var schedule = new Schedule(0, DateTime.MinValue, DateTime.MinValue.AddMinutes(1));
    var res = _appointmentService.CreateAppointment(appointment, schedule);

    Assert.True(res.IsFailure);
    Assert.Equal("Appointment out of schedule", res.Error);
  }

  [Fact] public void Creating_Appointment_With_Occupied_Time_Returns_Error()
  {
    List<Appointment> appointments = new()
    {
      new Appointment(0, 0, DateTime.MinValue, DateTime.MaxValue)
    };
    _appointmentRepositoryMock.Setup(x => x.GetAppointments(It.IsAny<int>())).Returns(() => appointments);

    var appointment = new Appointment(0, 0, DateTime.MinValue, DateTime.MaxValue);
    var schedule = new Schedule(0, DateTime.MinValue, DateTime.MaxValue);
    var check = _appointmentService.CreateAppointment(appointment, schedule);

    Assert.True(check.IsFailure);
    Assert.Equal("Time is occupied", check.Error);
  }

  [Fact] public void Error_When_Cant_Create_Appointment()
  {
    List<Appointment> appointments = new();
    _appointmentRepositoryMock.Setup(x => x.GetAppointments(It.IsAny<int>())).Returns(() => appointments);
    _appointmentRepositoryMock.Setup(x => x.CreateAppointment(It.IsAny<Appointment>())).Returns(() => false);

    var appointment = new Appointment(0, 0, DateTime.MinValue, DateTime.MaxValue);
    var schedule = new Schedule(0, DateTime.MinValue, DateTime.MaxValue);
    var check = _appointmentService.CreateAppointment(appointment, schedule);

    Assert.True(check.IsFailure);
    Assert.Equal("Cant create appointment", check.Error);
  }

  [Fact] public void Create_Valid_Appointment_Success()
  {
    List<Appointment> appointments = new();
    _appointmentRepositoryMock.Setup(x => x.GetAppointments(It.IsAny<int>())).Returns(() => appointments);
    _appointmentRepositoryMock.Setup(x => x.CreateAppointment(It.IsAny<Appointment>())).Returns(() => true);

    var appointment = new Appointment(0, 0, DateTime.MinValue, DateTime.MaxValue);
    var schedule = new Schedule(0, DateTime.MinValue, DateTime.MaxValue);
    var check = _appointmentService.CreateAppointment(appointment, schedule);

    Assert.True(check.Success);
  }

  [Fact] public void GetAppointments_Invalid_Specialization_Returns_Error()
  {
    var specialization = new Specialization(0, "");
    var check = _appointmentService.GetAppointments(specialization);

    Assert.True(check.IsFailure);
    Assert.Contains("Invalid specialization: ", check.Error);
  }

  [Fact] public void GetAppointments_Valid_Success()
  {
    List<Appointment> appointments = new()
    {
      new Appointment(0, 0, DateTime.UnixEpoch, DateTime.MaxValue),
      new Appointment(0, 0, DateTime.MinValue, DateTime.MaxValue)
    };
    IEnumerable<Appointment> a = appointments;
    _appointmentRepositoryMock.Setup(repository => repository.GetAppointments(It.IsAny<Specialization>())).Returns(() => a);

    var specialization = new Specialization(0, "a");
    var check = _appointmentService.GetAppointments(specialization);

    Assert.True(check.Success);
  }
}
