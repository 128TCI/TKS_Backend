
namespace DomainEntities.Dto;
public class ImportOvertimeApplicationDto
{
    public required string EmpCode { get; set; }
    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }
    public double NumOTHoursApproved { get; set; }
    public DateTime EarlyOTStartTime { get; set; }
    public DateTime EarlyTimeIn {  get; set; }
    public DateTime StartOTPM {  get; set; }
    public double MinHRSOTBreak { get; set; }
    public DateTime EarlyOTStartTimeRestHol { get; set; }
    public string? Reason { get; set; }
    public string? Remarks { get; set; }
    public double ApprovedOTBreaksHrs { get; set; }
    public DateTime STOTATS {  get; set; }
    public bool IsLateFiling { get; set; }
    public bool IsLateFilingProcessed { get; set; }
    public DateTime AppliedBeforeShiftDate { get; set; }
    public string Message { get; set; } = "";
    public int RowNumber { get; set; }
    public int ColumnNumber { get; set; }
}
