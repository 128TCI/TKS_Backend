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
    public class SectionSetUpRepository : ISectionSetUpRepository
    {
        private readonly TimekeepingContext _context;

        public SectionSetUpRepository(TimekeepingContext context)
        {
            _context = context;
        }

        public async Task<SectionSetUp> InsertAsync(SectionSetUp sectionSetUp)
        {
            _context.tbl_fsSection.Add(sectionSetUp);
            await _context.SaveChangesAsync();
            return sectionSetUp;
        }

        public async Task<SectionSetUp> UpdateAsync(SectionSetUp sectionSetUp)
        {
            _context.tbl_fsSection.Update(sectionSetUp);
            await _context.SaveChangesAsync();
            return sectionSetUp;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var sectionSetUp = await _context.tbl_fsSection.FindAsync(id);
            if (sectionSetUp == null) return false;

            _context.tbl_fsSection.Remove(sectionSetUp);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<SectionSetUp?> GetByIdAsync(int id)
        {
            return await _context.tbl_fsSection.FindAsync(id);
        }

        public async Task<List<SectionSetUp>> GetAllAsync()
        {
            return await _context.tbl_fsSection.ToListAsync();
        }
    }
}
