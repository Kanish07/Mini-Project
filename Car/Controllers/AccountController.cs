using Car.Helpers;
using Car.Models;
using Car.Services;
using Car.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Car.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly carContext _context;
    private readonly IUserServices _userServices;

    public AccountController(carContext context, IUserServices userServices)
    {
        _context = context;
        _userServices = userServices;
    }

    [HttpPost]
    public IActionResult Login(AuthRequest model)
    {
        var user = _context.Users.FirstOrDefault(x => x.Useremail == model.Email);
        if (user == null)
            return BadRequest(new { message = "Email not found!" });
        var verify = BCrypt.Net.BCrypt.Verify(model.Password, user!.Userpassword);
        if (!verify)
            return BadRequest(new { message = "Incorrect password!" });
        var response = _userServices.Authenticate(user);
        return Ok(response);
    }

    [Authorize]
    [HttpGet]
    public IActionResult GetAllUsers()
    {
        var users = _userServices.GetAll();
        return Ok(users);
    }
}