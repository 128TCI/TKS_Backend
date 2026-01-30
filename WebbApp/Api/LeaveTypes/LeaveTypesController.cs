using DomainEntities.Dto;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces.FileSetUp.Employment;
using Services.Interfaces.LeaveTypes;


namespace WebbApp.Controllers.LeaveTypes
{
    [Route("api/LeaveCode/[controller]")]
    [ApiController]
    public class LeaveTypesController(ILeaveTypesService leaveService) : ControllerBase
    {
        private readonly ILeaveTypesService _leaveService = leaveService;

        [HttpGet]
        public async Task<IActionResult> GetLeaveTypeByCode(string leaveCode)
        => Ok(await _leaveService.GetLeaveTypes(leaveCode));

    }
}
