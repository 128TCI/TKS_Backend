using DomainEntities.Dto;
using Microsoft.Data.SqlClient;

namespace Infrastructure.IRepositories.Import;

public interface IImportLeaveApplicationRepository
{
    Task UpdateImportLeaveApplication(ImportLeaveApplicationDto leaveApplicationDto);
    Task DeleteLeaveApplication(string empCode, DateTime? dateFrom, DateTime? dateTo);
}
