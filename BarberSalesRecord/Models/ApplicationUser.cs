using Microsoft.AspNetCore.Identity;

namespace BarberSalesRecord.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; } = string.Empty;
        public bool IsApproved { get; set; } = false;

    }
}
