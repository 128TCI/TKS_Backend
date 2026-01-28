using DomainEntities.DTO.FileSetUp.Employment;
using DomainEntities.DTO.FileSetUp.Process;
using DomainEntities.DTO.FileSetUp.Process.Allowance_and_Earnings;
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
    //File Setup/Process
    public DbSet<TimeKeepGroupSetUpDTO> tk_GroupSetUpDefinition { get; set; }
    //File Setup/Employee
    public DbSet<User> tk_Users { get; set; }
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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

    }
}