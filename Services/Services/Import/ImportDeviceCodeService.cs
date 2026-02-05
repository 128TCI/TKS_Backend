using ClosedXML.Excel;
using DomainEntities.Dto;
using DomainEntities.DTO;
using Infrastructure.IRepositories.Import;
using Infrastructure.IRepositories.Maintenance;
using Microsoft.AspNetCore.Http;
using Services.Extensions;
using Services.Interfaces.Import;

namespace Services.Services.Import
{
    public class ImportDeviceCodeService(
        IImportDeviceCodeRepository repository,
        IEmployeeMasterFileRepository employeeMasterFileRepository) : IImportDeviceCodeService
    {
        private readonly IImportDeviceCodeRepository _repository = repository;
        private readonly IEmployeeMasterFileRepository _employeeMasterFileRepository = employeeMasterFileRepository;

        public async Task<ReturnResult<List<ImportDeviceCodeDto>>> ImportDeviceCode(IFormCollection form, CancellationToken ct)
        {
            if (form.Files.Count == 0)
                return new ReturnResult<List<ImportDeviceCodeDto>>(resultData: [], messages: "", isSuccess: false, errors: ["No File found."]);
            
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
                return new ReturnResult<List<ImportDeviceCodeDto>>(resultData: [], messages: "", isSuccess: false, errors: ["No Data in the file"]);
            if (lastColumn is 0)
                return new ReturnResult<List<ImportDeviceCodeDto>>(resultData: [], messages: "", isSuccess: false, errors: ["No Data in the file"]);

            List<ImportDeviceCodeDto> imports = ImportDevice(worksheet, lastRow, lastColumn);
            #region Validate Import Data
            //ReturnResult<List<ImportDeviceCodeDto>> errors = await ValidateImportDeviceCode(imports, dateFrom, dateTo, ct);
            //if (!errors.IsSuccess)
            //{
            //    return errors;
            //}
            #endregion
            return new ReturnResult<List<ImportDeviceCodeDto>>(resultData: imports, messages: "");
        }

        public async Task<ReturnResult<string>> UpdateImportDeviceCode(ImportDeviceCodeFormDto param, CancellationToken ct)
        {

            if (param.Imports.Count(x => !x.Message.IsStringNullOrEmpty()) > 0)
            {
                return new ReturnResult<string>(resultData: "", messages: ResponseMessage.ERROR, isSuccess: false, errors: ["Cannot proceed to import. Please fix the data in the import list."]);
            }

            #region Validate Import Data
            ReturnResult<List<ImportDeviceCodeDto>> errors = await ValidateImportDeviceCode(param.Imports, ct);
            if (!errors.IsSuccess)
            {
                return new ReturnResult<string>(resultData: "", messages: ResponseMessage.ERROR, isSuccess: false, errors: ["Cannot proceed to import. Please fix the data in the import list."]);
            }
            #endregion

            if (param.IsDeleteExistingRecord)
            {
                string empCodes = string.Join(",", param.Imports.Select(x => x.EmpCode).Distinct().ToList());
                await _repository.DeleteLeaveApplication(empCodes);
            }
            foreach (var item in param.Imports)
            {
                if (!string.IsNullOrEmpty(item.EmpCode))
                {
                    await _repository.UpdateImportDeviceCode(item.EmpCode, item.EffectivityDate, item.ExpiryDate, item.Code);
                }
            }
            return new ReturnResult<string>(resultData: "", messages: ResponseMessage.SAVE);
        }
        internal static List<ImportDeviceCodeDto> ImportDevice(IXLWorksheet worksheet, int lastRow, int lastColumn)
        {
            List<ImportDeviceCodeDto> imports = [];
            int firstDataColumn = 1;
            
            for (int row = 2; row <= lastRow; row++)
            {  
                if (worksheet.Cell(row, 1).GetString() == null) continue;

                string empCode = worksheet.Cell(row, 1).Value.ToString();
                string effDate = worksheet.Cell(row, 2).Value.ToString(),
                       expDate = worksheet.Cell(row, 3).Value.ToString();
                string code = worksheet.Cell(row, 4).Value.ToString();

                DateTime effectivityDate = Convert.ToDateTime(effDate);
                DateTime expiryDate = Convert.ToDateTime(expDate);

                if (string.IsNullOrEmpty(empCode))
                    continue;

                for (int col = firstDataColumn; col <= lastColumn; col++)
                {
                    string rowAValue = worksheet.Cell(row, col).Value.ToString();
                    
                    imports.Add(new ImportDeviceCodeDto
                    {
                        EmpCode = empCode,
                        EffectivityDate = effectivityDate,
                        ExpiryDate = expiryDate,
                        Code = code
                    });
                }
            }

            return [.. imports];
        }
        internal async Task<ReturnResult<List<ImportDeviceCodeDto>>> ValidateImportDeviceCode(
                   List<ImportDeviceCodeDto> imports, CancellationToken ct)
        {
            if (imports.Count == 0)
            {
                return new ReturnResult<List<ImportDeviceCodeDto>>(resultData: [], messages: "", isSuccess: false, errors: ["No Data"]);
            }
            ImportDeviceCodeDto first = imports.First();
            ImportDeviceCodeDto last = imports.Last();

            List<ImportDeviceCodeDto> errors = [];
            for (int i = 0; i < imports.Count; i++)
            {
                ImportDeviceCodeDto import = imports[i];
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
                        errors.Add(new ImportDeviceCodeDto
                        {
                            EmpCode = import.EmpCode,
                            EffectivityDate = import.EffectivityDate,
                            ExpiryDate = import.ExpiryDate,
                            Code = import.Code,
                            Message = message,
                            RowNumber = import.RowNumber,
                            ColumnNumber = import.ColumnNumber
                        });
                    }
                }
            }
            return new ReturnResult<List<ImportDeviceCodeDto>>(resultData: errors, messages: errors.Count is 0 ? "" : "Error, Please see message in the table below", isSuccess: errors.Count is 0, errors: ["Error, Please see message in the table below"]);
        }
    }
}
