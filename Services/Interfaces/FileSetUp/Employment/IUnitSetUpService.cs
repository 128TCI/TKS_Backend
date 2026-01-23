using Services.DTOs.FileSetup.Employment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces.FileSetUp.Employment
{
    public interface IUnitSetUpService
    {
        Task<UnitSetUpDTO> CreateAsync(UnitSetUpDTO dto, CancellationToken ct = default); // Ensure 'ct' is here
        Task<UnitSetUpDTO> UpdateAsync(UnitSetUpDTO dto);
        Task<bool> DeleteAsync(int id);
        Task<UnitSetUpDTO?> GetByIdAsync(int id);
        Task<List<UnitSetUpDTO>> GetAllAsync();
    }
}
