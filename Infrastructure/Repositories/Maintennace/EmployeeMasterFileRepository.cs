using DomainEntities.DTO.FileSetUp.Employment;
using DomainEntities.DTO.Maintenance;
using Infrastructure.IRepositories.FileSetUp.Employment;
using Infrastructure.IRepositories.Maintenance;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timekeeping.Infrastructure.Data;

namespace Infrastructure.Repositories.Maintennace
{
    public class EmployeeMasterFileRepository : IEmployeeMasterFileRepository
    {
        private readonly TimekeepingContext _context;

        public EmployeeMasterFileRepository(TimekeepingContext context)
        {
            _context = context;
        }

        public async Task<EmployeeMasterFile> InsertAsync(EmployeeMasterFile employeeMasterFile)
        {
            _context.tbl_fmEmpMasterFile.Add(employeeMasterFile);
            await _context.SaveChangesAsync();
            return employeeMasterFile;
        }

        public async Task<EmployeeMasterFile> UpdateAsync(EmployeeMasterFile employeeMasterFile)
        {
            _context.tbl_fmEmpMasterFile.Update(employeeMasterFile);
            await _context.SaveChangesAsync();
            return employeeMasterFile;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var employeeMasterFile = await _context.tbl_fmEmpMasterFile.FindAsync(id);
            if (employeeMasterFile == null) return false;

            _context.tbl_fmEmpMasterFile.Remove(employeeMasterFile);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<EmployeeMasterFile?> GetByIdAsync(int id)
        {
            return await _context.tbl_fmEmpMasterFile.FindAsync(id);
        }

        public async Task<List<EmployeeMasterFile>> GetAllAsync()
        {
            return await _context.tbl_fmEmpMasterFile.ToListAsync();
        }
    }
}
