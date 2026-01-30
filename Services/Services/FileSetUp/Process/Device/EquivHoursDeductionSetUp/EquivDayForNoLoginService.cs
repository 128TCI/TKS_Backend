using DomainEntities.DTO.FileSetUp.Process.Device.EquivHoursDeductionSetUp;
using Infrastructure.IRepositories.FileSetUp.Process.Device.EquivHoursDeductionSetUp;
using Services.Interfaces.Encryption;
using Services.Interfaces.FileSetUp.Process.Device.EquivHoursDeductionSetUp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.FileSetUp.Process.Device.EquivHoursDeductionSetUp
{
    public class EquivDayForNoLoginService : IEquivDayForNoLoginService
    {
        private readonly IEquivDayForNoLoginRepository _repository;
        private readonly IEncryptionService _encryptionService; // Injected service

        public EquivDayForNoLoginService(IEquivDayForNoLoginRepository repository, IEncryptionService encryptionService)
        {
            _repository = repository;
            _encryptionService = encryptionService;
        }
        public async Task<EquivDayForNoLoginDTO> CreateAsync(EquivDayForNoLoginDTO dto, CancellationToken ct = default)
        {
            var user = MapToEntity(dto);

            // 6. Save to Database
            var result = await _repository.InsertAsync(user);

            return MapToDTO(result);
        }

        public async Task<EquivDayForNoLoginDTO> UpdateAsync(EquivDayForNoLoginDTO dto)
        {
            var user = MapToEntity(dto);

            var result = await _repository.UpdateAsync(user);
            return MapToDTO(result);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<EquivDayForNoLoginDTO?> GetByIdAsync(int id)
        {
            var user = await _repository.GetByIdAsync(id);
            return user == null ? null : MapToDTO(user);
        }

        public async Task<List<EquivDayForNoLoginDTO>> GetAllAsync()
        {
            var users = await _repository.GetAllAsync();

            if (users == null || !users.Any())
            {
                return new List<EquivDayForNoLoginDTO>(); // Return empty list instead of null
            }

            return users.Select(MapToDTO).ToList();
        }

        // --- Mapping Logic ---
        private EquivDayForNoLoginDTO MapToEntity(EquivDayForNoLoginDTO dto)
        {
            return new EquivDayForNoLoginDTO
            {
                ID = dto.ID,
                Code = dto.Code,
                Desc = dto.Desc,
                Monday = dto.Monday,
                Tuesday = dto.Tuesday,
                Wednesday = dto.Wednesday,
                Thursday = dto.Thursday,
                Friday = dto.Friday,
                Saturday = dto.Saturday,
                Sunday = dto.Sunday
            };
        }

        private EquivDayForNoLoginDTO MapToDTO(EquivDayForNoLoginDTO entity)
        {
            return new EquivDayForNoLoginDTO
            {
                ID = entity.ID,
                Code = entity.Code?.Trim(),
                Desc = entity.Desc,
                Monday = entity.Monday,
                Tuesday = entity.Tuesday,
                Wednesday = entity.Wednesday,
                Thursday = entity.Thursday,
                Friday = entity.Friday,
                Saturday = entity.Saturday,
                Sunday = entity.Sunday
            };
        }
    }
}
