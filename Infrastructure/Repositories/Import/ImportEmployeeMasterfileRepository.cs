using DomainEntities.Dto;
using Infrastructure.IRepositories.Import;
using Microsoft.EntityFrameworkCore;
using Timekeeping.Infrastructure.Data;

namespace Infrastructure.Repositories.Import;

public class ImportEmployeeMasterfileRepository(TimekeepingContext context) : IImportEmployeeMasterfileRepository
{
    private readonly TimekeepingContext _context = context;

    public async Task UpdateImportEmployeeMasterfile(ImportEmployeeMasterfileDto em)
    {
        await _context.Database.ExecuteSqlInterpolatedAsync
            ($"EXEC dbo.sp_tk_AddEmpMasterFile_Excel {em.EmpCode}, {em.StatusActive}, {em.EmpStatCode}, {em.Courtesy}, {em.LName}, {em.FName}, {em.MName}, {em.NickName}, {em.HAddress}, {em.PAddress}, {em.City}, {em.Province}, {em.PostalCode}, {em.CivilStatus}, {em.Citizenship}, {em.Religion}, {em.Sex}, {em.Email}, {em.Weight}, {em.Height}, {em.MobilePhone}, {em.HomePhone}, {em.PresentPhone}, {em.BirthPlace}, {em.BraCode}, {em.DivCode}, {em.DepCode}, {em.SecCode}, {em.UnitCode}, {em.LineCode}, {em.DesCode}, {em.Superior}, {em.GrdCode}, {em.SSSNo}, {em.PhilHealthNo}, {em.TIN}, {em.DateHired}, {em.DateRegularized}, {em.DateResigned}, {em.DateSuspended}, {em.ProbeStart}, {em.ProbeEnd}, {em.BirthDate}, {em.TKSGroup}, {em.GroupSchedCode}, {em.AllowOTDefault}, {em.TardyExemp}, {em.UTExempt}, {em.NDExempt}, {em.OTExempt}, {em.AbsenceExempt}, {em.OtherEarnExempt}, {em.HolidayExempt}, {em.UnproductiveExempt}, {em.DeviceCode}, {em.FixedRestDay1}, {em.FixedRestDay2}, {em.FixedRestDay3}, {em.DailySchedule}, {em.ClassificationCode}, {em.GSISNo}, {em.Suffix}, {em.OnlineAppCode}");
    }
}
