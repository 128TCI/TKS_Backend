
using Microsoft.EntityFrameworkCore;

namespace DomainEntities.Dto;
public class ImportAdjustmentDto
{
    public required string EmpCode { get; set; }
    public string? EmpName { get; set; }
    public DateTime TransactionDate { get; set; }
    public string? TransactionType { get; set; }
    public string? LeaveType { get; set; }
    public string? OvertimeCode { get; set; }
    public double NoOfHours { get; set; }
    public string? AdjustType { get; set; }
    public string? Remarks { get; set; }
    public string? Status { get; set; }
    public bool? IsLateFiling { get; set; }
    public DateTime IsLateFilingActualDate { get; set; }
    public string Message { get; set; } = "";
    public int RowNumber { get; set; }
    public int ColumnNumber { get; set; }
}
[Keyless]
public class EmpGroupImportAdjust
{
    public required string EmpCode { get; set; }
}
