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
    public class SectionSetUpService : ISectionSetUpService
    {
        private readonly ISectionSetUpRepository _repository;
        private readonly IEncryptionService _encryptionService; // Injected service

        public SectionSetUpService(ISectionSetUpRepository repository, IEncryptionService encryptionService)
        {
            _repository = repository;
            _encryptionService = encryptionService;
        }
        public async Task<SectionSetUpDTO> CreateAsync(SectionSetUpDTO dto, CancellationToken ct = default)
        {
            var user = MapToEntity(dto);

            // 6. Save to Database
            var result = await _repository.InsertAsync(user);

            return MapToDTO(result);
        }

        public async Task<SectionSetUpDTO> UpdateAsync(SectionSetUpDTO dto)
        {
            var user = MapToEntity(dto);

            var result = await _repository.UpdateAsync(user);
            return MapToDTO(result);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<SectionSetUpDTO?> GetByIdAsync(int id)
        {
            var user = await _repository.GetByIdAsync(id);
            return user == null ? null : MapToDTO(user);
        }

        public async Task<List<SectionSetUpDTO>> GetAllAsync()
        {
            var users = await _repository.GetAllAsync();

            if (users == null || !users.Any())
            {
                return new List<SectionSetUpDTO>(); // Return empty list instead of null
            }

            return users.Select(MapToDTO).ToList();
        }

        // --- Mapping Logic ---

        private SectionSetUp MapToEntity(SectionSetUpDTO dto)
        {
            return new SectionSetUp
            {
                SecID = dto.SecID,
                SecCode = dto.SecCode,
                DepCode = dto.DepCode,
                SecDesc = dto.SecDesc,
                SecHead = dto.SecHead,
                SecHeadCode = dto.SecHeadCode,
                DeviceName = dto.DeviceName
            };
        }

        private SectionSetUpDTO MapToDTO(SectionSetUp entity)
        { 
            return new SectionSetUpDTO
            {
                SecID = entity.SecID,
                SecCode = entity.SecCode,
                DepCode = entity.DepCode,
                SecDesc = entity.SecDesc,
                SecHead = entity.SecHead,
                SecHeadCode = entity.SecHeadCode,
                DeviceName = entity.DeviceName
            };
        }
    }
}
