using Services.DTOs.FileSetup.Employment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces.FileSetUp.Employment
{
    public interface IGroupSetUpService
    {
        Task<GroupSetUpDTO> CreateAsync(GroupSetUpDTO dto, CancellationToken ct = default); // Ensure 'ct' is here
        Task<GroupSetUpDTO> UpdateAsync(GroupSetUpDTO dto);
        Task<bool> DeleteAsync(int id);
        Task<GroupSetUpDTO?> GetByIdAsync(int id);
        Task<List<GroupSetUpDTO>> GetAllAsync();
    }
}
