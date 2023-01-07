using domain.Models;
using domain.Service;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[ApiController]
[Route("schedule")]
public class ScheduleController : Controller
{
  private readonly ScheduleService _service;
  private readonly DoctorService _serviceDoc;

  public ScheduleController(ScheduleService service, DoctorService doctorService)
  {
    _service = service;
    _serviceDoc = doctorService;
  }

  [HttpGet("get")]
  public IActionResult GetSchedule(int id)
  {
    var doc = _serviceDoc.GetDoctor(id);
    if (doc.IsFailure)
      return Problem(statusCode: 404, detail: doc.Error);

    var res = _service.GetSchedule(doc.Value);
    return res.IsFailure ? Problem(statusCode: 404, detail: res.Error) : Ok(res.Value);
  }

  [HttpPost("register")]
  public IActionResult AddSchedule(int doctorId, DateTime startTime, DateTime endTime)
  {
    Schedule schedule = new(0, doctorId, startTime, endTime);
    var res = _service.CreateSchedule(schedule);
    if (res.IsFailure)
      return Problem(statusCode: 404, detail: res.Error);
    return Ok();
  }

  [HttpGet("update")]
  public IActionResult UpdateSchedule(int scheduleId, int? doctorId, DateTime? startTime, DateTime? endTime)
  {
    var check = _service.GetSchedule(scheduleId);
    if (check.IsFailure)
      return Problem(statusCode: 404, detail: check.Error);

    var schedule = check.Value;

    if (doctorId != null)
      schedule.DoctorId = (int)doctorId;
    if (startTime != null && endTime != null)
    {
      schedule.StartTime = (DateTime)startTime;
      schedule.EndTime = (DateTime)endTime;
    }

    var check2 = _service.UpdateSchedule(schedule);
    if (check2.IsFailure)
      return Problem(statusCode: 404, detail: check2.Error);

    return Ok();
  }
}