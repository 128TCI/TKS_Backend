
using Infrastructure.IRepositories.FileSetUp.Employment;
using Infrastructure.IRepositories.FileSetUp.Process;
using Infrastructure.IRepositories.FileSetUp.Process.Alllowance_and_Earnings;
using Infrastructure.IRepositories.FileSetUp.Process.Device;
using Infrastructure.IRepositories.FileSetUp.Process.Device.EquivHoursDeductionSetUp;
using Infrastructure.IRepositories.FileSetUp.System;
using Infrastructure.IRepositories.Import;
using Infrastructure.IRepositories.LeaveTypes;
using Infrastructure.IRepositories.Maintenance;
using Infrastructure.IRepositories.UserRepository;
using Infrastructure.IRepositories.WorkShift;
using Infrastructure.Repositories.FileSetup.Employment;
using Infrastructure.Repositories.FileSetup.Process;
using Infrastructure.Repositories.FileSetup.Process.Allowance_and_Earnings;
using Infrastructure.Repositories.FileSetup.Process.Device;
using Infrastructure.Repositories.FileSetup.Process.Device.EquivHoursDeductionSetUp;
using Infrastructure.Repositories.FileSetup.System;
using Infrastructure.Repositories.Import;
using Infrastructure.Repositories.LeaveTypes;
using Infrastructure.Repositories.Maintennace;
using Infrastructure.Repositories.UserRepository;
using Infrastructure.Repositories.WorkShift;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Services.DTOs.Encryption;
using Services.Implementation.Authentication;
using Services.Interfaces.Authentication;
using Services.Interfaces.Employee;
using Services.Interfaces.Encryption;
using Services.Interfaces.FileSetUp.Employment;
using Services.Interfaces.FileSetUp.Process;
using Services.Interfaces.FileSetUp.Process.Allowance_and_Earnings;
using Services.Interfaces.FileSetUp.Process.Device;
using Services.Interfaces.FileSetUp.Process.Device.EquivHoursDeductionSetUp;
using Services.Interfaces.FileSetUp.System;
using Services.Interfaces.Import;
using Services.Interfaces.LeaveTypes;
using Services.Interfaces.Maintenence;
using Services.Services.FileSetUp.Employment;
using Services.Services.FileSetUp.Process;
using Services.Services.FileSetUp.Process.Allowance_and_Earnings;
using Services.Services.FileSetUp.Process.Device;
using Services.Services.FileSetUp.Process.Device.EquivHoursDeductionSetUp;
using Services.Services.FileSetUp.System;
using Services.Services.Import;
using Services.Services.LeaveTypes;
using Services.Services.Maintenance;
using Services.Services.UserRepository;
using System.Data;
using Timekeeping.Infrastructure.Data;
using WebbApp.Api.FileSetUp.Process.Allowance_and_Earnings;


var builder = WebApplication.CreateBuilder(args);
// 1. Database Connection (Fixed: Only one registration using TKS)
builder.Services.AddDbContext<TimekeepingContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TKS")));
// 1. Define the CORS Policy Name
var myAllowSpecificOrigins = "_myAllowSpecificOrigins";

// 2. Add CORS services to the container
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: myAllowSpecificOrigins,
                      policy =>
                      {
                          policy.AllowAnyOrigin()   // Allows your frontend URL
                                .AllowAnyHeader()   // Allows headers like 'Authorization'
                                .AllowAnyMethod();  // Allows GET, POST, PUT, DELETE
                      });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Database
builder.Services.AddDbContext<TimekeepingContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TKS")));
// Dapper (IDbConnection)
builder.Services.AddScoped<IDbConnection>(sp =>
    new SqlConnection(builder.Configuration.GetConnectionString("TKS")));
// Dependency Injection
//User
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
//Authentication
builder.Services.AddScoped<EncryptionHelper>();
//FileSetUp
    //Process
        //AllowanceBracketCodeSetUp
builder.Services.AddScoped<IAllowanceBracketCodeSetUpRepository, AllowanceBracketCodeSetUpRepository>();
builder.Services.AddScoped<IAllowanceBracketCodeSetUpService, AllowanceBracketCodeSetUpService>();
//AllowanceBracketingSetUp
builder.Services.AddScoped<IAllowanceBracketingSetUpRepository, AllowanceBracketingSetUpRepository>();
builder.Services.AddScoped<IAllowanceBracketingSetUpService, AllowanceBracketingSetUpService>();
//AllowancePerClassificationSetUp
builder.Services.AddScoped<IAllowancePerClassificationSetUpRepository, AllowancePerClassificationSetUpRepository>();
builder.Services.AddScoped<IAllowancePerClassificationSetUpService, AllowancePerClassificationSetUpService>();
//ClassificationSetUp
builder.Services.AddScoped<IClassificationSetUpRepository, ClassificationSetUpRepository>();
builder.Services.AddScoped<IClassificationSetUpService, ClassificationSetUpService>();
//EarningSetUp
builder.Services.AddScoped<IEarningsSetUpRepository, EarningsSetUpRepository>();
builder.Services.AddScoped<IEarningsSetUpService, EarningsSetUpService>();
//CalendarSetUp
builder.Services.AddScoped<ICalendarSetUpRepository, CalendarSetUpRepository>();
builder.Services.AddScoped<ICalendarSetUpService, CalendarSetUpService>();
//DayTypeSetUp
builder.Services.AddScoped<IDayTypeSetUpRepository, DayTypeSetUpRepository>();
builder.Services.AddScoped<IDayTypeSetUpService, DayTypeSetUpService>();
        //Device
//AMSDbConfigSetUp
builder.Services.AddScoped<IAMSDbConfigSetUpRepository, AMSDbConfigSetUpRepository>();
builder.Services.AddScoped<IAMSDbConfigSetUpService, AMSDbConfigSetUpService>();
//BorrowedDeviceName
builder.Services.AddScoped<IBorrowedDeviceNameRepositoy, BorrowedDeviceNameRepository>();
builder.Services.AddScoped<IBorrowedDeviceNameService, BorrowedDeviceNameService>();
//CoordinatesSetUP
builder.Services.AddScoped<ICoordinatesSetUpRepository, CoordinatesSetUpRepository>();
builder.Services.AddScoped<ICoordinatesSetUpService, CoordinatesSetUpService>();
//DeviceTypeSetUP
builder.Services.AddScoped<IDeviceTypeSetUpRepository, DeviceTypeSetUpRepository>();
builder.Services.AddScoped<IDeviceTypeSetUpService, DeviceTypeSetUpService>();
//DTRFlagSetUp
builder.Services.AddScoped<IDTRFlagSetUpRepository, DTRFlagSetUpRepository>();
builder.Services.AddScoped<IDTRFlagSetUpService, DTRFlagSetUpService>();
//HelpSetUp
builder.Services.AddScoped<IHelpSetUpRepository, HelpSetUpRepository>();
builder.Services.AddScoped<IHelpSetUpService, HelpSetUpService>();
//LeaveTypeSetUp
builder.Services.AddScoped<ILeaveTypeSetUpRepository, LeaveTypeSetUpRepository>();
builder.Services.AddScoped<ILeaveTypeSetUpService, LeaveTypeSetUpService>();
//DTRLogFieldsSetUp
builder.Services.AddScoped<IDTRLogFieldSetUpRepository, DTRLogFieldsSetUpRepository>();
builder.Services.AddScoped<IDTRLogFieldsSetUpService, DTRLogFieldsSetUpService>();
//ForAbsent
builder.Services.AddScoped<IEquivDayForAbsentRepository, EquivDayForAbsentRepository>();
builder.Services.AddScoped<IEquivDayForAbsentService, EquivDayForAbsentService>();
//ForNoLogin
builder.Services.AddScoped<IEquivDayForNoLoginRepository, EquivDayForNologinRepository>();
builder.Services.AddScoped<IEquivDayForNoLoginService, EquivDayForNoLoginService>();
//ForNoLogout
builder.Services.AddScoped<IEquivDayForNoLogOutRepository, EquivDayForNoLogoutRepository>();
builder.Services.AddScoped<IEquivDayForNoLogoutService, EquivDayForNoLogoutService>();
//ForNoBreak2In
builder.Services.AddScoped<IEquivDayForNoBreak2InRepository, EquivDayForNoBreak2InRepository>();
builder.Services.AddScoped<IEquivDayForNoBreak2InService, EquivDayForNoBreak2InService>();
//ForNoBreak2Out
builder.Services.AddScoped<IEquivDayForNoBreak2OutRepository, EquivDayForNoBreak2OutRepository>();
builder.Services.AddScoped<IEquivDayForNoBreak2OutService, EquivDayForNoBreak2OutService>();
//MySQLDbConfig
builder.Services.AddScoped<IMySQLDbConfigSetUpRepository, MySQLDbConfigSetUpRepository>();
builder.Services.AddScoped<IMySQLDbConfigSetUpService, MySQLDbConfigSetUpService>();
//SDKListSetUp
builder.Services.AddScoped<ISDKListSetUpRepository, SDKListSetUpRepository>();
builder.Services.AddScoped<ISDKListSetUpService, SDKListSetUpService>();
//TimeKeepGroupSetUp
builder.Services.AddScoped<ITimeKeepGroupSetUpRepository, TimeKeepGroupSetUpRepository>();
builder.Services.AddScoped<ITimeKeepGroupSetUpService, TimeKeepGroupSetUpService>();
//FileSetUp/Status
//CompanyInformation
builder.Services.AddScoped<ICompanyInformationRepository, CompanyInformationRepository>();
builder.Services.AddScoped<ICompanyInformationService, CompanyInfromationService>();
//FileSetUp/Employee
//AreaSetUp
builder.Services.AddScoped<IAreaSetUpRepository, AreaSetUpRepository>();
builder.Services.AddScoped<IAreaSetUpService, AreaSetUpService>();
//BranchSetUp
builder.Services.AddScoped<IBranchSetUpRepository, BranchSetUpRepository>();
builder.Services.AddScoped<IBranchSetUpService, BranchSetUpService>();
//DepartmentSetUp
builder.Services.AddScoped<IDepartmentSetUpRepository, DepartmentSetUpRepository>();
builder.Services.AddScoped<IDepartmentSetUpService, DepartmentSetUpService>();
//DivisionSetUp
builder.Services.AddScoped<IDivisionSetUpRepository, DivisionSetUpRepository>();
builder.Services.AddScoped<IDivisionSetUpService, DivisionSetUpService>();
//DesignationSetUp
builder.Services.AddScoped<IDesignationSetUpRepository, DesignationSetUpRepository>();
builder.Services.AddScoped<IDesignationSetUpService, DesignationSetUpService>();
//EmployeeStatusSetUp
builder.Services.AddScoped<IEmployeeStatusSetUpRepository, EmployeeStatusSetUpRepository>();
builder.Services.AddScoped<IEmployeeStatusSetUpService, EmployeeStatusSetUpService>();
//GroupSetUp
builder.Services.AddScoped<IGroupSetUpRepository, GroupSetUpRepository>();
builder.Services.AddScoped<IGroupSetUpService, GroupSetUpService>();
//JobLevelSetUp
builder.Services.AddScoped<IJobLevelSetUpRepository, JobLevelSetUpRepository>();
builder.Services.AddScoped<IJobLevelSetUpService, JobLevelSetUpService>();
//LocationSetUp
builder.Services.AddScoped<ILocationSetUpRepository, LocationSetUpRepository>();
builder.Services.AddScoped<ILocationSetUpService, LocationSetUpService>();
//OnlineApprovalSetUp
builder.Services.AddScoped<IOnlineApprovalSetUpRepository, OnlineApprovalSetUpRepository>();
builder.Services.AddScoped<IOnlineApprovalSetUpService, OnlineApprovalSetUpService>();
//PayHouseSetUp
builder.Services.AddScoped<IPayHouseSetUpRepository, PayHouseSetUpRepository>();
builder.Services.AddScoped<IPayHouseSetUpService, PayHouseSetUpService>();
//SectionSetUp
builder.Services.AddScoped<ISectionSetUpRepository, SectionSetUpRepository>();
builder.Services.AddScoped<ISectionSetUpService, SectionSetUpService>();
//UnitSetUp
builder.Services.AddScoped<IUnitSetUpRepository, UnitSetUpRepository>();
builder.Services.AddScoped<IUnitSetUpService, UnitSetUpService>();
//UnitSetUp
builder.Services.AddScoped<IUnitSetUpRepository, UnitSetUpRepository>();
builder.Services.AddScoped<IUnitSetUpService, UnitSetUpService>();


//Maintenance

//EmployeeMasterFile
builder.Services.AddScoped<IEmployeeMasterFileRepository, EmployeeMasterFileRepository>();
builder.Services.AddScoped<IEmployeeMasterFileService, EmployeeMasterFileService>();
//Authentication
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IEncryptionService, EncryptionService>();
// Add this line to enable the cache service
builder.Services.AddMemoryCache();
//LeaveTypes
builder.Services.AddScoped<ILeaveTypesRepository, LeaveTypesRepository>();
builder.Services.AddScoped<ILeaveTypesService, LeaveTypesService>();

//Utilities
//Utilities/Employee Configuration
//Update Status
builder.Services.AddScoped<IUpdateStatusRepository, UpdateStatusRepository>();
builder.Services.AddScoped<IUpdateStatusService, UpdateStatusService>();


//ImportWorkshiftVariable
builder.Services.AddScoped<IImportWorkshiftVariableRepository, ImportWorkshiftVariableRepository>();
builder.Services.AddScoped<IImportWorkshiftVariableService, ImportWorkshiftVariableService>();

//ImportWorkshiftRestday
builder.Services.AddScoped<IImportWorkshiftRestdayService, ImportWorkshiftRestdayService>();

//ImportLeaveApplication
builder.Services.AddScoped<IImportLeaveApplicationRepository, ImportLeaveApplicationRepository>();
builder.Services.AddScoped<IImportLeaveApplicationService, ImportLeaveApplicationService>();

//ImportOvertimeApplication
builder.Services.AddScoped<IImportOvertimeApplicationRepository, ImportOvertimeApplicationRepository>();
builder.Services.AddScoped<IImportOvertimeApplicationService, ImportOvertimeApplicationService>();

//ImportDeviceCode
builder.Services.AddScoped<IImportDeviceCodeRepository, ImportDeviceCodeRepository>();
builder.Services.AddScoped<IImportDeviceCodeService, ImportDeviceCodeService>();

//WorkShift
builder.Services.AddScoped<IWorkShiftRepository, WorkShiftRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(builder.Environment.ContentRootPath, "uploaded")),
    RequestPath = "/uploaded"
});
// 3. Enable CORS middleware (Must be between UseRouting and UseAuthorization)
app.UseCors(myAllowSpecificOrigins);

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();