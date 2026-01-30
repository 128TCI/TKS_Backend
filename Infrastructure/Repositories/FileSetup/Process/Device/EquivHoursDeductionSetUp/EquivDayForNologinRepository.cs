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
    public class EquivDayForNologinRepository : IEquivDayForNoLoginRepository
    {
        private readonly TimekeepingContext _context;

        public EquivDayForNologinRepository(TimekeepingContext context)
        {
            _context = context;
        }

        public async Task<EquivDayForNoLoginDTO> InsertAsync(EquivDayForNoLoginDTO data)
        {
            _context.tk_EquivDayForNoLogin.Add(data);
            await _context.SaveChangesAsync();
            return data;
        }

        public async Task<EquivDayForNoLoginDTO> UpdateAsync(EquivDayForNoLoginDTO data)
        {
            _context.tk_EquivDayForNoLogin.Update(data);
            await _context.SaveChangesAsync();
            return data;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var data = await _context.tk_EquivDayForNoLogin.FindAsync(id);
            if (data == null) return false;

            _context.tk_EquivDayForNoLogin.Remove(data);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<EquivDayForNoLoginDTO?> GetByIdAsync(int id)
        {
            return await _context.tk_EquivDayForNoLogin.FindAsync(id);
        }

        public async Task<List<EquivDayForNoLoginDTO>> GetAllAsync()
        {
            return await _context.tk_EquivDayForNoLogin.ToListAsync();
        }
    }
}
