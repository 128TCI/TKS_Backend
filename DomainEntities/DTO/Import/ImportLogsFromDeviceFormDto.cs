namespace DomainEntities.Dto;

public class ImportLogsFromDeviceFormDto
{
    public string? EmployeeCode { get; set; }
    public DateTime? DateFrom { get; set; }
    public DateTime? DateTo { get; set; }
    public string? Devices { get; set; }
    public string? Worksheets { get; set; }
    public bool EnableDeviceCode { get; set; }
    public bool DoNotIncludeResignedEmployees { get; set; }
    public List<ImportLogsFromDeviceDto> Imports { get; set; } = [];
}
