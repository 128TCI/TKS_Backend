
using DomainEntities.Dto;
using Microsoft.AspNetCore.Http;
using Services.DTOs;


namespace Services.Interfaces.Utilities;

public interface IUpdateStatusService
{
    Task<ReturnResult<List<UpdateStatusDto >>> UpdateStatus(IFormCollection form, CancellationToken ct);
    Task<ReturnResult<string>> UpdateStatusDto(UpdateStatusDto param, CancellationToken ct);
}

