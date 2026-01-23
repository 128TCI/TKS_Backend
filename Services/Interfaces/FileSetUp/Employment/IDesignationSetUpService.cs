using Services.DTOs.FileSetup.Employment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces.FileSetUp.Employment
{
    public interface IDesignationSetUpService
    {
        Task<DesignationSetUpDTO> CreateAsync(DesignationSetUpDTO dto, CancellationToken ct = default); // Ensure 'ct' is here
        Task<DesignationSetUpDTO> UpdateAsync(DesignationSetUpDTO dto);
        Task<bool> DeleteAsync(int id);
        Task<DesignationSetUpDTO?> GetByIdAsync(int id);
        Task<List<DesignationSetUpDTO>> GetAllAsync();
    }
}
