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
    public class AllowancePerClassificationSetUpRepository : IAllowancePerClassificationSetUpRepository
    {
        private readonly TimekeepingContext _context;

        public AllowancePerClassificationSetUpRepository(TimekeepingContext context)
        {
            _context = context;
        }

        public async Task<AllowancePerClassificationSetUpDTO> InsertAsync(AllowancePerClassificationSetUpDTO allowancesPerClassificationSetup)
        {
            _context.tk_AllowancesPerClassificationSetup.Add(allowancesPerClassificationSetup);
            await _context.SaveChangesAsync();
            return allowancesPerClassificationSetup;
        }

        public async Task<AllowancePerClassificationSetUpDTO> UpdateAsync(AllowancePerClassificationSetUpDTO allowancesPerClassificationSetup)
        {
            _context.tk_AllowancesPerClassificationSetup.Update(allowancesPerClassificationSetup);
            await _context.SaveChangesAsync();
            return allowancesPerClassificationSetup;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var allowancesPerClassificationSetup = await _context.tk_AllowancesPerClassificationSetup.FindAsync(id);
            if (allowancesPerClassificationSetup == null) return false;

            _context.tk_AllowancesPerClassificationSetup.Remove(allowancesPerClassificationSetup);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<AllowancePerClassificationSetUpDTO?> GetByIdAsync(int id)
        {
            return await _context.tk_AllowancesPerClassificationSetup.FindAsync(id);
        }

        public async Task<List<AllowancePerClassificationSetUpDTO>> GetAllAsync()
        {
            return await _context.tk_AllowancesPerClassificationSetup.ToListAsync();
        }
    }
}
