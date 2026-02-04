using DomainEntities.Dto;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces.Import;


namespace WebbApp.Controllers.Import
{
    [Route("api/Utilities/Import/[action]")]
    [ApiController]
    public class ImportEmployeeMasterfileController(IImportEmployeeMasterfileService service) : ControllerBase
    {
        private readonly IImportEmployeeMasterfileService _service = service;

        [HttpPost]
        public async Task<IActionResult> ImportEmployeeMasterfile(IFormCollection form, CancellationToken ct)
           => Ok(await _service.ImportEmployeeMasterfile(form, ct));

        [HttpPost]
        public async Task<IActionResult> UpdateImportEmployeeMasterfile(List<ImportEmployeeMasterfileDto> param, CancellationToken ct)
        => Ok(await _service.UpdateImportEmployeeMasterfile(param, ct));

    }
}
