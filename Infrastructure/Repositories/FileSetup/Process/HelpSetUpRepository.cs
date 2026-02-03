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

namespace Infrastructure.Repositories.FileSetup.Process
{
    public class HelpSetUpRepository : IHelpSetUpRepository
    {
        private readonly TimekeepingContext _context;

        public HelpSetUpRepository(TimekeepingContext context)
        {
            _context = context;
        }

        public async Task<HelpSetUpDTO> InsertAsync(HelpSetUpDTO data)
        {
            _context.tk_HelpSetup.Add(data);
            await _context.SaveChangesAsync();
            return data;
        }

        public async Task<HelpSetUpDTO> UpdateAsync(HelpSetUpDTO data)
        {
            _context.tk_HelpSetup.Update(data);
            await _context.SaveChangesAsync();
            return data;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var data = await _context.tk_HelpSetup.FindAsync(id);
            if (data == null) return false;

            _context.tk_HelpSetup.Remove(data);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<HelpSetUpDTO?> GetByIdAsync(int id)
        {
            return await _context.tk_HelpSetup.FindAsync(id);
        }

        public async Task<List<HelpSetUpDTO>> GetAllAsync()
        {
            return await _context.tk_HelpSetup.ToListAsync();
        }
    }
}
