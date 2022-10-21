namespace UnitTests;

using System;
using System.Collections.Generic;

public class ScheduleServiceTests
{
  private readonly ScheduleService _scheduleService;
  private readonly Mock<IScheduleRepository> _scheduleRepositoryMock;

  public ScheduleServiceTests()
  {
    _scheduleRepositoryMock = new Mock<IScheduleRepository>();
    _scheduleService = new ScheduleService(_scheduleRepositoryMock.Object);
  }

  [Fact] public void GetSchedule_Ivnalid_Doctor_Error()
  {
    var doctor = new Doctor(-1, "", new Specialization(-1, ""));
    var check = _scheduleService.GetSchedule(doctor);

    Assert.True(check.IsFailure);
    Assert.Contains("Invalid doctor: ", check.Error);
  }

  [Fact] public void GetSchedule_Valid_Success()
  {
    List<Schedule> scheds = new()
    {
      new Schedule(0, DateTime.MinValue, DateTime.MaxValue)
    };
    IEnumerable<Schedule> s = scheds;
    _scheduleRepositoryMock.Setup(rep => rep.GetSchedule(It.IsAny<Doctor>())).Returns(() => s);

    var doctor = new Doctor(0, "0", new Specialization(0, "0"));
    var result = _scheduleService.GetSchedule(doctor);

    Assert.True(result.Success);
  }

  [Fact] public void CreateSchedule_Invalid_Doctor()
  {
    var doctor = new Doctor(-1, "", new Specialization(-1, ""));
    var schedule = new Schedule(0, DateTime.MinValue, DateTime.MaxValue);
    var result = _scheduleService.CreateSchedule(doctor, schedule);

    Assert.True(result.IsFailure);
    Assert.Contains("Invalid doctor: ", result.Error);
  }

  [Fact] public void Create_Schedule_Invalid_Schedule()
  {
    var doctor = new Doctor(0, "0", new Specialization(0, "0"));
    var schedule = new Schedule(-1, DateTime.MinValue, DateTime.MinValue);
    var result = _scheduleService.CreateSchedule(doctor, schedule);

    Assert.True(result.IsFailure);
    Assert.Contains("Invalid schedule: ", result.Error);
  }

  [Fact] public void Error_When_Creating_Schedule()
  {
    _scheduleRepositoryMock.Setup(rep => rep.CreateSchedule(It.IsAny<Doctor>(), It.IsAny<Schedule>())).Returns(() => false);

    var doctor = new Doctor(0, "0", new Specialization(0, "0"));
    var schedule = new Schedule(0, DateTime.MinValue, DateTime.MaxValue);
    var result = _scheduleService.CreateSchedule(doctor, schedule);

    Assert.True(result.IsFailure);
    Assert.Equal("Cant create schedule", result.Error);
  }

  [Fact] public void Create_Valid_Schedule_Success()
  {
    _scheduleRepositoryMock.Setup(rep => rep.CreateSchedule(It.IsAny<Doctor>(), It.IsAny<Schedule>())).Returns(() => true);

    var doctor = new Doctor(0, "0", new Specialization(0, "0"));
    var schedule = new Schedule(0, DateTime.MinValue, DateTime.MaxValue);
    var result = _scheduleService.CreateSchedule(doctor, schedule);

    Assert.True(result.Success);
  }

  [Fact] public void Update_Schedule_Invalid_Doctor_Error()
  {
    var doctor = new Doctor(-1, "", new Specialization(-1, ""));
    var schedule = new Schedule(0, DateTime.MinValue, DateTime.MaxValue);
    var result = _scheduleService.UpdateSchedule(doctor, schedule);

    Assert.True(result.IsFailure);
    Assert.Contains("Invalid doctor: ", result.Error);
  }

  [Fact] public void Update_Schedule_Invalid_Schedule_Error()
  {
    var doctor = new Doctor(0, "0", new Specialization(0, "0"));
    var schedule = new Schedule(-1, DateTime.MinValue, DateTime.MinValue);
    var result = _scheduleService.UpdateSchedule(doctor, schedule);

    Assert.True(result.IsFailure);
    Assert.Contains("Invalid schedule: ", result.Error);
  }

  [Fact] public void Error_When_Updating()
  {
    _scheduleRepositoryMock.Setup(rep => rep.UpdateSchedule(It.IsAny<Doctor>(), It.IsAny<Schedule>())).Returns(() => false);

    var doctor = new Doctor(0, "0", new Specialization(0, "0"));
    var schedule = new Schedule(0, DateTime.MinValue, DateTime.MaxValue);
    var result = _scheduleService.UpdateSchedule(doctor, schedule);

    Assert.True(result.IsFailure);
    Assert.Equal("Cant update schedule", result.Error);
  }

  [Fact] public void Update_Valid_Schedule_Success()
  {
    _scheduleRepositoryMock.Setup(rep => rep.UpdateSchedule(It.IsAny<Doctor>(), It.IsAny<Schedule>())).Returns(() => true);

    var doctor = new Doctor(0, "0", new Specialization(0, "0"));
    var schedule = new Schedule(0, DateTime.MinValue, DateTime.MaxValue);
    var result = _scheduleService.UpdateSchedule(doctor, schedule);

    Assert.True(result.Success);
  }
}
