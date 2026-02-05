using DomainEntities.Dto;
using Microsoft.Data.SqlClient;

namespace Infrastructure.IRepositories.Import;

public interface IImportDeviceCodeRepository
{
    Task UpdateImportDeviceCode(string empCode, DateTime effectivityDate, DateTime expiryDate, string code);
    Task DeleteLeaveApplication(string empCode);
}
