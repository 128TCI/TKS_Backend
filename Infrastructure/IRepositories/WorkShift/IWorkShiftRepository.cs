using DomainEntities.Dto;
using Microsoft.Data.SqlClient;

namespace Infrastructure.IRepositories.WorkShift;

public interface IWorkShiftRepository
{
    Task<WorkShiftDto?> GetWorkShift(string shiftCode);
}
