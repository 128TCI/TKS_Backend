using DomainEntities.Dto;
using Infrastructure.IRepositories.LeaveTypes;
using Microsoft.EntityFrameworkCore;
using Timekeeping.Infrastructure.Data;

namespace Infrastructure.Repositories.LeaveTypes;

public class LeaveTypesRepository(TimekeepingContext context) : ILeaveTypesRepository
{
    private readonly TimekeepingContext _context = context;

    public async Task<LeaveTypesDto?> GetLeaveTypeByCode(string leaveCode)
    {
        return await _context.tbl_fsLeaveType.FromSql($"SELECT * FROM tbl_fsLeaveType WHERE LeaveCode = {leaveCode}").FirstOrDefaultAsync();
    }
}