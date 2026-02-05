using DomainEntities.DTO.FileSetUp.Process.Device;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IRepositories.FileSetUp.Process.Device
{
    public interface ICoordinatesSetUpRepository
    {
        Task<CoordinatesSetUpDTO> InsertAsync(CoordinatesSetUpDTO data);
        Task<CoordinatesSetUpDTO> UpdateAsync(CoordinatesSetUpDTO data);
        Task<bool> DeleteAsync(int id);
        Task<CoordinatesSetUpDTO?> GetByIdAsync(int id);
        Task<List<CoordinatesSetUpDTO>> GetAllAsync();
    }
}
