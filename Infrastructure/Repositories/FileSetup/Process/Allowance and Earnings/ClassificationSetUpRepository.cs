using DomainEntities.DTO.FileSetUp.Process.Allowance_and_Earnings;
using Infrastructure.IRepositories.FileSetUp.Process.Alllowance_and_Earnings;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timekeeping.Infrastructure.Data;

namespace Infrastructure.Repositories.FileSetup.Process.Allowance_and_Earnings
{
    public class ClassificationSetUpRepository : IClassificationSetUpRepository
    {
        private readonly TimekeepingContext _context;

        public ClassificationSetUpRepository(TimekeepingContext context)
        {
            _context = context;
        }

        public async Task<ClassificationSetUpDTO> InsertAsync(ClassificationSetUpDTO classificationSetUp)
        {
            _context.tk_Classification.Add(classificationSetUp);
            await _context.SaveChangesAsync();
            return classificationSetUp;
        }

        public async Task<ClassificationSetUpDTO> UpdateAsync(ClassificationSetUpDTO classificationSetUp)
        {
            _context.tk_Classification.Update(classificationSetUp);
            await _context.SaveChangesAsync();
            return classificationSetUp;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var classificationSetUp = await _context.tk_Classification.FindAsync(id);
            if (classificationSetUp == null) return false;

            _context.tk_Classification.Remove(classificationSetUp);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<ClassificationSetUpDTO?> GetByIdAsync(int id)
        {
            return await _context.tk_Classification.FindAsync(id);
        }

        public async Task<List<ClassificationSetUpDTO>> GetAllAsync()
        {
            return await _context.tk_Classification.ToListAsync();
        }
    }
}
