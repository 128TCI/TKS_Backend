
using Microsoft.EntityFrameworkCore;

namespace DomainEntities.Dto;
public class ImportLogsFromDeviceDto
{
    public long ID { get; set; }
    public string? EmpCode { get; set; }
    public string? Name { get; set; }
    public DateTime DateIn { get; set; }
    public DateTime DateOut { get; set; }
    public DateTime? TimeIn { get; set; }
    public DateTime? TimeOut { get; set; }
    public string? WorkShiftCode { get; set; }
    public string? DayType { get; set; }
    public string? Flag { get; set; }
    public string? RawID { get; set; }
    public int? Copy { get; set; }
    public string? Hash { get; set; }
    public DateTime? Break1Out { get; set; }
    public DateTime? Break1In { get; set; }
    public DateTime? Break2Out { get; set; }
    public DateTime? Break2In { get; set; }
    public DateTime? Break3Out { get; set; }
    public DateTime? Break3In { get; set; }
    public string? TerminalID { get; set; }
    public string? DeviceName { get; set; }

    public DateTime? ActualDateIn { get; set; }

    public string? Identifier { get; set; }
    public string Message { get; set; } = "";
    public int RowNumber { get; set; }
    public int ColumnNumber { get; set; }
}

[Keyless]
public class OTApprovedDto
{
    public bool AllowOTDefault { get; set; }
}
