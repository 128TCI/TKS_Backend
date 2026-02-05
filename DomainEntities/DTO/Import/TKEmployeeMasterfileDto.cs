
using DomainEntities.DTO.Maintenance;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DomainEntities.Dto;

public class TKEmployeeMasterfileDto: EmployeeMasterFile
{
    public string? EmpStatDesc { get; set; }
    public string? BranchDesc { get; set; }
    public string? DivDesc { get; set; }
    public string? DepDesc { get; set; }
    public string? SecDesc { get; set; }
    public string? UnitDesc { get; set; }
    public string? LineDesc { get; set; }
    public string? DesDesc { get; set; }
    public string? ShiftDesc { get; set; }
    public string? BasicGroupScheduleCode { get; set; }
    public string? GroupCode { get; set; }
    public string? PayLoc {  get; set; }
    public string? FormattedName { get; set; }

}
