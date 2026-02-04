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
    public class DTRLogFieldsSetUpRepository : IDTRLogFieldSetUpRepository
    {
        private readonly TimekeepingContext _context;

        public DTRLogFieldsSetUpRepository(TimekeepingContext context)
        {
            _context = context;
        }

        public async Task<DTRLogFieldsSetUpDTO> InsertAsync(DTRLogFieldsSetUpDTO data)
        {
            _context.tk_DTRLogFields.Add(data);
            await _context.SaveChangesAsync();
            return data;
        }

        public async Task<DTRLogFieldsSetUpDTO> UpdateAsync(DTRLogFieldsSetUpDTO data)
        {
            _context.tk_DTRLogFields.Update(data);
            await _context.SaveChangesAsync();
            return data;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var data = await _context.tk_DTRLogFields.FindAsync(id);
            if (data == null) return false;

            _context.tk_DTRLogFields.Remove(data);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<DTRLogFieldsSetUpDTO?> GetByIdAsync(int id)
        {
            return await _context.tk_DTRLogFields.FindAsync(id);
        }

        public async Task<List<DTRLogFieldsSetUpDTO>> GetAllAsync()
        {
            return await _context.tk_DTRLogFields.ToListAsync();
        }
    }
}
