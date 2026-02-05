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
    public class EquivDayForNoLogoutRepository : IEquivDayForNoLogOutRepository
    {
        private readonly TimekeepingContext _context;

        public EquivDayForNoLogoutRepository(TimekeepingContext context)
        {
            _context = context;
        }

        public async Task<EquivDayForNoLogoutDTO> InsertAsync(EquivDayForNoLogoutDTO data)
        {
            _context.tk_EquivDayForNoLogout.Add(data);
            await _context.SaveChangesAsync();
            return data;
        }

        public async Task<EquivDayForNoLogoutDTO> UpdateAsync(EquivDayForNoLogoutDTO data)
        {
            _context.tk_EquivDayForNoLogout.Update(data);
            await _context.SaveChangesAsync();
            return data;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var data = await _context.tk_EquivDayForNoLogout.FindAsync(id);
            if (data == null) return false;

            _context.tk_EquivDayForNoLogout.Remove(data);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<EquivDayForNoLogoutDTO?> GetByIdAsync(int id)
        {
            return await _context.tk_EquivDayForNoLogout.FindAsync(id);
        }

        public async Task<List<EquivDayForNoLogoutDTO>> GetAllAsync()
        {
            return await _context.tk_EquivDayForNoLogout.ToListAsync();
        }
    }
}
