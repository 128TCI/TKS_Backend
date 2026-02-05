using DomainEntities.Dto;
using Microsoft.Data.SqlClient;

namespace Infrastructure.IRepositories.Import;

public interface IImportLogsFromDeviceRepository
{
    Task<List<RawDataDto>> GetRawData(DateTime rawDateInFrom, DateTime rawDateInTo, string empCode);
    Task<List<WorkShiftAsOfDateDto>> GetWorkShiftAsOfDate(string empCode, DateTime date);
    Task<List<WorkShiftByInOrOut>> GetWorkShiftByInOrOut(string empCode, DateTime dateFrom, DateTime dateTo, DateTime dateToCutOff);
    Task<List<DayTypeDto>> GetDayTypePerEmployee(string empCode, DateTime date);
    Task<List<OTApprovedDto>> GetOTApproved(string empCode);
    Task<List<TKEmployeeMasterfileDto>> GetTKEmployeeMasterfile();
    Task UpdateRawData(ImportLogsFromDeviceDto logs);
    Task UpdateOTApproved(string empCode);
    Task UpdateImportToRawData(RawDataDto raw);
}
