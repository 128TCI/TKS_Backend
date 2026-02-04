using DomainEntities.Dto;
using DomainEntities.DTO.FileSetUp.Employment;
using DomainEntities.DTO.FileSetUp.Process;
using DomainEntities.DTO.FileSetUp.Process.Allowance_and_Earnings;
using DomainEntities.DTO.FileSetUp.Process.Device;
using DomainEntities.DTO.FileSetUp.Process.Device.EquivHoursDeductionSetUp;
using DomainEntities.DTO.FileSetUp.Process.Overtime;
using DomainEntities.DTO.FileSetUp.System;
using DomainEntities.DTO.Maintenance;
using DomainEntities.DTO.User;
using Microsoft.EntityFrameworkCore;

namespace Timekeeping.Infrastructure.Data;

public class TimekeepingContext : DbContext
{
    public TimekeepingContext(DbContextOptions<TimekeepingContext> options)
        : base(options)
    {
    }

    // DbSet properties named to match your physical database tables
    //File Setup
    //File Setup/Process
    public DbSet<CompanyInformationDTO> tbl_fsCompanyInfo { get; set; }
    //File Setup/Process
    public DbSet<AllowanceBracketCodeSetUpDTO> tbl_fsAllowBracketCode { get; set; }
    public DbSet<AllowanceBracketingSetUpDTO> tk_AllowBaseOnDayType  { get; set; }
    public DbSet<AllowancePerClassificationSetUpDTO> tk_AllowancesPerClassificationSetup  { get; set; }
    public DbSet<ClassificationSetUpDTO> tk_Classification { get; set; }
    public DbSet<EarningSetUpDTO> tbl_fsEarnings { get; set; }
    public DbSet<CalendarSetUpDTO> tk_Calendar  { get; set; }
    public DbSet<TimeKeepGroupSetUpDTO> tk_GroupSetUpDefinition { get; set; }
    public DbSet<DayTypeSetUpDTO> tk_DayTypeSetup  { get; set; }
    public DbSet<AMSDbConfigSetUpDTO> tk_AmsDbConfiguration { get; set; }
    public DbSet<BorrowedDeviceNameDTO> tbl_fsBorrowedDeviceName { get; set; }
    public DbSet<CoordinatesSetUpDTO> tbl_fsCoordinates { get; set; }
    public DbSet<DeviceTypeSetUpDTO> tbl_fsDeviceType { get; set; }
    public DbSet<DTRFlagSetUpDTO> tk_DTRFlag { get; set; }
    public DbSet<DTRLogFieldsSetUpDTO> tk_DTRLogFields { get; set; }
    public DbSet<DeviceType2SetUpDTO> tbl_fsDeviceType2 { get; set; }
    public DbSet<MySQLDbConfigSetUpDTO> tk_MySQLDbConfiguration { get; set; }
    public DbSet<SDKListSetUpDTO> tbl_SDKList { get; set; }
    public DbSet<EquivDayForAbsentDTO> tk_EquivDayForAbsent { get; set; }
    public DbSet<EquivDayForNoLoginDTO> tk_EquivDayForNoLogin { get; set; }
    public DbSet<EquivDayForNoLogoutDTO> tk_EquivDayForNoLogout { get; set; }
    public DbSet<EquivDayForNoBreat2InDTO> tk_EquivDayForNoBreak2In { get; set; }
    public DbSet<EquivDayForNoBreat2OutDTO> tk_EquivDayForNoBreak2Out { get; set; }
    public DbSet<HelpSetUpDTO> tk_HelpSetup { get; set; }
    public DbSet<RestDayOTRateSetUpDTO> tk_RestDayOTRatesSetUp { get; set; }
    //File Setup/Employee
    public DbSet<UserDTO> tk_Users { get; set; }
    public DbSet<AreaSetUp> tbl_fsArea { get; set; }
    public DbSet<BranchSetUp> tbl_fsBranch { get; set; }
    public DbSet<DepartmentSetUp> tbl_fsDepartment { get; set; }
    public DbSet<DivisionSetUp> tbl_fsDivision { get; set; }
    public DbSet<DesignationSetUp> tbl_fsDesignation { get; set; }
    public DbSet<EmployeeStatusSetUp> tbl_fsEmployeeStatus { get; set; }
    public DbSet<GroupSetUp> tbl_fsGroup { get; set; }
    public DbSet<JobLevelSetUp> tbl_fsJobLevel { get; set; }
    public DbSet<LocationSetUp> tbl_fsLocation { get; set; }
    public DbSet<PayHouseSetUp> tk_line {  get; set; }
    public DbSet<OnlineApprovalSetUp> tbl_fsOnlineApproval  { get; set; }
    public DbSet<SectionSetUp> tbl_fsSection { get; set; }
    public DbSet<UnitSetUp> tk_unit { get; set; }
   

    //Maintenance
    public DbSet<EmployeeMasterFile> tbl_fmEmpMasterFile { get; set; }

    //ImportWorkshift
    public DbSet<ImportWorkShiftVariableDto> tk_EmpTKConfigWorkShiftVariableMaint { get; set; }

    //WorkShift
    public DbSet<WorkShiftDto> tk_WorkShiftCode { get; set; }

    //LeaveTypes
    public DbSet<LeaveTypesDto> tbl_fsLeaveType { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

    }
}