using DomainEntities.DTO.FileSetUp.Process.Device;
using Infrastructure.IRepositories.FileSetUp.Process.Device;
using Services.Interfaces.Encryption;
using Services.Interfaces.FileSetUp.Process.Device;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.FileSetUp.Process.Device
{
    public class DTRLogFieldsSetUpService : IDTRLogFieldsSetUpService
    {
        private readonly IDTRLogFieldSetUpRepository _repository;
        private readonly IEncryptionService _encryptionService; // Injected service

        public DTRLogFieldsSetUpService(IDTRLogFieldSetUpRepository repository, IEncryptionService encryptionService)
        {
            _repository = repository;
            _encryptionService = encryptionService;
        }
        public async Task<DTRLogFieldsSetUpDTO> CreateAsync(DTRLogFieldsSetUpDTO dto, CancellationToken ct = default)
        {
            var user = MapToEntity(dto);

            // 6. Save to Database
            var result = await _repository.InsertAsync(user);

            return MapToDTO(result);
        }

        public async Task<DTRLogFieldsSetUpDTO> UpdateAsync(DTRLogFieldsSetUpDTO dto)
        {
            var user = MapToEntity(dto);

            var result = await _repository.UpdateAsync(user);
            return MapToDTO(result);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<DTRLogFieldsSetUpDTO?> GetByIdAsync(int id)
        {
            var user = await _repository.GetByIdAsync(id);
            return user == null ? null : MapToDTO(user);
        }

        public async Task<List<DTRLogFieldsSetUpDTO>> GetAllAsync()
        {
            var users = await _repository.GetAllAsync();

            if (users == null || !users.Any())
            {
                return new List<DTRLogFieldsSetUpDTO>(); // Return empty list instead of null
            }

            return users.Select(MapToDTO).ToList();
        }

        // --- Mapping Logic ---
        private DTRLogFieldsSetUpDTO MapToEntity(DTRLogFieldsSetUpDTO dto)
        {
            return new DTRLogFieldsSetUpDTO
            {
                ID = dto.ID,
                Code = dto.Code,
                Description = dto.Description,
                DeviceFormat = dto.DeviceFormat,
                FlagCode = dto.FlagCode,
                EmpCodePos = dto.EmpCodePos,
                EmpCodeNoOfChar = dto.EmpCodeNoOfChar,
                DatePos = dto.DatePos,
                DateNoOfChar = dto.DateNoOfChar,
                TimePos = dto.TimePos,
                TimeNoOfChar = dto.TimeNoOfChar,
                FlagPos = dto.FlagPos,
                FlagNoOfChar = dto.FlagNoOfChar,
                TerminalPos = dto.TerminalPos,
                TerminalNoOfChar = dto.TerminalNoOfChar,
                DeviceType = dto.DeviceType,
                MonthPos = dto.MonthPos,
                MonthNoOfChar = dto.MonthNoOfChar,
                DayPos = dto.DayPos,
                DayNoOfChar = dto.DayNoOfChar,
                YearPos = dto.YearPos,
                YearNoOfChar = dto.YearNoOfChar,
                HourPos = dto.HourPos,
                HourNoOfChar = dto.HourNoOfChar,
                MinutesPos = dto.MinutesPos,
                MinutesNoOfChar = dto.MinutesNoOfChar,
                TimePeriodPos = dto.TimePeriodPos,
                TimePeriodNoOfChar = dto.TimePeriodNoOfChar,
                CombineDateTime = dto.CombineDateTime,
                DateFormat = dto.DateFormat,
                DateSeparator = dto.DateSeparator,
                IdentifierPos = dto.IdentifierPos,
                IdentifierNoOfChar = dto.IdentifierNoOfChar
            };
        }

        private DTRLogFieldsSetUpDTO MapToDTO(DTRLogFieldsSetUpDTO entity)
        {
            return new DTRLogFieldsSetUpDTO
            {
                ID = entity.ID,
                Code = entity.Code?.Trim(), // Trimming helps handle legacy char/varchar whitespace
                Description = entity.Description,
                DeviceFormat = entity.DeviceFormat,
                FlagCode = entity.FlagCode,
                EmpCodePos = entity.EmpCodePos,
                EmpCodeNoOfChar = entity.EmpCodeNoOfChar,
                DatePos = entity.DatePos,
                DateNoOfChar = entity.DateNoOfChar,
                TimePos = entity.TimePos,
                TimeNoOfChar = entity.TimeNoOfChar,
                FlagPos = entity.FlagPos,
                FlagNoOfChar = entity.FlagNoOfChar,
                TerminalPos = entity.TerminalPos,
                TerminalNoOfChar = entity.TerminalNoOfChar,
                DeviceType = entity.DeviceType,
                MonthPos = entity.MonthPos,
                MonthNoOfChar = entity.MonthNoOfChar,
                DayPos = entity.DayPos,
                DayNoOfChar = entity.DayNoOfChar,
                YearPos = entity.YearPos,
                YearNoOfChar = entity.YearNoOfChar,
                HourPos = entity.HourPos,
                HourNoOfChar = entity.HourNoOfChar,
                MinutesPos = entity.MinutesPos,
                MinutesNoOfChar = entity.MinutesNoOfChar,
                TimePeriodPos = entity.TimePeriodPos,
                TimePeriodNoOfChar = entity.TimePeriodNoOfChar,
                CombineDateTime = entity.CombineDateTime,
                DateFormat = entity.DateFormat,
                DateSeparator = entity.DateSeparator,
                IdentifierPos = entity.IdentifierPos,
                IdentifierNoOfChar = entity.IdentifierNoOfChar
            };
        }
    }
}
