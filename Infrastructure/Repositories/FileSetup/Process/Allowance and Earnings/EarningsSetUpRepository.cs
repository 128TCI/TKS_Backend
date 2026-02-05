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
    public class EarningsSetUpRepository : IEarningsSetUpRepository
    {
        private readonly TimekeepingContext _context;

        public EarningsSetUpRepository(TimekeepingContext context)
        {
            _context = context;
        }

        public async Task<EarningSetUpDTO> InsertAsync(EarningSetUpDTO earningSetUp)
        {
            _context.tbl_fsEarnings.Add(earningSetUp);
            await _context.SaveChangesAsync();
            return earningSetUp;
        }

        public async Task<EarningSetUpDTO> UpdateAsync(EarningSetUpDTO earningSetUp)
        {
            _context.tbl_fsEarnings.Update(earningSetUp);
            await _context.SaveChangesAsync();
            return earningSetUp;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var earningSetUp = await _context.tbl_fsEarnings.FindAsync(id);
            if (earningSetUp == null) return false;

            _context.tbl_fsEarnings.Remove(earningSetUp);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<EarningSetUpDTO?> GetByIdAsync(int id)
        {
            return await _context.tbl_fsEarnings.FindAsync(id);
        }

        public async Task<List<EarningSetUpDTO>> GetAllAsync()
        {
            return await _context.tbl_fsEarnings.ToListAsync();
        }
    }
}
