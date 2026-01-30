using DomainEntities.Dto;
using Infrastructure.IRepositories.WorkShift;
using Microsoft.EntityFrameworkCore;
using Timekeeping.Infrastructure.Data;

namespace Infrastructure.Repositories.WorkShift;

public class WorkShiftRepository(TimekeepingContext context) : IWorkShiftRepository
{
    private readonly TimekeepingContext _context = context;

    public async Task<WorkShiftDto?> GetWorkShift(string shiftCode)
    {
        return await _context.tk_WorkShiftCode.FromSql($"SELECT WorkShiftCode, WorkShiftDesc FROM tk_WorkShiftCode WHERE WorkShiftCode = {shiftCode}").FirstOrDefaultAsync();
    }
}
