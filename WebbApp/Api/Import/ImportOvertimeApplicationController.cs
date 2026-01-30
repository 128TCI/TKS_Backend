using DomainEntities.Dto;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces.Import;


namespace WebbApp.Controllers.Import
{
    [Route("api/Utilities/Import/[action]")]
    [ApiController]
    public class ImportOvertimeApplicationController(IImportOvertimeApplicationService service) : ControllerBase
    {
        private readonly IImportOvertimeApplicationService _service = service;

        [HttpPost]
        public async Task<IActionResult> ImportOvertimeApplication(IFormCollection form, CancellationToken ct)
           => Ok(await _service.ImportOvertimeApplication(form, ct));

        [HttpPost]
        public async Task<IActionResult> UpdateImportOvertimeApplication(ImportOvertimeApplicationFormDto param, CancellationToken ct)
        => Ok(await _service.UpdateImportOvertimeApplication(param, ct));

    }
}
