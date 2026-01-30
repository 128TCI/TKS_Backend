using DomainEntities.DTO.FileSetUp.Process.Device.EquivHoursDeductionSetUp;
using Infrastructure.IRepositories.FileSetUp.Process.Device.EquivHoursDeductionSetUp;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timekeeping.Infrastructure.Data;

namespace Infrastructure.Repositories.FileSetup.Process.Device.EquivHoursDeductionSetUp
{
    public class EquivDayForAbsentRepository : IEquivDayForAbsentRepository
    {
        private readonly TimekeepingContext _context;

        public EquivDayForAbsentRepository(TimekeepingContext context)
        {
            _context = context;
        }

        public async Task<EquivDayForAbsentDTO> InsertAsync(EquivDayForAbsentDTO data)
        {
            _context.tk_EquivDayForAbsent.Add(data);
            await _context.SaveChangesAsync();
            return data;
        }

        public async Task<EquivDayForAbsentDTO> UpdateAsync(EquivDayForAbsentDTO data)
        {
            _context.tk_EquivDayForAbsent.Update(data);
            await _context.SaveChangesAsync();
            return data;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var data = await _context.tk_EquivDayForAbsent.FindAsync(id);
            if (data == null) return false;

            _context.tk_EquivDayForAbsent.Remove(data);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<EquivDayForAbsentDTO?> GetByIdAsync(int id)
        {
            return await _context.tk_EquivDayForAbsent.FindAsync(id);
        }

        public async Task<List<EquivDayForAbsentDTO>> GetAllAsync()
        {
            return await _context.tk_EquivDayForAbsent.ToListAsync();
        }
    }
}
