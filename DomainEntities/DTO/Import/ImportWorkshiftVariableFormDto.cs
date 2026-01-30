namespace DomainEntities.Dto;

public class ImportWorkshiftVariableFormDto
{
    public DateTime? DateFrom { get; set; }
    public DateTime? DateTo { get; set; }
    public bool IsDeleteExistingRecord { get; set; }
    public List<ImportWorkShiftVariableDto> Imports { get; set; } = [];
}
