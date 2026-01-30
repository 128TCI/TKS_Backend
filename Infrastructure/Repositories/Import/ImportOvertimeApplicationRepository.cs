using DomainEntities.Dto;
using Infrastructure.IRepositories.Import;
using Microsoft.EntityFrameworkCore;
using Timekeeping.Infrastructure.Data;

namespace Infrastructure.Repositories.Import;

public class ImportOvertimeApplicationRepository(TimekeepingContext context) : IImportOvertimeApplicationRepository
{
    private readonly TimekeepingContext _context = context;

    public async Task UpdateImportOvertimeApplication(ImportOvertimeApplicationDto overtimeApplicationDto)
    {
        await _context.Database.ExecuteSqlInterpolatedAsync
            ($"EXEC dbo.sp_tk_AddEmpTKConfigLeaveApplication {overtimeApplicationDto.EmpCode}, {overtimeApplicationDto.DateFrom}");
    }
    public async Task DeleteOvertimeApplication(string empCode, DateTime? dateFrom)
    {
        await _context.Database.ExecuteSqlInterpolatedAsync($"EXEC dbo.sp_tk_DeleteEmpTKConfigLeaveApplication_2 {empCode}, {dateFrom}");
    }
}
