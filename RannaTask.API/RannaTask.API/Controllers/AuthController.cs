using Microsoft.AspNetCore.Mvc;
using RannaTask.API.Models;
using RannaTask.API.Token;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    [HttpPost("Login")]
    public IActionResult Login([FromBody] User user)
    {
        if (user.UserName == "admin" && user.Password == "12345")
            return Created("", new BuildToken().CreateToken());

        return Unauthorized();
    }
}
