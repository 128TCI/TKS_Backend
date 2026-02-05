using DomainEntities.DTO.FileSetUp.Process.Device;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IRepositories.FileSetUp.Process.Device
{
    public interface ISDKListSetUpRepository
    {
        Task<SDKListSetUpDTO> InsertAsync(SDKListSetUpDTO data);
        Task<SDKListSetUpDTO> UpdateAsync(SDKListSetUpDTO data);
        Task<bool> DeleteAsync(int id);
        Task<SDKListSetUpDTO?> GetByIdAsync(int id);
        Task<List<SDKListSetUpDTO>> GetAllAsync();
    }
}
