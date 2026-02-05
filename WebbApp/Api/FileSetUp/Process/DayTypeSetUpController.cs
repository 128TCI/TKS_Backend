using DomainEntities.DTO.FileSetUp.Process;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces.FileSetUp.Process;
using Services.Services.FileSetUp.Process;

namespace WebbApp.Api.FileSetUp.Process
{
    [Route("api/Fs/Process/[controller]")]
    [ApiController]
    public class DayTypeSetUpController : ControllerBase
    {
        private readonly IDayTypeSetUpService _DayTypeSetUpService;

        public DayTypeSetUpController(IDayTypeSetUpService dayTypeSetUpService)
        {
            _DayTypeSetUpService = dayTypeSetUpService;
        }

        [HttpGet]
        public async Task<ActionResult<List<DayTypeSetUpDTO>>> GetAll()
        {
            var calendarSetUpService = await _DayTypeSetUpService.GetAllAsync();
            return Ok(calendarSetUpService);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DayTypeSetUpDTO>> GetById(int id)
        {
            var calendarSetUpService = await _DayTypeSetUpService.GetByIdAsync(id);
            if (calendarSetUpService == null)
                return NotFound();

            return Ok(calendarSetUpService);
        }

        [HttpPost]
        public async Task<ActionResult<DayTypeSetUpDTO>> Create([FromBody] DayTypeSetUpDTO dto)
        {
            if (dto == null)
            {
                return Unauthorized("null");
            }

            var result = await _DayTypeSetUpService.CreateAsync(dto);

            // REMOVED: CreatedAtAction
            // ADDED: Simple Ok result
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<DayTypeSetUpDTO>> Update(int id, [FromBody] DayTypeSetUpDTO dto)
        {
            if (id != dto.ID)
                return BadRequest("ID mismatch");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _DayTypeSetUpService.UpdateAsync(dto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var success = await _DayTypeSetUpService.DeleteAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}
