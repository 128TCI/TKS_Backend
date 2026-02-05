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
    public class CoordinatesSetUpService : ICoordinatesSetUpService
    {
        private readonly ICoordinatesSetUpRepository _repository;
        private readonly IEncryptionService _encryptionService; // Injected service

        public CoordinatesSetUpService(ICoordinatesSetUpRepository repository, IEncryptionService encryptionService)
        {
            _repository = repository;
            _encryptionService = encryptionService;
        }
        public async Task<CoordinatesSetUpDTO> CreateAsync(CoordinatesSetUpDTO dto, CancellationToken ct = default)
        {
            var user = MapToEntity(dto);

            // 6. Save to Database
            var result = await _repository.InsertAsync(user);

            return MapToDTO(result);
        }

        public async Task<CoordinatesSetUpDTO> UpdateAsync(CoordinatesSetUpDTO dto)
        {
            var user = MapToEntity(dto);

            var result = await _repository.UpdateAsync(user);
            return MapToDTO(result);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<CoordinatesSetUpDTO?> GetByIdAsync(int id)
        {
            var user = await _repository.GetByIdAsync(id);
            return user == null ? null : MapToDTO(user);
        }

        public async Task<List<CoordinatesSetUpDTO>> GetAllAsync()
        {
            var users = await _repository.GetAllAsync();

            if (users == null || !users.Any())
            {
                return new List<CoordinatesSetUpDTO>(); // Return empty list instead of null
            }

            return users.Select(MapToDTO).ToList();
        }

        // --- Mapping Logic ---
        private CoordinatesSetUpDTO MapToEntity(CoordinatesSetUpDTO dto)
        {
            return new CoordinatesSetUpDTO
            {
                ID = dto.ID,
                Code = dto.Code,
                Description = dto.Description,
                Longitude = dto.Longitude,
                Latitude = dto.Latitude,
                Distance = dto.Distance
            };
        }

        private CoordinatesSetUpDTO MapToDTO(CoordinatesSetUpDTO entity)
        {
            return new CoordinatesSetUpDTO
            {
                ID = entity.ID,
                Code = entity.Code?.Trim(),
                Description = entity.Description,
                Longitude = entity.Longitude,
                Latitude = entity.Latitude,
                Distance = entity.Distance
            };
        }
    }
}
