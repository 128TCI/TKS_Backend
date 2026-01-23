using DomainEntities.DTO;
using Infrastructure.IRepositories.FileSetUp.Employment;
using Infrastructure.Repositories.FileSetup.Employment;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces.Authentication;
using Services.Interfaces.Employee;
using Services.Interfaces.Encryption;
using Timekeeping.Infrastructure.Data;
using Services.Services.FileSetUp.Employment;
using Services.Interfaces.FileSetUp.Employment;
using Infrastructure.IRepositories.UserRepository;
using Infrastructure.Repositories.UserRepository;
using Services.Services.UserRepository;
using Infrastructure.IRepositories.Maintenance;
using Infrastructure.Repositories.Maintennace;
using Services.Services.Maintenance;
using Services.Interfaces.Maintenence;


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

// Dependency Injection
//User
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

//FileSetUp

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

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// 3. Enable CORS middleware (Must be between UseRouting and UseAuthorization)
app.UseCors(myAllowSpecificOrigins);

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();