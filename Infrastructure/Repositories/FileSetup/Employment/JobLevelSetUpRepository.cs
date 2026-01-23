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
    public class JobLevelSetUpRepository : IJobLevelSetUpRepository
    {
        private readonly TimekeepingContext _context;

        public JobLevelSetUpRepository(TimekeepingContext context)
        {
            _context = context;
        }

        public async Task<JobLevelSetUp> InsertAsync(JobLevelSetUp jobLevelSetUp)
        {
            _context.tbl_fsJobLevel.Add(jobLevelSetUp);
            await _context.SaveChangesAsync();
            return jobLevelSetUp;
        }

        public async Task<JobLevelSetUp> UpdateAsync(JobLevelSetUp jobLevelSetUp)
        {
            _context.tbl_fsJobLevel.Update(jobLevelSetUp);
            await _context.SaveChangesAsync();
            return jobLevelSetUp;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var groupSetUp = await _context.tbl_fsJobLevel.FindAsync(id);
            if (groupSetUp == null) return false;

            _context.tbl_fsJobLevel.Remove(groupSetUp);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<JobLevelSetUp?> GetByIdAsync(int id)
        {
            return await _context.tbl_fsJobLevel.FindAsync(id);
        }

        public async Task<List<JobLevelSetUp>> GetAllAsync()
        {
            return await _context.tbl_fsJobLevel.ToListAsync();
        }

    }
}
