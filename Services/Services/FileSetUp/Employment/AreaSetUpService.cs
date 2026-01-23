using DomainEntities.DTO.FileSetUp.Employment;
using Infrastructure.IRepositories;
using Infrastructure.IRepositories.FileSetUp.Employment;
using Services.DTOs;
using Services.DTOs.FileSetup.Employment;
using Services.Interfaces.Employee;
using Services.Interfaces.Encryption;
using Services.Interfaces.FileSetUp.Employment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.FileSetUp.Employment
{
    public class AreaSetUpService : IAreaSetUpService
    {
        private readonly IAreaSetUpRepository _repository;
        private readonly IEncryptionService _encryptionService; // Injected service

        public AreaSetUpService(IAreaSetUpRepository repository, IEncryptionService encryptionService)
        {
            _repository = repository;
            _encryptionService = encryptionService;
        }
        public async Task<AreaSetUpDTO> CreateAsync(AreaSetUpDTO dto, CancellationToken ct = default)
        {
            var user = MapToEntity(dto);

            // 6. Save to Database
            var result = await _repository.InsertAsync(user);

            return MapToDTO(result);
        }

        public async Task<AreaSetUpDTO> UpdateAsync(AreaSetUpDTO dto)
        {
            var user = MapToEntity(dto);

            var result = await _repository.UpdateAsync(user);
            return MapToDTO(result);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<AreaSetUpDTO?> GetByIdAsync(int id)
        {
            var user = await _repository.GetByIdAsync(id);
            return user == null ? null : MapToDTO(user);
        }

        public async Task<List<AreaSetUpDTO>> GetAllAsync()
        {
            var users = await _repository.GetAllAsync();

            if (users == null || !users.Any())
            {
                return new List<AreaSetUpDTO>(); // Return empty list instead of null
            }

            return users.Select(MapToDTO).ToList();
        }

        // --- Mapping Logic ---

        private AreaSetUp MapToEntity(AreaSetUpDTO dto)
        {
            return new AreaSetUp
            {
                ID = dto.ID,
                AreaCode = dto.AreaCode,
                AreaDesc = dto.AreaDesc,
                Head = dto.Head,
                HeadCode = dto.HeadCode,
                AcctCode = dto.AcctCode,
                CreatedBy = dto.CreatedBy,
                CreatedDate = dto.CreatedDate,
                EditedBy = dto.EditedBy,
                EditedDate = dto.EditedDate,
                DeviceName = dto.DeviceName
            };
        }

        private AreaSetUpDTO MapToDTO(AreaSetUp entity)
        {
            return new AreaSetUpDTO
            {
                ID = entity.ID,
                AreaCode = entity.AreaCode,
                AreaDesc = entity.AreaDesc,
                Head = entity.Head,
                HeadCode = entity.HeadCode,
                AcctCode = entity.AcctCode,
                CreatedBy = entity.CreatedBy,
                CreatedDate = entity.CreatedDate,
                EditedBy = entity.EditedBy,
                EditedDate = entity.EditedDate,
                DeviceName = entity.DeviceName
            };
        }
    }
    }
