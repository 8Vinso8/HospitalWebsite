using domain.Logic;
using domain.Models;
using domain.Service;
using Microsoft.AspNetCore.Mvc;
using WebApp.Views;

namespace WebApp.Controllers;

[ApiController]
[Route("user")]
public class UserController : Controller
{
  private readonly UserService _service;

  public UserController(UserService service)
  {
    _service = service;
  }

  [HttpGet("get/{username}")]
  public ActionResult GetUser(string username)
  {
    var user = _service.GetUserByUsername(username);
    if (user.IsFailure)
      return Problem(statusCode: 404, detail: user.Error);
    return Ok(new UserSearchView()
    {
      Id = user.Value.Id,
      Username = user.Value.Username
    });
  }

  [HttpGet("get/{id:int}")]
  public ActionResult GetUser(int id)
  {
    var user = _service.GetUserById(id);
    if (user.IsFailure)
      return Problem(statusCode: 404, detail: user.Error);
    return Ok(new UserSearchView()
    {
      Id = user.Value.Id,
      Username = user.Value.Username
    });
  }

  [HttpPost("register")]
  public IActionResult Register(string username, string password, string phone, string fullName, Role role)
  {
    User user = new(0, phone, fullName, role, username, password);
    var register = _service.Register(user);
    if (register.IsFailure)
      return Problem(statusCode: 404, detail: register.Error);
    return Ok(new UserSearchView()
    {
      Id = register.Value.Id,
      Username = register.Value.Username
    });
  }
}