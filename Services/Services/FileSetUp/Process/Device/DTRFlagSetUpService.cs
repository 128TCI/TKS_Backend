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
    public class DTRFlagSetUpService : IDTRFlagSetUpService
    {
        private readonly IDTRFlagSetUpRepository _repository;
        private readonly IEncryptionService _encryptionService; // Injected service

        public DTRFlagSetUpService(IDTRFlagSetUpRepository repository, IEncryptionService encryptionService)
        {
            _repository = repository;
            _encryptionService = encryptionService;
        }
        public async Task<DTRFlagSetUpDTO> CreateAsync(DTRFlagSetUpDTO dto, CancellationToken ct = default)
        {
            var user = MapToEntity(dto);

            // 6. Save to Database
            var result = await _repository.InsertAsync(user);

            return MapToDTO(result);
        }

        public async Task<DTRFlagSetUpDTO> UpdateAsync(DTRFlagSetUpDTO dto)
        {
            var user = MapToEntity(dto);

            var result = await _repository.UpdateAsync(user);
            return MapToDTO(result);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<DTRFlagSetUpDTO?> GetByIdAsync(int id)
        {
            var user = await _repository.GetByIdAsync(id);
            return user == null ? null : MapToDTO(user);
        }

        public async Task<List<DTRFlagSetUpDTO>> GetAllAsync()
        {
            var users = await _repository.GetAllAsync();

            if (users == null || !users.Any())
            {
                return new List<DTRFlagSetUpDTO>(); // Return empty list instead of null
            }

            return users.Select(MapToDTO)
                .OrderBy(x => x.FlagCode)
                .ToList();
        }

        // --- Mapping Logic ---
        private DTRFlagSetUpDTO MapToEntity(DTRFlagSetUpDTO dto)
        {
            return new DTRFlagSetUpDTO
            {
                ID = dto.ID,
                FlagCode = dto.FlagCode,
                TimeIn = dto.TimeIn,
                TimeOut = dto.TimeOut,
                Break1Out = dto.Break1Out,
                Break1In = dto.Break1In,
                Break2Out = dto.Break2Out,
                Break2In = dto.Break2In,
                Break3Out = dto.Break3Out,
                Break3In = dto.Break3In
            };
        }

        private DTRFlagSetUpDTO MapToDTO(DTRFlagSetUpDTO entity)
        {
            return new DTRFlagSetUpDTO
            {
                ID = entity.ID,
                FlagCode = entity.FlagCode?.Trim(),
                TimeIn = entity.TimeIn,
                TimeOut = entity.TimeOut,
                Break1Out = entity.Break1Out,
                Break1In = entity.Break1In,
                Break2Out = entity.Break2Out,
                Break2In = entity.Break2In,
                Break3Out = entity.Break3Out,
                Break3In = entity.Break3In
            };
        }
    }
}
