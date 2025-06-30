using BarberSalesRecord.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BarberSalesRecord.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]  // any authenticated user
    public class ProfileController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ProfileController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetProfile()
        {
            // 1. Get the user ID from the JWT claims
            var userId = User.Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
                return Unauthorized("Invalid token: no user ID.");

            // 2. Load the ApplicationUser
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound("User not found.");

            // 3. Load the roles
            var roles = await _userManager.GetRolesAsync(user);

            // 4. Return a profile DTO
            return Ok(new
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Roles = roles
            });
        }
    }
}
