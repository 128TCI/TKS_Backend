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
    public class MySQLDbConfigSetUpService : IMySQLDbConfigSetUpService
    {
        private readonly IMySQLDbConfigSetUpRepository _repository;
        private readonly IEncryptionService _encryptionService; // Injected service

        public MySQLDbConfigSetUpService(IMySQLDbConfigSetUpRepository repository, IEncryptionService encryptionService)
        {
            _repository = repository;
            _encryptionService = encryptionService;
        }
        public async Task<MySQLDbConfigSetUpDTO> CreateAsync(MySQLDbConfigSetUpDTO dto, CancellationToken ct = default)
        {
            var user = MapToEntity(dto);

            // 6. Save to Database
            var result = await _repository.InsertAsync(user);

            return MapToDTO(result);
        }

        public async Task<MySQLDbConfigSetUpDTO> UpdateAsync(MySQLDbConfigSetUpDTO dto)
        {
            var user = MapToEntity(dto);

            var result = await _repository.UpdateAsync(user);
            return MapToDTO(result);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<MySQLDbConfigSetUpDTO?> GetByIdAsync(int id)
        {
            var user = await _repository.GetByIdAsync(id);
            return user == null ? null : MapToDTO(user);
        }

        public async Task<List<MySQLDbConfigSetUpDTO>> GetAllAsync()
        {
            var users = await _repository.GetAllAsync();

            if (users == null || !users.Any())
            {
                return new List<MySQLDbConfigSetUpDTO>(); // Return empty list instead of null
            }

            return users.Select(MapToDTO).ToList();
        }

        // --- Mapping Logic ---
        private MySQLDbConfigSetUpDTO MapToEntity(MySQLDbConfigSetUpDTO dto)
        {
            return new MySQLDbConfigSetUpDTO
            {
                ID = dto.ID,
                Description = dto.Description,
                Server = dto.Server,
                DatabaseName = dto.DatabaseName,
                Username = dto.Username,
                Password = dto.Password,
                LastDateUpdated = dto.LastDateUpdated,
                LastDateUpdateReplica = dto.LastDateUpdateReplica,
                LastDateUpdateTo = dto.LastDateUpdateTo,
                LastDateUpdateFlag = dto.LastDateUpdateFlag,
                LastDateUpdateFrom = dto.LastDateUpdateFrom,
                WithDeviceCode = dto.WithDeviceCode
            };
        }

        private MySQLDbConfigSetUpDTO MapToDTO(MySQLDbConfigSetUpDTO entity)
        {
            return new MySQLDbConfigSetUpDTO
            {
                ID = entity.ID,
                Description = entity.Description,
                Server = entity.Server,
                DatabaseName = entity.DatabaseName,
                Username = entity.Username,
                Password = entity.Password, // Consider masking this in the UI
                LastDateUpdated = entity.LastDateUpdated,
                LastDateUpdateReplica = entity.LastDateUpdateReplica,
                LastDateUpdateTo = entity.LastDateUpdateTo,
                LastDateUpdateFlag = entity.LastDateUpdateFlag,
                LastDateUpdateFrom = entity.LastDateUpdateFrom,
                WithDeviceCode = entity.WithDeviceCode
            };
        }
    }
}
