using DomainEntities.Dto;
using Microsoft.Data.SqlClient;

namespace Infrastructure.IRepositories.Utilities;

public interface IUpdateStatusRepository
{
    Task UpdateStatus(string empCode, DateTime? dateFrom, DateTime? dateTo);
}
