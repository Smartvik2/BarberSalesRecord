using BarberSalesRecord.DTOs;

namespace BarberSalesRecord.Interfaces
{
    public interface IProfitService
    {
        Task<ProfitReportDto> CalculateProfitAsync(ProfitDto request);
    }
}
