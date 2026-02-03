using DomainEntities.DTO.FileSetUp.Process;
using DomainEntities.DTO.FileSetUp.Process.Device;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces.FileSetUp.Process
{
    public interface IHelpSetUpService
    {
        Task<HelpSetUpDTO> CreateAsync(HelpSetUpDTO dto, CancellationToken ct = default); // Ensure 'ct' is here
        Task<HelpSetUpDTO> UpdateAsync(HelpSetUpDTO dto);
        Task<bool> DeleteAsync(int id);
        Task<HelpSetUpDTO?> GetByIdAsync(int id);
        Task<List<HelpSetUpDTO>> GetAllAsync();
    }
}
