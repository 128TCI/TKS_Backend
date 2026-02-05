
using DomainEntities.Dto;
using Microsoft.AspNetCore.Http;
using Services.DTOs;


namespace Services.Interfaces.Import;

public interface IImportEmployeeMasterfileService
{
    Task<ReturnResult<List<ImportEmployeeMasterfileDto>>> ImportEmployeeMasterfile(IFormCollection form, CancellationToken ct);
    Task<ReturnResult<string>> UpdateImportEmployeeMasterfile(List<ImportEmployeeMasterfileDto> param, CancellationToken ct);
}

