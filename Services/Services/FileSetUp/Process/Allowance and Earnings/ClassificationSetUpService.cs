using DomainEntities.DTO.FileSetUp.Process.Allowance_and_Earnings;
using Infrastructure.IRepositories.FileSetUp.Process.Alllowance_and_Earnings;
using Services.Interfaces.Encryption;
using Services.Interfaces.FileSetUp.Process.Allowance_and_Earnings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.FileSetUp.Process.Allowance_and_Earnings
{
    public class ClassificationSetUpService : IClassificationSetUpService
    {
        private readonly IClassificationSetUpRepository _repository;
        private readonly IEncryptionService _encryptionService; // Injected service

        public ClassificationSetUpService(IClassificationSetUpRepository repository, IEncryptionService encryptionService)
        {
            _repository = repository;
            _encryptionService = encryptionService;
        }
        public async Task<ClassificationSetUpDTO> CreateAsync(ClassificationSetUpDTO   dto, CancellationToken ct = default)
        {
            var user = MapToEntity(dto);

            // 6. Save to Database
            var result = await _repository.InsertAsync(user);

            return MapToDTO(result);
        }

        public async Task<ClassificationSetUpDTO> UpdateAsync(ClassificationSetUpDTO dto)
        {
            var user = MapToEntity(dto);

            var result = await _repository.UpdateAsync(user);
            return MapToDTO(result);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<ClassificationSetUpDTO?> GetByIdAsync(int id)
        {
            var user = await _repository.GetByIdAsync(id);
            return user == null ? null : MapToDTO(user);
        }

        public async Task<List<ClassificationSetUpDTO>> GetAllAsync()
        {
            var users = await _repository.GetAllAsync();

            if (users == null || !users.Any())
            {
                return new List<ClassificationSetUpDTO>(); // Return empty list instead of null
            }

            return users.Select(MapToDTO).ToList();
        }

        // --- Mapping Logic ---

        private ClassificationSetUpDTO MapToEntity(ClassificationSetUpDTO dto)
        {
            return new ClassificationSetUpDTO
            {
                ClassId = dto.ClassId,
                ClassCode = dto.ClassCode,
                ClassDesc = dto.ClassDesc,
            };
        }

        private ClassificationSetUpDTO MapToDTO(ClassificationSetUpDTO entity)
        {
            return new ClassificationSetUpDTO
            {
                ClassId = entity.ClassId,
                ClassCode = entity.ClassCode,
                ClassDesc = entity.ClassDesc,
            };
        }
    }
}
