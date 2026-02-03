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
    public class DTRFlagSetUpRepository : IDTRFlagSetUpRepository
    {
        private readonly TimekeepingContext _context;

        public DTRFlagSetUpRepository(TimekeepingContext context)
        {
            _context = context;
        }

        public async Task<DTRFlagSetUpDTO> InsertAsync(DTRFlagSetUpDTO data)
        {
            _context.tk_DTRFlag.Add(data);
            await _context.SaveChangesAsync();
            return data;
        }

        public async Task<DTRFlagSetUpDTO> UpdateAsync(DTRFlagSetUpDTO data)
        {
            _context.tk_DTRFlag.Update(data);
            await _context.SaveChangesAsync();
            return data;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var data = await _context.tk_DTRFlag.FindAsync(id);
            if (data == null) return false;

            _context.tk_DTRFlag.Remove(data);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<DTRFlagSetUpDTO?> GetByIdAsync(int id)
        {
            return await _context.tk_DTRFlag.FindAsync(id);
        }

        public async Task<List<DTRFlagSetUpDTO>> GetAllAsync()
        {
            return await _context.tk_DTRFlag.ToListAsync();
        }
    }
}
