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
    public class SDKListSetUpRepository : ISDKListSetUpRepository
    {
        private readonly TimekeepingContext _context;

        public SDKListSetUpRepository(TimekeepingContext context)
        {
            _context = context;
        }

        public async Task<SDKListSetUpDTO> InsertAsync(SDKListSetUpDTO data)
        {
            _context.tbl_SDKList.Add(data);
            await _context.SaveChangesAsync();
            return data;
        }

        public async Task<SDKListSetUpDTO> UpdateAsync(SDKListSetUpDTO data)
        {
            _context.tbl_SDKList.Update(data);
            await _context.SaveChangesAsync();
            return data;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var data = await _context.tbl_SDKList.FindAsync(id);
            if (data == null) return false;

            _context.tbl_SDKList.Remove(data);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<SDKListSetUpDTO?> GetByIdAsync(int id)
        {
            return await _context.tbl_SDKList.FindAsync(id);
        }

        public async Task<List<SDKListSetUpDTO>> GetAllAsync()
        {
            return await _context.tbl_SDKList.ToListAsync();
        }
    }
    }
