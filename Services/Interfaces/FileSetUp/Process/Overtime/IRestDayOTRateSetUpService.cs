using DomainEntities.Dto;
using DomainEntities.DTO.FileSetUp.Process.Overtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces.FileSetUp.Process.Overtime
{
    public interface IRestDayOTRateSetUpService
    {
        Task<RestDayOTRateSetUpDTO> CreateAsync(RestDayOTRateSetUpDTO dto, CancellationToken ct = default); // Ensure 'ct' is here
        Task<RestDayOTRateSetUpDTO> UpdateAsync(RestDayOTRateSetUpDTO dto);
        Task<bool> DeleteAsync(int id);
        Task<RestDayOTRateSetUpDTO?> GetByIdAsync(int id);
        Task<List<RestDayOTRateSetUpDTO>> GetAllAsync();
    }
}
