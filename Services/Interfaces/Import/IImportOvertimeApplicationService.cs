
using DomainEntities.Dto;
using Microsoft.AspNetCore.Http;
using Services.DTOs;


namespace Services.Interfaces.Import;

public interface IImportOvertimeApplicationService
{
    Task<ReturnResult<List<ImportOvertimeApplicationDto>>> ImportOvertimeApplication(IFormCollection form, CancellationToken ct);
    Task<ReturnResult<string>> UpdateImportOvertimeApplication(ImportOvertimeApplicationFormDto param, CancellationToken ct);
}

