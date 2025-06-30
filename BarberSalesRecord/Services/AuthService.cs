using BarberSalesRecord.DTOs;
using BarberSalesRecord.Helpers;
using BarberSalesRecord.Interfaces;
using BarberSalesRecord.Models;
using Microsoft.AspNetCore.Identity;

namespace BarberSalesRecord.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _config;

        public AuthService(UserManager<ApplicationUser> userManager, IConfiguration config)
        {
            _userManager = userManager;
            _config = config;
        }

        public async Task<string> RegisterAsync(RegisterDto dto)
        {
            var existing = await _userManager.FindByEmailAsync(dto.Email);
            if (existing != null)
                return "Email already exists.";

            var user = new ApplicationUser
            {
                Name = dto.Name,
                Email = dto.Email,
                UserName = dto.Email,
                IsApproved = dto.Email == "excellentmmesoma6@gmail.com"
            };

            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
                return string.Join(" | ", result.Errors.Select(e => e.Description));

            if (user.IsApproved)
                await _userManager.AddToRoleAsync(user, "Owner");

            return user.IsApproved
                ? "Owner registered successfully."
                : "Registered successfully. Awaiting approval.";
        }

        public async Task<string> LoginAsync(LoginDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, dto.Password))
                return "Invalid credentials.";

            if (!user.IsApproved)
                return "Account not yet approved by Owner.";

            var roles = await _userManager.GetRolesAsync(user);
            var token = JwtHelper.GenerateToken(user, roles, _config);
            return token;
        }
    }
}
