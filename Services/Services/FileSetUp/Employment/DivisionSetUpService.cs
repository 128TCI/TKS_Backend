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
    public class DivisionSetUpService : IDivisionSetUpService
    {
        private readonly IDivisionSetUpRepository _repository;
        private readonly IEncryptionService _encryptionService; // Injected service

        public DivisionSetUpService(IDivisionSetUpRepository repository, IEncryptionService encryptionService)
        {
            _repository = repository;
            _encryptionService = encryptionService;
        }
        public async Task<DivisionSetUpDTO> CreateAsync(DivisionSetUpDTO dto, CancellationToken ct = default)
        {
            var user = MapToEntity(dto);

            // 6. Save to Database
            var result = await _repository.InsertAsync(user);

            return MapToDTO(result);
        }

        public async Task<DivisionSetUpDTO> UpdateAsync(DivisionSetUpDTO dto)
        {
            var user = MapToEntity(dto);

            var result = await _repository.UpdateAsync(user);
            return MapToDTO(result);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<DivisionSetUpDTO?> GetByIdAsync(int id)
        {
            var user = await _repository.GetByIdAsync(id);
            return user == null ? null : MapToDTO(user);
        }

        public async Task<List<DivisionSetUpDTO>> GetAllAsync()
        {
            var users = await _repository.GetAllAsync();

            if (users == null || !users.Any())
            {
                return new List<DivisionSetUpDTO>(); // Return empty list instead of null
            }

            return users.Select(MapToDTO).ToList();
        }

        // --- Mapping Logic ---

        private DivisionSetUp MapToEntity(DivisionSetUpDTO dto)
        {
            return new DivisionSetUp
            {
                DivID = dto.DivID,
                DivCode = dto.DivCode,
                DivDesc = dto.DivDesc,
                DivHead = dto.DivHead,
                DivHeadCode = dto.DivHeadCode,
                DeviceName = dto.DeviceName
            };
        }

        private DivisionSetUpDTO MapToDTO(DivisionSetUp entity)
        {
            return new DivisionSetUpDTO
            {
                DivID = entity.DivID,
                DivCode = entity.DivCode,
                DivDesc = entity.DivDesc,
                DivHead = entity.DivHead,
                DivHeadCode = entity.DivHeadCode,
                DeviceName = entity.DeviceName
            };
        }
    }
}
