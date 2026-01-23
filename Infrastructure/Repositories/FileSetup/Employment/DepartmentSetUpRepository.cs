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
    public class DepartmentSetUpRepository : IDepartmentSetUpRepository
    {
        private readonly TimekeepingContext _context;

        public DepartmentSetUpRepository(TimekeepingContext context)
        {
            _context = context;
        }

        public async Task<DepartmentSetUp> InsertAsync(DepartmentSetUp departmentSetUp)
        {
            _context.tbl_fsDepartment.Add(departmentSetUp);
            await _context.SaveChangesAsync();
            return departmentSetUp;
        }

        public async Task<DepartmentSetUp> UpdateAsync(DepartmentSetUp departmentSetUp)
        {
            _context.tbl_fsDepartment.Update(departmentSetUp);
            await _context.SaveChangesAsync();
            return departmentSetUp;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var departmentSetUp = await _context.tbl_fsDepartment.FindAsync(id);
            if (departmentSetUp == null) return false;

            _context.tbl_fsDepartment.Remove(departmentSetUp);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<DepartmentSetUp?> GetByIdAsync(int id)
        {
            return await _context.tbl_fsDepartment.FindAsync(id);
        }

        public async Task<List<DepartmentSetUp>> GetAllAsync()
        {
            return await _context.tbl_fsDepartment.ToListAsync();
        }

    }
}
