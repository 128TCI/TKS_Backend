using Services.DTOs.FileSetup.Employment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces.FileSetUp.Employment
{
    public interface ILocationSetUpService
    {
        Task<LocationSetUpDTO> CreateAsync(LocationSetUpDTO dto, CancellationToken ct = default); // Ensure 'ct' is here
        Task<LocationSetUpDTO> UpdateAsync(LocationSetUpDTO dto);
        Task<bool> DeleteAsync(int id);
        Task<LocationSetUpDTO?> GetByIdAsync(int id);
        Task<List<LocationSetUpDTO>> GetAllAsync();
    }
}
