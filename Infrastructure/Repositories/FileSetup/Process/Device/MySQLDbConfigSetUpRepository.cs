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
    public class MySQLDbConfigSetUpRepository : IMySQLDbConfigSetUpRepository
    {
        private readonly TimekeepingContext _context;

        public MySQLDbConfigSetUpRepository(TimekeepingContext context)
        {
            _context = context;
        }

        public async Task<MySQLDbConfigSetUpDTO> InsertAsync(MySQLDbConfigSetUpDTO data)
        {
            _context.tk_MySQLDbConfiguration.Add(data);
            await _context.SaveChangesAsync();
            return data;
        }

        public async Task<MySQLDbConfigSetUpDTO> UpdateAsync(MySQLDbConfigSetUpDTO data)
        {
            _context.tk_MySQLDbConfiguration.Update(data);
            await _context.SaveChangesAsync();
            return data;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var data = await _context.tk_MySQLDbConfiguration.FindAsync(id);
            if (data == null) return false;

            _context.tk_MySQLDbConfiguration.Remove(data);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<MySQLDbConfigSetUpDTO?> GetByIdAsync(int id)
        {
            return await _context.tk_MySQLDbConfiguration.FindAsync(id);
        }

        public async Task<List<MySQLDbConfigSetUpDTO>> GetAllAsync()
        {
            return await _context.tk_MySQLDbConfiguration.ToListAsync();
        }
    }
}
