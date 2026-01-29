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
    public class AllowanceBracketingSetUpService : IAllowanceBracketingSetUpService
    {
        private readonly IAllowanceBracketingSetUpRepository _repository;
        private readonly IEncryptionService _encryptionService; // Injected service

        public AllowanceBracketingSetUpService(IAllowanceBracketingSetUpRepository repository, IEncryptionService encryptionService)
        {
            _repository = repository;
            _encryptionService = encryptionService;
        }
        public async Task<AllowanceBracketingSetUpDTO> CreateAsync(AllowanceBracketingSetUpDTO dto, CancellationToken ct = default)
        {
            var user = MapToEntity(dto);

            // 6. Save to Database
            var result = await _repository.InsertAsync(user);

            return MapToDTO(result);
        }

        public async Task<AllowanceBracketingSetUpDTO> UpdateAsync(AllowanceBracketingSetUpDTO dto)
        {
            var user = MapToEntity(dto);

            var result = await _repository.UpdateAsync(user);
            return MapToDTO(result);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<AllowanceBracketingSetUpDTO?> GetByIdAsync(int id)
        {
            var user = await _repository.GetByIdAsync(id);
            return user == null ? null : MapToDTO(user);
        }

        public async Task<List<AllowanceBracketingSetUpDTO>> GetAllAsync()
        {
            var users = await _repository.GetAllAsync();

            if (users == null || !users.Any())
            {
                return new List<AllowanceBracketingSetUpDTO>(); // Return empty list instead of null
            }

            return users.Select(MapToDTO).ToList();
        }

        // --- Mapping Logic ---
        private AllowanceBracketingSetUpDTO MapToEntity(AllowanceBracketingSetUpDTO dto)
        {
            return new AllowanceBracketingSetUpDTO
            {
                ID = dto.ID,
                DayType = dto.DayType,
                NoOfHrs = dto.NoOfHrs,
                EarningCode = dto.EarningCode,
                Amount = dto.Amount,
                Code = dto.Code,
                WorkShiftCode = dto.WorkShiftCode,
                ByEmploymentStatFlag = dto.ByEmploymentStatFlag,
                EmploymentStatus = dto.EmploymentStatus
            };
        }

        private AllowanceBracketingSetUpDTO MapToDTO(AllowanceBracketingSetUpDTO entity)
        {
            return new AllowanceBracketingSetUpDTO
            {
                ID = entity.ID,
                DayType = entity.DayType,
                NoOfHrs = entity.NoOfHrs,
                EarningCode = entity.EarningCode,
                Amount = entity.Amount,
                Code = entity.Code,
                WorkShiftCode = entity.WorkShiftCode,
                ByEmploymentStatFlag = entity.ByEmploymentStatFlag,
                EmploymentStatus = entity.EmploymentStatus
            };
        }
    }
}
