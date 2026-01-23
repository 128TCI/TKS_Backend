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
    public class GroupSetUpService : IGroupSetUpService
    {
        private readonly IGroupSetUpRepository _repository;
        private readonly IEncryptionService _encryptionService; // Injected service

        public GroupSetUpService(IGroupSetUpRepository repository, IEncryptionService encryptionService)
        {
            _repository = repository;
            _encryptionService = encryptionService;
        }
        public async Task<GroupSetUpDTO> CreateAsync(GroupSetUpDTO dto, CancellationToken ct = default)
        {
            var user = MapToEntity(dto);

            // 6. Save to Database
            var result = await _repository.InsertAsync(user);

            return MapToDTO(result);
        }

        public async Task<GroupSetUpDTO> UpdateAsync(GroupSetUpDTO dto)
        {
            var user = MapToEntity(dto);

            var result = await _repository.UpdateAsync(user);
            return MapToDTO(result);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<GroupSetUpDTO?> GetByIdAsync(int id)
        {
            var user = await _repository.GetByIdAsync(id);
            return user == null ? null : MapToDTO(user);
        }

        public async Task<List<GroupSetUpDTO>> GetAllAsync()
        {
            var users = await _repository.GetAllAsync();

            if (users == null || !users.Any())
            {
                return new List<GroupSetUpDTO>(); // Return empty list instead of null
            }

            return users.Select(MapToDTO).ToList();
        }

        // --- Mapping Logic ---

        private GroupSetUp MapToEntity(GroupSetUpDTO dto)
        {
            return new GroupSetUp
            {
                ID = dto.ID,
                GrpCode = dto.GrpCode,
                GrpDesc = dto.GrpDesc,
                GrpHead = dto.GrpHead,
                GrpDesig = dto.GrpDesig,
                GrpHeadCode = dto.GrpHeadCode
            };
        }

        private GroupSetUpDTO MapToDTO(GroupSetUp entity)
        {
            return new GroupSetUpDTO
            {
                ID = entity.ID,
                GrpCode = entity.GrpCode,
                GrpDesc = entity.GrpDesc,
                GrpHead = entity.GrpHead,
                GrpDesig = entity.GrpDesig,
                GrpHeadCode = entity.GrpHeadCode
            };
        }
    }
}
