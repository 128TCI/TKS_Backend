namespace DomainEntities.Dto;

public class ImportWorkshiftRestDayFormDto
{
    public DateTime? DateFrom { get; set; }
    public DateTime? DateTo { get; set; }
    public bool IsDeleteExistingRecord { get; set; }
    public List<ImportWorkshiftRestdayDto> Imports { get; set; } = [];
}
