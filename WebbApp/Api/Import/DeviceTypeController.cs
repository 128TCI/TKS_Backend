using DomainEntities.Dto;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces.FileSetUp.Employment;
using Services.Interfaces.Import;
using Services.Interfaces.LeaveTypes;


namespace WebbApp.Controllers.Import
{
    [Route("api/DeviceType/[controller]")]
    [ApiController]
    public class DeviceTypeController(IDeviceTypeService service) : ControllerBase
    {
        private readonly IDeviceTypeService _service = service;

        [HttpGet]
        public async Task<IActionResult> GetDeviceType()
        => Ok(await _service.GetDeviceType());

    }
}
