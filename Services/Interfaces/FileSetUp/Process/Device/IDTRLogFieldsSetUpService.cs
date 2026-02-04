using DomainEntities.DTO.FileSetUp.Process.Device;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces.FileSetUp.Process.Device
{
    public interface IDTRLogFieldsSetUpService
    {
        Task<DTRLogFieldsSetUpDTO> CreateAsync(DTRLogFieldsSetUpDTO dto, CancellationToken ct = default); // Ensure 'ct' is here
        Task<DTRLogFieldsSetUpDTO> UpdateAsync(DTRLogFieldsSetUpDTO dto);
        Task<bool> DeleteAsync(int id);
        Task<DTRLogFieldsSetUpDTO?> GetByIdAsync(int id);
        Task<List<DTRLogFieldsSetUpDTO>> GetAllAsync();
    }
}
