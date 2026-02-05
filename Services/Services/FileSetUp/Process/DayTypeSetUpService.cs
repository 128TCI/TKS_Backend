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
    public class DayTypeSetUpService : IDayTypeSetUpService
    {
        private readonly IDayTypeSetUpRepository _repository;
        private readonly IEncryptionService _encryptionService; // Injected service

        public DayTypeSetUpService(IDayTypeSetUpRepository repository, IEncryptionService encryptionService)
        {
            _repository = repository;
            _encryptionService = encryptionService;
        }
        public async Task<DayTypeSetUpDTO> CreateAsync(DayTypeSetUpDTO dto, CancellationToken ct = default)
        {
            var user = MapToEntity(dto);

            // 6. Save to Database
            var result = await _repository.InsertAsync(user);

            return MapToDTO(result);
        }

        public async Task<DayTypeSetUpDTO> UpdateAsync(DayTypeSetUpDTO dto)
        {
            var user = MapToEntity(dto);

            var result = await _repository.UpdateAsync(user);
            return MapToDTO(result);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<DayTypeSetUpDTO?> GetByIdAsync(int id)
        {
            var user = await _repository.GetByIdAsync(id);
            return user == null ? null : MapToDTO(user);
        }

        public async Task<List<DayTypeSetUpDTO>> GetAllAsync()
        {
            var users = await _repository.GetAllAsync();

            if (users == null || !users.Any())
            {
                return new List<DayTypeSetUpDTO>(); // Return empty list instead of null
            }

            return users.Select(MapToDTO).ToList();
        }

        // --- Mapping Logic ---
        private DayTypeSetUpDTO MapToEntity(DayTypeSetUpDTO dto)
        {
            return new DayTypeSetUpDTO
            {
                ID = dto.ID,
                RegularDay = dto.RegularDay,
                RestDay = dto.RestDay,
                LegalHoliday = dto.LegalHoliday,
                SpecialHoliday = dto.SpecialHoliday,
                LegalHolidayFallRestDay = dto.LegalHolidayFallRestDay,
                SpecialHolidayFallRestDay = dto.SpecialHolidayFallRestDay,
                DoubleLegalHoliday = dto.DoubleLegalHoliday,
                DoubleLegalHolidayFallRestday = dto.DoubleLegalHolidayFallRestday,
                SpecialHoliday2 = dto.SpecialHoliday2,
                SpecialHoliday2FallRestDay = dto.SpecialHoliday2FallRestDay,
                NonWorkingHoliday = dto.NonWorkingHoliday,
                NonWorkingHolidayFallRestDay = dto.NonWorkingHolidayFallRestDay
            };
        }

        private DayTypeSetUpDTO MapToDTO(DayTypeSetUpDTO entity)
        {
            return new DayTypeSetUpDTO
            {
                ID = entity.ID,
                RegularDay = entity.RegularDay?.Trim(),
                RestDay = entity.RestDay?.Trim(),
                LegalHoliday = entity.LegalHoliday?.Trim(),
                SpecialHoliday = entity.SpecialHoliday?.Trim(),
                LegalHolidayFallRestDay = entity.LegalHolidayFallRestDay?.Trim(),
                SpecialHolidayFallRestDay = entity.SpecialHolidayFallRestDay?.Trim(),
                DoubleLegalHoliday = entity.DoubleLegalHoliday?.Trim(),
                DoubleLegalHolidayFallRestday = entity.DoubleLegalHolidayFallRestday?.Trim(),
                SpecialHoliday2 = entity.SpecialHoliday2?.Trim(),
                SpecialHoliday2FallRestDay = entity.SpecialHoliday2FallRestDay?.Trim(),
                NonWorkingHoliday = entity.NonWorkingHoliday?.Trim(),
                NonWorkingHolidayFallRestDay = entity.NonWorkingHolidayFallRestDay?.Trim()
            };
        }
    }
}
