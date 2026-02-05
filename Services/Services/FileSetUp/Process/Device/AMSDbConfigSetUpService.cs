using DomainEntities.DTO.FileSetUp.Process;
using DomainEntities.DTO.FileSetUp.Process.Device;
using Infrastructure.IRepositories.FileSetUp.Process;
using Infrastructure.IRepositories.FileSetUp.Process.Device;
using Services.Interfaces.Encryption;
using Services.Interfaces.FileSetUp.Process;
using Services.Interfaces.FileSetUp.Process.Device;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.FileSetUp.Process.Device
{
    public class AMSDbConfigSetUpService : IAMSDbConfigSetUpService
    {
        private readonly IAMSDbConfigSetUpRepository _repository;
        private readonly IEncryptionService _encryptionService; // Injected service

        public AMSDbConfigSetUpService(IAMSDbConfigSetUpRepository repository, IEncryptionService encryptionService)
        {
            _repository = repository;
            _encryptionService = encryptionService;
        }
        public async Task<AMSDbConfigSetUpDTO> CreateAsync(AMSDbConfigSetUpDTO dto, CancellationToken ct = default)
        {
            var user = MapToEntity(dto);

            // 6. Save to Database
            var result = await _repository.InsertAsync(user);

            return MapToDTO(result);
        }

        public async Task<AMSDbConfigSetUpDTO> UpdateAsync(AMSDbConfigSetUpDTO dto)
        {
            var user = MapToEntity(dto);

            var result = await _repository.UpdateAsync(user);
            return MapToDTO(result);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<AMSDbConfigSetUpDTO?> GetByIdAsync(int id)
        {
            var user = await _repository.GetByIdAsync(id);
            return user == null ? null : MapToDTO(user);
        }

        public async Task<List<AMSDbConfigSetUpDTO>> GetAllAsync()
        {
            var users = await _repository.GetAllAsync();

            if (users == null || !users.Any())
            {
                return new List<AMSDbConfigSetUpDTO>(); // Return empty list instead of null
            }

            return users.Select(MapToDTO).ToList();
        }

        // --- Mapping Logic ---
        private AMSDbConfigSetUpDTO MapToEntity(AMSDbConfigSetUpDTO dto)
        {
            return new AMSDbConfigSetUpDTO
            {
                ID = dto.ID,
                Description = dto.Description,
                Server = dto.Server,
                DatabaseName = dto.DatabaseName,
                Username = dto.Username,
                Password = dto.Password,
                LastDateUpdated = dto.LastDateUpdated,
                WithDeviceCode = dto.WithDeviceCode,
                TableName = dto.TableName,
                EmpCode = dto.EmpCode,
                TimeStamp = dto.TimeStamp,
                Flag = dto.Flag,
                FlagCode = dto.FlagCode,
                IsAutomaticEmpCode = dto.IsAutomaticEmpCode,
                EmployeeCodeTable = dto.EmployeeCodeTable,
                EmployeeCodeCol = dto.EmployeeCodeCol,
                EmpoyeeCodeIDCol = dto.EmpoyeeCodeIDCol,
                DateDaysAhead = dto.DateDaysAhead,
                LastDateUpdateReplica = dto.LastDateUpdateReplica,
                LastDateUpdateTo = dto.LastDateUpdateTo,
                LastDateUpdateFlag = dto.LastDateUpdateFlag,
                LastDateUpdateFrom = dto.LastDateUpdateFrom,
                DeviceNameCol = dto.DeviceNameCol
            };
        }

        private AMSDbConfigSetUpDTO MapToDTO(AMSDbConfigSetUpDTO entity)
        {
            return new AMSDbConfigSetUpDTO
            {
                ID = entity.ID,
                Description = entity.Description,
                Server = entity.Server,
                DatabaseName = entity.DatabaseName,
                Username = entity.Username,
                Password = entity.Password,
                LastDateUpdated = entity.LastDateUpdated,
                WithDeviceCode = entity.WithDeviceCode,
                TableName = entity.TableName,
                EmpCode = entity.EmpCode,
                TimeStamp = entity.TimeStamp,
                Flag = entity.Flag,
                FlagCode = entity.FlagCode,
                IsAutomaticEmpCode = entity.IsAutomaticEmpCode,
                EmployeeCodeTable = entity.EmployeeCodeTable,
                EmployeeCodeCol = entity.EmployeeCodeCol,
                EmpoyeeCodeIDCol = entity.EmpoyeeCodeIDCol,
                DateDaysAhead = entity.DateDaysAhead,
                LastDateUpdateReplica = entity.LastDateUpdateReplica,
                LastDateUpdateTo = entity.LastDateUpdateTo,
                LastDateUpdateFlag = entity.LastDateUpdateFlag,
                LastDateUpdateFrom = entity.LastDateUpdateFrom,
                DeviceNameCol = entity.DeviceNameCol
            };
        }
    }
}
