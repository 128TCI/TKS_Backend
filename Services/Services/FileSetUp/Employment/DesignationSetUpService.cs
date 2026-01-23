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
    public class DesignationSetUpService : IDesignationSetUpService
    {
        private readonly IDesignationSetUpRepository _repository;
        private readonly IEncryptionService _encryptionService; // Injected service

        public DesignationSetUpService(IDesignationSetUpRepository repository, IEncryptionService encryptionService)
        {
            _repository = repository;
            _encryptionService = encryptionService;
        }
        public async Task<DesignationSetUpDTO> CreateAsync(DesignationSetUpDTO dto, CancellationToken ct = default)
        {
            var user = MapToEntity(dto);

            // 6. Save to Database
            var result = await _repository.InsertAsync(user);

            return MapToDTO(result);
        }

        public async Task<DesignationSetUpDTO> UpdateAsync(DesignationSetUpDTO dto)
        {
            var user = MapToEntity(dto);

            var result = await _repository.UpdateAsync(user);
            return MapToDTO(result);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<DesignationSetUpDTO?> GetByIdAsync(int id)
        {
            var user = await _repository.GetByIdAsync(id);
            return user == null ? null : MapToDTO(user);
        }

        public async Task<List<DesignationSetUpDTO>> GetAllAsync()
        {
            var users = await _repository.GetAllAsync();

            if (users == null || !users.Any())
            {
                return new List<DesignationSetUpDTO>(); // Return empty list instead of null
            }

            return users.Select(MapToDTO).ToList();
        }

        // --- Mapping Logic ---

        private DesignationSetUp MapToEntity(DesignationSetUpDTO dto)
        {
            return new DesignationSetUp
            {
                DesID = dto.DesID,
                DesCode = dto.DesCode,
                DesDesc = dto.DesDesc,
                RateCode = dto.RateCode,
                ClsCode = dto.ClsCode,
                GrdCode = dto.GrdCode,
                JobLevelCode = dto.JobLevelCode,
                DeviceName = dto.DeviceName
            };
        }

        private DesignationSetUpDTO MapToDTO(DesignationSetUp entity)
        {
            return new DesignationSetUpDTO
            {
                DesID = entity.DesID,
                DesCode = entity.DesCode,
                DesDesc = entity.DesDesc,
                RateCode = entity.RateCode,
                ClsCode = entity.ClsCode,
                GrdCode = entity.GrdCode,
                JobLevelCode = entity.JobLevelCode,
                DeviceName = entity.DeviceName
            };
        }
    }
}
