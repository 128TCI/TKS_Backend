using DomainEntities.Dto;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces.FileSetUp.Employment;
using Services.Interfaces.Import;
using Services.Interfaces.LeaveTypes;


namespace WebbApp.Controllers.Import
{
    [Route("api/Import/LogsFromDevice/[action]")]
    [ApiController]
    public class ImportLogsFromDeviceController(IImportLogsFromDeviceService service) : ControllerBase
    {
        private readonly IImportLogsFromDeviceService _service = service;

        [HttpGet]
        public async Task<IActionResult> GetWorkShiftByInOrOut(string empCode, DateTime dateFrom, DateTime dateTo, DateTime dateToCutOff)
        => Ok(await _service.GetWorkShiftByInOrOut(empCode, dateFrom, dateTo, dateToCutOff));

        [HttpGet]
        public async Task<IActionResult> GetOTApproved(string empCode)
        => Ok(await _service.GetOTApproved(empCode));

        [HttpPost]
        public async Task<IActionResult> ImportLogsFromDevice(IFormCollection form, CancellationToken ct)
          => Ok(await _service.ImportLogsFromDevice(form, ct));

        [HttpPost]
        public async Task<IActionResult> UpdateImportLogsFromDevice(ImportLogsFromDeviceFormDto param, CancellationToken ct)
        => Ok(await _service.UpdateLogsFromDevice(param, ct));

    }
}
