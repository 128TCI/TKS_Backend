namespace DomainEntities.Dto;

public class UpdateStatusDto
{
    public required string EmpCode { get; set; }
    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }
    public string Message { get; set; } = "";
    public int RowNumber { get; set; }
    public int ColumnNumber { get; set; }
    public bool IsDeleteExistingRecord { get; set; }
    public List<UpdateStatusDto> Updates { get; set; } = [];
}
