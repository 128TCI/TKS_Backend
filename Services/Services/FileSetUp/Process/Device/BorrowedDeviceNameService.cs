using DomainEntities.DTO.FileSetUp.Employment;
using DomainEntities.DTO.FileSetUp.Process.Device;
using Infrastructure.IRepositories.FileSetUp.Process.Device;
using Services.DTOs.FileSetup.Employment;
using Services.Interfaces.Encryption;
using Services.Interfaces.FileSetUp.Process.Device;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.FileSetUp.Process.Device
{
    public class BorrowedDeviceNameService : IBorrowedDeviceNameService
    {
        private readonly IBorrowedDeviceNameRepositoy _repository;
        private readonly IEncryptionService _encryptionService; // Injected service

        public BorrowedDeviceNameService(IBorrowedDeviceNameRepositoy repository, IEncryptionService encryptionService)
        {
            _repository = repository;
            _encryptionService = encryptionService;
        }
        public async Task<BorrowedDeviceNameDTO> CreateAsync(BorrowedDeviceNameDTO dto, CancellationToken ct = default)
        {
            var user = MapToEntity(dto);

            // 6. Save to Database
            var result = await _repository.InsertAsync(user);

            return MapToDTO(result);
        }

        public async Task<BorrowedDeviceNameDTO> UpdateAsync(BorrowedDeviceNameDTO dto)
        {
            var user = MapToEntity(dto);

            var result = await _repository.UpdateAsync(user);
            return MapToDTO(result);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<BorrowedDeviceNameDTO?> GetByIdAsync(int id)
        {
            var user = await _repository.GetByIdAsync(id);
            return user == null ? null : MapToDTO(user);
        }

        public async Task<List<BorrowedDeviceNameDTO>> GetAllAsync()
        {
            var users = await _repository.GetAllAsync();

            if (users == null || !users.Any())
            {
                return new List<BorrowedDeviceNameDTO>(); // Return empty list instead of null
            }

            return users.Select(MapToDTO).ToList();
        }

        // --- Mapping Logic ---
        private BorrowedDeviceNameDTO MapToEntity(BorrowedDeviceNameDTO dto)
        {
            return new BorrowedDeviceNameDTO
            {
                ID = dto.ID,
                Code = dto.Code,
                Description = dto.Description
            };
        }

        private BorrowedDeviceNameDTO MapToDTO(BorrowedDeviceNameDTO entity)
        {
            return new BorrowedDeviceNameDTO
            {
                ID = entity.ID,
                Code = entity.Code?.Trim(),
                Description = entity.Description,
            };
        }
    }
}
