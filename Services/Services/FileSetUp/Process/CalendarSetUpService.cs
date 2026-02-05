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
    public class CalendarSetUpService : ICalendarSetUpService
    {
        private readonly ICalendarSetUpRepository _repository;
        private readonly IEncryptionService _encryptionService; // Injected service

        public CalendarSetUpService(ICalendarSetUpRepository repository, IEncryptionService encryptionService)
        {
            _repository = repository;
            _encryptionService = encryptionService;
        }
        public async Task<CalendarSetUpDTO> CreateAsync(CalendarSetUpDTO dto, CancellationToken ct = default)
        {
            var user = MapToEntity(dto);

            // 6. Save to Database
            var result = await _repository.InsertAsync(user);

            return MapToDTO(result);
        }

        public async Task<CalendarSetUpDTO> UpdateAsync(CalendarSetUpDTO dto)
        {
            var user = MapToEntity(dto);

            var result = await _repository.UpdateAsync(user);
            return MapToDTO(result);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<CalendarSetUpDTO?> GetByIdAsync(int id)
        {
            var user = await _repository.GetByIdAsync(id);
            return user == null ? null : MapToDTO(user);
        }

        public async Task<List<CalendarSetUpDTO>> GetAllAsync()
        {
            var users = await _repository.GetAllAsync();

            if (users == null || !users.Any())
            {
                return new List<CalendarSetUpDTO>(); // Return empty list instead of null
            }

            return users.Select(MapToDTO).ToList();
        }

        // --- Mapping Logic ---
        private CalendarSetUpDTO MapToEntity(CalendarSetUpDTO dto)
        {
            return new CalendarSetUpDTO
            {
                ID = dto.ID,
                Year = dto.Year,
                Day = dto.Day,
                Month = dto.Month,
                Description = dto.Description,
                HolidayType = dto.HolidayType,
                Branch = dto.Branch,
                Time = dto.Time,
                Tdate = dto.Tdate,
                Time2 = dto.Time2
            };
        }

        private CalendarSetUpDTO MapToDTO(CalendarSetUpDTO entity)
        {
            return new CalendarSetUpDTO
            {
                ID = entity.ID,
                Year = entity.Year,
                Day = entity.Day,
                Month = entity.Month,
                Description = entity.Description,
                HolidayType = entity.HolidayType,
                Branch = entity.Branch,
                Time = entity.Time,
                Tdate = entity.Tdate,
                Time2 = entity.Time2
            };
        }
    }
}
