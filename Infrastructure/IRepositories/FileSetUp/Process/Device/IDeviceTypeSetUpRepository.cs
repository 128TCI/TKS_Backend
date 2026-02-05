using Azure.Identity;
using DomainEntities.DTO.FileSetUp.Process.Device;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IRepositories.FileSetUp.Process.Device
{
    public interface IDeviceTypeSetUpRepository
    {
        Task<DeviceTypeSetUpDTO> InsertAsync(DeviceTypeSetUpDTO data);
        Task<DeviceTypeSetUpDTO> UpdateAsync(DeviceTypeSetUpDTO data);
        Task<bool> DeleteAsync(int id);
        Task<DeviceTypeSetUpDTO?> GetByIdAsync(int id);
        Task<List<DeviceTypeSetUpDTO>> GetAllAsync();
        //DeviceType2
        Task<DeviceType2SetUpDTO> Insert2Async(DeviceType2SetUpDTO data);
        Task<DeviceType2SetUpDTO> Update2Async(DeviceType2SetUpDTO data);
        Task<bool> Delete2Async(int ide);
        Task<DeviceType2SetUpDTO?> GetById2Async(int id);
        Task<List<DeviceType2SetUpDTO>> GetAll2Async();
    }
}
