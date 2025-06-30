using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
//using System.Linq;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class DebugController : ControllerBase
{
    [HttpGet("claims")]
    public IActionResult GetClaims()
    {
        return Ok(User.Claims.Select(c => new { c.Type, c.Value }));
    }
}
