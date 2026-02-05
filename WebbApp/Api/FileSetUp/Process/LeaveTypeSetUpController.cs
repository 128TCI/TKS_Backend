using DomainEntities.Dto;
using DomainEntities.DTO.FileSetUp.Process;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces.FileSetUp.Process;
using Services.Services.FileSetUp.Process;

namespace WebbApp.Api.FileSetUp.Process
{
    [Route("api/Fs/Process/[controller]")]
    [ApiController]
    public class LeaveTypeSetUpController : ControllerBase
    {
        private readonly ILeaveTypeSetUpService _LeaveTypeSetUpService;

        public LeaveTypeSetUpController(ILeaveTypeSetUpService service)
        {
            _LeaveTypeSetUpService = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<LeaveTypesDto>>> GetAll()
        {
            var data = await _LeaveTypeSetUpService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LeaveTypesDto>> GetById(int id)
        {
            var data = await _LeaveTypeSetUpService.GetByIdAsync(id);
            if (data == null)
                return NotFound();

            return Ok(data);
        }

        [HttpPost]
        public async Task<ActionResult<LeaveTypesDto>> Create([FromBody] LeaveTypesDto dto)
        {
            if (dto == null)
            {
                return Unauthorized("null");
            }

            var result = await _LeaveTypeSetUpService.CreateAsync(dto);

            // REMOVED: CreatedAtAction
            // ADDED: Simple Ok result
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<LeaveTypesDto>> Update(int id, [FromBody] LeaveTypesDto dto)
        {
            if (id != dto.LeaveID)
                return BadRequest("ID mismatch");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _LeaveTypeSetUpService.UpdateAsync(dto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var success = await _LeaveTypeSetUpService.DeleteAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}
