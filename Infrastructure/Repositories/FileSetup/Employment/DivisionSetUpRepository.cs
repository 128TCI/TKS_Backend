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
    public class DivisionSetUpRepository : IDivisionSetUpRepository
    {
        private readonly TimekeepingContext _context;

        public DivisionSetUpRepository(TimekeepingContext context)
        {
            _context = context;
        }

        public async Task<DivisionSetUp> InsertAsync(DivisionSetUp divisiontSetUp)
        {
            _context.tbl_fsDivision.Add(divisiontSetUp);
            await _context.SaveChangesAsync();
            return divisiontSetUp;
        }

        public async Task<DivisionSetUp> UpdateAsync(DivisionSetUp divisiontSetUp)
        {
            _context.tbl_fsDivision.Update(divisiontSetUp);
            await _context.SaveChangesAsync();
            return divisiontSetUp;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var divisiontSetUp = await _context.tbl_fsDivision.FindAsync(id);
            if (divisiontSetUp == null) return false;

            _context.tbl_fsDivision.Remove(divisiontSetUp);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<DivisionSetUp?> GetByIdAsync(int id)
        {
            return await _context.tbl_fsDivision.FindAsync(id);
        }

        public async Task<List<DivisionSetUp>> GetAllAsync()
        {
            return await _context.tbl_fsDivision.ToListAsync();
        }

    }
}
