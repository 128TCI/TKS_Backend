
namespace DomainEntities.Dto;
public class ImportLeaveApplicationDto
{
    public required string EmpCode { get; set; }
    public string? EmpName { get; set; }
    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }
    public double? NumApprovedHrs { get; set; }
    public string? LeaveCode { get; set; }
    public string? Period { get; set; }
    public string? TKSGroup { get; set; }
    public string? Reason { get; set; }
    public string? Remarks { get; set; }
    public bool? WithPay { get; set; }
    public bool? SSSNotif { get; set; }
    public bool? IsLateFiling { get; set; }
    public string Message { get; set; } = "";
    public int RowNumber { get; set; }
    public int ColumnNumber { get; set; }
}
