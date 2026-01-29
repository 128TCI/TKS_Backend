using DomainEntities.DTO.FileSetUp.Process;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces.FileSetUp.Process
{
    public interface IDayTypeSetUpService
    {
        Task<DayTypeSetUpDTO> CreateAsync(DayTypeSetUpDTO dto, CancellationToken ct = default); // Ensure 'ct' is here
        Task<DayTypeSetUpDTO> UpdateAsync(DayTypeSetUpDTO dto);
        Task<bool> DeleteAsync(int id);
        Task<DayTypeSetUpDTO?> GetByIdAsync(int id);
        Task<List<DayTypeSetUpDTO>> GetAllAsync();
    }
}
