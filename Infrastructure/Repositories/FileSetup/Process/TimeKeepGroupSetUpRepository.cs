using DomainEntities.DTO.FileSetUp.Process;
using DomainEntities.DTO.FileSetUp.Process.Allowance_and_Earnings;
using Infrastructure.IRepositories.FileSetUp.Process;
using Infrastructure.IRepositories.FileSetUp.Process.Alllowance_and_Earnings;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timekeeping.Infrastructure.Data;

namespace Infrastructure.Repositories.FileSetup.Process
{
    public class TimeKeepGroupSetUpRepository : ITimeKeepGroupSetUpRepository
    {
        private readonly TimekeepingContext _context;

        public TimeKeepGroupSetUpRepository(TimekeepingContext context)
        {
            _context = context;
        }

        public async Task<TimeKeepGroupSetUpDTO> InsertAsync(TimeKeepGroupSetUpDTO timeKeepGroupSetUp)
        {
            _context.tk_GroupSetUpDefinition.Add(timeKeepGroupSetUp);
            await _context.SaveChangesAsync();
            return timeKeepGroupSetUp;
        }

        public async Task<TimeKeepGroupSetUpDTO> UpdateAsync(TimeKeepGroupSetUpDTO timeKeepGroupSetUp)
        {
            _context.tk_GroupSetUpDefinition.Update(timeKeepGroupSetUp);
            await _context.SaveChangesAsync();
            return timeKeepGroupSetUp;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var timeKeepGroupSetUp = await _context.tk_GroupSetUpDefinition.FindAsync(id);
            if (timeKeepGroupSetUp == null) return false;

            _context.tk_GroupSetUpDefinition.Remove(timeKeepGroupSetUp);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<TimeKeepGroupSetUpDTO?> GetByIdAsync(int id)
        {
            return await _context.tk_GroupSetUpDefinition.FindAsync(id);
        }

        public async Task<List<TimeKeepGroupSetUpDTO>> GetAllAsync()
        {
            return await _context.tk_GroupSetUpDefinition.ToListAsync();
        }

        public async Task<List<TimeKeepGroupSetUpDTO>> GetForImport()
        {
            return await _context.tk_GroupSetUpDefinition.FromSql($"SELECT * FROM dbo.tbl_tmpImpWorkShiftVar INNER JOIN dbo.tk_GroupSetUpDefinition ON tks_grp = GroupCode").ToListAsync();
        }
    }
}
