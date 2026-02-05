using DomainEntities.DTO.FileSetUp.Process.Device;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces.FileSetUp.Process.Device
{
    public interface ICoordinatesSetUpService
    {
        Task<CoordinatesSetUpDTO> CreateAsync(CoordinatesSetUpDTO dto, CancellationToken ct = default); // Ensure 'ct' is here
        Task<CoordinatesSetUpDTO> UpdateAsync(CoordinatesSetUpDTO dto);
        Task<bool> DeleteAsync(int id);
        Task<CoordinatesSetUpDTO?> GetByIdAsync(int id);
        Task<List<CoordinatesSetUpDTO>> GetAllAsync();
    }
}
