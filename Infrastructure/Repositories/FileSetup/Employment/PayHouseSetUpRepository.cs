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
    public class PayHouseSetUpRepository : IPayHouseSetUpRepository
    {
        private readonly TimekeepingContext _context;

        public PayHouseSetUpRepository(TimekeepingContext context)
        {
            _context = context;
        }

        public async Task<PayHouseSetUp> InsertAsync(PayHouseSetUp payHouseSetUp)
        {
            _context.tk_line.Add(payHouseSetUp);
            await _context.SaveChangesAsync();
            return payHouseSetUp;
        }

        public async Task<PayHouseSetUp> UpdateAsync(PayHouseSetUp payHouseSetUp)
        {
            _context.tk_line.Update(payHouseSetUp);
            await _context.SaveChangesAsync();
            return payHouseSetUp;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var payHouseSetUp = await _context.tk_line.FindAsync(id);
            if (payHouseSetUp == null) return false;

            _context.tk_line.Remove(payHouseSetUp);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<PayHouseSetUp?> GetByIdAsync(int id)
        {
            return await _context.tk_line.FindAsync(id);
        }

        public async Task<List<PayHouseSetUp>> GetAllAsync()
        {
            return await _context.tk_line.ToListAsync();
        }

    }
}
