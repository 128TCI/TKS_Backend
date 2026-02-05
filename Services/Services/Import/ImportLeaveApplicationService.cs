using ClosedXML.Excel;
using DomainEntities.Dto;
using DomainEntities.DTO;
using Infrastructure.IRepositories.Import;
using Infrastructure.IRepositories.LeaveTypes;
using Infrastructure.IRepositories.Maintenance;
using Microsoft.AspNetCore.Http;
using Services.Extensions;
using Services.Interfaces.Import;

namespace Services.Services.Import
{
    public class ImportLeaveApplicationService(
        IImportLeaveApplicationRepository leaveApplicationRepository,
        ILeaveTypesRepository leaveTypesRepository,
        IEmployeeMasterFileRepository employeeMasterFileRepository) : IImportLeaveApplicationService
    {
        private readonly IImportLeaveApplicationRepository _repository = leaveApplicationRepository;
        private readonly ILeaveTypesRepository _leaveTypesRepository = leaveTypesRepository;
        private readonly IEmployeeMasterFileRepository _employeeMasterFileRepository = employeeMasterFileRepository;

        public async Task<ReturnResult<List<ImportLeaveApplicationDto>>> ImportLeaveApplication(IFormCollection form, CancellationToken ct)
        {
            if (form.Files.Count == 0)
                return new ReturnResult<List<ImportLeaveApplicationDto>>(resultData: [], messages: "", isSuccess: false, errors: ["No File found."]);
            
            string? dateFromString = null, dateToString = null;
            if (form.ContainsKey("DateFrom"))
                dateFromString = form["DateFrom"].ToString();
            if (form.ContainsKey("DateTo"))
                dateToString = form["DateTo"].ToString();
            if (dateFromString == null && dateToString == null)
                return new ReturnResult<List<ImportLeaveApplicationDto>>(resultData: [], messages: "", isSuccess: false, errors: ["Please select Date Range"]);

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
                return new ReturnResult<List<ImportLeaveApplicationDto>>(resultData: [], messages: "", isSuccess: false, errors: ["No Data in the file"]);
            if (lastColumn is 0)
                return new ReturnResult<List<ImportLeaveApplicationDto>>(resultData: [], messages: "", isSuccess: false, errors: ["No Data in the file"]);

            List<ImportLeaveApplicationDto> imports = ImportLeaveApp(worksheet, lastRow, lastColumn);
            #region Validate Import Data
            ReturnResult<List<ImportLeaveApplicationDto>> errors = await ValidateImportLeaveApplication(imports, dateFrom, dateTo, ct);
            if (!errors.IsSuccess)
            {
                return errors;
            }
            #endregion
            return new ReturnResult<List<ImportLeaveApplicationDto>>(resultData: imports, messages: "");
        }

        public async Task<ReturnResult<string>> UpdateImportLeaveApplication(ImportLeaveApplicationFormDto param, CancellationToken ct)
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
            ReturnResult<List<ImportLeaveApplicationDto>> errors = await ValidateImportLeaveApplication(param.Imports, param.DateFrom!.Value, param.DateTo!.Value, ct);
            if (!errors.IsSuccess)
            {
                return new ReturnResult<string>(resultData: "", messages: ResponseMessage.ERROR, isSuccess: false, errors: ["Cannot proceed to import. Please fix the data in the import list."]);
            }
            #endregion

            if (param.IsDeleteExistingRecord)
            {
                string empCodes = string.Join(",", param.Imports.Select(x => x.EmpCode).Distinct().ToList());
                await _repository.DeleteLeaveApplication(empCodes, param.DateFrom, param.DateTo);
            }
            foreach (var item in param.Imports.Where(x => x.DateFrom.Date >= param.DateFrom!.Value.Date && x.DateTo.Date <= param.DateTo!.Value.Date))
            {
                if (!string.IsNullOrEmpty(item.LeaveCode))
                {
                    await _repository.UpdateImportLeaveApplication(item);
                }
            }
            return new ReturnResult<string>(resultData: "", messages: ResponseMessage.SAVE);
        }
        internal static List<ImportLeaveApplicationDto> ImportLeaveApp(IXLWorksheet worksheet, int lastRow, int lastColumn)
        {
            List<ImportLeaveApplicationDto> imports = [];
            int firstDataColumn = 1;
            
            for (int row = 2; row <= lastRow; row++)
            {  
                if (worksheet.Cell(row, 1).GetString() == null) continue;

                string empCode = worksheet.Cell(row, 1).Value.ToString();
                string dFrom = worksheet.Cell(row, 2).Value.ToString(),
                       dTo = worksheet.Cell(row, 3).Value.ToString();
                string numHrs = worksheet.Cell(row, 4).Value.ToString();
                string leaveCode = worksheet.Cell(row, 5).Value.ToString();
                string period = worksheet.Cell(row, 6).Value.ToString();
                string reason = worksheet.Cell(row, 7).Value.ToString();
                string remarks = worksheet.Cell(row, 8).Value.ToString();
                string wPay = worksheet.Cell(row, 9).Value.ToString();
                string sss = worksheet.Cell(row, 10).Value.ToString();
                string lateFiling = worksheet.Cell(row, 11).Value.ToString();

                double numAppHrs = Convert.ToDouble(numHrs);

                DateTime dateFrom = Convert.ToDateTime(dFrom);
                DateTime dateTo = Convert.ToDateTime(dTo);

                bool withPay = Convert.ToBoolean(wPay);
                bool sssNotif = Convert.ToBoolean(sss);
                bool isLateFiling = Convert.ToBoolean(lateFiling); 

                if (string.IsNullOrEmpty(empCode))
                    continue;

                for (int col = firstDataColumn; col <= lastColumn; col++)
                {
                    string rowAValue = worksheet.Cell(row, col).Value.ToString();
                    
                    imports.Add(new ImportLeaveApplicationDto
                    {
                        EmpCode = empCode,
                        DateFrom = dateFrom,
                        DateTo = dateTo,
                        NumApprovedHrs = numAppHrs,
                        LeaveCode = leaveCode,
                        Period = period,
                        Reason = reason,
                        Remarks = remarks,
                        WithPay = withPay,
                        SSSNotif = sssNotif,
                        IsLateFiling = isLateFiling
                    });
                }
            }

            return imports.OrderBy(x => x.DateFrom).ToList();
        }
        internal async Task<ReturnResult<List<ImportLeaveApplicationDto>>> ValidateImportLeaveApplication(
                   List<ImportLeaveApplicationDto> imports,
                   DateTime dateFrom,
                   DateTime dateTo,
                   CancellationToken ct)
        {
            if (imports.Count == 0)
            {
                return new ReturnResult<List<ImportLeaveApplicationDto>>(resultData: [], messages: "", isSuccess: false, errors: ["No Data"]);
            }
            ImportLeaveApplicationDto first = imports.First();
            ImportLeaveApplicationDto last = imports.Last();
            if ((dateFrom.Month != first.DateFrom.Month || dateFrom.Year != first.DateFrom.Year) ||
                (dateTo.Month != last.DateTo.Month || dateTo.Year != last.DateTo.Year))
            {
                return new ReturnResult<List<ImportLeaveApplicationDto>>(resultData: [], messages: "", isSuccess: false, errors: ["Selected date range is mismatch in the importing file"]);
            }
            List<ImportLeaveApplicationDto> errors = [];
            for (int i = 0; i < imports.Count; i++)
            {
                ImportLeaveApplicationDto import = imports[i];
                string message = "";
                var isEmployeeExists = await _employeeMasterFileRepository.GetByEmpCode(import.EmpCode);
                var leaveCode = await _leaveTypesRepository.GetLeaveTypeByCode(import.LeaveCode!);
                if (isEmployeeExists is null)
                {
                    message += "Employee does not exists. ";
                }
                if (!string.IsNullOrEmpty(import.LeaveCode) && string.IsNullOrEmpty(import.LeaveCode) && leaveCode == null)
                {
                    message += "Leave Code does not exists. ";
                }
                if (!string.IsNullOrEmpty(message))
                {
                    message += $". Row Number {import.RowNumber}, Column {import.ColumnNumber}";
                    if (!errors.Where(x => x.EmpCode == import.EmpCode && x.LeaveCode == import.LeaveCode).Any())
                    {
                        errors.Add(new ImportLeaveApplicationDto
                        {
                            EmpCode = import.EmpCode,
                            Message = message,
                            DateFrom = import.DateFrom,
                            DateTo = import.DateTo,
                            NumApprovedHrs = import.NumApprovedHrs,
                            LeaveCode = import.LeaveCode,
                            Period = import.Period,
                            Reason = import.Reason,
                            Remarks = import.Remarks,
                            WithPay = import.WithPay,
                            SSSNotif = import.SSSNotif,
                            IsLateFiling = import.IsLateFiling,
                            RowNumber = import.RowNumber,
                            ColumnNumber = import.ColumnNumber
                        });
                    }
                }
            }
            return new ReturnResult<List<ImportLeaveApplicationDto>>(resultData: errors, messages: errors.Count is 0 ? "" : "Error, Please see message in the table below", isSuccess: errors.Count is 0, errors: ["Error, Please see message in the table below"]);
        }
    }
}
