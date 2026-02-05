using DomainEntities.DTO.FileSetUp.Process.Allowance_and_Earnings;
using Infrastructure.IRepositories.FileSetUp.Process.Alllowance_and_Earnings;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timekeeping.Infrastructure.Data;

namespace Infrastructure.Repositories.FileSetup.Process.Allowance_and_Earnings
{
    public class AllowanceBracketingSetUpRepository : IAllowanceBracketingSetUpRepository
    {
        private readonly TimekeepingContext _context;

        public AllowanceBracketingSetUpRepository(TimekeepingContext context)
        {
            _context = context;
        }

        public async Task<AllowanceBracketingSetUpDTO> InsertAsync(AllowanceBracketingSetUpDTO allowanceBracketingSetUp)
        {
            _context.tk_AllowBaseOnDayType.Add(allowanceBracketingSetUp);
            await _context.SaveChangesAsync();
            return allowanceBracketingSetUp;
        }

        public async Task<AllowanceBracketingSetUpDTO> UpdateAsync(AllowanceBracketingSetUpDTO allowanceBracketingSetUp)
        {
            _context.tk_AllowBaseOnDayType.Update(allowanceBracketingSetUp);
            await _context.SaveChangesAsync();
            return allowanceBracketingSetUp;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var allowanceBracketingSetUp = await _context.tk_AllowBaseOnDayType.FindAsync(id);
            if (allowanceBracketingSetUp == null) return false;

            _context.tk_AllowBaseOnDayType.Remove(allowanceBracketingSetUp);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<AllowanceBracketingSetUpDTO?> GetByIdAsync(int id)
        {
            return await _context.tk_AllowBaseOnDayType.FindAsync(id);
        }

        public async Task<List<AllowanceBracketingSetUpDTO>> GetAllAsync()
        {
            return await _context.tk_AllowBaseOnDayType.ToListAsync();
        }
    }
}
