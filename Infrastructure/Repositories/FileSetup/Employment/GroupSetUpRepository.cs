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
    public class GroupSetUpRepository : IGroupSetUpRepository
    {
        private readonly TimekeepingContext _context;

        public GroupSetUpRepository(TimekeepingContext context)
        {
            _context = context;
        }

        public async Task<GroupSetUp> InsertAsync(GroupSetUp groupSetUp)
        {
            _context.tbl_fsGroup.Add(groupSetUp);
            await _context.SaveChangesAsync();
            return groupSetUp;
        }

        public async Task<GroupSetUp> UpdateAsync(GroupSetUp groupSetUp)
        {
            _context.tbl_fsGroup.Update(groupSetUp);
            await _context.SaveChangesAsync();
            return groupSetUp;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var groupSetUp = await _context.tbl_fsGroup.FindAsync(id);
            if (groupSetUp == null) return false;

            _context.tbl_fsGroup.Remove(groupSetUp);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<GroupSetUp?> GetByIdAsync(int id)
        {
            return await _context.tbl_fsGroup.FindAsync(id);
        }

        public async Task<List<GroupSetUp>> GetAllAsync()
        {
            return await _context.tbl_fsGroup.ToListAsync();
        }

    }
}
