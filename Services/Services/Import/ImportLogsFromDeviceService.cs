using BCrypt.Net;
using ClosedXML.Excel;
using DomainEntities.Dto;
using DomainEntities.DTO;
using Infrastructure.IRepositories.Import;
using Infrastructure.IRepositories.Maintenance;
using Infrastructure.IRepositories.WorkShift;
using Microsoft.AspNetCore.Http;
using Services.DTOs.Encryption;
using Services.Extensions;
using Services.Interfaces.Import;

namespace Services.Services.Import
{
    public class ImportLogsFromDeviceService(
        IImportLogsFromDeviceRepository logsRepository,
        IWorkShiftRepository workShiftRepository,
        IEmployeeMasterFileRepository employeeMasterFileRepository) : IImportLogsFromDeviceService
    {
        private readonly IImportLogsFromDeviceRepository _logsRepository = logsRepository;
        private readonly IWorkShiftRepository _workShiftRepository = workShiftRepository;
        private readonly IEmployeeMasterFileRepository _employeeMasterFileRepository = employeeMasterFileRepository;

        public async Task<ReturnResult<List<ImportLogsFromDeviceDto>>> ImportLogsFromDevice(IFormCollection form, CancellationToken ct)
        {
            if (form.Files.Count == 0)
                return new ReturnResult<List<ImportLogsFromDeviceDto>>(resultData: [], messages: "", isSuccess: false, errors: ["No File found."]);
            
            string? dateFromString = null, dateToString = null;
            if (form.ContainsKey("DateFrom"))
                dateFromString = form["DateFrom"].ToString();
            if (form.ContainsKey("DateTo"))
                dateToString = form["DateTo"].ToString();
            if (dateFromString == null && dateToString == null)
                return new ReturnResult<List<ImportLogsFromDeviceDto>>(resultData: [], messages: "", isSuccess: false, errors: ["Please select Date Range"]);

            DateTime dateFrom = dateFromString!.ToDateTime(),
                     dateTo = dateToString!.ToDateTime();

            using MemoryStream stream = new();

            await form.Files[0].CopyToAsync(stream, ct);
            stream.Position = 0;

            using XLWorkbook workbook = new(stream);
            IXLWorksheet worksheet = workbook.Worksheet(1);

            int lastRow = worksheet.LastRowUsed()?.RowNumber() ?? 0;
            IXLCell? lastCell = worksheet.Row(2).CellsUsed()
                      .Where(c => c.Address.ColumnNumber >= 4)
                      .LastOrDefault();
            int lastColumn = lastCell?.Address.ColumnNumber ?? 0;
            if (lastRow is 0)
                return new ReturnResult<List<ImportLogsFromDeviceDto>>(resultData: [], messages: "", isSuccess: false, errors: ["No Data in the file"]);
            if (lastColumn is 0)
                return new ReturnResult<List<ImportLogsFromDeviceDto>>(resultData: [], messages: "", isSuccess: false, errors: ["No Data in the file"]);

            List<ImportLogsFromDeviceDto> imports = ImportLogsDevice(worksheet, lastRow, lastColumn);
            #region Validate Import Data
            ReturnResult<List<ImportLogsFromDeviceDto>> errors = await ValidateImportLogsFromDevice(imports, dateFrom, dateTo, ct);
            if (!errors.IsSuccess)
            {
                return errors;
            }
            #endregion
            return new ReturnResult<List<ImportLogsFromDeviceDto>>(resultData: imports, messages: "");
        }

        //Update
        public async Task<ReturnResult<string>> UpdateLogsFromDevice(ImportLogsFromDeviceFormDto param, CancellationToken ct)
        {

            if (!param.DateFrom.HasValue && !param.DateFrom.HasValue)
            {
                return new ReturnResult<string>(resultData: "", messages: ResponseMessage.ERROR, isSuccess: false, errors: ["Please select Date Range"]);
            }
            if (param.Imports.Where(x => !x.Message.IsStringNullOrEmpty()).Count() > 0)
            {
                return new ReturnResult<string>(resultData: "", messages: ResponseMessage.ERROR, isSuccess: false, errors: ["Cannot proceed to import. Please fix the data in the import list."]);
            }

            #region Validate Import Data
            ReturnResult<List<ImportLogsFromDeviceDto>> errors = await ValidateImportLogsFromDevice(param.Imports, param.DateFrom!.Value, param.DateTo!.Value, ct);
            if (!errors.IsSuccess)
            {
                return new ReturnResult<string>(resultData: "", messages: ResponseMessage.ERROR, isSuccess: false, errors: ["Cannot proceed to import. Please fix the data in the import list."]);
            }
            #endregion

            foreach (var item in param.Imports.Where(x => x.DateIn.Date >= param.DateFrom!.Value.Date && x.DateIn.Date <= param.DateTo!.Value.Date))
            {
                await Task.WhenAll(
                    _logsRepository.UpdateRawData(item),
                    _logsRepository.UpdateOTApproved(item.EmpCode!)
                );
            }
            return new ReturnResult<string>(resultData: "", messages: ResponseMessage.SAVE);
        }

        internal static List<ImportLogsFromDeviceDto> ImportLogsDevice(IXLWorksheet worksheet, int lastRow, int lastColumn)
        {
            List<ImportLogsFromDeviceDto> imports = [];
            int firstDataColumn = 1;
          
            for (int row = 2; row <= lastRow; row++)
            {
                if (worksheet.Cell(row, 1).GetString() == null) continue;
                string empCode = worksheet.Cell(row, 1).Value.ToString();
                string dFrom = worksheet.Cell(row, 2).Value.ToString(),
                       dTo = worksheet.Cell(row, 3).Value.ToString();

                string shiftCode = worksheet.Cell(row, 4).Value.ToString();

                DateTime dateFrom = Convert.ToDateTime(dFrom);
                DateTime dateTo = Convert.ToDateTime(dTo);

                if (string.IsNullOrEmpty(empCode))
                    continue;

                for (int col = firstDataColumn; col <= lastColumn; col++)
                {                    
                    imports.Add(new ImportLogsFromDeviceDto
                    {
                        EmpCode = empCode
                    });
                }

            }
            return imports.ToList();
        }
        public async Task<ReturnResult<List<ImportLogsFromDeviceDto>>> ValidateImportLogsFromDevice(
                   List<ImportLogsFromDeviceDto> imports,
                   DateTime dateFrom,
                   DateTime dateTo,
                   CancellationToken ct)
        {
            if (imports.Count == 0)
            {
                return new ReturnResult<List<ImportLogsFromDeviceDto>>(resultData: [], messages: "", isSuccess: false, errors: ["No Data"]);
            }
            ImportLogsFromDeviceDto first = imports.First();
            ImportLogsFromDeviceDto last = imports.Last();
            if ((dateFrom.Month != first.DateIn.Month || dateFrom.Year != first.DateIn.Year) ||
                (dateTo.Month != last.DateOut.Month || dateTo.Year != last.DateOut.Year))
            {
                return new ReturnResult<List<ImportLogsFromDeviceDto>>(resultData: [], messages: "", isSuccess: false, errors: ["Selected date range is mismatch in the importing file"]);
            }
            List<ImportLogsFromDeviceDto> errors = [];
            for (int i = 0; i < imports.Count; i++)
            {
                ImportLogsFromDeviceDto import = imports[i];
                string message = "";
                var isEmployeeExists = await _employeeMasterFileRepository.GetByEmpCode(import.EmpCode!);
                var workshifts = await _workShiftRepository.GetWorkShift(import.WorkShiftCode!);
                if (isEmployeeExists is null)
                {
                    message += "Employee does not exists. ";
                }
                if (!string.IsNullOrEmpty(import.WorkShiftCode) && string.IsNullOrEmpty(import.WorkShiftCode) && workshifts == null)
                {
                    message += "Workshift Code does not exists. ";
                }
                if (!string.IsNullOrEmpty(message))
                {
                    message += $". Row Number {import.RowNumber}, Column {import.ColumnNumber}";
                    if (!errors.Where(x => x.EmpCode == import.EmpCode && x.WorkShiftCode == import.WorkShiftCode).Any())
                    {
                        errors.Add(new ImportLogsFromDeviceDto
                        {
                            EmpCode = import.EmpCode,
                            Message = message,
                            RowNumber = import.RowNumber,
                            ColumnNumber = import.ColumnNumber
                        });
                    }
                }
            }

            return new ReturnResult<List<ImportLogsFromDeviceDto>>(resultData: errors, messages: errors.Count is 0 ? "" : "Error, Please see message in the table below", isSuccess: errors.Count is 0, errors: ["Error, Please see message in the table below"]);
        }
        public async Task<List<WorkShiftByInOrOut>> GetWorkShiftByInOrOut(string empCode, DateTime dateFrom, DateTime dateTo, DateTime dateToCutOff)
        {
            var workShift = await _logsRepository.GetWorkShiftByInOrOut(empCode, dateFrom, dateTo, dateToCutOff);
            return workShift;
        }
        public async Task<List<OTApprovedDto>> GetOTApproved(string empCode)
        {
            var otApproved = await _logsRepository.GetOTApproved(empCode);
            return otApproved;
        }
    }
}
