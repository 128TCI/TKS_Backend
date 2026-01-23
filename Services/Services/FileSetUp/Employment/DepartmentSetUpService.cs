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
    public class DepartmentSetUpService : IDepartmentSetUpService
    {
        private readonly IDepartmentSetUpRepository _repository;
        private readonly IEncryptionService _encryptionService; // Injected service

        public DepartmentSetUpService(IDepartmentSetUpRepository repository, IEncryptionService encryptionService)
        {
            _repository = repository;
            _encryptionService = encryptionService;
        }
        public async Task<DepartmentSetUpDTO> CreateAsync(DepartmentSetUpDTO dto, CancellationToken ct = default)
        {
            var user = MapToEntity(dto);

            // 6. Save to Database
            var result = await _repository.InsertAsync(user);

            return MapToDTO(result);
        }

        public async Task<DepartmentSetUpDTO> UpdateAsync(DepartmentSetUpDTO dto)
        {
            var user = MapToEntity(dto);

            var result = await _repository.UpdateAsync(user);
            return MapToDTO(result);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<DepartmentSetUpDTO?> GetByIdAsync(int id)
        {
            var user = await _repository.GetByIdAsync(id);
            return user == null ? null : MapToDTO(user);
        }

        public async Task<List<DepartmentSetUpDTO>> GetAllAsync()
        {
            var users = await _repository.GetAllAsync();

            if (users == null || !users.Any())
            {
                return new List<DepartmentSetUpDTO>(); // Return empty list instead of null
            }

            return users.Select(MapToDTO).ToList();
        }

        // --- Mapping Logic ---

        private DepartmentSetUp MapToEntity(DepartmentSetUpDTO dto)
        {
            return new DepartmentSetUp
            {
                DepID = dto.DepID,
                DepCode = dto.DepCode,
                DivCode = dto.DivCode,
                DepDesc = dto.DepDesc,
                DepHead = dto.DepHead,
                DepHeadCode = dto.DepHeadCode,
                Head1 = dto.Head1,
                Head2 = dto.Head2,
                Email1 = dto.Email1,
                Email2 = dto.Email2,
                DeviceName = dto.DeviceName
            };
        }

        private DepartmentSetUpDTO MapToDTO(DepartmentSetUp entity)
        {
            return new DepartmentSetUpDTO
            {
                DepID = entity.DepID,
                DepCode = entity.DepCode,
                DivCode = entity.DivCode,
                DepDesc = entity.DepDesc,
                DepHead = entity.DepHead,
                DepHeadCode = entity.DepHeadCode,
                Head1 = entity.Head1,
                Head2 = entity.Head2,
                Email1 = entity.Email1,
                Email2 = entity.Email2,
                DeviceName = entity.DeviceName
            };
        }
    }
}
