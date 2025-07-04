using BarberSalesRecord.Data;
using BarberSalesRecord.DTOs;
using BarberSalesRecord.Interfaces;
using BarberSalesRecord.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace BarberSalesRecord.Services
{
    public class BarberService : IBarberService
    {
        private readonly ApplicationDbContext _context;

        public BarberService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<BarbersDto> AddBarberAsync(BarbersDto dto)
        {
            var barber = new Barber { 
                Name = dto.Name,
                CreatedAt = DateTime.UtcNow
            };
            _context.Barbers.Add(barber);
            await _context.SaveChangesAsync();
            //return barber;
            return new BarbersDto
            {
                //Id = barber.Id,
                Name = barber.Name,
                CreatedAt = barber.CreatedAt
            };
        }

        public async Task<List<Barber>> GetBarbersAsync()
        {
            return await _context.Barbers.ToListAsync();
        }

        public async Task<Barber?> GetByIdAsync(int id)
        {
            return await _context.Barbers.FindAsync(id);
        }

        public async Task UpdateAsync(int id, string name)
        {
            var barber = await GetByIdAsync(id);
            if (barber == null) throw new KeyNotFoundException("Barber not found.");
            barber.Name = name;
            _context.Barbers.Update(barber);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var barber = await GetByIdAsync(id);
            if (barber == null) throw new KeyNotFoundException("Barber not found.");
            _context.Barbers.Remove(barber);
            await _context.SaveChangesAsync();
        }
    }
}
