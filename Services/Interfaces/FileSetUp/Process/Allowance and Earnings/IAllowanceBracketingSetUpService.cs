using DomainEntities.DTO.FileSetUp.Process.Allowance_and_Earnings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces.FileSetUp.Process.Allowance_and_Earnings
{
    public interface IAllowanceBracketingSetUpService
    {
        Task<AllowanceBracketingSetUpDTO> CreateAsync(AllowanceBracketingSetUpDTO dto, CancellationToken ct = default); // Ensure 'ct' is here
        Task<AllowanceBracketingSetUpDTO> UpdateAsync(AllowanceBracketingSetUpDTO dto);
        Task<bool> DeleteAsync(int id);
        Task<AllowanceBracketingSetUpDTO?> GetByIdAsync(int id);
        Task<List<AllowanceBracketingSetUpDTO>> GetAllAsync();
    }
}
