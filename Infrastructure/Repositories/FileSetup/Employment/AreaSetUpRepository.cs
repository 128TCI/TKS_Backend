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
    public class AreaSetUpRepository: IAreaSetUpRepository
    {
        private readonly TimekeepingContext _context;

        public AreaSetUpRepository(TimekeepingContext context)
        {
            _context = context;
        }

        public async Task<AreaSetUp> InsertAsync(AreaSetUp areaSetUp)
        {
            _context.tbl_fsArea.Add(areaSetUp);
            await _context.SaveChangesAsync();
            return areaSetUp;
        }

        public async Task<AreaSetUp> UpdateAsync(AreaSetUp areaSetUp)
        {
            _context.tbl_fsArea.Update(areaSetUp);
            await _context.SaveChangesAsync();
            return areaSetUp;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var areaSetUp = await _context.tbl_fsArea.FindAsync(id);
            if (areaSetUp == null) return false;

            _context.tbl_fsArea.Remove(areaSetUp);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<AreaSetUp?> GetByIdAsync(int id)
        {
            return await _context.tbl_fsArea.FindAsync(id);
        }

        public async Task<List<AreaSetUp>> GetAllAsync()
        {
            return await _context.tbl_fsArea.ToListAsync();
        }
        
    }
}
