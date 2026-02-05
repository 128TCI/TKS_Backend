using ClosedXML.Excel;
using DomainEntities.Dto;
using DomainEntities.DTO;
using Infrastructure.IRepositories.Import;
using Infrastructure.IRepositories.Maintenance;
using Infrastructure.IRepositories.WorkShift;
using Microsoft.AspNetCore.Http;
using Services.Extensions;
using Services.Interfaces.Import;
using Timekeeping.Infrastructure.Data;

namespace Services.Services.Import
{
    public class ImportWorkshiftVariableService(
        IImportWorkshiftVariableRepository repository,
        IWorkShiftRepository workShiftRepository,
        IEmployeeMasterFileRepository employeeMasterFileRepository) : IImportWorkshiftVariableService
    {
        private readonly IImportWorkshiftVariableRepository _variableRepository = repository;
        private readonly IWorkShiftRepository _workShiftRepository = workShiftRepository;
        private readonly IEmployeeMasterFileRepository _employeeMasterFileRepository = employeeMasterFileRepository;

        public async Task<ReturnResult<List<ImportWorkShiftVariableDto>>> ImportWorkshiftVariable(IFormCollection form, CancellationToken ct)
        {
            if (form.Files.Count == 0)
                return new ReturnResult<List<ImportWorkShiftVariableDto>>(resultData: [], messages: "", isSuccess: false, errors: ["No File found."]);
            
            string? dateFromString = null, dateToString = null;
            if (form.ContainsKey("DateFrom"))
                dateFromString = form["DateFrom"].ToString();
            if (form.ContainsKey("DateTo"))
                dateToString = form["DateTo"].ToString();
            if (dateFromString == null && dateToString == null)
                return new ReturnResult<List<ImportWorkShiftVariableDto>>(resultData: [], messages: "", isSuccess: false, errors: ["Please select Date Range"]);

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
                return new ReturnResult<List<ImportWorkShiftVariableDto>>(resultData: [], messages: "", isSuccess: false, errors: ["No Data in the file"]);
            if (lastColumn is 0)
                return new ReturnResult<List<ImportWorkShiftVariableDto>>(resultData: [], messages: "", isSuccess: false, errors: ["No Data in the file"]);

            List<ImportWorkShiftVariableDto> imports = ImportWorkshift(worksheet, lastRow, lastColumn);
            #region Validate Import Data
            ReturnResult<List<ImportWorkShiftVariableDto>> errors = await ValidateImportWorkshiftVariable(imports, dateFrom, dateTo, ct);
            if (!errors.IsSuccess)
            {
                return errors;
            }
            #endregion
            return new ReturnResult<List<ImportWorkShiftVariableDto>>(resultData: imports, messages: "");
        }

        //Update
        public async Task<ReturnResult<string>> UpdateWorkshiftVariable(ImportWorkshiftVariableFormDto param, CancellationToken ct)
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
            ReturnResult<List<ImportWorkShiftVariableDto>> errors = await ValidateImportWorkshiftVariable(param.Imports, param.DateFrom!.Value, param.DateTo!.Value, ct);
            if (!errors.IsSuccess)
            {
                return new ReturnResult<string>(resultData: "", messages: ResponseMessage.ERROR, isSuccess: false, errors: ["Cannot proceed to import. Please fix the data in the import list."]);
            }
            #endregion

            if (param.IsDeleteExistingRecord)
            {
                string empCodes = string.Join(",", param.Imports.Select(x => x.EmpCode).Distinct().ToList());
                await _variableRepository.DeleteWorkshiftVariable(empCodes, param.DateFrom, param.DateTo);
            }
            foreach (var item in param.Imports.Where(x => x.DateFrom.Date >= param.DateFrom!.Value.Date && x.DateTo.Date <= param.DateTo!.Value.Date))
            {
                if (!string.IsNullOrEmpty(item.ShiftCode))
                {
                    await _variableRepository.UpdateWorkshiftVariable(item.EmpCode, item.DateFrom, item.DateTo, item.ShiftCode);
                }
            }
            return new ReturnResult<string>(resultData: "", messages: ResponseMessage.SAVE);
        }

        internal static List<ImportWorkShiftVariableDto> ImportWorkshift(IXLWorksheet worksheet, int lastRow, int lastColumn)
        {
            List<ImportWorkShiftVariableDto> imports = [];
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
                    imports.Add(new ImportWorkShiftVariableDto
                    {
                        EmpCode = empCode,
                        DateFrom = dateFrom,
                        DateTo = dateTo,
                        ShiftCode = shiftCode
                    });
                }

            }
            return imports.OrderBy(x => x.DateFrom).ToList();
        }
        internal async Task<ReturnResult<List<ImportWorkShiftVariableDto>>> ValidateImportWorkshiftVariable(
                   List<ImportWorkShiftVariableDto> imports,
                   DateTime dateFrom,
                   DateTime dateTo,
                   CancellationToken ct)
        {
            if (imports.Count == 0)
            {
                return new ReturnResult<List<ImportWorkShiftVariableDto>>(resultData: [], messages: "", isSuccess: false, errors: ["No Data"]);
            }
            ImportWorkShiftVariableDto first = imports.First();
            ImportWorkShiftVariableDto last = imports.Last();
            if ((dateFrom.Month != first.DateFrom.Month || dateFrom.Year != first.DateFrom.Year) ||
                (dateTo.Month != last.DateTo.Month || dateTo.Year != last.DateTo.Year))
            {
                return new ReturnResult<List<ImportWorkShiftVariableDto>>(resultData: [], messages: "", isSuccess: false, errors: ["Selected date range is mismatch in the importing file"]);
            }
            List<ImportWorkShiftVariableDto> errors = [];
            for (int i = 0; i < imports.Count; i++)
            {
                ImportWorkShiftVariableDto import = imports[i];
                string message = "";
                var isEmployeeExists = await _employeeMasterFileRepository.GetByEmpCode(import.EmpCode);
                var workshifts = await _workShiftRepository.GetWorkShift(import.ShiftCode);
                if (isEmployeeExists is null)
                {
                    message += "Employee does not exists. ";
                }
                if (!string.IsNullOrEmpty(import.ShiftCode) && string.IsNullOrEmpty(import.ShiftCode) && workshifts == null)
                {
                    message += "Workshift Code does not exists. ";
                }
                if (!string.IsNullOrEmpty(message))
                {
                    message += $". Row Number {import.RowNumber}, Column {import.ColumnNumber}";
                    if (!errors.Where(x => x.EmpCode == import.EmpCode && x.ShiftCode == import.ShiftCode).Any())
                    {
                        errors.Add(new ImportWorkShiftVariableDto
                        {
                            EmpCode = import.EmpCode,
                            Message = message,
                            DateFrom = import.DateFrom,
                            DateTo = import.DateTo,
                            ShiftCode = import.ShiftCode,
                            RowNumber = import.RowNumber,
                            ColumnNumber = import.ColumnNumber
                        });
                    }
                }
            }

            return new ReturnResult<List<ImportWorkShiftVariableDto>>(resultData: errors, messages: errors.Count is 0 ? "" : "Error, Please see message in the table below", isSuccess: errors.Count is 0, errors: ["Error, Please see message in the table below"]);
        }
    }
}
