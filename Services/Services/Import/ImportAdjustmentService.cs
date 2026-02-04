using ClosedXML.Excel;
using DomainEntities.Dto;
using DomainEntities.DTO;
using Infrastructure.IRepositories.Import;
using Infrastructure.IRepositories.LeaveTypes;
using Microsoft.AspNetCore.Http;
using Services.Extensions;
using Services.Interfaces.Import;

namespace Services.Services.Import
{
    public class ImportAdjustmentService(
        IImportAdjustmentRepository repository,
        ILeaveTypesRepository leaveTypesRepository) : IImportAdjustmentService
    {
        private readonly IImportAdjustmentRepository _repository = repository;
        private readonly ILeaveTypesRepository _leaveTypesRepository = leaveTypesRepository;

        public async Task<ReturnResult<List<ImportAdjustmentDto>>> ImportAdjustment(IFormCollection form, CancellationToken ct)
        {
            if (form.Files.Count == 0)
                return new ReturnResult<List<ImportAdjustmentDto>>(resultData: [], messages: "", isSuccess: false, errors: ["No File found."]);
            
            string? dateFromString = null, dateToString = null;
            if (form.ContainsKey("DateFrom"))
                dateFromString = form["DateFrom"].ToString();
            if (form.ContainsKey("DateTo"))
                dateToString = form["DateTo"].ToString();
            if (dateFromString == null && dateToString == null)
                return new ReturnResult<List<ImportAdjustmentDto>>(resultData: [], messages: "", isSuccess: false, errors: ["Please select Date Range"]);

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
                return new ReturnResult<List<ImportAdjustmentDto>>(resultData: [], messages: "", isSuccess: false, errors: ["No Data in the file"]);
            if (lastColumn is 0)
                return new ReturnResult<List<ImportAdjustmentDto>>(resultData: [], messages: "", isSuccess: false, errors: ["No Data in the file"]);

            List<ImportAdjustmentDto> imports = ImportProcAdjustment(worksheet, lastRow, lastColumn);
            #region Validate Import Data
            ReturnResult<List<ImportAdjustmentDto>> errors = await ValidateImportAdjustment(imports, dateFrom, dateTo, ct);
            if (!errors.IsSuccess)
            {
                return errors;
            }
            #endregion
            return new ReturnResult<List<ImportAdjustmentDto>>(resultData: imports, messages: "");
        }

        public async Task<ReturnResult<string>> UpdateImportAdjustment(ImportAdjustmentFormDto param, CancellationToken ct)
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
            ReturnResult<List<ImportAdjustmentDto>> errors = await ValidateImportAdjustment(param.Imports, param.DateFrom!.Value, param.DateTo!.Value, ct);
            if (!errors.IsSuccess)
            {
                return new ReturnResult<string>(resultData: "", messages: ResponseMessage.ERROR, isSuccess: false, errors: ["Cannot proceed to import. Please fix the data in the import list."]);
            }
            #endregion
            foreach (var item in param.Imports.Where(x => x.TransactionDate.Date >= param.DateFrom!.Value.Date && x.TransactionDate.Date <= param.DateTo!.Value.Date))
            {
                if (!string.IsNullOrEmpty(item.EmpCode))
                {
                    await _repository.UpdateImportAdjustment(item);
                }

            }
            return new ReturnResult<string>(resultData: "", messages: ResponseMessage.SAVE);
        }
        internal static List<ImportAdjustmentDto> ImportProcAdjustment(IXLWorksheet worksheet, int lastRow, int lastColumn)
        {
            List<ImportAdjustmentDto> imports = [];
            int firstDataColumn = 1;

            for (int row = 2; row <= lastRow; row++)
            {  
                if (worksheet.Cell(row, 1).GetString() == null) continue;

                string empCode = worksheet.Cell(row, 1).Value.ToString();
                string empName = worksheet.Cell(row, 2).Value.ToString();
                string transDate = worksheet.Cell(row, 3).Value.ToString();
                string transType = worksheet.Cell(row, 4).Value.ToString();
                string leaveType = worksheet.Cell(row, 5).Value.ToString();
                string otCode = worksheet.Cell(row, 6).Value.ToString();
                string numHrs = worksheet.Cell(row, 7).Value.ToString();
                string adjType = worksheet.Cell(row, 8).Value.ToString();
                string remarks = worksheet.Cell(row, 9).Value.ToString();
                string lateFiling = worksheet.Cell(row, 10).Value.ToString();
                string actualDate = worksheet.Cell(row, 11).Value.ToString();

                double noOfHrs = Convert.ToDouble(numHrs);

                DateTime transactionDate = Convert.ToDateTime(transDate);
                DateTime lateFilingActualDate = Convert.ToDateTime(actualDate);

                bool isLateFiling = Convert.ToBoolean(lateFiling); 

                if (string.IsNullOrEmpty(empCode))
                    continue;

                for (int col = firstDataColumn; col <= lastColumn; col++)
                {
                    string rowAValue = worksheet.Cell(row, col).Value.ToString();
                    
                    imports.Add(new ImportAdjustmentDto
                    {
                        EmpCode = empCode,
                        EmpName = empName,
                        TransactionDate = transactionDate,
                        TransactionType = transType,
                        LeaveType = leaveType,
                        OvertimeCode = otCode,
                        NoOfHours = noOfHrs,
                        AdjustType = adjType,
                        Remarks = remarks,
                        IsLateFiling = isLateFiling,
                        IsLateFilingActualDate = lateFilingActualDate
                    });
                }
            }

            return imports.ToList();
        }
        internal async Task<ReturnResult<List<ImportAdjustmentDto>>> ValidateImportAdjustment(
                   List<ImportAdjustmentDto> imports,
                   DateTime dateFrom,
                   DateTime dateTo,
                   CancellationToken ct)
        {
            if (imports.Count == 0)
            {
                return new ReturnResult<List<ImportAdjustmentDto>>(resultData: [], messages: "", isSuccess: false, errors: ["No Data"]);
            }
            ImportAdjustmentDto first = imports.First();
            ImportAdjustmentDto last = imports.Last();
            if ((dateFrom.Month != first.TransactionDate.Month || dateFrom.Year != first.TransactionDate.Year) ||
                (dateTo.Month != last.TransactionDate.Month || dateTo.Year != last.TransactionDate.Year))
            {
                return new ReturnResult<List<ImportAdjustmentDto>>(resultData: [], messages: "", isSuccess: false, errors: ["Selected date range is mismatch in the importing file"]);
            }
            List<ImportAdjustmentDto> errors = [];
            for (int i = 0; i < imports.Count; i++)
            {
                ImportAdjustmentDto import = imports[i];
                string message = "";
                var isEmployeeExists = await _repository.GetEmployeeGroupImport(import.EmpCode);
                var leaveCode = await _leaveTypesRepository.GetLeaveTypeByCode(import.LeaveType!);

                if (isEmployeeExists is null)
                {
                    message += "Employee does not exists. ";
                }
                if (import.TransactionType is null || import.TransactionType == "LATE"
                    || import.TransactionType == "UT" || import.TransactionType == "OT")
                {
                    message += "Invalid Transaction Type. ";
                }
                if(import.TransactionType == "LVAB" && (import.AdjustType == "" || import.AdjustType is null))
                {
                    message += "Invalid Leave Adjustment Type.";
                }
                if (import.TransactionType == "LVAB" && (import.LeaveType == "" || import.LeaveType is null))
                {
                    message += "Invalid Leave Type.";
                }
                if (import.TransactionType == "OT" && (import.OvertimeCode == "" || import.OvertimeCode is null))
                {
                    message += "Invalid Overtime Code.";
                }
                if (import.IsLateFiling is null)
                {
                    message += "Invalid IsLateFiling.";
                }
                if (!string.IsNullOrEmpty(message))
                {
                    message += $". Row Number {import.RowNumber}, Column {import.ColumnNumber}";
                    if (!errors.Where(x => x.EmpCode == import.EmpCode).Any())
                    {
                        errors.Add(new ImportAdjustmentDto
                        {
                            EmpCode = import.EmpCode,
                            TransactionDate = import.TransactionDate,
                            TransactionType = import.TransactionType,
                            LeaveType = import.LeaveType,
                            OvertimeCode = import.OvertimeCode,
                            NoOfHours = import.NoOfHours,
                            Remarks = import.Remarks,
                            AdjustType = import.AdjustType,
                            IsLateFilingActualDate = import.IsLateFilingActualDate,
                            IsLateFiling = import.IsLateFiling,
                            Message = message,
                            RowNumber = import.RowNumber,
                            ColumnNumber = import.ColumnNumber
                        });
                    }
                }
            }
            return new ReturnResult<List<ImportAdjustmentDto>>(resultData: errors, messages: errors.Count is 0 ? "" : "Error, Please see message in the table below", isSuccess: errors.Count is 0, errors: ["Error, Please see message in the table below"]);
        }
    }
}
