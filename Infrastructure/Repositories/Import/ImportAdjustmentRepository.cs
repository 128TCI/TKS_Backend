using DomainEntities.Dto;
using Infrastructure.IRepositories.Import;
using Microsoft.EntityFrameworkCore;
using Timekeeping.Infrastructure.Data;

namespace Infrastructure.Repositories.Import;

public class ImportAdjustmentRepository(TimekeepingContext context) : IImportAdjustmentRepository
{
    private readonly TimekeepingContext _context = context;

    public async Task<EmpGroupImportAdjust?> GetEmployeeGroupImport(string empCode)
    {
        return await _context.tk_EmpTKConfigBasicMaint.FromSql($"EXEC dbo.sp_tk_GetEmpGroupImportAdjust {empCode}").FirstOrDefaultAsync();
    }

    public async Task UpdateImportAdjustment(ImportAdjustmentDto adjustmentDto)
    {
        await _context.Database.ExecuteSqlInterpolatedAsync
            ($"EXEC dbo.sp_tk_ImportProcessedDataAdjustment {adjustmentDto.EmpCode}, {adjustmentDto.TransactionDate}, {adjustmentDto.TransactionType}, {adjustmentDto.LeaveType}, {adjustmentDto.OvertimeCode}, {adjustmentDto.NoOfHours}, {adjustmentDto.Remarks}, {adjustmentDto.IsLateFiling}, {adjustmentDto.IsLateFilingActualDate}");
    }

}
