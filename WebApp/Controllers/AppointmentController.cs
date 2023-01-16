using domain.Models;
using domain.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[ApiController]
[Route("appointment")]
public class AppointmentController : ControllerBase
{
  private readonly AppointmentService _service;
  private readonly ScheduleService _serviceSchedule;

  public AppointmentController(AppointmentService service, ScheduleService scheduleService)
  {
    _service = service;
    _serviceSchedule = scheduleService;
  }

  [Authorize]
  [HttpPost("add")]
  public IActionResult SaveAppointment(int patientId, int doctorId, DateTime startTime, DateTime endTime,
    int scheduleId)
  {
    Appointment appointment = new(0, doctorId, patientId, startTime, endTime);
    var schedule = _serviceSchedule.GetSchedule(scheduleId);
    if (schedule.IsFailure)
      return Problem(statusCode: 404, detail: schedule.Error);

    var res = _service.CreateAppointment(appointment, schedule.Value);

    return res.IsFailure ? Problem(statusCode: 404, detail: res.Error) : Ok(res.Value);
  }

  [HttpGet("get/occupied")]
  public IActionResult GetExistingAppointments(int doctorId)
  {
    var res = _service.GetAppointments(doctorId);
    return res.IsFailure ? Problem(statusCode: 404, detail: res.Error) : Ok(res.Value);
  }

  [HttpGet("get/free")]
  public IActionResult GetFreeAppointments(int specializationId, DateOnly date)
  {
    Specialization spec = new(specializationId, " ");
    var res = _service.GetFreeAppointments(spec, date);

    return res.IsFailure ? Problem(statusCode: 404, detail: res.Error) : Ok(res.Value);
  }
}