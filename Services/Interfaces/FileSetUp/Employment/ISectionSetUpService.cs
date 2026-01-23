using Services.DTOs.FileSetup.Employment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces.FileSetUp.Employment
{
    public interface ISectionSetUpService
    {
        Task<SectionSetUpDTO> CreateAsync(SectionSetUpDTO dto, CancellationToken ct = default); // Ensure 'ct' is here
        Task<SectionSetUpDTO> UpdateAsync(SectionSetUpDTO dto);
        Task<bool> DeleteAsync(int id);
        Task<SectionSetUpDTO?> GetByIdAsync(int id);
        Task<List<SectionSetUpDTO>> GetAllAsync();
    }
}
