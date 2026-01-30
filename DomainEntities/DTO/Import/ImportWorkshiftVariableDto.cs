
namespace DomainEntities.Dto;
public class ImportWorkShiftVariableDto
{
    public int ID { get; set; }
    public required string EmpCode { get; set; }
    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }
    public required string ShiftCode { get; set; }
    public string Message { get; set; } = "";
    public int RowNumber { get; set; }
    public int ColumnNumber { get; set; }
}
