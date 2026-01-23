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
    public class OnlineApprovalSetUpService : IOnlineApprovalSetUpService
    {
        private readonly IOnlineApprovalSetUpRepository _repository;
        private readonly IEncryptionService _encryptionService; // Injected service

        public OnlineApprovalSetUpService(IOnlineApprovalSetUpRepository repository, IEncryptionService encryptionService)
        {
            _repository = repository;
            _encryptionService = encryptionService;
        }
        public async Task<OnlineApprovalSetUpDTO> CreateAsync(OnlineApprovalSetUpDTO dto, CancellationToken ct = default)
        {
            var user = MapToEntity(dto);

            // 6. Save to Database
            var result = await _repository.InsertAsync(user);

            return MapToDTO(result);
        }

        public async Task<OnlineApprovalSetUpDTO> UpdateAsync(OnlineApprovalSetUpDTO dto)
        {
            var user = MapToEntity(dto);

            var result = await _repository.UpdateAsync(user);
            return MapToDTO(result);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<OnlineApprovalSetUpDTO?> GetByIdAsync(int id)
        {
            var user = await _repository.GetByIdAsync(id);
            return user == null ? null : MapToDTO(user);
        }

        public async Task<List<OnlineApprovalSetUpDTO>> GetAllAsync()
        {
            var users = await _repository.GetAllAsync();

            if (users == null || !users.Any())
            {
                return new List<OnlineApprovalSetUpDTO>(); // Return empty list instead of null
            }

            return users.Select(MapToDTO).ToList();
        }

        // --- Mapping Logic ---

        private OnlineApprovalSetUp MapToEntity(OnlineApprovalSetUpDTO dto)
        {
            return new OnlineApprovalSetUp
            {
                ID = dto.ID,
                OnlineAppCode = dto.OnlineAppCode,
                OnlineAppDesc = dto.OnlineAppDesc,
                OnlineAppMngr = dto.OnlineAppMngr,
                CreatedDate = dto.CreatedDate,
                CreatedBy = dto.CreatedBy,
                EditedDate = dto.EditedDate,
                EditedBy = dto.EditedBy,
                DeviceName = dto.DeviceName
            };
        }

        private OnlineApprovalSetUpDTO MapToDTO(OnlineApprovalSetUp entity)
        {
            return new OnlineApprovalSetUpDTO
            {
                ID = entity.ID,
                OnlineAppCode = entity.OnlineAppCode,
                OnlineAppDesc = entity.OnlineAppDesc,
                OnlineAppMngr = entity.OnlineAppMngr,
                CreatedDate = entity.CreatedDate,
                CreatedBy = entity.CreatedBy,
                EditedDate = entity.EditedDate,
                EditedBy = entity.EditedBy,
                DeviceName = entity.DeviceName
            };
        }
    }
}
