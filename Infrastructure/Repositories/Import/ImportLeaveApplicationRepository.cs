using DomainEntities.Dto;
using Infrastructure.IRepositories.Import;
using Microsoft.EntityFrameworkCore;
using Timekeeping.Infrastructure.Data;

namespace Infrastructure.Repositories.Import;

public class ImportLeaveApplicationRepository(TimekeepingContext context) : IImportLeaveApplicationRepository
{
    private readonly TimekeepingContext _context = context;

    public async Task UpdateImportLeaveApplication(ImportLeaveApplicationDto leaveApplicationDto)
    {
        await _context.Database.ExecuteSqlInterpolatedAsync
            ($"EXEC dbo.sp_tk_AddEmpTKConfigLeaveApplication {leaveApplicationDto.EmpCode}, {leaveApplicationDto.DateFrom}, {leaveApplicationDto.NumApprovedHrs}, {leaveApplicationDto.LeaveCode}, {leaveApplicationDto.Period}, {leaveApplicationDto.Reason}, {leaveApplicationDto.Remarks}, {leaveApplicationDto.WithPay}, {leaveApplicationDto.SSSNotif}, {leaveApplicationDto.IsLateFiling}");
    }
    public async Task DeleteLeaveApplication(string empCode, DateTime? dateFrom, DateTime? dateTo)
    {
        await _context.Database.ExecuteSqlInterpolatedAsync($"EXEC dbo.sp_tk_DeleteEmpTKConfigLeaveApplication_2 {empCode}, {dateFrom}, {dateTo}");
    }
}
