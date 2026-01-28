using DomainEntities.DTO.FileSetUp.Process.Allowance_and_Earnings;
using Services.DTOs.FileSetup.Employment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces.FileSetUp.Process.Allowance_and_Earnings
{
    public interface IAllowanceBracketCodeSetUpService
    {
        Task<AllowanceBracketCodeSetUpDTO> CreateAsync(AllowanceBracketCodeSetUpDTO dto, CancellationToken ct = default); // Ensure 'ct' is here
        Task<AllowanceBracketCodeSetUpDTO> UpdateAsync(AllowanceBracketCodeSetUpDTO dto);
        Task<bool> DeleteAsync(int id);
        Task<AllowanceBracketCodeSetUpDTO?> GetByIdAsync(int id);
        Task<List<AllowanceBracketCodeSetUpDTO>> GetAllAsync();
    }
}
