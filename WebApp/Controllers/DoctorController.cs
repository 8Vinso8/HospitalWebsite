using domain.Logic;
using domain.Models;
using domain.Service;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[ApiController]
[Route("doctor")]
public class DoctorController : Controller
{
  private readonly DoctorService _service;

  public DoctorController(DoctorService service)
  {
    _service = service;
  }

  [HttpPost("add")]
  public IActionResult CreateDoctor(string fullName, Specialization specialization)
  {
    Doctor doctor = new(0, fullName, specialization);
    var res = _service.AddDoctor(doctor);

    return res.IsFailure ? Problem(statusCode: 404, detail: res.Error) : Ok(res.Value);
  }

  [HttpDelete("delete")]
  public IActionResult DeleteDoctor(int id)
  {
    var res = _service.RemoveDoctor(id);
    return res.IsFailure ? Problem(statusCode: 404, detail: res.Error) : Ok(res.Value);
  }

  [HttpGet("get")]
  public IActionResult GetAllDoctors()
  {
    var res = _service.GetAllDoctors();
    return res.IsFailure ? Problem(statusCode: 404, detail: res.Error) : Ok(res.Value);
  }

  [HttpGet("get/id/{id:int}")]
  public IActionResult GetDoctor(int id)
  {
    var res = _service.GetDoctor(id);
    return res.IsFailure ? Problem(statusCode: 404, detail: res.Error) : Ok(res.Value);
  }

  [HttpGet("get/spec/{id:int}")]
  public IActionResult GetDoctorsBySpec(int specialization)
  {
    Specialization spec = new(specialization, "a");
    var res = _service.GetDoctor(spec);
    return res.IsFailure ? Problem(statusCode: 404, detail: res.Error) : Ok(res.Value);
  }
}