using DomainEntities.Dto;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces.Import;


namespace WebbApp.Controllers.Import
{
    [Route("api/Utilities/Import/[action]")]
    [ApiController]
    public class ImportWorkshiftVariableController(IImportWorkshiftVariableService variableService) : ControllerBase
    {
        private readonly IImportWorkshiftVariableService _variableService = variableService;

        [HttpPost]
        public async Task<IActionResult> ImportWorkshiftVariable(IFormCollection form, CancellationToken ct)
           => Ok(await _variableService.ImportWorkshiftVariable(form, ct));

        [HttpPost]
        public async Task<IActionResult> UpdateWorkshiftVariable(ImportWorkshiftVariableFormDto param, CancellationToken ct)
        => Ok(await _variableService.UpdateWorkshiftVariable(param, ct));

    }
}
