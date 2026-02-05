using DomainEntities.Dto;
using Infrastructure.IRepositories.Import;
using Services.Interfaces.Import;


namespace Services.Services.Import
{
    public class DeviceTypeService(IDeviceTypeRepository repository) : IDeviceTypeService
    {
        private readonly IDeviceTypeRepository _repository = repository;
        public async Task<List<DeviceTypeDto>> GetDeviceType()
        {
            var deviceType = await _repository.GetDeviceType();
            return [.. deviceType]; 
        }
    }
}
