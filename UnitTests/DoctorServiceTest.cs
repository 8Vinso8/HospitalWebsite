namespace UnitTests;

using System;
using System.Collections.Generic;

public class DoctorServiceTests
{
  private readonly DoctorService _doctorService;
  private readonly Mock<IDoctorRepository> _doctorRepositoryMock;

  public DoctorServiceTests()
  {
    _doctorRepositoryMock = new Mock<IDoctorRepository>();
    _doctorService = new DoctorService(_doctorRepositoryMock.Object);
  }

  [Fact] public void Add_Invalid_Doctor()
  {
    var doctor = new Doctor(-1, "", new Specialization(-1, ""));
    var check = _doctorService.AddDoctor(doctor);

    Assert.True(check.IsFailure);
    Assert.Contains("Invalid doctor: ", check.Error);
  }

  [Fact] public void AddDoctor_Error_When_Adding()
  {
    _doctorRepositoryMock.Setup(repository => repository.Create(It.IsAny<Doctor>())).Returns(() => false);
    var doctor = new Doctor(0, "0", new Specialization(0, "0"));
    var check = _doctorService.AddDoctor(doctor);

    Assert.True(check.IsFailure);
    Assert.Equal("Cant add doctor", check.Error);
  }

  [Fact] public void Add_Valid_Doctor_Success()
  {
    _doctorRepositoryMock.Setup(repository => repository.Create(It.IsAny<Doctor>())).Returns(() => true);
    var doctor = new Doctor(0, "0", new Specialization(0, "0"));
    var check = _doctorService.AddDoctor(doctor);

    Assert.True(check.Success);
  }

  [Fact] public void Delete_Not_Existing_Doctor_Error()
  {
    var _appointmentRepositoryMock = new Mock<IAppointmentRepository>();
    List<Appointment> apps = new();
    _appointmentRepositoryMock.Setup(x => x.GetAppointments(It.IsAny<int>())).Returns(apps);

    var check = _doctorService.RemoveDoctor(0, _appointmentRepositoryMock.Object);

    Assert.True(check.IsFailure);
    Assert.Equal("Cant get doctor", check.Error);
  }

  [Fact] public void Remove_Doctor_With_Appointments_Returns_Error()
  {
    var _appointmentRepositoryMock = new Mock<IAppointmentRepository>();
    List<Appointment> apps = new()
    {
      new Appointment(0, 0, DateTime.MinValue, DateTime.MaxValue)
    };
    _appointmentRepositoryMock.Setup(x => x.GetAppointments(It.IsAny<int>())).Returns(apps);

    var result = _doctorService.RemoveDoctor(0, _appointmentRepositoryMock.Object);

    Assert.True(result.IsFailure);
    Assert.Equal("Doctor has appointments", result.Error);
  }

  [Fact] public void Error_When_Removing()
  {
    var _appointmentRepositoryMock = new Mock<IAppointmentRepository>();
    List<Appointment> apps = new();
    _appointmentRepositoryMock.Setup(x => x.GetAppointments(It.IsAny<int>())).Returns(apps);
    _doctorRepositoryMock.Setup(repository => repository.GetDoctor(It.IsAny<int>())).Returns(() => new Doctor(0, "a", new Specialization(0, "a")));
    _doctorRepositoryMock.Setup(repository => repository.Delete(It.IsAny<int>())).Returns(() => false);

    var result = _doctorService.RemoveDoctor(0, _appointmentRepositoryMock.Object);

    Assert.True(result.IsFailure);
    Assert.Equal("Cant remove doctor", result.Error);
  }

  [Fact] public void Valid_Removal_Success()
  {
    var _appointmentRepositoryMock = new Mock<IAppointmentRepository>();
    List<Appointment> apps = new();
    _appointmentRepositoryMock.Setup(x => x.GetAppointments(It.IsAny<int>())).Returns(apps);
    _doctorRepositoryMock.Setup(repository => repository.GetDoctor(It.IsAny<int>())).Returns(() => new Doctor(0, "0", new Specialization(0, "0")));
    _doctorRepositoryMock.Setup(repository => repository.Delete(It.IsAny<int>())).Returns(() => true);

    var result = _doctorService.RemoveDoctor(0, _appointmentRepositoryMock.Object);

    Assert.True(result.Success);
  }

  [Fact] public void GetAllDoctors_Success()
  {
    List<Doctor> doctors = new()
    {
      new Doctor(0, "0", new Specialization(0, "0")),
      new Doctor(1, "00", new Specialization(0, "0"))
    };
    IEnumerable<Doctor> d = doctors;
    _doctorRepositoryMock.Setup(repository => repository.GetAllDoctors()).Returns(() => d);

    var result = _doctorService.GetAllDoctors();

    Assert.True(result.Success);
  }

  [Fact] public void GetDoctor_Invalid_Id_Error()
  {
    var result = _doctorService.GetDoctor(-1);

    Assert.True(result.IsFailure);
    Assert.Equal("Invalid id", result.Error);
  }

  [Fact] public void GetDoctor_Not_Found()
  {
    _doctorRepositoryMock.Setup(repository => repository.GetDoctor(It.IsAny<int>())).Returns(() => null);

    var result = _doctorService.GetDoctor(0);

    Assert.True(result.IsFailure);
    Assert.Equal("Cant get doctor", result.Error);
  }

  [Fact] public void Get_Doctor_By_Id_Success()
  {
    _doctorRepositoryMock.Setup(repository => repository.GetDoctor(It.IsAny<int>())).Returns(() => new Doctor(0, "0", new Specialization(0, "0")));

    var result = _doctorService.GetDoctor(0);

    Assert.True(result.Success);
  }

  [Fact] public void GetDoctor_Specialization_Invalid_Error()
  {
    var result = _doctorService.GetDoctor(new Specialization(-1, ""));

    Assert.True(result.IsFailure);
    Assert.Contains("Invalid specialization: ", result.Error);
  }
  
  [Fact] public void GetDoctor_Specialization_Success()
  {
    List<Doctor> doctors = new()
    {
      new Doctor(0, "0", new Specialization(0, "0"))
    };
    _doctorRepositoryMock.Setup(repository => repository.GetDoctor(It.IsAny<Specialization>())).Returns(() => doctors);

    var result = _doctorService.GetDoctor(new Specialization(0, "0"));

    Assert.True(result.Success);
  }

}
