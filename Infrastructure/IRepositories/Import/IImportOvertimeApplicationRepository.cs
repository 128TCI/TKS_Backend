using DomainEntities.Dto;
using Microsoft.Data.SqlClient;

namespace Infrastructure.IRepositories.Import;

public interface IImportOvertimeApplicationRepository
{
    Task UpdateImportOvertimeApplication(ImportOvertimeApplicationDto overtimeApplicationDto);
    Task DeleteOvertimeApplication(string empCode, DateTime? dateFrom);
}
