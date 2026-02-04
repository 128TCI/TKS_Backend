using DomainEntities.DTO.FileSetUp.Process.Device;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces.FileSetUp.Process.Device
{
    public interface IDTRFlagSetUpService
    {
        Task<DTRFlagSetUpDTO> CreateAsync(DTRFlagSetUpDTO dto, CancellationToken ct = default); // Ensure 'ct' is here
        Task<DTRFlagSetUpDTO> UpdateAsync(DTRFlagSetUpDTO dto);
        Task<bool> DeleteAsync(int id);
        Task<DTRFlagSetUpDTO?> GetByIdAsync(int id);
        Task<List<DTRFlagSetUpDTO>> GetAllAsync();
    }
}
