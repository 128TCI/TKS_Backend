using ClosedXML.Excel;
using DomainEntities.Dto;
using DomainEntities.DTO;
using Infrastructure.IRepositories.Maintenance;
using Infrastructure.IRepositories.Utilities;
using Microsoft.AspNetCore.Http;
using Services.Extensions;
using Services.Interfaces.Utilities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Services.Services.Utilities
{
    public class UpdateStatusService(
        IUpdateStatusRepository updateStatusRepository,
        IEmployeeMasterFileRepository employeeMasterFileRepository) : IUpdateStatusService
    {
        private readonly IUpdateStatusRepository _repository = updateStatusRepository;
        private readonly IEmployeeMasterFileRepository _employeeMasterFileRepository = employeeMasterFileRepository;

        public async Task<ReturnResult<List<UpdateStatusDto>>> UpdateStatus(IFormCollection form, CancellationToken ct)
        {
            if (form.Files.Count == 0)
                return new ReturnResult<List<UpdateStatusDto>>(resultData: [], messages: "", isSuccess: false, errors: ["No File found."]);
            
            string? dateFromString = null, dateToString = null;
            if (form.ContainsKey("DateFrom"))
                dateFromString = form["DateFrom"].ToString();
            if (form.ContainsKey("DateTo"))
                dateToString = form["DateTo"].ToString();
            if (dateFromString == null && dateToString == null)
                return new ReturnResult<List<UpdateStatusDto>>(resultData: [], messages: "", isSuccess: false, errors: ["Please select Date Range"]);

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
                return new ReturnResult<List<UpdateStatusDto>>(resultData: [], messages: "", isSuccess: false, errors: ["No Data in the file"]);
            if (lastColumn is 0)
                return new ReturnResult<List<UpdateStatusDto>>(resultData: [], messages: "", isSuccess: false, errors: ["No Data in the file"]);

            List<UpdateStatusDto> updates = [];
            #region Validate Update Data
            ReturnResult<List<UpdateStatusDto>> errors = await ValidateUpdateStatus(updates, dateFrom, dateTo, ct);
            if (!errors.IsSuccess)
            {
                return errors;
            }
            #endregion
            return new ReturnResult<List<UpdateStatusDto>>(resultData: updates, messages: "");
        }

        public Task<ReturnResult<string>> UpdateStatusDto(UpdateStatusDto param, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        internal async Task<ReturnResult<List<UpdateStatusDto>>> ValidateUpdateStatus(
                   List<UpdateStatusDto> updates,
                   DateTime dateFrom,
                   DateTime dateTo,
                   CancellationToken ct)
        {
            if (updates.Count == 0)
            {
                return new ReturnResult<List<UpdateStatusDto>>(resultData: [], messages: "", isSuccess: false, errors: ["No Data"]);
            }
            UpdateStatusDto first = updates.First();
            UpdateStatusDto last = updates.Last();
            if ((dateFrom.Month != first.DateFrom.Month || dateFrom.Year != first.DateFrom.Year) ||
                (dateTo.Month != last.DateTo.Month || dateTo.Year != last.DateTo.Year))
            {
                return new ReturnResult<List<UpdateStatusDto>>(resultData: [], messages: "", isSuccess: false, errors: ["Selected date range is mismatch in the importing file"]);
            }
            List<UpdateStatusDto> errors = [];
            for (int i = 0; i < updates.Count; i++)
            {
                UpdateStatusDto update  = updates[i];
                string message = "";
                var isEmployeeExists = await _employeeMasterFileRepository.GetByEmpCode(update.EmpCode);
                if (isEmployeeExists is null)
                {
                    message += "Employee does not exists. ";
                }

                if (!string.IsNullOrEmpty(message))
                {
                    message += $". Row Number {update.RowNumber}, Column {update.ColumnNumber}";
                    if (!errors.Where(x => x.EmpCode == update.EmpCode).Any())
                    {
                        errors.Add(new UpdateStatusDto
                        {
                            EmpCode = update.EmpCode,
                            Message = message,
                            DateFrom = update.DateFrom,
                            DateTo = update.DateTo
                        });
                    }
                }
            }
            return new ReturnResult<List<UpdateStatusDto>>(resultData: errors, messages: errors.Count is 0 ? "" : "Error, Please see message in the table below", isSuccess: errors.Count is 0, errors: ["Error, Please see message in the table below"]);
        }
    }
}
