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
    public class EquivDayForNoBreak2InRepository : IEquivDayForNoBreak2InRepository
    {
        private readonly TimekeepingContext _context;

        public EquivDayForNoBreak2InRepository(TimekeepingContext context)
        {
            _context = context;
        }

        public async Task<EquivDayForNoBreat2InDTO> InsertAsync(EquivDayForNoBreat2InDTO data)
        {
            _context.tk_EquivDayForNoBreak2In.Add(data);
            await _context.SaveChangesAsync();
            return data;
        }

        public async Task<EquivDayForNoBreat2InDTO> UpdateAsync(EquivDayForNoBreat2InDTO data)
        {
            _context.tk_EquivDayForNoBreak2In.Update(data);
            await _context.SaveChangesAsync();
            return data;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var data = await _context.tk_EquivDayForNoBreak2In.FindAsync(id);
            if (data == null) return false;

            _context.tk_EquivDayForNoBreak2In.Remove(data);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<EquivDayForNoBreat2InDTO?> GetByIdAsync(int id)
        {
            return await _context.tk_EquivDayForNoBreak2In.FindAsync(id);
        }

        public async Task<List<EquivDayForNoBreat2InDTO>> GetAllAsync()
        {
            return await _context.tk_EquivDayForNoBreak2In.ToListAsync();
        }
    }
}
