using ClosedXML.Excel;
using DomainEntities.Dto;
using DomainEntities.DTO;
using DomainEntities.DTO.Maintenance;
using Infrastructure.IRepositories.Import;
using Infrastructure.IRepositories.Maintenance;
using Infrastructure.IRepositories.WorkShift;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Services.Extensions;
using Services.Interfaces.Import;
using Timekeeping.Infrastructure.Data;

namespace Services.Services.Import
{
    public class ImportWorkshiftRestdayService(
        IImportWorkshiftVariableRepository repository,
        IWorkShiftRepository workShiftRepository,
        IEmployeeMasterFileRepository employeeMasterFileRepository) : IImportWorkshiftRestdayService
    {
        private readonly IImportWorkshiftVariableRepository _variableRepository = repository;
        private readonly IWorkShiftRepository _workShiftRepository = workShiftRepository;
        private readonly IEmployeeMasterFileRepository _employeeMasterFileRepository = employeeMasterFileRepository;

        public async Task<ReturnResult<List<ImportWorkshiftRestdayDto>>> ImportWorkshiftWRestDay(IFormCollection form, CancellationToken ct)
        {
            if (form.Files.Count == 0)
                return new ReturnResult<List<ImportWorkshiftRestdayDto>>(resultData: [], messages: "", isSuccess: false, errors: ["No file Found"]);
            
            string? dateFromString = null, dateToString = null;
            if (form.ContainsKey("DateFrom"))
                dateFromString = form["DateFrom"].ToString();
            if (form.ContainsKey("DateTo"))
                dateToString = form["DateTo"].ToString();
            if (dateFromString == null && dateToString == null)
                return new ReturnResult<List<ImportWorkshiftRestdayDto>>(resultData: [], messages: "", isSuccess: false, errors: ["Please select Date Range"]);

            DateTime dateFrom = dateFromString!.ToDateTime(),
                     dateTo = dateToString!.ToDateTime();

            using MemoryStream stream = new();

            await form.Files[0].CopyToAsync(stream, ct);
            stream.Position = 0;

            using XLWorkbook workbook = new(stream);
            IXLWorksheet worksheet = workbook.Worksheet(1);

            int lastRow = worksheet.LastRowUsed()?.RowNumber() ?? 0;
            IXLCell? lastCell = worksheet.Row(7).CellsUsed()
                      .Where(c => c.Address.ColumnNumber >= 4)
                      .LastOrDefault();
            int lastColumn = lastCell?.Address.ColumnNumber ?? 0;
            if (lastRow is 0)
                return new ReturnResult<List<ImportWorkshiftRestdayDto>>(resultData: [], messages: "", isSuccess: false, errors: ["No Data in the file"]);
            if (lastColumn is 0)
                return new ReturnResult<List<ImportWorkshiftRestdayDto>>(resultData: [], messages: "", isSuccess: false, errors: ["No Data in the file"]);

            List<ImportWorkshiftRestdayDto> imports = ImportWorkshiftWRestday(worksheet, lastRow, lastColumn);
            #region Validate Import Data
            ReturnResult<List<ImportWorkshiftRestdayDto>> errors = await ValidateImportWorkshiftWRestday(imports, dateFrom, dateTo, ct);
            if (!errors.IsSuccess)
            {
                return errors;
            }
            #endregion
            return new ReturnResult<List<ImportWorkshiftRestdayDto>>(resultData: imports, messages: "");
        }
        public async Task<ReturnResult<string>> UpdateRestDay(ImportWorkshiftRestDayFormDto param, CancellationToken ct)
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
            ReturnResult<List<ImportWorkshiftRestdayDto>> errors = await ValidateImportWorkshiftWRestday(param.Imports, param.DateFrom!.Value, param.DateTo!.Value, ct);
            if (!errors.IsSuccess)
            {
                return new ReturnResult<string>(resultData: "", messages: ResponseMessage.ERROR, isSuccess: false, errors: ["Cannot proceed to import. Please fix the data in the import list."]);
            }
            #endregion

            if (param.IsDeleteExistingRecord)
            {
                string empCodes = string.Join(",", param.Imports.Select(x => x.EmpCode).Distinct().ToList());
                //_context.Database.ExecuteSqlInterpolated($"EXEC dbo.sp_tk_DeleteExistingRestDayVariable {empCodes}, {param.DateFrom}, {param.DateTo}");
                await _variableRepository.DeleteWorkshiftVariable(empCodes, param.DateFrom, param.DateTo);
            }
            foreach (var item in param.Imports.Where(x => x.DateFrom.Date >= param.DateFrom!.Value.Date && x.DateTo.Date <= param.DateTo!.Value.Date))
            {
                if (!string.IsNullOrEmpty(item.WorkshiftCode))
                {
                    //_context.Database.ExecuteSqlInterpolated($"EXEC dbo.sp_tk_ImportWorkshift {item.EmpCode}, {item.DateFrom}, {item.DateTo}, {item.WorkshiftCode}");
                    await _variableRepository.UpdateWorkshiftVariable(item.EmpCode, item.DateFrom, item.DateTo, item.WorkshiftCode);
                }
                if (!string.IsNullOrEmpty(item.RestDay))
                {
                    //_context.Database.ExecuteSqlInterpolated($"EXEC dbo.sp_tk_ImportWorkshiftWithRestDay {item.EmpCode}, {item.DateFrom}, {item.DateTo}");
                    await _variableRepository.UpdateWorkshiftWithRestDay(item.EmpCode, item.DateFrom, item.DateTo);
                }
            }
            return new ReturnResult<string>(resultData: "", messages: ResponseMessage.SAVE);
        }
        internal static List<ImportWorkshiftRestdayDto> ImportWorkshiftWRestday(IXLWorksheet worksheet, int lastRow, int lastColumn)
        {
            List<ImportWorkshiftRestdayDto> imports = [];
            string month = worksheet.Cell(5, 2).Value.ToString(),
                   year = worksheet.Cell(6, 2).Value.ToString();

            DateTime baseDate = Convert.ToDateTime($"{month} 01, {year}");
            int firstDataColumn = 4;
            for (int row = 10; row <= lastRow; row++)
            {
                if (worksheet.Cell(row, 1).GetString() == null) continue;
                string empCode = worksheet.Cell(row, 1).Value.ToString();
                string empName = worksheet.Cell(row, 2).Value.ToString();
                if (string.IsNullOrEmpty(empCode))
                    continue;

                for (int col = firstDataColumn; col <= lastColumn; col++)
                {
                    string rowAValue = worksheet.Cell(row, col).Value.ToString();
                    string dayNumberString = worksheet.Cell(7, col).Value.ToString();
                    if (string.IsNullOrEmpty(dayNumberString) || string.IsNullOrEmpty(rowAValue))
                        continue;

                    int dayNumber = Convert.ToInt32(dayNumberString);
                    string restDay = "";
                    string workshiftCode = rowAValue.Substring(0, 2) == "RD" ? rowAValue.Substring(2).Trim() : rowAValue;

                    // Create the full date (assuming the day numbers are valid for that month)
                    DateTime fullDate;
                    try
                    {
                        fullDate = new DateTime(baseDate.Year, baseDate.Month, dayNumber);
                    }
                    catch
                    {
                        throw;
                    }

                    if (rowAValue.Substring(0, 2) == "RD")
                    {
                        if (fullDate.DayOfWeek == DayOfWeek.Sunday)
                            restDay = "Sunday";
                        if (fullDate.DayOfWeek == DayOfWeek.Monday)
                            restDay = "Monday";
                        if (fullDate.DayOfWeek == DayOfWeek.Tuesday)
                            restDay = "Tuesday";
                        if (fullDate.DayOfWeek == DayOfWeek.Wednesday)
                            restDay = "Wednesday";
                        if (fullDate.DayOfWeek == DayOfWeek.Thursday)
                            restDay = "Thursday";
                        if (fullDate.DayOfWeek == DayOfWeek.Friday)
                            restDay = "Friday";
                        if (fullDate.DayOfWeek == DayOfWeek.Saturday)
                            restDay = "Saturday";
                    }
                    imports.Add(new ImportWorkshiftRestdayDto
                    {
                        EmpCode = empCode,
                        EmployeeName = empName,
                        RowNumber = row,
                        ColumnNumber = col,
                        DateFrom = fullDate,
                        DateTo = fullDate,
                        WorkshiftCode = workshiftCode,
                        RestDay = restDay
                    });
                }
            }

            return imports.OrderBy(x => x.DateFrom).ToList();
        }
        internal async Task<ReturnResult<List<ImportWorkshiftRestdayDto>>> ValidateImportWorkshiftWRestday(
           List<ImportWorkshiftRestdayDto> imports,
           DateTime dateFrom,
           DateTime dateTo,
           CancellationToken ct)
        {
            if (imports.Count == 0)
            {
                return new ReturnResult<List<ImportWorkshiftRestdayDto>>(resultData: [], messages: "", isSuccess: false, errors: ["No Data"]);
            }
            ImportWorkshiftRestdayDto first = imports.First();
            ImportWorkshiftRestdayDto last = imports.Last();
            if((dateFrom.Month != first.DateFrom.Month || dateFrom.Year != first.DateFrom.Year) ||
                (dateTo.Month != last.DateTo.Month || dateTo.Year != last.DateTo.Year))
            {
                return new ReturnResult<List<ImportWorkshiftRestdayDto>>(resultData: [], messages: "", isSuccess: false, errors: ["Selected date range is mismatch in the importing file"]);
            }         
            List<ImportWorkshiftRestdayDto> errors = [];
            for (int i = 0; i < imports.Count; i++)
            {
                ImportWorkshiftRestdayDto import = imports[i];
                string message = "";
                var isEmployeeExists = await _employeeMasterFileRepository.GetByEmpCode(import.EmpCode);
                var workshifts = await _workShiftRepository.GetWorkShift(import.WorkshiftCode);
                if (isEmployeeExists is null)
                {
                    message += "Employee does not exists. ";
                }
                if (!string.IsNullOrEmpty(import.WorkshiftCode) && string.IsNullOrEmpty(import.RestDay) && workshifts == null)
                {
                    message += "Workshift Code does not exists. ";
                }
                if (!string.IsNullOrEmpty(message))
                {
                    message += $". Row Number {import.RowNumber}, Column {import.ColumnNumber}";
                    if (!errors.Where(x => x.EmpCode == import.EmpCode && x.WorkshiftCode == import.WorkshiftCode).Any())
                    {
                        errors.Add(new ImportWorkshiftRestdayDto
                        {
                            EmpCode = import.EmpCode,
                            EmployeeName = import.EmployeeName,
                            Message = message,
                            DateFrom = import.DateFrom,
                            DateTo = import.DateTo,
                            WorkshiftCode = import.WorkshiftCode,
                            RestDay = import.RestDay,
                            RowNumber = import.RowNumber,
                            ColumnNumber = import.ColumnNumber
                        });
                    }
                }
            }

            return new ReturnResult<List<ImportWorkshiftRestdayDto>>(resultData: errors, messages: errors.Count is 0 ? "" : "Error, Please see message in the table below", isSuccess: errors.Count is 0, errors: ["Error, Please see message in the table below"]);
        }
    }
}
