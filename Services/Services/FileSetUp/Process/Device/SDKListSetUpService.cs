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
    public class SDKListSetUpService : ISDKListSetUpService
    {
        private readonly ISDKListSetUpRepository _repository;
        private readonly IEncryptionService _encryptionService; // Injected service

        public SDKListSetUpService(ISDKListSetUpRepository repository, IEncryptionService encryptionService)
        {
            _repository = repository;
            _encryptionService = encryptionService;
        }
        public async Task<SDKListSetUpDTO> CreateAsync(SDKListSetUpDTO dto, CancellationToken ct = default)
        {
            var user = MapToEntity(dto);

            // 6. Save to Database
            var result = await _repository.InsertAsync(user);

            return MapToDTO(result);
        }

        public async Task<SDKListSetUpDTO> UpdateAsync(SDKListSetUpDTO dto)
        {
            var user = MapToEntity(dto);

            var result = await _repository.UpdateAsync(user);
            return MapToDTO(result);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<SDKListSetUpDTO?> GetByIdAsync(int id)
        {
            var user = await _repository.GetByIdAsync(id);
            return user == null ? null : MapToDTO(user);
        }

        public async Task<List<SDKListSetUpDTO>> GetAllAsync()
        {
            var users = await _repository.GetAllAsync();

            if (users == null || !users.Any())
            {
                return new List<SDKListSetUpDTO>(); // Return empty list instead of null
            }

            return users.Select(MapToDTO).ToList();
        }

        // --- Mapping Logic ---
        private SDKListSetUpDTO MapToEntity(SDKListSetUpDTO dto)
        {
            return new SDKListSetUpDTO
            {
                ID = dto.ID,
                IPAdd = dto.IPAdd,
                Port = dto.Port,
                MachID = dto.MachID,
                wDeviceCode = dto.wDeviceCode,
                FlagCode = dto.FlagCode
            };
        }

        private SDKListSetUpDTO MapToDTO(SDKListSetUpDTO entity)
        {
            return new SDKListSetUpDTO
            {
                ID = entity.ID,
                IPAdd = entity.IPAdd?.Trim(),
                Port = entity.Port?.Trim(),
                MachID = entity.MachID,
                wDeviceCode = entity.wDeviceCode,
                FlagCode = entity.FlagCode
            };
        }
    }
}
