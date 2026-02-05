
using DomainEntities.Dto;
using Microsoft.AspNetCore.Http;


namespace Services.Interfaces.Import;

public interface IImportLogsFromDeviceService
{
    Task<ReturnResult<List<ImportLogsFromDeviceDto>>> ImportLogsFromDevice(IFormCollection form, CancellationToken ct);
    Task<ReturnResult<string>> UpdateLogsFromDevice(ImportLogsFromDeviceFormDto param, CancellationToken ct);
    Task<ReturnResult<List<ImportLogsFromDeviceDto>>> ValidateImportLogsFromDevice(
                   List<ImportLogsFromDeviceDto> imports,
                   DateTime dateFrom,
                   DateTime dateTo,
                   CancellationToken ct);
    Task<List<WorkShiftByInOrOut>> GetWorkShiftByInOrOut(string empCode, DateTime dateFrom, DateTime dateTo, DateTime dateToCutOff);
    Task<List<OTApprovedDto>> GetOTApproved(string empCode);
}

