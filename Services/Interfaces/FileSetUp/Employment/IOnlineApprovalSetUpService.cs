using Services.DTOs.FileSetup.Employment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces.FileSetUp.Employment
{
    public interface IOnlineApprovalSetUpService
    {
        Task<OnlineApprovalSetUpDTO> CreateAsync(OnlineApprovalSetUpDTO dto, CancellationToken ct = default); // Ensure 'ct' is here
        Task<OnlineApprovalSetUpDTO> UpdateAsync(OnlineApprovalSetUpDTO dto);
        Task<bool> DeleteAsync(int id);
        Task<OnlineApprovalSetUpDTO?> GetByIdAsync(int id);
        Task<List<OnlineApprovalSetUpDTO>> GetAllAsync();
    }
}
