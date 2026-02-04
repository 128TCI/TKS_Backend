using DomainEntities.Dto;
using DomainEntities.DTO.FileSetUp.Process;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IRepositories.FileSetUp.Process
{
    public interface ILeaveTypeSetUpRepository
    {
        Task<LeaveTypesDto> InsertAsync(LeaveTypesDto data);
        Task<LeaveTypesDto> UpdateAsync(LeaveTypesDto data);
        Task<bool> DeleteAsync(int id);
        Task<LeaveTypesDto?> GetByIdAsync(int id);
        Task<List<LeaveTypesDto>> GetAllAsync();
    }
}
