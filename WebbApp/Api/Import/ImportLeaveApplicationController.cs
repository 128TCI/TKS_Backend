using DomainEntities.Dto;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces.Import;


namespace WebbApp.Controllers.Import
{
    [Route("api/Utilities/Import/[action]")]
    [ApiController]
    public class ImportLeaveApplicationController(IImportLeaveApplicationService service) : ControllerBase
    {
        private readonly IImportLeaveApplicationService _service = service;

        [HttpPost]
        public async Task<IActionResult> ImportLeaveApplication(IFormCollection form, CancellationToken ct)
           => Ok(await _service.ImportLeaveApplication(form, ct));

        [HttpPost]
        public async Task<IActionResult> UpdateImportLeaveApplication(ImportLeaveApplicationFormDto param, CancellationToken ct)
        => Ok(await _service.UpdateImportLeaveApplication(param, ct));

    }
}
