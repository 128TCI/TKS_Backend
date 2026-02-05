using DomainEntities.DTO.FileSetUp.Process.Allowance_and_Earnings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces.FileSetUp.Process.Allowance_and_Earnings
{
    public interface IAllowancePerClassificationSetUpService
    {
        Task<AllowancePerClassificationSetUpDTO> CreateAsync(AllowancePerClassificationSetUpDTO dto, CancellationToken ct = default); // Ensure 'ct' is here
        Task<AllowancePerClassificationSetUpDTO> UpdateAsync(AllowancePerClassificationSetUpDTO dto);
        Task<bool> DeleteAsync(int id);
        Task<AllowancePerClassificationSetUpDTO?> GetByIdAsync(int id);
        Task<List<AllowancePerClassificationSetUpDTO>> GetAllAsync();
    }
}
