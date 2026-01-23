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
    public class EmployeeStatusSetUpService : IEmployeeStatusSetUpService
    {
        private readonly IEmployeeStatusSetUpRepository _repository;
        private readonly IEncryptionService _encryptionService; // Injected service

        public EmployeeStatusSetUpService(IEmployeeStatusSetUpRepository repository, IEncryptionService encryptionService)
        {
            _repository = repository;
            _encryptionService = encryptionService;
        }
        public async Task<EmployeeStatusSetUpDTO> CreateAsync(EmployeeStatusSetUpDTO dto, CancellationToken ct = default)
        {
            var user = MapToEntity(dto);

            // 6. Save to Database
            var result = await _repository.InsertAsync(user);

            return MapToDTO(result);
        }

        public async Task<EmployeeStatusSetUpDTO> UpdateAsync(EmployeeStatusSetUpDTO dto)
        {
            var user = MapToEntity(dto);

            var result = await _repository.UpdateAsync(user);
            return MapToDTO(result);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<EmployeeStatusSetUpDTO?> GetByIdAsync(int id)
        {
            var user = await _repository.GetByIdAsync(id);
            return user == null ? null : MapToDTO(user);
        }

        public async Task<List<EmployeeStatusSetUpDTO>> GetAllAsync()
        {
            var users = await _repository.GetAllAsync();

            if (users == null || !users.Any())
            {
                return new List<EmployeeStatusSetUpDTO>(); // Return empty list instead of null
            }

            return users.Select(MapToDTO).ToList();
        }

        // --- Mapping Logic ---

        private EmployeeStatusSetUp MapToEntity(EmployeeStatusSetUpDTO dto)
        {
            return new EmployeeStatusSetUp
            {
                EmpStatID = dto.EmpStatID,
                EmpStatCode = dto.EmpStatCode,
                EmpStatDesc = dto.EmpStatDesc,
            };
        }

        private EmployeeStatusSetUpDTO MapToDTO(EmployeeStatusSetUp entity)
        {
            return new EmployeeStatusSetUpDTO
            {
                EmpStatID = entity.EmpStatID,
                EmpStatCode = entity.EmpStatCode,
                EmpStatDesc = entity.EmpStatDesc,
            };
        }
    }
}
