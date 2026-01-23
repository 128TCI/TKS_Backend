using DomainEntities.DTO.FileSetUp.Employment;
using Infrastructure.IRepositories.FileSetUp.Employment;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timekeeping.Infrastructure.Data;

namespace Infrastructure.Repositories.FileSetup.Employment
{
    public class EmployeeStatusSetUpRepository : IEmployeeStatusSetUpRepository
    {
        private readonly TimekeepingContext _context;

        public EmployeeStatusSetUpRepository(TimekeepingContext context)
        {
            _context = context;
        }

        public async Task<EmployeeStatusSetUp> InsertAsync(EmployeeStatusSetUp employeeStatustSetUp)
        {
            _context.tbl_fsEmployeeStatus.Add(employeeStatustSetUp);
            await _context.SaveChangesAsync();
            return employeeStatustSetUp;
        }

        public async Task<EmployeeStatusSetUp> UpdateAsync(EmployeeStatusSetUp employeeStatustSetUp)
        {
            _context.tbl_fsEmployeeStatus.Update(employeeStatustSetUp);
            await _context.SaveChangesAsync();
            return employeeStatustSetUp;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var employeeStatustSetUp = await _context.tbl_fsEmployeeStatus.FindAsync(id);
            if (employeeStatustSetUp == null) return false;

            _context.tbl_fsEmployeeStatus.Remove(employeeStatustSetUp);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<EmployeeStatusSetUp?> GetByIdAsync(int id)
        {
            return await _context.tbl_fsEmployeeStatus.FindAsync(id);
        }

        public async Task<List<EmployeeStatusSetUp>> GetAllAsync()
        {
            return await _context.tbl_fsEmployeeStatus.ToListAsync();
        }

    }
}
