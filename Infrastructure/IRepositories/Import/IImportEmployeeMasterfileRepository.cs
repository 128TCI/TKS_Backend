using DomainEntities.Dto;
using Microsoft.Data.SqlClient;

namespace Infrastructure.IRepositories.Import;

public interface IImportEmployeeMasterfileRepository
{
    Task UpdateImportEmployeeMasterfile(ImportEmployeeMasterfileDto em);
}
