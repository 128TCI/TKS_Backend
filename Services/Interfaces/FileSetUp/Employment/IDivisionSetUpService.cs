using Services.DTOs.FileSetup.Employment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces.FileSetUp.Employment
{
    public interface IDivisionSetUpService
    {
        Task<DivisionSetUpDTO> CreateAsync(DivisionSetUpDTO dto, CancellationToken ct = default); // Ensure 'ct' is here
        Task<DivisionSetUpDTO> UpdateAsync(DivisionSetUpDTO dto);
        Task<bool> DeleteAsync(int id);
        Task<DivisionSetUpDTO?> GetByIdAsync(int id);
        Task<List<DivisionSetUpDTO>> GetAllAsync();
    }
}
