using domain.Logic;
using domain.Models;
using domain.Service;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[ApiController]
[Route("specialization")]
public class SpecializationController : Controller
{
  private readonly SpecializationService _service;

  public SpecializationController(SpecializationService service)
  {
    _service = service;
  }

  [HttpGet("get")]
  public ActionResult GetAll()
  {
    var answer = _service.GetAllSpecializations();
    return answer.IsFailure ? Problem(statusCode: 404, detail: answer.Error) : Ok(answer.Value);
  }

  [HttpGet("get/{name}")]
  public ActionResult GetItem(string name)
  {
    var answer = _service.GetSpecialization(name);
    return answer.IsFailure ? Problem(statusCode: 404, detail: answer.Error) : Ok(answer.Value);
  }

  [HttpGet("get/{id:int}")]
  public ActionResult GetItem(int id)
  {
    var answer = _service.GetSpecialization(id);
    return answer.IsFailure ? Problem(statusCode: 404, detail: answer.Error) : Ok(answer.Value);
  }

  [HttpPost("add")]
  public ActionResult Add(string name)
  {
    Specialization specialization = new(0, name);
    var res = specialization.IsValid();
    if (res.IsFailure)
      return Problem(statusCode: 404, detail: res.Error);
    if (_service.Create(specialization).IsFailure)
      return Problem(statusCode: 404, detail: "Error while creating");
    _service.Save();
    return Ok(_service.GetSpecialization(name).Value);
  }

  [HttpDelete("delete/{id:int}")]
  public ActionResult Delete(int id)
  {
    var res = _service.Delete(_service.GetSpecialization(id).Value);
    if (res.IsFailure)
      return Problem(statusCode: 404, detail: res.Error);
    _service.Save();
    return Ok(res.Value);
  }

  [HttpDelete("delete/{name}")]
  public ActionResult Delete(string name)
  {
    var res = _service.Delete(_service.GetSpecialization(name).Value);
    if (res.IsFailure)
      return Problem(statusCode: 404, detail: res.Error);
    _service.Save();
    return Ok(res.Value);
  }
}