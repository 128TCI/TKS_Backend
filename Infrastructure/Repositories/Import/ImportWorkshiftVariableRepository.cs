using Infrastructure.IRepositories.Import;
using Microsoft.EntityFrameworkCore;
using Timekeeping.Infrastructure.Data;

namespace Infrastructure.Repositories.Import;

public class ImportWorkshiftVariableRepository(TimekeepingContext context) : IImportWorkshiftVariableRepository
{
    private readonly TimekeepingContext _context = context;

    public async Task UpdateWorkshiftVariable(string empCode, DateTime dateFrom, DateTime dateTo, string shiftCode)
    {
        await _context.Database.ExecuteSqlInterpolatedAsync($"EXEC dbo.sp_tk_ImportWorkShift {empCode}, {dateFrom}, {dateTo}, {shiftCode}");
    }
    public async Task UpdateWorkshiftWithRestDay(string empCode, DateTime dateFrom, DateTime dateTo)
    {
        await _context.Database.ExecuteSqlInterpolatedAsync($"EXEC dbo.sp_tk_ImportWorkshiftWithRestDay {empCode}, {dateFrom}, {dateTo}");
    }
    public async Task DeleteWorkshiftVariable(string empCode, DateTime? dateFrom, DateTime? dateTo)
    {
        await _context.Database.ExecuteSqlInterpolatedAsync($"EXEC dbo.sp_tk_DeleteExistingRestDayVariable {empCode}, {dateFrom}, {dateTo}");
    }
}
