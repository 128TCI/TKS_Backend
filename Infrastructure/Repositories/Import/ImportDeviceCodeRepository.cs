using DomainEntities.Dto;
using DomainEntities.DTO.FileSetUp.Employment;
using Infrastructure.IRepositories.Import;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Timekeeping.Infrastructure.Data;

namespace Infrastructure.Repositories.Import;

public class ImportDeviceCodeRepository : IImportDeviceCodeRepository
{
    private readonly TimekeepingContext _context;
    public ImportDeviceCodeRepository(TimekeepingContext context)
    {
        _context = context;
    }
    public async Task UpdateImportDeviceCode(string empCode, DateTime effectivityDate, DateTime expiryDate, string code)
    {
        await _context.Database.ExecuteSqlInterpolatedAsync
            ($"EXEC dbo.sp_tk_AddEmpTKConfigDeviceCodeOtherDeviceMaint {empCode}, {effectivityDate}, {expiryDate}, {code}");
    }
    public async Task DeleteLeaveApplication(string empCode)
    {
        await _context.Database.ExecuteSqlInterpolatedAsync($"EXEC dbo.sp_tk_DeleteEmpTKConfigDeviceCodeOtherDeviceMaint {empCode}");
    }
}
