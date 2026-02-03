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
    public class DeviceTypeSetUpRepository : IDeviceTypeSetUpRepository
    {
        private readonly TimekeepingContext _context;

        public DeviceTypeSetUpRepository(TimekeepingContext context)
        {
            _context = context;
        }

        public async Task<DeviceTypeSetUpDTO> InsertAsync(DeviceTypeSetUpDTO data)
        {
            _context.tbl_fsDeviceType.Add(data);
            await _context.SaveChangesAsync();
            return data;
        }

        public async Task<DeviceTypeSetUpDTO> UpdateAsync(DeviceTypeSetUpDTO data)
        {
            _context.tbl_fsDeviceType.Update(data);
            await _context.SaveChangesAsync();
            return data;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var data = await _context.tbl_fsDeviceType.FindAsync(id);
            if (data == null) return false;

            _context.tbl_fsDeviceType.Remove(data);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<DeviceTypeSetUpDTO?> GetByIdAsync(int id)
        {
            return await _context.tbl_fsDeviceType.FindAsync(id);
        }

        public async Task<List<DeviceTypeSetUpDTO>> GetAllAsync()
        {
            return await _context.tbl_fsDeviceType.ToListAsync();
        }
        //DeviceType2
        public async Task<DeviceType2SetUpDTO> Insert2Async(DeviceType2SetUpDTO data)
        {
            _context.tbl_fsDeviceType2.Add(data);
            await _context.SaveChangesAsync();
            return data;
        }

        public async Task<DeviceType2SetUpDTO> Update2Async(DeviceType2SetUpDTO data)
        {
            _context.tbl_fsDeviceType2.Update(data);
            await _context.SaveChangesAsync();
            return data;
        }

        public async Task<bool> Delete2Async(int id)
        {
            var data = await _context.tbl_fsDeviceType2.FindAsync(id);
            if (data == null) return false;

            _context.tbl_fsDeviceType2.Remove(data);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<DeviceType2SetUpDTO?> GetById2Async(int id)
        {
            return await _context.tbl_fsDeviceType2.FindAsync(id);
        }

        public async Task<List<DeviceType2SetUpDTO>> GetAll2Async()
        {
            return await _context.tbl_fsDeviceType2.ToListAsync();
        }
    }
}
