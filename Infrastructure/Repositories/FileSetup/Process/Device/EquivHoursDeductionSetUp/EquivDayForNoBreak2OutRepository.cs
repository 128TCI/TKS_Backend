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
    public class EquivDayForNoBreak2OutRepository : IEquivDayForNoBreak2OutRepository
    {
        private readonly TimekeepingContext _context;

        public EquivDayForNoBreak2OutRepository(TimekeepingContext context)
        {
            _context = context;
        }

        public async Task<EquivDayForNoBreat2OutDTO> InsertAsync(EquivDayForNoBreat2OutDTO data)
        {
            _context.tk_EquivDayForNoBreak2Out.Add(data);
            await _context.SaveChangesAsync();
            return data;
        }

        public async Task<EquivDayForNoBreat2OutDTO> UpdateAsync(EquivDayForNoBreat2OutDTO data)
        {
            _context.tk_EquivDayForNoBreak2Out.Update(data);
            await _context.SaveChangesAsync();
            return data;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var data = await _context.tk_EquivDayForNoBreak2Out.FindAsync(id);
            if (data == null) return false;

            _context.tk_EquivDayForNoBreak2Out.Remove(data);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<EquivDayForNoBreat2OutDTO?> GetByIdAsync(int id)
        {
            return await _context.tk_EquivDayForNoBreak2Out.FindAsync(id);
        }

        public async Task<List<EquivDayForNoBreat2OutDTO>> GetAllAsync()
        {
            return await _context.tk_EquivDayForNoBreak2Out.ToListAsync();
        }
    }
}
