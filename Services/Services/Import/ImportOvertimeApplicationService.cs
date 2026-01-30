using ClosedXML.Excel;
using DomainEntities.Dto;
using DomainEntities.DTO;
using DomainEntities.DTO.Maintenance;
using Infrastructure.IRepositories.Import;
using Infrastructure.IRepositories.Maintenance;
using Microsoft.AspNetCore.Http;
using Services.Extensions;
using Services.Interfaces.Import;
using System.Reflection.Emit;

namespace Services.Services.Import
{
    public class ImportOvertimeApplicationService(
        IImportOvertimeApplicationRepository repository,
        IEmployeeMasterFileRepository employeeMasterFileRepository) : IImportOvertimeApplicationService
    {
        private readonly IImportOvertimeApplicationRepository _repository = repository;
        private readonly IEmployeeMasterFileRepository _employeeMasterFileRepository = employeeMasterFileRepository;

        public async Task<ReturnResult<List<ImportOvertimeApplicationDto>>> ImportOvertimeApplication(IFormCollection form, CancellationToken ct)
        {
            if (form.Files.Count == 0)
                return new ReturnResult<List<ImportOvertimeApplicationDto>>(resultData: [], messages: "", isSuccess: false, errors: ["No File found."]);
            
            string? dateFromString = null, dateToString = null;
            if (form.ContainsKey("DateFrom"))
                dateFromString = form["DateFrom"].ToString();
            if (form.ContainsKey("DateTo"))
                dateToString = form["DateTo"].ToString();
            if (dateFromString == null && dateToString == null)
                return new ReturnResult<List<ImportOvertimeApplicationDto>>(resultData: [], messages: "", isSuccess: false, errors: ["Please select Date Range"]);

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
                return new ReturnResult<List<ImportOvertimeApplicationDto>>(resultData: [], messages: "", isSuccess: false, errors: ["No Data in the file"]);
            if (lastColumn is 0)
                return new ReturnResult<List<ImportOvertimeApplicationDto>>(resultData: [], messages: "", isSuccess: false, errors: ["No Data in the file"]);

            List<ImportOvertimeApplicationDto> imports = ImportOvertimeApp(worksheet, lastRow, lastColumn);
            #region Validate Import Data
            ReturnResult<List<ImportOvertimeApplicationDto>> errors = await ValidateImportOvertimeApplication(imports, dateFrom, dateTo, ct);
            if (!errors.IsSuccess)
            {
                return errors;
            }
            #endregion
            return new ReturnResult<List<ImportOvertimeApplicationDto>>(resultData: imports, messages: "");
        }

        public async Task<ReturnResult<string>> UpdateImportOvertimeApplication(ImportOvertimeApplicationFormDto param, CancellationToken ct)
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
            ReturnResult<List<ImportOvertimeApplicationDto>> errors = await ValidateImportOvertimeApplication(param.Imports, param.DateFrom!.Value, param.DateTo!.Value, ct);
            if (!errors.IsSuccess)
            {
                return new ReturnResult<string>(resultData: "", messages: ResponseMessage.ERROR, isSuccess: false, errors: ["Cannot proceed to import. Please fix the data in the import list."]);
            }
            #endregion

            if (param.IsDeleteExistingRecord)
            {
                string empCodes = string.Join(",", param.Imports.Select(x => x.EmpCode).Distinct().ToList());
                await _repository.DeleteOvertimeApplication(empCodes, param.DateFrom);
            }
            foreach (var item in param.Imports.Where(x => x.DateFrom.Date >= param.DateFrom!.Value.Date && x.DateTo.Date <= param.DateTo!.Value.Date))
            {
                if (!string.IsNullOrEmpty(item.EmpCode))
                {
                    await _repository.UpdateImportOvertimeApplication(item);
                }
            }
            return new ReturnResult<string>(resultData: "", messages: ResponseMessage.SAVE);
        }
        internal static List<ImportOvertimeApplicationDto> ImportOvertimeApp(IXLWorksheet worksheet, int lastRow, int lastColumn)
        {
            List<ImportOvertimeApplicationDto> imports = [];
            int firstDataColumn = 1;
            
            for (int row = 2; row <= lastRow; row++)
            {  
                if (worksheet.Cell(row, 1).GetString() == null) continue;

                string empCode = worksheet.Cell(row, 1).Value.ToString();
                string dFrom = worksheet.Cell(row, 2).Value.ToString(),
                       dTo = worksheet.Cell(row, 3).Value.ToString();
                string numHrs = worksheet.Cell(row, 4).Value.ToString();
                string reason = worksheet.Cell(row, 5).Value.ToString();
                string remarks = worksheet.Cell(row, 6).Value.ToString();
                string otBShift = worksheet.Cell(row, 7).Value.ToString();
                string bNum = worksheet.Cell(row, 8).Value.ToString();
                string startTime = worksheet.Cell(row, 9).Value.ToString();
                string lateFiling = worksheet.Cell(row, 10).Value.ToString();
                string actualDate = worksheet.Cell(row, 11).Value.ToString();

                double numAppHrs = Convert.ToDouble(numHrs);

                DateTime dateFrom = Convert.ToDateTime(dFrom);
                DateTime dateTo = Convert.ToDateTime(dTo);
                DateTime beforeShift = Convert.ToDateTime(otBShift);
                DateTime startTimeOT = Convert.ToDateTime(startTime);
                double breakNum = Convert.ToDouble(bNum);

                bool isLateFiling = Convert.ToBoolean(lateFiling); 

                if (string.IsNullOrEmpty(empCode))
                    continue;

                for (int col = firstDataColumn; col <= lastColumn; col++)
                {
                    string rowAValue = worksheet.Cell(row, col).Value.ToString();

                    imports.Add(new ImportOvertimeApplicationDto
                    {
                        EmpCode = empCode,
                        DateFrom = dateFrom,
                        DateTo = dateTo,
                        NumOTHoursApproved = numAppHrs,
                        Reason = reason,
                        Remarks = remarks,
                        AppliedBeforeShiftDate = beforeShift,
                        ApprovedOTBreaksHrs = breakNum,
                        EarlyOTStartTime = startTimeOT,
                        IsLateFiling = isLateFiling
                    });
                }
            }

            return imports.OrderBy(x => x.DateFrom).ToList();
        }
        internal async Task<ReturnResult<List<ImportOvertimeApplicationDto>>> ValidateImportOvertimeApplication(
                   List<ImportOvertimeApplicationDto> imports,
                   DateTime dateFrom,
                   DateTime dateTo,
                   CancellationToken ct)
        {
            if (imports.Count == 0)
            {
                return new ReturnResult<List<ImportOvertimeApplicationDto>>(resultData: [], messages: "", isSuccess: false, errors: ["No Data"]);
            }
            ImportOvertimeApplicationDto first = imports.First();
            ImportOvertimeApplicationDto last = imports.Last();
            if ((dateFrom.Month != first.DateFrom.Month || dateFrom.Year != first.DateFrom.Year) ||
                (dateTo.Month != last.DateTo.Month || dateTo.Year != last.DateTo.Year))
            {
                return new ReturnResult<List<ImportOvertimeApplicationDto>>(resultData: [], messages: "", isSuccess: false, errors: ["Selected date range is mismatch in the importing file"]);
            }
            List<ImportOvertimeApplicationDto> errors = [];
            for (int i = 0; i < imports.Count; i++)
            {
                ImportOvertimeApplicationDto import = imports[i];
                string message = "";
                var isEmployeeExists = await _employeeMasterFileRepository.GetByEmpCode(import.EmpCode);

                if (isEmployeeExists is null)
                {
                    message += "Employee does not exists. ";
                }
                if (!string.IsNullOrEmpty(message))
                {
                    message += $". Row Number {import.RowNumber}, Column {import.ColumnNumber}";
                    if (!errors.Where(x => x.EmpCode == import.EmpCode).Any())
                    {
                        errors.Add(new ImportOvertimeApplicationDto
                        {
                            EmpCode = import.EmpCode,
                            DateFrom = import.DateFrom,
                            DateTo = import.DateTo,
                            NumOTHoursApproved = import.NumOTHoursApproved,
                            Reason = import.Reason,
                            Remarks = import.Remarks,
                            AppliedBeforeShiftDate = import.AppliedBeforeShiftDate,
                            ApprovedOTBreaksHrs = import.ApprovedOTBreaksHrs,
                            EarlyOTStartTime = import.EarlyOTStartTime,
                            IsLateFiling = import.IsLateFiling,
                            Message = message,
                            RowNumber = import.RowNumber,
                            ColumnNumber = import.ColumnNumber
                        });
                    }
                }
            }

            return new ReturnResult<List<ImportOvertimeApplicationDto>>(resultData: errors, messages: errors.Count is 0 ? "" : "Error, Please see message in the table below", isSuccess: errors.Count is 0, errors: ["Error, Please see message in the table below"]);
        }
    }
}
