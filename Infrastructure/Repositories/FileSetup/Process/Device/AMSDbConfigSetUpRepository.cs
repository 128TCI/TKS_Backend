using DomainEntities.DTO.FileSetUp.Process;
using DomainEntities.DTO.FileSetUp.Process.Device;
using Infrastructure.IRepositories.FileSetUp.Process;
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
    public class AMSDbConfigSetUpRepository : IAMSDbConfigSetUpRepository
    {
        private readonly TimekeepingContext _context;

        public AMSDbConfigSetUpRepository(TimekeepingContext context)
        {
            _context = context;
        }

        public async Task<AMSDbConfigSetUpDTO> InsertAsync(AMSDbConfigSetUpDTO data)
        {
            _context.tk_AmsDbConfiguration.Add(data);
            await _context.SaveChangesAsync();
            return data;
        }

        public async Task<AMSDbConfigSetUpDTO> UpdateAsync(AMSDbConfigSetUpDTO data)
        {
            _context.tk_AmsDbConfiguration.Update(data);
            await _context.SaveChangesAsync();
            return data;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var data = await _context.tk_AmsDbConfiguration.FindAsync(id);
            if (data == null) return false;

            _context.tk_AmsDbConfiguration.Remove(data);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<AMSDbConfigSetUpDTO?> GetByIdAsync(int id)
        {
            return await _context.tk_AmsDbConfiguration.FindAsync(id);
        }

        public async Task<List<AMSDbConfigSetUpDTO>> GetAllAsync()
        {
            return await _context.tk_AmsDbConfiguration.ToListAsync();
        }
    }
}
