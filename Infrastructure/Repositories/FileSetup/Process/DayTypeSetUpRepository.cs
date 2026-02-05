using DomainEntities.DTO.FileSetUp.Process;
using Infrastructure.IRepositories.FileSetUp.Process;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timekeeping.Infrastructure.Data;

namespace Infrastructure.Repositories.FileSetup.Process
{
    public class DayTypeSetUpRepository : IDayTypeSetUpRepository
    {
        private readonly TimekeepingContext _context;

        public DayTypeSetUpRepository(TimekeepingContext context)
        {
            _context = context;
        }

        public async Task<DayTypeSetUpDTO> InsertAsync(DayTypeSetUpDTO dayTypeSetUp)
        {
            _context.tk_DayTypeSetup.Add(dayTypeSetUp);
            await _context.SaveChangesAsync();
            return dayTypeSetUp;
        }

        public async Task<DayTypeSetUpDTO> UpdateAsync(DayTypeSetUpDTO dayTypeSetUp)
        {
            _context.tk_DayTypeSetup.Update(dayTypeSetUp);
            await _context.SaveChangesAsync();
            return dayTypeSetUp;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var dayTypeSetUp = await _context.tk_DayTypeSetup.FindAsync(id);
            if (dayTypeSetUp == null) return false;

            _context.tk_DayTypeSetup.Remove(dayTypeSetUp);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<DayTypeSetUpDTO?> GetByIdAsync(int id)
        {
            return await _context.tk_DayTypeSetup.FindAsync(id);
        }

        public async Task<List<DayTypeSetUpDTO>> GetAllAsync()
        {
            return await _context.tk_DayTypeSetup.ToListAsync();
        }
    }
}
