using DomainEntities.DTO.FileSetUp.Employment;
using DomainEntities.DTO.FileSetUp.Process.Allowance_and_Earnings;
using Infrastructure.IRepositories.FileSetUp.Employment;
using Infrastructure.IRepositories.FileSetUp.Process.Alllowance_and_Earnings;
using Services.DTOs.FileSetup.Employment;
using Services.Interfaces.Encryption;
using Services.Interfaces.FileSetUp.Employment;
using Services.Interfaces.FileSetUp.Process.Allowance_and_Earnings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.FileSetUp.Process.Allowance_and_Earnings
{
    public class AllowanceBracketCodeSetUpService : IAllowanceBracketCodeSetUpService
    {
        private readonly IAllowanceBracketCodeSetUpRepository _repository;
        private readonly IEncryptionService _encryptionService; // Injected service

        public AllowanceBracketCodeSetUpService(IAllowanceBracketCodeSetUpRepository repository, IEncryptionService encryptionService)
        {
            _repository = repository;
            _encryptionService = encryptionService;
        }
        public async Task<AllowanceBracketCodeSetUpDTO> CreateAsync(AllowanceBracketCodeSetUpDTO dto, CancellationToken ct = default)
        {
            var user = MapToEntity(dto);

            // 6. Save to Database
            var result = await _repository.InsertAsync(user);

            return MapToDTO(result);
        }

        public async Task<AllowanceBracketCodeSetUpDTO> UpdateAsync(AllowanceBracketCodeSetUpDTO dto)
        {
            var user = MapToEntity(dto);

            var result = await _repository.UpdateAsync(user);
            return MapToDTO(result);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<AllowanceBracketCodeSetUpDTO?> GetByIdAsync(int id)
        {
            var user = await _repository.GetByIdAsync(id);
            return user == null ? null : MapToDTO(user);
        }

        public async Task<List<AllowanceBracketCodeSetUpDTO>> GetAllAsync()
        {
            var users = await _repository.GetAllAsync();

            if (users == null || !users.Any())
            {
                return new List<AllowanceBracketCodeSetUpDTO>(); // Return empty list instead of null
            }

            return users.Select(MapToDTO).ToList();
        }

        // --- Mapping Logic ---

        private AllowanceBracketCodeSetUpDTO MapToEntity(AllowanceBracketCodeSetUpDTO dto)
        {
            return new AllowanceBracketCodeSetUpDTO
            {
                Id = dto.Id,
                Code = dto.Code,
                Description = dto.Description,
            };
        }

        private AllowanceBracketCodeSetUpDTO MapToDTO(AllowanceBracketCodeSetUpDTO entity)
        {
            return new AllowanceBracketCodeSetUpDTO
            {
                Id = entity.Id,
                Code = entity.Code,
                Description = entity.Description,
            };
        }
    }
}
