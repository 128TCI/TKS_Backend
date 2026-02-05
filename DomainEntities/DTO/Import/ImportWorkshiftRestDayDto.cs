
namespace DomainEntities.Dto;
public class ImportWorkshiftRestdayDto
{
    public string Message { get; set; } = "";
    public int RowNumber { get; set; }
    public int ColumnNumber { get; set; }
    public required string EmpCode { get; set; }
    public string EmployeeName { get; set; } = "";
    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }
    public required string WorkshiftCode { get; set; }
    public string RestDay { get; set; } = "";
}
