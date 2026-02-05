namespace DomainEntities.Dto;

public class ImportLeaveApplicationFormDto
{
    public DateTime? DateFrom { get; set; }
    public DateTime? DateTo { get; set; }
    public bool IsDeleteExistingRecord { get; set; }
    public List<ImportLeaveApplicationDto> Imports { get; set; } = [];
}
