using DomainEntities.DTO.FileSetUp.Employment;
using Infrastructure.IRepositories.FileSetUp.Employment;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timekeeping.Infrastructure.Data;

namespace Infrastructure.Repositories.FileSetup.Employment
{
    public class BranchSetUpRepository : IBranchSetUpRepository
    {
        private readonly TimekeepingContext _context;

        public BranchSetUpRepository(TimekeepingContext context)
        {
            _context = context;
        }

        public async Task<BranchSetUp> InsertAsync(BranchSetUp branchSetUp)
        {
            _context.tbl_fsBranch.Add(branchSetUp);
            await _context.SaveChangesAsync();
            return branchSetUp;
        }

        public async Task<BranchSetUp> UpdateAsync(BranchSetUp branchSetUp)
        {
            _context.tbl_fsBranch.Update(branchSetUp);
            await _context.SaveChangesAsync();
            return branchSetUp;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var branchSetUp = await _context.tbl_fsBranch.FindAsync(id);
            if (branchSetUp == null) return false;

            _context.tbl_fsBranch.Remove(branchSetUp);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<BranchSetUp?> GetByIdAsync(int id)
        {
            return await _context.tbl_fsBranch.FindAsync(id);
        }

        public async Task<List<BranchSetUp>> GetAllAsync()
        {
            return await _context.tbl_fsBranch.ToListAsync();
        }

    }
}
