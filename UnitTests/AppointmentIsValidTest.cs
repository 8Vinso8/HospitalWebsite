namespace UnitTests;

using System;

public class AppointmentIsValidTest
{
  [Fact] public void Negative_Doctor_Id_Returns_Error()
  {
    var appointment = new Appointment(-1, 0, DateTime.MinValue, DateTime.MaxValue);
    var check = appointment.IsValid();

    Assert.True(check.IsFailure);
    Assert.Equal("Invalid doctor id", check.Error);
  }
  
  [Fact] public void Negative_Patient_Id_Returns_Error()
  {
    var appointment = new Appointment(0, -1, DateTime.MinValue, DateTime.MaxValue);
    var check = appointment.IsValid();

    Assert.True(check.IsFailure);
    Assert.Equal("Invalid patient id", check.Error);
  }
  
  [Fact] public void EndTime_Less_Then_StartTime_Returns_Error()
  {
    var appointment = new Appointment(0, 0, DateTime.MaxValue, DateTime.MinValue);
    var check = appointment.IsValid();

    Assert.True(check.IsFailure);
    Assert.Equal("Invalid time", check.Error);
  }

  [Fact] public void Valid_Appointment_Returns_Success()
  {
    var appointment = new Appointment(0, 0, DateTime.MinValue, DateTime.MaxValue);
    var check = appointment.IsValid();
    
    Assert.True(check.Success);
  }
}
