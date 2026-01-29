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
    public class AllowancePerClassificationSetUpService : IAllowancePerClassificationSetUpService
    {
        private readonly IAllowancePerClassificationSetUpRepository _repository;
        private readonly IEncryptionService _encryptionService; // Injected service

        public AllowancePerClassificationSetUpService(IAllowancePerClassificationSetUpRepository repository, IEncryptionService encryptionService)
        {
            _repository = repository;
            _encryptionService = encryptionService;
        }
        public async Task<AllowancePerClassificationSetUpDTO> CreateAsync(AllowancePerClassificationSetUpDTO dto, CancellationToken ct = default)
        {
            var user = MapToEntity(dto);

            // 6. Save to Database
            var result = await _repository.InsertAsync(user);

            return MapToDTO(result);
        }

        public async Task<AllowancePerClassificationSetUpDTO> UpdateAsync(AllowancePerClassificationSetUpDTO dto)
        {
            var user = MapToEntity(dto);

            var result = await _repository.UpdateAsync(user);
            return MapToDTO(result);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<AllowancePerClassificationSetUpDTO?> GetByIdAsync(int id)
        {
            var user = await _repository.GetByIdAsync(id);
            return user == null ? null : MapToDTO(user);
        }

        public async Task<List<AllowancePerClassificationSetUpDTO>> GetAllAsync()
        {
            var users = await _repository.GetAllAsync();

            if (users == null || !users.Any())
            {
                return new List<AllowancePerClassificationSetUpDTO>(); // Return empty list instead of null
            }

            return users.Select(MapToDTO).ToList();
        }

        // --- Mapping Logic ---

        private AllowancePerClassificationSetUpDTO MapToEntity(AllowancePerClassificationSetUpDTO dto)
        {
            return new AllowancePerClassificationSetUpDTO
            {
                ID = dto.ID,
                RefNo = dto.RefNo,
                AllowanceCode = dto.AllowanceCode,
                WorkShiftCode = dto.WorkShiftCode,
                MinHrsRegDay = dto.MinHrsRegDay,
                MinAmtRegDay = dto.MinAmtRegDay,
                MaxHrsRegDay = dto.MaxHrsRegDay,
                MaxAmtRegDay = dto.MaxAmtRegDay,
                MinHrsRestDay = dto.MinHrsRestDay,
                MinAmtRestDay = dto.MinAmtRestDay,
                MaxHrsRestDay = dto.MaxHrsRestDay,
                MaxAmtRestDay = dto.MaxAmtRestDay,
                MinHrsHoliday = dto.MinHrsHoliday,
                MinAmtHoliday = dto.MinAmtHoliday,
                MaxHrsHoliday = dto.MaxHrsHoliday,
                MaxAmtHoliday = dto.MaxAmtHoliday,
                MinHrsHolidayRestDay = dto.MinHrsHolidayRestDay,
                MinAmountHolidayRestDay = dto.MinAmountHolidayRestDay,
                MaxHrsHolidayRestDay = dto.MaxHrsHolidayRestDay,
                MaxAmountHolidayRestDay = dto.MaxAmountHolidayRestDay,
                ClassificationCode = dto.ClassificationCode
            };
        }

        private AllowancePerClassificationSetUpDTO MapToDTO(AllowancePerClassificationSetUpDTO entity)
        {
            return new AllowancePerClassificationSetUpDTO
            {
                ID = entity.ID,
                RefNo = entity.RefNo?.Trim(),
                AllowanceCode = entity.AllowanceCode?.Trim(),
                WorkShiftCode = entity.WorkShiftCode?.Trim(),
                MinHrsRegDay = entity.MinHrsRegDay,
                MinAmtRegDay = entity.MinAmtRegDay,
                MaxHrsRegDay = entity.MaxHrsRegDay,
                MaxAmtRegDay = entity.MaxAmtRegDay,
                MinHrsRestDay = entity.MinHrsRestDay,
                MinAmtRestDay = entity.MinAmtRestDay,
                MaxHrsRestDay = entity.MaxHrsRestDay,
                MaxAmtRestDay = entity.MaxAmtRestDay,
                MinHrsHoliday = entity.MinHrsHoliday,
                MinAmtHoliday = entity.MinAmtHoliday,
                MaxHrsHoliday = entity.MaxHrsHoliday,
                MaxAmtHoliday = entity.MaxAmtHoliday,
                MinHrsHolidayRestDay = entity.MinHrsHolidayRestDay,
                MinAmountHolidayRestDay = entity.MinAmountHolidayRestDay,
                MaxHrsHolidayRestDay = entity.MaxHrsHolidayRestDay,
                MaxAmountHolidayRestDay = entity.MaxAmountHolidayRestDay,
                ClassificationCode = entity.ClassificationCode?.Trim()
            };
        }
    }
}
