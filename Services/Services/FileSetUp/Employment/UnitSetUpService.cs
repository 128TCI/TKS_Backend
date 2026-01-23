using DomainEntities.DTO.FileSetUp.Employment;
using Infrastructure.IRepositories.FileSetUp.Employment;
using Services.DTOs.FileSetup.Employment;
using Services.Interfaces.Encryption;
using Services.Interfaces.FileSetUp.Employment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.FileSetUp.Employment
{
    public class UnitSetUpService : IUnitSetUpService
    {
        private readonly IUnitSetUpRepository _repository;
        private readonly IEncryptionService _encryptionService; // Injected service

        public UnitSetUpService(IUnitSetUpRepository repository, IEncryptionService encryptionService)
        {
            _repository = repository;
            _encryptionService = encryptionService;
        }
        public async Task<UnitSetUpDTO> CreateAsync(UnitSetUpDTO dto, CancellationToken ct = default)
        {
            var user = MapToEntity(dto);

            // 6. Save to Database
            var result = await _repository.InsertAsync(user);

            return MapToDTO(result);
        }

        public async Task<UnitSetUpDTO> UpdateAsync(UnitSetUpDTO dto)
        {
            var user = MapToEntity(dto);

            var result = await _repository.UpdateAsync(user);
            return MapToDTO(result);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<UnitSetUpDTO?> GetByIdAsync(int id)
        {
            var user = await _repository.GetByIdAsync(id);
            return user == null ? null : MapToDTO(user);
        }

        public async Task<List<UnitSetUpDTO>> GetAllAsync()
        {
            var users = await _repository.GetAllAsync();

            if (users == null || !users.Any())
            {
                return new List<UnitSetUpDTO>(); // Return empty list instead of null
            }

            return users.Select(MapToDTO).ToList();
        }

        // --- Mapping Logic ---

        private UnitSetUp MapToEntity(UnitSetUpDTO dto)
        {
            return new UnitSetUp
            {
                UnitID = dto.UnitID,
                UnitCode = dto.UnitCode,
                UnitDesc = dto.UnitDesc,
                Head = dto.Head,
                Position = dto.Position,
                DeviceName = dto.DeviceName
            };
        }

        private UnitSetUpDTO MapToDTO(UnitSetUp entity)
        {
            return new UnitSetUpDTO
            {
                UnitID = entity.UnitID,
                UnitCode = entity.UnitCode,
                UnitDesc = entity.UnitDesc,
                Head = entity.Head,
                Position = entity.Position,
                DeviceName = entity.DeviceName
            };
        }
    }
}
