using DomainEntities.Dto;
using Infrastructure.IRepositories.Import;
using Microsoft.EntityFrameworkCore;
using Timekeeping.Infrastructure.Data;

namespace Infrastructure.Repositories.Import;

public class ImportLogsFromDeviceRepository(TimekeepingContext context) : IImportLogsFromDeviceRepository
{
    private readonly TimekeepingContext _context = context;

    public async Task<List<RawDataDto>> GetRawData(DateTime rawDateInFrom, DateTime rawDateInTo, string empCode)
    {
        return await _context.tbl_TKSRawData.FromSqlInterpolated($"EXEC dbo.sp_tk_GetRawData {rawDateInFrom}, {rawDateInTo}, {empCode}, {""}").ToListAsync();
    }
    public async Task<List<WorkShiftAsOfDateDto>> GetWorkShiftAsOfDate(string empCode, DateTime date)
    {
        return await _context.tk_EmpTKConfigWorkShiftFixedMaint.FromSqlInterpolated($"EXEC dbo.sp_tk_GetWorkShiftOfEmployeeAsOfDate {empCode}, {date}").ToListAsync();
    }
    public async Task<List<WorkShiftByInOrOut>> GetWorkShiftByInOrOut(string empCode, DateTime dateFrom, DateTime dateTo, DateTime dateToCutOff)
    {
        return await _context.tkWorkShiftCode.FromSqlInterpolated($"EXEC dbo.sp_tk_GetEmpWorkShiftByInOrOut {empCode}, {dateFrom}, {dateTo}, {dateToCutOff}").ToListAsync();
    }
    public async Task<List<DayTypeDto>> GetDayTypePerEmployee(string empCode, DateTime date)
    {
        return await _context.DayTypeOfDatePerEmployee.FromSqlInterpolated($"EXEC dbo.sp_tk_GetDayTypeOfDatePerEmployee {empCode}, {date}").ToListAsync();
    }
    public async Task<List<OTApprovedDto>> GetOTApproved(string empCode)
    {
        return await _context.OTApproved.FromSqlInterpolated($"EXEC dbo.sp_tk_GetOTApproved {empCode}").ToListAsync();
    }
    public async Task<List<TKEmployeeMasterfileDto>> GetTKEmployeeMasterfile()
    {
        return await _context.Database.SqlQuery<TKEmployeeMasterfileDto>($"EXEC dbo.sp_tk_GetEmployeeMasterFile").ToListAsync();
    }
    public async Task UpdateRawData(ImportLogsFromDeviceDto logs)
    {
        var otApproved = await GetOTApproved(logs.EmpCode!);

        OTApprovedDto oTApprovedDto = otApproved[1];
        
        await _context.Database.ExecuteSqlInterpolatedAsync
            ($"EXEC dbo.sp_tk_Import_To_UpdateRawData {logs.EmpCode}, {logs.DateIn}, {logs.WorkShiftCode}, {logs.DayType}, {logs.TimeIn}, {logs.Break1In}, {logs.Break1Out}, {logs.Break2In}, {logs.Break2Out}, {logs.Break3In}, {logs.Break3Out}, {logs.TimeOut}, {logs.DateOut}, {oTApprovedDto.AllowOTDefault}");
    }
    public async Task UpdateOTApproved(string empCode)
    {
        await _context.Database.ExecuteSqlInterpolatedAsync
            ($"EXEC dbo.sptk_UpdateRawOTApprovedFlag {empCode}");
    }
    public async Task UpdateImportToRawData(RawDataDto raw)
    {
        await _context.Database.ExecuteSqlInterpolatedAsync
            ($"EXEC dbo.sp_tk_Import_To_RawData {raw.EmpCode}, {raw.RawDateIn}, {raw.WorkShiftCode}, {raw.DayType}, {raw.RawTimeIn}, {raw.RawBreak1In}, {raw.RawBreak1Out}, {raw.RawBreak2In}, {raw.RawBreak2Out}, {raw.RawBreak3In}, {raw.RawBreak3Out}, {raw.RawOTApproved}, {raw.ID}, {raw.TerminalID}");
    }
}
