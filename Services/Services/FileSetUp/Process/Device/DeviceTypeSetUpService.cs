using DomainEntities.DTO.FileSetUp.Process.Device;
using Infrastructure.IRepositories.FileSetUp.Process.Device;
using Services.Interfaces.Encryption;
using Services.Interfaces.FileSetUp.Process.Device;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.FileSetUp.Process.Device
{
    public class DeviceTypeSetUpService : IDeviceTypeSetUpService
    {
        private readonly IDeviceTypeSetUpRepository _repository;
        private readonly IEncryptionService _encryptionService; // Injected service

        public DeviceTypeSetUpService(IDeviceTypeSetUpRepository repository, IEncryptionService encryptionService)
        {
            _repository = repository;
            _encryptionService = encryptionService;
        }
        public async Task<DeviceTypeSetUpDTO> CreateAsync(DeviceTypeSetUpDTO dto, CancellationToken ct = default)
        {
            var user = MapToEntity(dto);

            // 6. Save to Database
            var result = await _repository.InsertAsync(user);

            return MapToDTO(result);
        }

        public async Task<DeviceTypeSetUpDTO> UpdateAsync(DeviceTypeSetUpDTO dto)
        {
            var user = MapToEntity(dto);

            var result = await _repository.UpdateAsync(user);
            return MapToDTO(result);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<DeviceTypeSetUpDTO?> GetByIdAsync(int id)
        {
            var user = await _repository.GetByIdAsync(id);
            return user == null ? null : MapToDTO(user);
        }

        public async Task<List<DeviceTypeSetUpDTO>> GetAllAsync()
        {
            var users = await _repository.GetAllAsync();

            if (users == null || !users.Any())
            {
                return new List<DeviceTypeSetUpDTO>(); // Return empty list instead of null
            }

            return users.Select(MapToDTO).ToList();
        }

        // --- Mapping Logic ---
        private DeviceTypeSetUpDTO MapToEntity(DeviceTypeSetUpDTO dto)
        {
            return new DeviceTypeSetUpDTO
            {
                DeviceName = dto.DeviceName,
              
            };
        }

        private DeviceTypeSetUpDTO MapToDTO(DeviceTypeSetUpDTO entity)
        {
            return new DeviceTypeSetUpDTO
            {
                DeviceName = entity.DeviceName,
              
            };
        }
        //DeviceType2
        public async Task<DeviceType2SetUpDTO> Create2Async(DeviceType2SetUpDTO dto, CancellationToken ct = default)
        {
            var user = MapToEntity2(dto);

            // 6. Save to Database
            var result = await _repository.Insert2Async(user);

            return MapToDTO2(result);
        }

        public async Task<DeviceType2SetUpDTO> Update2Async(DeviceType2SetUpDTO dto)
        {
            var user = MapToEntity2(dto);

            var result = await _repository.Update2Async(user);
            return MapToDTO2(result);
        }

        public async Task<bool> Delete2Async(int id)
        {
            return await _repository.Delete2Async(id);
        }

        public async Task<DeviceType2SetUpDTO?> GetById2Async(int id)
        {
            var user = await _repository.GetById2Async(id);
            return user == null ? null : MapToDTO2(user);
        }

        public async Task<List<DeviceType2SetUpDTO>> GetAll2Async()
        {
            var users = await _repository.GetAll2Async();

            if (users == null || !users.Any())
            {
                return new List<DeviceType2SetUpDTO>(); // Return empty list instead of null
            }

            return users.Select(MapToDTO2).ToList();
        }

        // --- Mapping Logic ---
        private DeviceType2SetUpDTO MapToEntity2(DeviceType2SetUpDTO dto)
        {
            return new DeviceType2SetUpDTO
            {
                ID = dto.ID,
                DeviceName = dto.DeviceName,

            };
        }

        private DeviceType2SetUpDTO MapToDTO2(DeviceType2SetUpDTO entity)
        {
            return new DeviceType2SetUpDTO
            {
                ID = entity.ID,
                DeviceName = entity.DeviceName,

            };
        }
    } 
}
