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
    public class EarningsSetUpService : IEarningsSetUpService
    {
        private readonly IEarningsSetUpRepository _repository;
        private readonly IEncryptionService _encryptionService; // Injected service

        public EarningsSetUpService(IEarningsSetUpRepository repository, IEncryptionService encryptionService)
        {
            _repository = repository;
            _encryptionService = encryptionService;
        }
        public async Task<EarningSetUpDTO> CreateAsync(EarningSetUpDTO dto, CancellationToken ct = default)
        {
            var user = MapToEntity(dto);

            // 6. Save to Database
            var result = await _repository.InsertAsync(user);

            return MapToDTO(result);
        }

        public async Task<EarningSetUpDTO> UpdateAsync(EarningSetUpDTO dto)
        {
            var user = MapToEntity(dto);

            var result = await _repository.UpdateAsync(user);
            return MapToDTO(result);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<EarningSetUpDTO?> GetByIdAsync(int id)
        {
            var user = await _repository.GetByIdAsync(id);
            return user == null ? null : MapToDTO(user);
        }

        public async Task<List<EarningSetUpDTO>> GetAllAsync()
        {
            var users = await _repository.GetAllAsync();

            if (users == null || !users.Any())
            {
                return new List<EarningSetUpDTO>(); // Return empty list instead of null
            }

            return users.Select(MapToDTO).ToList();
        }

        // --- Mapping Logic ---

        private EarningSetUpDTO MapToEntity(EarningSetUpDTO dto)
        {
            return new EarningSetUpDTO
            {
                EarnID = dto.EarnID,
                EarnCode = dto.EarnCode,
                EarnDesc = dto.EarnDesc,
                EarnType = dto.EarnType,
                SysId = dto.SysId,
            };
        }

        private EarningSetUpDTO MapToDTO(EarningSetUpDTO entity)
        {
            return new EarningSetUpDTO
            {
                EarnID = entity.EarnID,
                EarnCode = entity.EarnCode,
                EarnDesc = entity.EarnDesc,
                EarnType = entity.EarnType,
                SysId = entity.SysId,
            };
        }
    }
}
