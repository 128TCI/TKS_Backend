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
    public class LocationSetUpRepository : ILocationSetUpRepository
    {
        private readonly TimekeepingContext _context;

        public LocationSetUpRepository(TimekeepingContext context)
        {
            _context = context;
        }

        public async Task<LocationSetUp> InsertAsync(LocationSetUp locationSetUp)
        {
            _context.tbl_fsLocation.Add(locationSetUp);
            await _context.SaveChangesAsync();
            return locationSetUp;
        }

        public async Task<LocationSetUp> UpdateAsync(LocationSetUp locationSetUp)
        {
            _context.tbl_fsLocation.Update(locationSetUp);
            await _context.SaveChangesAsync();
            return locationSetUp;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var locationSetUp = await _context.tbl_fsLocation.FindAsync(id);
            if (locationSetUp == null) return false;

            _context.tbl_fsLocation.Remove(locationSetUp);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<LocationSetUp?> GetByIdAsync(int id)
        {
            return await _context.tbl_fsLocation.FindAsync(id);
        }

        public async Task<List<LocationSetUp>> GetAllAsync()
        {
            return await _context.tbl_fsLocation.ToListAsync();
        }

    }
}
