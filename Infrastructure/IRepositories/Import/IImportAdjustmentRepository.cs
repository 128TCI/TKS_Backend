using DomainEntities.Dto;
using Microsoft.Data.SqlClient;

namespace Infrastructure.IRepositories.Import;

public interface IImportAdjustmentRepository
{
    Task<EmpGroupImportAdjust?> GetEmployeeGroupImport(string empCode);
    Task UpdateImportAdjustment(ImportAdjustmentDto adjustmentDto);
}
