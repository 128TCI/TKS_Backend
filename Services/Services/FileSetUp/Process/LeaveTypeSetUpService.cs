using DomainEntities.Dto;
using DomainEntities.DTO.FileSetUp.Process;
using Infrastructure.IRepositories.FileSetUp.Process;
using Services.Interfaces.Encryption;
using Services.Interfaces.FileSetUp.Process;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.FileSetUp.Process
{
    public class LeaveTypeSetUpService : ILeaveTypeSetUpService
    {
        private readonly ILeaveTypeSetUpRepository _repository;
        private readonly IEncryptionService _encryptionService; // Injected service

        public LeaveTypeSetUpService(ILeaveTypeSetUpRepository repository, IEncryptionService encryptionService)
        {
            _repository = repository;
            _encryptionService = encryptionService;
        }
        public async Task<LeaveTypesDto> CreateAsync(LeaveTypesDto dto, CancellationToken ct = default)
        {
            var user = MapToEntity(dto);

            // 6. Save to Database
            var result = await _repository.InsertAsync(user);

            return MapToDTO(result);
        }

        public async Task<LeaveTypesDto> UpdateAsync(LeaveTypesDto dto)
        {
            var user = MapToEntity(dto);

            var result = await _repository.UpdateAsync(user);
            return MapToDTO(result);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<LeaveTypesDto?> GetByIdAsync(int id)
        {
            var user = await _repository.GetByIdAsync(id);
            return user == null ? null : MapToDTO(user);
        }

        public async Task<List<LeaveTypesDto>> GetAllAsync()
        {
            var users = await _repository.GetAllAsync();

            if (users == null || !users.Any())
            {
                return new List<LeaveTypesDto>(); // Return empty list instead of null
            }

            return users.Select(MapToDTO).ToList();
        }

        // --- Mapping Logic ---
        private LeaveTypesDto MapToEntity(LeaveTypesDto dto)
        {
            return new LeaveTypesDto
            {
                LeaveID = dto.LeaveID,
                LeaveCode = dto.LeaveCode,
                LeaveDesc = dto.LeaveDesc,
                ChargeableTo = dto.ChargeableTo,
                WithPay = dto.WithPay,
                SubTypeRequired = dto.SubTypeRequired,
                BasedOnTenure = dto.BasedOnTenure,
                WithDateDuration = dto.WithDateDuration,
                NoBalance = dto.NoBalance,
                LegalFileAsLeave = dto.LegalFileAsLeave,
                SphFileAsLeave = dto.SphFileAsLeave,
                DbleLegalFileAsLeave = dto.DbleLegalFileAsLeave,
                Sph2FileAsLeave = dto.Sph2FileAsLeave,
                PrevYrLvCode = dto.PrevYrLvCode,
                NwhFileAsLeave = dto.NwhFileAsLeave,
                RequiredAdvanceFiling = dto.RequiredAdvanceFiling,
                ExemptFromAllowDeduction = dto.ExemptFromAllowDeduction
            };
        }

        private LeaveTypesDto MapToDTO(LeaveTypesDto entity)
        {
            return new LeaveTypesDto
            {
                LeaveID = entity.LeaveID,
                LeaveCode = entity.LeaveCode?.Trim(),
                LeaveDesc = entity.LeaveDesc,
                ChargeableTo = entity.ChargeableTo,
                WithPay = entity.WithPay,
                SubTypeRequired = entity.SubTypeRequired,
                BasedOnTenure = entity.BasedOnTenure,
                WithDateDuration = entity.WithDateDuration,
                NoBalance = entity.NoBalance,
                LegalFileAsLeave = entity.LegalFileAsLeave,
                SphFileAsLeave = entity.SphFileAsLeave,
                DbleLegalFileAsLeave = entity.DbleLegalFileAsLeave,
                Sph2FileAsLeave = entity.Sph2FileAsLeave,
                PrevYrLvCode = entity.PrevYrLvCode?.Trim(),
                NwhFileAsLeave = entity.NwhFileAsLeave,
                RequiredAdvanceFiling = entity.RequiredAdvanceFiling,
                ExemptFromAllowDeduction = entity.ExemptFromAllowDeduction
            };
        }
    }
}
