namespace DomainEntities.Dto;

public class ImportAdjustmentFormDto
{
    public DateTime? DateFrom { get; set; }
    public DateTime? DateTo { get; set; }
    public List<ImportAdjustmentDto> Imports { get; set; } = [];
}
