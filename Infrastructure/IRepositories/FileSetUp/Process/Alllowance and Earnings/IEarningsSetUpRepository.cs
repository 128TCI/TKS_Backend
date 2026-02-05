using DomainEntities.DTO.FileSetUp.Process.Allowance_and_Earnings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IRepositories.FileSetUp.Process.Alllowance_and_Earnings
{
    public interface IEarningsSetUpRepository
    {
        Task<EarningSetUpDTO> InsertAsync(EarningSetUpDTO earningSetUp);
        Task<EarningSetUpDTO> UpdateAsync(EarningSetUpDTO earningSetUp);
        Task<bool> DeleteAsync(int id);
        Task<EarningSetUpDTO?> GetByIdAsync(int id);
        Task<List<EarningSetUpDTO>> GetAllAsync();
    }
}
