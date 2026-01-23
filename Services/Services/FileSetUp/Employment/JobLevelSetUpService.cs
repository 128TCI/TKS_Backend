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
    public class JobLevelSetUpService : IJobLevelSetUpService
    {
        private readonly IJobLevelSetUpRepository _repository;
        private readonly IEncryptionService _encryptionService; // Injected service

        public JobLevelSetUpService(IJobLevelSetUpRepository repository, IEncryptionService encryptionService)
        {
            _repository = repository;
            _encryptionService = encryptionService;
        }
        public async Task<JobLevelSetUpDTO> CreateAsync(JobLevelSetUpDTO dto, CancellationToken ct = default)
        {
            var user = MapToEntity(dto);

            // 6. Save to Database
            var result = await _repository.InsertAsync(user);

            return MapToDTO(result);
        }

        public async Task<JobLevelSetUpDTO> UpdateAsync(JobLevelSetUpDTO dto)
        {
            var user = MapToEntity(dto);

            var result = await _repository.UpdateAsync(user);
            return MapToDTO(result);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<JobLevelSetUpDTO?> GetByIdAsync(int id)
        {
            var user = await _repository.GetByIdAsync(id);
            return user == null ? null : MapToDTO(user);
        }

        public async Task<List<JobLevelSetUpDTO>> GetAllAsync()
        {
            var users = await _repository.GetAllAsync();

            if (users == null || !users.Any())
            {
                return new List<JobLevelSetUpDTO>(); // Return empty list instead of null
            }

            return users.Select(MapToDTO).ToList();
        }

        // --- Mapping Logic ---

        private JobLevelSetUp MapToEntity(JobLevelSetUpDTO dto)
        {
            return new JobLevelSetUp
            {
               JobLevelID = dto.JobLevelID,
               JobLevelCode = dto.JobLevelCode, 
               JobLevelDesc = dto.JobLevelDesc,
            };
        }

        private JobLevelSetUpDTO MapToDTO(JobLevelSetUp entity)
        {
            return new JobLevelSetUpDTO
            {
               JobLevelID = entity.JobLevelID,
               JobLevelCode = entity.JobLevelCode,
               JobLevelDesc = entity.JobLevelDesc,
            };
        }
    }
}
