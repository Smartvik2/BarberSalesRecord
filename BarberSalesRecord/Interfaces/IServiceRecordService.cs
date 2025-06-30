using BarberSalesRecord.Models;
using BarberSalesRecord.DTOs;

namespace BarberSalesRecord.Interfaces
{
    public interface IServiceRecordService
    {
        Task<ServiceRecord> CreateAsync(ServiceRecordDto dto);
        Task<List<ServiceRecordDto>> GetByBarberAsync(int barberId);
        Task<List<ServiceRecordDto>> GetAllAsync();
        Task<bool> DeleteServiceRecordAsync(int id);

    }
}
