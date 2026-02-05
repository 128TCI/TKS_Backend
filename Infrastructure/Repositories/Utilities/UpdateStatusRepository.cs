using DomainEntities.Dto;
using Infrastructure.IRepositories.Utilities;
using Microsoft.EntityFrameworkCore;
using Timekeeping.Infrastructure.Data;

namespace Infrastructure.Repositories.Utilities;

public class UpdateStatusRepository(TimekeepingContext context) : IUpdateStatusRepository
{
    private readonly TimekeepingContext _context = context;

    public async Task UdpdateStatus(UpdateStatusDto updatestatusDto)
    {
        await _context.Database.ExecuteSqlInterpolatedAsync
            ($"EXEC dbo.sp_tk_AddTmpUpdateBasedOnAttendance {updatestatusDto.EmpCode}, {updatestatusDto.DateFrom}");
    }
    public async Task UpdateStatus(string empCode, DateTime? dateFrom, DateTime? dateTo)
    {
        await _context.Database.ExecuteSqlInterpolatedAsync
            ($"EXEC dbo.sp_tk_UpdateEmpTKConfigBasicMaintActive {empCode}, {dateFrom}, {dateTo}");
    }
}
