using BarberSalesRecord.Data;
using BarberSalesRecord.Interfaces;
using BarberSalesRecord.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BarberSalesRecord.Services
{
    public class OwnerService : IOwnerService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public OwnerService(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<string> ApproveSecretaryAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return "User not found.";

            if (await _userManager.IsInRoleAsync(user, "Owner"))
                return "You cannot approve an Owner.";

            user.IsApproved = true;
            await _userManager.UpdateAsync(user);
            await _userManager.AddToRoleAsync(user, "Secretary");

            return $"User {user.Email} approved as Secretary.";
        }

        public async Task<bool> DeleteBarberAsync(int id)
        {
            var barber = await _context.Barbers.FindAsync(id);
            if (barber == null) return false;

            _context.Barbers.Remove(barber);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteSecretaryByIdAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null || !await _userManager.IsInRoleAsync(user, "Secretary"))
                return false;

            var result = await _userManager.DeleteAsync(user);
            return result.Succeeded;
        }



    }
}
