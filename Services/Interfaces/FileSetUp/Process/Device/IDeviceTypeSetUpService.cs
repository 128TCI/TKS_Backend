using DomainEntities.DTO.FileSetUp.Process.Device;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces.FileSetUp.Process.Device
{
    public interface IDeviceTypeSetUpService
    {
        Task<DeviceTypeSetUpDTO> CreateAsync(DeviceTypeSetUpDTO dto, CancellationToken ct = default); // Ensure 'ct' is here
        Task<DeviceTypeSetUpDTO> UpdateAsync(DeviceTypeSetUpDTO dto);
        Task<bool> DeleteAsync(int id);
        Task<DeviceTypeSetUpDTO?> GetByIdAsync(int id);
        Task<List<DeviceTypeSetUpDTO>> GetAllAsync();
        //DeviceType2
        Task<DeviceType2SetUpDTO> Create2Async(DeviceType2SetUpDTO dto, CancellationToken ct = default); // Ensure 'ct' is here
        Task<DeviceType2SetUpDTO> Update2Async(DeviceType2SetUpDTO dto);
        Task<bool> Delete2Async(int id);
        Task<DeviceType2SetUpDTO?> GetById2Async(int id);
        Task<List<DeviceType2SetUpDTO>> GetAll2Async();
    }
}
