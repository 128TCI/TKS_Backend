using DomainEntities.DTO.FileSetUp.Employment;
using Infrastructure.IRepositories.FileSetUp.Employment;
using Services.DTOs.FileSetup.Employment;
using Services.Interfaces.Encryption;
using Services.Interfaces.FileSetUp.Employment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.FileSetUp.Employment
{
    public class PayHouseSetUpService : IPayHouseSetUpService
    {
        private readonly IPayHouseSetUpRepository _repository;
        private readonly IEncryptionService _encryptionService; // Injected service

        public PayHouseSetUpService(IPayHouseSetUpRepository repository, IEncryptionService encryptionService)
        {
            _repository = repository;
            _encryptionService = encryptionService;
        }
        public async Task<PayHouseSetUpDTO> CreateAsync(PayHouseSetUpDTO dto, CancellationToken ct = default)
        {
            var user = MapToEntity(dto);

            // 6. Save to Database
            var result = await _repository.InsertAsync(user);

            return MapToDTO(result);
        }

        public async Task<PayHouseSetUpDTO> UpdateAsync(PayHouseSetUpDTO dto)
        {
            var user = MapToEntity(dto);

            var result = await _repository.UpdateAsync(user);
            return MapToDTO(result);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<PayHouseSetUpDTO?> GetByIdAsync(int id)
        {
            var user = await _repository.GetByIdAsync(id);
            return user == null ? null : MapToDTO(user);
        }

        public async Task<List<PayHouseSetUpDTO>> GetAllAsync()
        {
            var users = await _repository.GetAllAsync();

            if (users == null || !users.Any())
            {
                return new List<PayHouseSetUpDTO>(); // Return empty list instead of null
            }

            return users.Select(MapToDTO).ToList();
        }

        // --- Mapping Logic ---

        private PayHouseSetUp MapToEntity(PayHouseSetUpDTO dto)
        {
            return new PayHouseSetUp
            {
                LineID = dto.LineID,
                LineCode = dto.LineCode,
                LineDesc = dto.LineDesc,
                Head = dto.Head,
                Position = dto.Position,
                HeadCode = dto.HeadCode,
                DeviceName = dto.DeviceName
            };
        }

        private PayHouseSetUpDTO MapToDTO(PayHouseSetUp entity)
        {
            return new PayHouseSetUpDTO
            {
                LineID = entity.LineID,
                LineCode = entity.LineCode,
                LineDesc = entity.LineDesc,
                Head = entity.Head,
                Position = entity.Position,
                HeadCode = entity.HeadCode,
                DeviceName = entity.DeviceName
            };
        }
    }
}
