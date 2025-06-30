using BarberSalesRecord.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BarberSalesRecord.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Barber> Barbers { get; set; }
        public DbSet<ServiceRecord> ServiceRecords { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<ProfitReport> ProfitReports { get; set; }

    }


}
