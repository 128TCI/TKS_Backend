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
    public class LocationSetUpService : ILocationSetUpService
    {
        private readonly ILocationSetUpRepository _repository;
        private readonly IEncryptionService _encryptionService; // Injected service

        public LocationSetUpService(ILocationSetUpRepository repository, IEncryptionService encryptionService)
        {
            _repository = repository;
            _encryptionService = encryptionService;
        }
        public async Task<LocationSetUpDTO> CreateAsync(LocationSetUpDTO dto, CancellationToken ct = default)
        {
            var user = MapToEntity(dto);

            // 6. Save to Database
            var result = await _repository.InsertAsync(user);

            return MapToDTO(result);
        }

        public async Task<LocationSetUpDTO> UpdateAsync(LocationSetUpDTO dto)
        {
            var user = MapToEntity(dto);

            var result = await _repository.UpdateAsync(user);
            return MapToDTO(result);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<LocationSetUpDTO?> GetByIdAsync(int id)
        {
            var user = await _repository.GetByIdAsync(id);
            return user == null ? null : MapToDTO(user);
        }

        public async Task<List<LocationSetUpDTO>> GetAllAsync()
        {
            var users = await _repository.GetAllAsync();

            if (users == null || !users.Any())
            {
                return new List<LocationSetUpDTO>(); // Return empty list instead of null
            }

            return users.Select(MapToDTO).ToList();
        }

        // --- Mapping Logic ---

        private LocationSetUp MapToEntity(LocationSetUpDTO dto)
        {
            if (dto == null) return null;

            return new LocationSetUp
            {
                ID = dto.ID,
                LocationCode = dto.LocationCode,
                LocationDesc = dto.LocationDesc,
                Head = dto.Head,
                HeadCode = dto.HeadCode,
                AcctCode = dto.AcctCode,
                CreatedBy = dto.CreatedBy,
                CreatedDate = dto.CreatedDate,
                EditedBy = dto.EditedBy,
                EditedDate = dto.EditedDate,
                DeviceName = dto.DeviceName
            };
        }

        private LocationSetUpDTO MapToDTO(LocationSetUp entity)
        {
            if (entity == null) return null;

            return new LocationSetUpDTO
            {
                ID = entity.ID,
                LocationCode = entity.LocationCode,
                LocationDesc = entity.LocationDesc,
                Head = entity.Head,
                HeadCode = entity.HeadCode,
                AcctCode = entity.AcctCode,
                CreatedBy = entity.CreatedBy,
                CreatedDate = entity.CreatedDate,
                EditedBy = entity.EditedBy,
                EditedDate = entity.EditedDate,
                DeviceName = entity.DeviceName
            };
        }
    }
}
