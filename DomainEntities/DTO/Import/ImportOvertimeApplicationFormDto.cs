namespace DomainEntities.Dto;

public class ImportOvertimeApplicationFormDto
{
    public DateTime? DateFrom { get; set; }
    public DateTime? DateTo { get; set; }
    public bool IsDeleteExistingRecord { get; set; }
    public List<ImportOvertimeApplicationDto> Imports { get; set; } = [];
}
