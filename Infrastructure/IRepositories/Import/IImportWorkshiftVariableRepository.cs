using DomainEntities.Dto;
using Microsoft.Data.SqlClient;

namespace Infrastructure.IRepositories.Import;

public interface IImportWorkshiftVariableRepository
{
    Task UpdateWorkshiftVariable(string empCode, DateTime dateFrom, DateTime dateTo, string shiftCode);
    Task UpdateWorkshiftWithRestDay(string empCode, DateTime dateFrom, DateTime dateTo);
    Task DeleteWorkshiftVariable(string empCode, DateTime? dateFrom, DateTime? dateTo);
}
