using DomainEntities.DTO.FileSetUp.Process;
using DomainEntities.DTO.FileSetUp.Process.Allowance_and_Earnings;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces.FileSetUp.Process;
using Services.Interfaces.FileSetUp.Process.Allowance_and_Earnings;
using Services.Services.FileSetUp.Process;

namespace WebbApp.Api.FileSetUp.Process
{
    [Route("api/Fs/Process/[controller]")]
    [ApiController]
    public class TimeKeepGroupSetUpController : ControllerBase
    {
        private readonly ITimeKeepGroupSetUpService _TimeKeepGroupSetUpService;

        public TimeKeepGroupSetUpController(ITimeKeepGroupSetUpService timeKeepGroupSetUpService)
        {
            _TimeKeepGroupSetUpService = timeKeepGroupSetUpService;
        }

        [HttpGet]
        public async Task<ActionResult<List<TimeKeepGroupSetUpDTO>>> GetAll()
        {
            var timeKeepGroupSetUpService = await _TimeKeepGroupSetUpService.GetAllAsync();
            return Ok(timeKeepGroupSetUpService);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TimeKeepGroupSetUpDTO>> GetById(int id)
        {
            var timeKeepGroupSetUpService = await _TimeKeepGroupSetUpService.GetByIdAsync(id);
            if (timeKeepGroupSetUpService == null)
                return NotFound();

            return Ok(timeKeepGroupSetUpService);
        }

        [HttpPost]
        public async Task<ActionResult<TimeKeepGroupSetUpDTO>> Create([FromBody] TimeKeepGroupSetUpDTO dto)
        {
            if (dto == null)
            {
                return Unauthorized("null");
            }

            var result = await _TimeKeepGroupSetUpService.CreateAsync(dto);

            // REMOVED: CreatedAtAction
            // ADDED: Simple Ok result
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TimeKeepGroupSetUpDTO>> Update(int id, [FromBody] TimeKeepGroupSetUpDTO dto)
        {
            if (id != dto.ID)
                return BadRequest("ID mismatch");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _TimeKeepGroupSetUpService.UpdateAsync(dto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var success = await _TimeKeepGroupSetUpService.DeleteAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}
