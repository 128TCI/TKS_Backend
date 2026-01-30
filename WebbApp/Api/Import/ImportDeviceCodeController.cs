using DomainEntities.Dto;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces.Import;


namespace WebbApp.Controllers.Import
{
    [Route("api/Utilities/Import/[action]")]
    [ApiController]
    public class ImportDeviceCodeController(IImportDeviceCodeService service) : ControllerBase
    {
        private readonly IImportDeviceCodeService _service = service;

        [HttpPost]
        public async Task<IActionResult> ImportDeviceCode(IFormCollection form, CancellationToken ct)
           => Ok(await _service.ImportDeviceCode(form, ct));

        [HttpPost]
        public async Task<IActionResult> UpdateImportDeviceCode(ImportDeviceCodeFormDto param, CancellationToken ct)
        => Ok(await _service.UpdateImportDeviceCode(param, ct));

    }
}
