using DomainEntities.Dto;
using DomainEntities.DTO.FileSetUp.Process;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces.FileSetUp.Process
{
    public interface ILeaveTypeSetUpService
    {
        Task<LeaveTypesDto> CreateAsync(LeaveTypesDto dto, CancellationToken ct = default); // Ensure 'ct' is here
        Task<LeaveTypesDto> UpdateAsync(LeaveTypesDto dto);
        Task<bool> DeleteAsync(int id);
        Task<LeaveTypesDto?> GetByIdAsync(int id);
        Task<List<LeaveTypesDto>> GetAllAsync();
    }
}
