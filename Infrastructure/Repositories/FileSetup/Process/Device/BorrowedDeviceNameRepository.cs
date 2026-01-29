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
    public class BorrowedDeviceNameRepository : IBorrowedDeviceNameRepositoy
    {
        private readonly TimekeepingContext _context;

        public BorrowedDeviceNameRepository(TimekeepingContext context)
        {
            _context = context;
        }

        public async Task<BorrowedDeviceNameDTO> InsertAsync(BorrowedDeviceNameDTO data)
        {
            _context.tbl_fsBorrowedDeviceName.Add(data);
            await _context.SaveChangesAsync();
            return data;
        }

        public async Task<BorrowedDeviceNameDTO> UpdateAsync(BorrowedDeviceNameDTO data)
        {
            _context.tbl_fsBorrowedDeviceName.Update(data);
            await _context.SaveChangesAsync();
            return data;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var data = await _context.tbl_fsBorrowedDeviceName.FindAsync(id);
            if (data == null) return false;

            _context.tbl_fsBorrowedDeviceName.Remove(data);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<BorrowedDeviceNameDTO?> GetByIdAsync(int id)
        {
            return await _context.tbl_fsBorrowedDeviceName.FindAsync(id);
        }

        public async Task<List<BorrowedDeviceNameDTO>> GetAllAsync()
        {
            return await _context.tbl_fsBorrowedDeviceName.ToListAsync();
        }
    }
}
