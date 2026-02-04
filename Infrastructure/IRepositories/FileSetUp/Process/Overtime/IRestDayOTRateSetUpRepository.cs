using DomainEntities.DTO.FileSetUp.Process.Device;
using DomainEntities.DTO.FileSetUp.Process.Overtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IRepositories.FileSetUp.Process.Overtime
{
    public interface IRestDayOTRateSetUpRepository
    {
        Task<RestDayOTRateSetUpDTO> InsertAsync(RestDayOTRateSetUpDTO data);
        Task<RestDayOTRateSetUpDTO> UpdateAsync(RestDayOTRateSetUpDTO data);
        Task<bool> DeleteAsync(int id);
        Task<RestDayOTRateSetUpDTO?> GetByIdAsync(int id);
        Task<List<RestDayOTRateSetUpDTO>> GetAllAsync();
    }
}
