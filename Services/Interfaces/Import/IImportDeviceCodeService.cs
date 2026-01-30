
using DomainEntities.Dto;
using Microsoft.AspNetCore.Http;
using Services.DTOs;


namespace Services.Interfaces.Import;

public interface IImportDeviceCodeService
{
    Task<ReturnResult<List<ImportDeviceCodeDto>>> ImportDeviceCode(IFormCollection form, CancellationToken ct);
    Task<ReturnResult<string>> UpdateImportDeviceCode(ImportDeviceCodeFormDto param, CancellationToken ct);
}

