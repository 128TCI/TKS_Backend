using DomainEntities.Dto;
using Infrastructure.IRepositories.Import;
using Microsoft.EntityFrameworkCore;
using Timekeeping.Infrastructure.Data;

namespace Infrastructure.Repositories.Import;

public class DeviceTypeRepository(TimekeepingContext context) : IDeviceTypeRepository
{
    private readonly TimekeepingContext _context = context;

    public async Task<List<DeviceTypeDto>> GetDeviceType()
    {
        return await _context.tbl_fsDeviceType.ToListAsync();
    }
}
