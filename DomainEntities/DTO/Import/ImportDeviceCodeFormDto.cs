namespace DomainEntities.Dto;

public class ImportDeviceCodeFormDto
{
    public bool IsDeleteExistingRecord { get; set; }
    public List<ImportDeviceCodeDto> Imports { get; set; } = [];
}
