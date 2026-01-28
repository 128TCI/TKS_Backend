using DomainEntities.DTO.FileSetUp.Process;
using DomainEntities.DTO.FileSetUp.Process.Allowance_and_Earnings;
using Infrastructure.IRepositories.FileSetUp.Process;
using Infrastructure.IRepositories.FileSetUp.Process.Alllowance_and_Earnings;
using Services.Interfaces.Encryption;
using Services.Interfaces.FileSetUp.Process;
using Services.Interfaces.FileSetUp.Process.Allowance_and_Earnings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.FileSetUp.Process
{
    public class TimeKeepGroupSetUpService : ITimeKeepGroupSetUpService
    {
        private readonly ITimeKeepGroupSetUpRepository _repository;
        private readonly IEncryptionService _encryptionService; // Injected service

        public TimeKeepGroupSetUpService(ITimeKeepGroupSetUpRepository repository, IEncryptionService encryptionService)
        {
            _repository = repository;
            _encryptionService = encryptionService;
        }
        public async Task<TimeKeepGroupSetUpDTO> CreateAsync(TimeKeepGroupSetUpDTO dto, CancellationToken ct = default)
        {
            var user = MapToEntity(dto);

            // 6. Save to Database
            var result = await _repository.InsertAsync(user);

            return MapToDTO(result);
        }

        public async Task<TimeKeepGroupSetUpDTO> UpdateAsync(TimeKeepGroupSetUpDTO dto)
        {
            var user = MapToEntity(dto);

            var result = await _repository.UpdateAsync(user);
            return MapToDTO(result);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<TimeKeepGroupSetUpDTO?> GetByIdAsync(int id)
        {
            var user = await _repository.GetByIdAsync(id);
            return user == null ? null : MapToDTO(user);
        }

        public async Task<List<TimeKeepGroupSetUpDTO>> GetAllAsync()
        {
            var users = await _repository.GetAllAsync();

            if (users == null || !users.Any())
            {
                return new List<TimeKeepGroupSetUpDTO>(); // Return empty list instead of null
            }

            return users.Select(MapToDTO).ToList();
        }

        // --- Mapping Logic ---
        private TimeKeepGroupSetUpDTO MapToEntity(TimeKeepGroupSetUpDTO dto)
        {
            return new TimeKeepGroupSetUpDTO
            {
                ID = dto.ID,
                GroupCode = dto.GroupCode,
                GroupDescription = dto.GroupDescription,
                PayrollGroup = dto.PayrollGroup,
                CutOffDateFrom = dto.CutOffDateFrom,
                CutOffDateTo = dto.CutOffDateTo,
                CutOffDateMonth = dto.CutOffDateMonth,
                CutOffDatePeriod = dto.CutOffDatePeriod,
                IntegrationPayroll = dto.IntegrationPayroll,
                IntegrationHRIS = dto.IntegrationHRIS,
                PreparedBy = dto.PreparedBy,
                PreparedByPostion = dto.PreparedByPostion,
                CheckedBy = dto.CheckedBy,
                CheckedByPosition = dto.CheckedByPosition,
                NotedBy = dto.NotedBy,
                NotedByPosition = dto.NotedByPosition,
                ApprovedBy = dto.ApprovedBy,
                ApprovedByPosition = dto.ApprovedByPosition,
                TerminalID = dto.TerminalID,
                AutoPairLogsDateFrom = dto.AutoPairLogsDateFrom,
                AutoPairLogsDateTo = dto.AutoPairLogsDateTo
            };
        }

        private TimeKeepGroupSetUpDTO MapToDTO(TimeKeepGroupSetUpDTO entity)
        {
            return new TimeKeepGroupSetUpDTO
            {
                ID = entity.ID,
                GroupCode = entity.GroupCode,
                GroupDescription = entity.GroupDescription,
                PayrollGroup = entity.PayrollGroup,
                CutOffDateFrom = entity.CutOffDateFrom,
                CutOffDateTo = entity.CutOffDateTo,
                CutOffDateMonth = entity.CutOffDateMonth,
                CutOffDatePeriod = entity.CutOffDatePeriod,
                IntegrationPayroll = entity.IntegrationPayroll,
                IntegrationHRIS = entity.IntegrationHRIS,
                PreparedBy = entity.PreparedBy,
                PreparedByPostion = entity.PreparedByPostion,
                CheckedBy = entity.CheckedBy,
                CheckedByPosition = entity.CheckedByPosition,
                NotedBy = entity.NotedBy,
                NotedByPosition = entity.NotedByPosition,
                ApprovedBy = entity.ApprovedBy,
                ApprovedByPosition = entity.ApprovedByPosition,
                TerminalID = entity.TerminalID,
                AutoPairLogsDateFrom = entity.AutoPairLogsDateFrom,
                AutoPairLogsDateTo = entity.AutoPairLogsDateTo
            };
        }
    }
}
