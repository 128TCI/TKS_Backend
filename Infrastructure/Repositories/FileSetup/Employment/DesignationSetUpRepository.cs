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
    public class DesignationSetUpRepository : IDesignationSetUpRepository
    {
        private readonly TimekeepingContext _context;

        public DesignationSetUpRepository(TimekeepingContext context)
        {
            _context = context;
        }

        public async Task<DesignationSetUp> InsertAsync(DesignationSetUp designationtSetUp)
        {
            _context.tbl_fsDesignation.Add(designationtSetUp);
            await _context.SaveChangesAsync();
            return designationtSetUp;
        }

        public async Task<DesignationSetUp> UpdateAsync(DesignationSetUp designationtSetUp)
        {
            _context.tbl_fsDesignation.Update(designationtSetUp);
            await _context.SaveChangesAsync();
            return designationtSetUp;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var designationtSetUp = await _context.tbl_fsDesignation.FindAsync(id);
            if (designationtSetUp == null) return false;

            _context.tbl_fsDesignation.Remove(designationtSetUp);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<DesignationSetUp?> GetByIdAsync(int id)
        {
            return await _context.tbl_fsDesignation.FindAsync(id);
        }

        public async Task<List<DesignationSetUp>> GetAllAsync()
        {
            return await _context.tbl_fsDesignation.ToListAsync();
        }

    }
}
