using DomainEntities.Dto;
using Microsoft.Data.SqlClient;

namespace Infrastructure.IRepositories.Import;

public interface IDeviceTypeRepository
{
    Task<List<DeviceTypeDto>> GetDeviceType();
}
