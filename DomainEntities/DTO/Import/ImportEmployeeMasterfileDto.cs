
namespace DomainEntities.Dto;
public class ImportEmployeeMasterfileDto
{
    public required string EmpCode { get; set; }
    public string? StatusActive { get; set; }
    public string? EmpStatCode { get; set; }
    public string? Courtesy { get; set; }
    public string? LName { get; set; }
    public string? FName { get; set; }
    public string? MName { get; set; }
    public string? NickName { get; set; }
    public string? HAddress { get; set; }
    public string? PAddress { get; set; }
    public string? City { get; set; }
    public string? Province { get; set; }
    public string? PostalCode { get; set; }
    public string? CivilStatus { get; set; }
    public string? Citizenship { get; set; }
    public string? Religion { get; set; }
    public string? Sex { get; set; }
    public string? Email { get; set; }
    public string? Weight { get; set; }
    public string? Height { get; set; }
    public string? MobilePhone { get; set; }
    public string? HomePhone { get; set; }
    public string? PresentPhone { get; set; }
    public string? BirthPlace { get; set; }
    public string? BraCode { get; set; }
    public string? DivCode { get; set; }
    public string? DepCode { get; set; }
    public string? SecCode { get; set; }
    public string? UnitCode { get; set; }
    public string? LineCode { get; set; }
    public string? DesCode { get; set; }
    public string? Superior {  get; set; }
    public string? GrdCode { get; set; }
    public string? SSSNo { get; set; }
    public string? PhilHealthNo { get; set; }
    public string? TIN {  get; set; }
    public DateTime DateHired { get; set; }
    public DateTime DateRegularized {  get; set; }
    public DateTime DateResigned {  get; set; }
    public DateTime DateSuspended { get; set; }
    public DateTime ProbeStart { get; set; }
    public DateTime ProbeEnd { get; set; }
    public DateTime BirthDate { get; set; }
    public string? TKSGroup {  get; set; }
    public string? GroupSchedCode { get; set; }
    public string? AllowOTDefault { get; set; }
    public string? TardyExemp { get; set; }
    public string? UTExempt { get; set; }
    public string? NDExempt { get; set; }
    public string? OTExempt { get; set; }
    public string? AbsenceExempt { get; set; }
    public string? OtherEarnExempt { get; set; }
    public string? HolidayExempt { get; set; }
    public string? UnproductiveExempt { get; set; }
    public string? DeviceCode { get; set; }
    public string? FixedRestDay1 { get; set; }
    public string? FixedRestDay2 { get; set; }
    public string? FixedRestDay3 { get; set; }
    public string? DailySchedule {  get; set; }
    public string? ClassificationCode { get; set; }
    public string? GSISNo { get; set; }
    public string? Suffix { get; set; }
    public string? OnlineAppCode { get; set; }
    public string Message { get; set; } = "";
    public int RowNumber { get; set; }
    public int ColumnNumber { get; set; }
}
