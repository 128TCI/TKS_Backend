using DomainEntities.Dto;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces.Utilities;


namespace WebbApp.Controllers.Utilities
{
    [Route("api/Utilities/[action]")]
    [ApiController]
    public class UpdateStatusController(IUpdateStatusService service) : ControllerBase
    {
        private readonly IUpdateStatusService _service = service;


        [HttpPost]
        public async Task<IActionResult> UpdateStatus(IFormCollection form, CancellationToken ct)
           => Ok(await _service.UpdateStatus(form, ct));

    }
}
