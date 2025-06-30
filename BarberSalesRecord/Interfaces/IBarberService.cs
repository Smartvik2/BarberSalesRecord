using BarberSalesRecord.Models;

namespace BarberSalesRecord.Interfaces
{
    public interface IBarberService
    {
        Task<Barber> AddBarberAsync(string name);
        Task<List<Barber>> GetBarbersAsync();
        Task<Barber?> GetByIdAsync(int id);
        Task UpdateAsync(int id, string name);
        Task DeleteAsync(int id);
    }
}
