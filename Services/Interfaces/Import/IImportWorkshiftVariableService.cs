
using DomainEntities.Dto;
using Microsoft.AspNetCore.Http;


namespace Services.Interfaces.Import;

public interface IImportWorkshiftVariableService
{
    Task<ReturnResult<List<ImportWorkShiftVariableDto>>> ImportWorkshiftVariable(IFormCollection form, CancellationToken ct);
    Task<ReturnResult<string>> UpdateWorkshiftVariable(ImportWorkshiftVariableFormDto param, CancellationToken ct);
}

