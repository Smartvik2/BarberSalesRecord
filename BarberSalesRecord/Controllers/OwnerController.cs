using BarberSalesRecord.Interfaces;
using BarberSalesRecord.Models;
using BarberSalesRecord.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BarberSalesRecord.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Owner")]
    public class OwnerController : ControllerBase
    {
        private readonly IOwnerService _ownerService;
        private readonly UserManager<ApplicationUser> _userManager;

        public OwnerController(IOwnerService ownerService, UserManager<ApplicationUser> userManager)
        {
            _ownerService = ownerService;
            _userManager = userManager;
        }

        [HttpPut("approve-secretary/{userId}")]
        public async Task<IActionResult> ApproveSecretary(string userId)
        {
            var result = await _ownerService.ApproveSecretaryAsync(userId);
            return Ok(new { message = result });
        }

        [HttpGet("pending-secretaries")]
        public async Task<IActionResult> GetPendingSecretaries()
        {
            var users = _userManager.Users
                .Where(u => !u.IsApproved && u.Email != "excellentmmesoma6@gmail.com")
                .Select(u => new { u.Id, u.Name, u.Email });

            return Ok(await users.ToListAsync());
        }

        [HttpDelete("barber/{id}")]
        public async Task<IActionResult> DeleteBarber(int id)
        {
            var result = await _ownerService.DeleteBarberAsync(id);
            return Ok(new { message = "Barber deleted successfully." });
        }

        [HttpDelete("secretaries/{id}")]
        public async Task<IActionResult> DeleteSecretary(string id)
        {
            var deleted = await _ownerService.DeleteSecretaryByIdAsync(id);
            //return deleted ? NoContent() : NotFound("Secretary not found or not a secretary");
            return Ok(new { message = "Secretary deleted successfully." });
               
        }


    }
}
