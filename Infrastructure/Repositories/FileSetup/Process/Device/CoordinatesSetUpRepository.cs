using DomainEntities.DTO.FileSetUp.Process.Device;
using Infrastructure.IRepositories.FileSetUp.Process.Device;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timekeeping.Infrastructure.Data;

namespace Infrastructure.Repositories.FileSetup.Process.Device
{
    public class CoordinatesSetUpRepository : ICoordinatesSetUpRepository
    {
        private readonly TimekeepingContext _context;

        public CoordinatesSetUpRepository(TimekeepingContext context)
        {
            _context = context;
        }

        public async Task<CoordinatesSetUpDTO> InsertAsync(CoordinatesSetUpDTO data)
        {
            _context.tbl_fsCoordinates.Add(data);
            await _context.SaveChangesAsync();
            return data;
        }

        public async Task<CoordinatesSetUpDTO> UpdateAsync(CoordinatesSetUpDTO data)
        {
            _context.tbl_fsCoordinates.Update(data);
            await _context.SaveChangesAsync();
            return data;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var data = await _context.tbl_fsCoordinates.FindAsync(id);
            if (data == null) return false;

            _context.tbl_fsCoordinates.Remove(data);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<CoordinatesSetUpDTO?> GetByIdAsync(int id)
        {
            return await _context.tbl_fsCoordinates.FindAsync(id);
        }

        public async Task<List<CoordinatesSetUpDTO>> GetAllAsync()
        {
            return await _context.tbl_fsCoordinates.ToListAsync();
        }
    }
}
