
namespace DomainEntities.Dto;
public class ImportDeviceCodeDto
{
    public required string EmpCode { get; set; }
    public DateTime EffectivityDate { get; set; }
    public DateTime ExpiryDate { get; set; }
    public required string Code { get; set; }
    public string Message { get; set; } = "";
    public int RowNumber { get; set; }
    public int ColumnNumber { get; set; }
}
