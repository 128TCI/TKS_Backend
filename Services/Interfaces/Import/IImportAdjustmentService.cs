
using DomainEntities.Dto;
using Microsoft.AspNetCore.Http;
using Services.DTOs;


namespace Services.Interfaces.Import;

public interface IImportAdjustmentService
{
    Task<ReturnResult<List<ImportAdjustmentDto>>> ImportAdjustment(IFormCollection form, CancellationToken ct);
    Task<ReturnResult<string>> UpdateImportAdjustment(ImportAdjustmentFormDto param, CancellationToken ct);
}

