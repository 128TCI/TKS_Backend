using DomainEntities.Dto;
using DomainEntities.DTO.FileSetUp.Process;
using Infrastructure.IRepositories.FileSetUp.Process;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timekeeping.Infrastructure.Data;

namespace Infrastructure.Repositories.FileSetup.Process
{
    public class LeaveTypeSetUpRepository : ILeaveTypeSetUpRepository
    {
        private readonly TimekeepingContext _context;

        public LeaveTypeSetUpRepository(TimekeepingContext context)
        {
            _context = context;
        }

        public async Task<LeaveTypesDto> InsertAsync(LeaveTypesDto data)
        {
            _context.tbl_fsLeaveType.Add(data);
            await _context.SaveChangesAsync();
            return data;
        }

        public async Task<LeaveTypesDto> UpdateAsync(LeaveTypesDto data)
        {
            _context.tbl_fsLeaveType.Update(data);
            await _context.SaveChangesAsync();
            return data;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var data = await _context.tbl_fsLeaveType.FindAsync(id);
            if (data == null) return false;

            _context.tbl_fsLeaveType.Remove(data);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<LeaveTypesDto?> GetByIdAsync(int id)
        {
            return await _context.tbl_fsLeaveType.FindAsync(id);
        }

        public async Task<List<LeaveTypesDto>> GetAllAsync()
        {
            return await _context.tbl_fsLeaveType.ToListAsync();
        }
    }
}
