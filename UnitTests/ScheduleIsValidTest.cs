namespace UnitTests;

using System;

public class ScheduleIsValidTest
{
  [Fact] public void Negative_Doctor_Id_Returns_Error()
  {
    var schedule = new Schedule(-1, DateTime.MinValue, DateTime.MaxValue);
    var check = schedule.IsValid();

    Assert.True(check.IsFailure);
    Assert.Equal("Invalid doctor id", check.Error);
  }

  [Fact] public void EndTime_Less_Then_StartTime_Returns_Error()
  {
    var schedule = new Schedule(0, DateTime.MaxValue, DateTime.MinValue);
    var check = schedule.IsValid();

    Assert.True(check.IsFailure);
    Assert.Equal("Invalid time", check.Error);
  }

  [Fact] public void Valid_Schedule_Returns_Success()
  {
    var schedule = new Schedule(0, DateTime.MinValue, DateTime.MaxValue);
    var check = schedule.IsValid();

    Assert.True(check.Success);
  }
}
