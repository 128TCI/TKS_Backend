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
    public class UnitSetUpRepository : IUnitSetUpRepository
    {
        private readonly TimekeepingContext _context;

        public UnitSetUpRepository(TimekeepingContext context)
        {
            _context = context;
        }

        public async Task<UnitSetUp> InsertAsync(UnitSetUp unitSetUp)
        {
            _context.tk_unit.Add(unitSetUp);
            await _context.SaveChangesAsync();
            return unitSetUp;
        }

        public async Task<UnitSetUp> UpdateAsync(UnitSetUp unitSetUp)
        {
            _context.tk_unit.Update(unitSetUp);
            await _context.SaveChangesAsync();
            return unitSetUp;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var unitSetUp = await _context.tk_unit.FindAsync(id);
            if (unitSetUp == null) return false;

            _context.tk_unit.Remove(unitSetUp);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<UnitSetUp?> GetByIdAsync(int id)
        {
            return await _context.tk_unit.FindAsync(id);
        }

        public async Task<List<UnitSetUp>> GetAllAsync()
        {
            return await _context.tk_unit.ToListAsync();
        }
    }
}
