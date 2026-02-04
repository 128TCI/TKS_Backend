using DomainEntities.Dto;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces.Import;


namespace WebbApp.Controllers.Import
{
    [Route("api/Utilities/Import/[action]")]
    [ApiController]
    public class ImportAdjustmentController(IImportAdjustmentService service) : ControllerBase
    {
        private readonly IImportAdjustmentService _service = service;

        [HttpPost]
        public async Task<IActionResult> ImportAdjustment(IFormCollection form, CancellationToken ct)
           => Ok(await _service.ImportAdjustment(form, ct));

        [HttpPost]
        public async Task<IActionResult> UpdateImportAdjustment(ImportAdjustmentFormDto param, CancellationToken ct)
        => Ok(await _service.UpdateImportAdjustment(param, ct));

    }
}
