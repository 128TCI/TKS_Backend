
using DomainEntities.Dto;
using Microsoft.AspNetCore.Http;
using Services.DTOs;


namespace Services.Interfaces.Import;

public interface IImportLeaveApplicationService
{
    Task<ReturnResult<List<ImportLeaveApplicationDto>>> ImportLeaveApplication(IFormCollection form, CancellationToken ct);
    Task<ReturnResult<string>> UpdateImportLeaveApplication(ImportLeaveApplicationFormDto param, CancellationToken ct);
}

