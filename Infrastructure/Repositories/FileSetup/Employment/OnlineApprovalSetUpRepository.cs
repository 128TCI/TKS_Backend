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
    public class OnlineApprovalSetUpRepository : IOnlineApprovalSetUpRepository
    {
        private readonly TimekeepingContext _context;

        public OnlineApprovalSetUpRepository(TimekeepingContext context)
        {
            _context = context;
        }

        public async Task<OnlineApprovalSetUp> InsertAsync(OnlineApprovalSetUp payHouseSetUp)
        {
            _context.tbl_fsOnlineApproval.Add(payHouseSetUp);
            await _context.SaveChangesAsync();
            return payHouseSetUp;
        }

        public async Task<OnlineApprovalSetUp> UpdateAsync(OnlineApprovalSetUp payHouseSetUp)
        {
            _context.tbl_fsOnlineApproval.Update(payHouseSetUp);
            await _context.SaveChangesAsync();
            return payHouseSetUp;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var payHouseSetUp = await _context.tbl_fsOnlineApproval.FindAsync(id);
            if (payHouseSetUp == null) return false;

            _context.tbl_fsOnlineApproval.Remove(payHouseSetUp);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<OnlineApprovalSetUp?> GetByIdAsync(int id)
        {
            return await _context.tbl_fsOnlineApproval.FindAsync(id);
        }

        public async Task<List<OnlineApprovalSetUp>> GetAllAsync()
        {
            return await _context.tbl_fsOnlineApproval.ToListAsync();
        }
    }
}
