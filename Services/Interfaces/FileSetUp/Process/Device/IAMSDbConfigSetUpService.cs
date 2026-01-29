using DomainEntities.DTO.FileSetUp.Process;
using DomainEntities.DTO.FileSetUp.Process.Device;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces.FileSetUp.Process.Device
{
    public interface IAMSDbConfigSetUpService
    {
        Task<AMSDbConfigSetUpDTO> CreateAsync(AMSDbConfigSetUpDTO dto, CancellationToken ct = default); // Ensure 'ct' is here
        Task<AMSDbConfigSetUpDTO> UpdateAsync(AMSDbConfigSetUpDTO dto);
        Task<bool> DeleteAsync(int id);
        Task<AMSDbConfigSetUpDTO?> GetByIdAsync(int id);
        Task<List<AMSDbConfigSetUpDTO>> GetAllAsync();
    }
}
