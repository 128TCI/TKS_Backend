using DomainEntities.DTO.FileSetUp.Employment;
using DomainEntities.DTO.FileSetUp.Process.Allowance_and_Earnings;
using Infrastructure.IRepositories.FileSetUp.Employment;
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
    public class AllowanceBracketCodeSetUpRepository : IAllowanceBracketCodeSetUpRepository
    {
        private readonly TimekeepingContext _context;

        public AllowanceBracketCodeSetUpRepository(TimekeepingContext context)
        {
            _context = context;
        }

        public async Task<AllowanceBracketCodeSetUpDTO> InsertAsync(AllowanceBracketCodeSetUpDTO allowanceBracketCodeSetup)
        {
            _context.tbl_fsAllowBracketCode.Add(allowanceBracketCodeSetup);
            await _context.SaveChangesAsync();
            return allowanceBracketCodeSetup;
        }

        public async Task<AllowanceBracketCodeSetUpDTO> UpdateAsync(AllowanceBracketCodeSetUpDTO allowanceBracketCodeSetup)
        {
            _context.tbl_fsAllowBracketCode.Update(allowanceBracketCodeSetup);
            await _context.SaveChangesAsync();
            return allowanceBracketCodeSetup;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var allowanceBracketCodeSetup = await _context.tbl_fsAllowBracketCode.FindAsync(id);
            if (allowanceBracketCodeSetup == null) return false;

            _context.tbl_fsAllowBracketCode.Remove(allowanceBracketCodeSetup);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<AllowanceBracketCodeSetUpDTO?> GetByIdAsync(int id)
        {
            return await _context.tbl_fsAllowBracketCode.FindAsync(id);
        }

        public async Task<List<AllowanceBracketCodeSetUpDTO>> GetAllAsync()
        {
            return await _context.tbl_fsAllowBracketCode.ToListAsync();
        }
    }
}
