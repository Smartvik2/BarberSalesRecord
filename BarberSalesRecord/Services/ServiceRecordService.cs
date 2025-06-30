using BarberSalesRecord.Data;
using BarberSalesRecord.Interfaces;
using BarberSalesRecord.Models;
using BarberSalesRecord.DTOs;
using Microsoft.EntityFrameworkCore;

namespace BarberSalesRecord.Services
{
    public class ServiceRecordService : IServiceRecordService
    {
        private readonly ApplicationDbContext _context;

        public ServiceRecordService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceRecord> CreateAsync(ServiceRecordDto dto)
        {
            // ensure barber exists
            var barber = await _context.Barbers.FindAsync(dto.BarberId)
                ?? throw new KeyNotFoundException("Barber not found");

            var rec = new ServiceRecord
            {
                BarberId = dto.BarberId,
                Date = dto.Date,
                ServiceType = dto.ServiceName,
                AmountCharged = dto.AmountCharged
            };
            _context.ServiceRecords.Add(rec);
            await _context.SaveChangesAsync();
            return rec;
        }

        public async Task<List<ServiceRecordDto>> GetByBarberAsync(int barberId)
        {
            return await _context.ServiceRecords
                .Where(r => r.BarberId == barberId)
                .Include(r => r.Barber)
                .Select(r => new ServiceRecordDto
                {
                    BarberId = r.BarberId,
                    BarberName = r.Barber.Name,
                    ServiceName = r.ServiceType,
                    AmountCharged = r.AmountCharged,
                    Date = r.Date
                }).ToListAsync();
        }


        public async Task<List<ServiceRecordDto>> GetAllAsync()
        {
            return await _context.ServiceRecords
                .Include(r => r.Barber)
                .Select(r => new ServiceRecordDto
                {
                    BarberId = r.BarberId,
                    BarberName = r.Barber.Name,
                    ServiceName = r.ServiceType,
                    AmountCharged = r.AmountCharged,
                    Date = r.Date
                }).ToListAsync();
        }

        public async Task<bool> DeleteServiceRecordAsync(int id)
        {
            var rec = await _context.ServiceRecords.FindAsync(id);
            if (rec == null) return false;

            _context.ServiceRecords.Remove(rec);
            await _context.SaveChangesAsync();
            return true;
        }


    }
}
