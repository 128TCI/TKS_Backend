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
    public class BranchSetUpService : IBranchSetUpService
    {
        private readonly IBranchSetUpRepository _repository;
        private readonly IEncryptionService _encryptionService; // Injected service

        public BranchSetUpService(IBranchSetUpRepository repository, IEncryptionService encryptionService)
        {
            _repository = repository;
            _encryptionService = encryptionService;
        }
        public async Task<BranchSetUpDTO> CreateAsync(BranchSetUpDTO dto, CancellationToken ct = default)
        {
            var user = MapToEntity(dto);

            // 6. Save to Database
            var result = await _repository.InsertAsync(user);

            return MapToDTO(result);
        }

        public async Task<BranchSetUpDTO> UpdateAsync(BranchSetUpDTO dto)
        {
            var user = MapToEntity(dto);

            var result = await _repository.UpdateAsync(user);
            return MapToDTO(result);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<BranchSetUpDTO?> GetByIdAsync(int id)
        {
            var user = await _repository.GetByIdAsync(id);
            return user == null ? null : MapToDTO(user);
        }

        public async Task<List<BranchSetUpDTO>> GetAllAsync()
        {
            var users = await _repository.GetAllAsync();

            if (users == null || !users.Any())
            {
                return new List<BranchSetUpDTO>(); // Return empty list instead of null
            }

            return users.Select(MapToDTO).ToList();
        }

        // --- Mapping Logic ---

        private BranchSetUp MapToEntity(BranchSetUpDTO dto)
        {
            return new BranchSetUp
            {
                BraID = dto.BraID,
                BraCode = dto.BraCode,
                BraDesc = dto.BraDesc,
                BraMngr = dto.BraMngr,
                BraMngrCode = dto.BraMngrCode,
                DeviceName = dto.DeviceName
            };
        }

        private BranchSetUpDTO MapToDTO(BranchSetUp entity)
        {
            return new BranchSetUpDTO
            {
                BraID = entity.BraID,
                BraCode = entity.BraCode,
                BraDesc = entity.BraDesc,
                BraMngr = entity.BraMngr,
                BraMngrCode = entity.BraMngrCode,
                DeviceName = entity.DeviceName
            };
        }
    }
}
