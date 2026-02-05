
using DomainEntities.Dto;


namespace Services.Interfaces.Import;

public interface IDeviceTypeService
{
    Task<List<DeviceTypeDto>> GetDeviceType();
}

