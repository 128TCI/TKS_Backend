
namespace DomainEntities.Dto;
public class RawDataDto
{
    public long ID { get; set; }
    public string? EmpCode { get; set; }
    public string? FullName { get; set; }
    public DateTime RawDateIn { get; set; }
    public string? WorkShiftCode { get; set; }
    public string? WorkShiftDesc { get; set; }
    public string? DayType { get; set; }
    public DateTime? RawTimeIn { get; set; }
    public DateTime? RawBreak1In { get; set; }
    public DateTime? RawBreak1Out { get; set; }
    public DateTime? RawBreak2In { get; set; }
    public DateTime? RawBreak2Out { get; set; }
    public DateTime? RawBreak3In { get; set; }
    public DateTime? RawBreak3Out { get; set; }
    public DateTime? RawTimeOut { get; set; }
    public DateTime RawDateOut { get; set; }
    public bool RawOTApproved { get; set; }
    public string? RawRemarks { get; set; }
    public string? EntryFlag { get; set; }
    public string? TerminalID { get; set; }
    public string? DayTypeDOLE { get; set; }
    public DateTime? AprOTTime { get; set; }
}
