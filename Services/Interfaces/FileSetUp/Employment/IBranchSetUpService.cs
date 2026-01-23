using Services.DTOs.FileSetup.Employment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces.FileSetUp.Employment
{
    public interface IBranchSetUpService
    {
        Task<BranchSetUpDTO> CreateAsync(BranchSetUpDTO dto, CancellationToken ct = default); // Ensure 'ct' is here
        Task<BranchSetUpDTO> UpdateAsync(BranchSetUpDTO dto);
        Task<bool> DeleteAsync(int id);
        Task<BranchSetUpDTO?> GetByIdAsync(int id);
        Task<List<BranchSetUpDTO>> GetAllAsync();
    }
}
