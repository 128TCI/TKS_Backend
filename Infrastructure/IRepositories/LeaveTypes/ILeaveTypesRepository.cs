using DomainEntities.Dto;
using Microsoft.Data.SqlClient;

namespace Infrastructure.IRepositories.LeaveTypes;

public interface ILeaveTypesRepository
{
    Task<LeaveTypesDto?> GetLeaveTypeByCode(string leaveCode);
}
