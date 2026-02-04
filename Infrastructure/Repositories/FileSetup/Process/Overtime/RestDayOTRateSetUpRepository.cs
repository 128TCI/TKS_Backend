using DomainEntities.Dto;
using DomainEntities.DTO.FileSetUp.Process.Device;
using DomainEntities.DTO.FileSetUp.Process.Overtime;
using Infrastructure.IRepositories.FileSetUp.Process.Device;
using Infrastructure.IRepositories.FileSetUp.Process.Overtime;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timekeeping.Infrastructure.Data;

namespace Infrastructure.Repositories.FileSetup.Process.Overtime
{
    public class RestDayOTRateSetUpRepository : IRestDayOTRateSetUpRepository
    {
        private readonly TimekeepingContext _context;

        public RestDayOTRateSetUpRepository(TimekeepingContext context)
        {
            _context = context;
        }

        public async Task<RestDayOTRateSetUpDTO> InsertAsync(RestDayOTRateSetUpDTO data)
        {
            _context.tk_RestDayOTRatesSetUp.Add(data);
            await _context.SaveChangesAsync();
            return data;
        }

        public async Task<RestDayOTRateSetUpDTO> UpdateAsync(RestDayOTRateSetUpDTO data)
        {
            _context.tk_RestDayOTRatesSetUp.Update(data);
            await _context.SaveChangesAsync();
            return data;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var data = await _context.tk_RestDayOTRatesSetUp.FindAsync(id);
            if (data == null) return false;

            _context.tk_RestDayOTRatesSetUp.Remove(data);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<RestDayOTRateSetUpDTO?> GetByIdAsync(int id)
        {
            return await _context.tk_RestDayOTRatesSetUp.FindAsync(id);
        }

        public async Task<List<RestDayOTRateSetUpDTO>> GetAllAsync()
        {
            return await _context.tk_RestDayOTRatesSetUp.ToListAsync();
        }
    }
}
