using DomainEntities.DTO.FileSetUp.Process.Allowance_and_Earnings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces.FileSetUp.Process.Allowance_and_Earnings
{
    public interface IEarningsSetUpService
    {
        Task<EarningSetUpDTO> CreateAsync(EarningSetUpDTO dto, CancellationToken ct = default); // Ensure 'ct' is here
        Task<EarningSetUpDTO> UpdateAsync(EarningSetUpDTO dto);
        Task<bool> DeleteAsync(int id);
        Task<EarningSetUpDTO?> GetByIdAsync(int id);
        Task<List<EarningSetUpDTO>> GetAllAsync();
    }
}
