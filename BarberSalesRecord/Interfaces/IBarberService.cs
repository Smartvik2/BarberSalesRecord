using BarberSalesRecord.DTOs;
using BarberSalesRecord.Models;

namespace BarberSalesRecord.Interfaces
{
    public interface IBarberService
    {
        Task<BarbersDto> AddBarberAsync(BarbersDto dto);
        Task<List<Barber>> GetBarbersAsync();
        Task<Barber?> GetByIdAsync(int id);
        Task UpdateAsync(int id, string name);
        Task DeleteAsync(int id);
    }
}
