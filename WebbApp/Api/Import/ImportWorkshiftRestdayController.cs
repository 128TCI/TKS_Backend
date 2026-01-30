using DomainEntities.Dto;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces.Import;


namespace WebbApp.Controllers.Import
{
    [Route("api/Utilities/ImportRestDay/[action]")]
    [ApiController]
    public class ImportWorkshiftRestdayController(IImportWorkshiftRestdayService restdayService) : ControllerBase
    {
        private readonly IImportWorkshiftRestdayService _restdayService = restdayService;

        [HttpPost]
        public async Task<IActionResult> ImportRestDay(IFormCollection form, CancellationToken ct)
        => Ok(await _restdayService.ImportWorkshiftWRestDay(form, ct));

        [HttpPost]
        public async Task<IActionResult> UpdateWorkshiftRestDay(ImportWorkshiftRestDayFormDto param, CancellationToken ct)
        => Ok(await _restdayService.UpdateRestDay(param, ct));

    }
}
