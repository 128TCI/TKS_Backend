
using DomainEntities.Dto;
using Microsoft.AspNetCore.Http;
using Services.DTOs;


namespace Services.Interfaces.Import;

public interface IImportWorkshiftRestdayService
{
    Task<ReturnResult<List<ImportWorkshiftRestdayDto>>> ImportWorkshiftWRestDay(IFormCollection form, CancellationToken ct);
    Task<ReturnResult<string>> UpdateRestDay(ImportWorkshiftRestDayFormDto param, CancellationToken ct);
}

